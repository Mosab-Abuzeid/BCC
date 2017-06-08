using DistEventBroker.PubSub;
using DistEventBroker.PubSub.Subscribtion;
using Grpc.Core;
using Noqoush.AdFalcon.Server.BillingController.Server.Models;
using Noqoush.AdFalcon.Banker.Common;
using Noqoush.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;
using Noqoush.AdFalcon.EventDTOs;
using Noqoush.Framework;

namespace Noqoush.AdFalcon.Server.BillingController.Server
{
    public class BillingController
    {
        public event Action<IList<int>> AdGroupExpired;
        public event Action<IList<int>> AdGroupInActive;
        public event Action<IList<int>> AdGroupBudgetConsumed;
        public event Action<IList<int>> AdGroupDailyBudgetConsumed;

        private string _masterBankerAddress;
        private MasterBankerService.MasterBankerServiceClient _rpcClient;
        private Channel _rpcChannel;
        private ILog _log;
        private static bool _isInitialized = false;
        private static readonly BillingController _current = new BillingController();
        private IBillingControllerRepository _repository;
        private IBillingControllerCache _cache;
        private static readonly object _lockObj = new object();

        private BillingController() { }
        public static BillingController Current
        {
            get
            {
                return _current;
            }
        }
        public BillingController Init(string databaseName, int spendBatchSize, string loggerName)
        {
            if (_isInitialized) return this;
            lock (_lockObj)
            {
                if (_isInitialized) return this;
                if (string.IsNullOrEmpty(loggerName)) throw new ArgumentException("Parameter loggerName is null or empty");

                var appSettings = ConfigurationManager.AppSettings;
                string configVal = appSettings[Constants.Commons.MasterBankerAddressCfg];
                if (string.IsNullOrEmpty(configVal))
                    throw new ConfigurationErrorsException($"Appsetting with key {Constants.Commons.MasterBankerAddressCfg} does not exist or has invalid value");
                _masterBankerAddress = configVal;

                _log = LogManager.GetLogger(loggerName);
                _repository = IoC.Instance.Resolve<IBillingControllerRepository>();
                _cache = IoC.Instance.Resolve<IBillingControllerCache>();
                _repository.Init(databaseName, spendBatchSize);
                InitializeRpcClient();
                InitializeEventSubscrptions();
                _isInitialized = true;
            }
            return this;
        }
        private void InitializeRpcClient()
        {
            try
            {
                _rpcChannel = new Channel(_masterBankerAddress, ChannelCredentials.Insecure);
                _rpcClient = new MasterBankerService.MasterBankerServiceClient(_rpcChannel);
                _log.Info($"MasterBankerService grpc client was created on address {_masterBankerAddress})");
            }
            catch (RpcException ex)
            {
                _log.Error($"Unexpected error occured while initializing grpc client {_masterBankerAddress})", ex);
                throw new BillingControllerException("Unexpected error occured while initializing grpc client)", ex);
            }
        }
        private void InitializeEventSubscrptions()
        {
            try
            {
                EventSubscribtionProxy.Current.SubscribeAsync<FundChanged>(Constants.AccountTrackInfos.EventSubscriberId, OnFundChanged);
                EventSubscribtionProxy.Current.SubscribeAsync<CampaignBillingInfoChanged>(Constants.AccountTrackInfos.EventSubscriberId, OnCampaignBillingInfoChanged);
                EventSubscribtionProxy.Current.SubscribeAsync<AdGroupBillingInfoChanged>(Constants.AccountTrackInfos.EventSubscriberId, OnAdGroupBillingInfoChanged);
                EventSubscribtionProxy.Current.SubscribeAsync<AccountClosed>(Constants.AccountTrackInfos.EventSubscriberId, OnAccountClosed);
                _log.Info("Subscription to events completed successfully");
            }
            catch (EventPubSubException ex)
            {
                _log.Error("An error occured while subscribing to evens", ex);
                throw new BillingControllerException("An error occured while subscribing to evens", ex);
            }
        }
        private void OnFundChanged(FundChanged evt)
        {
            try
            {
                var request = new CreateOrUpdateFundAccountRequest
                {
                    AccountKey = evt.GetAccountKey(),
                    ExtraFund = Utilities.ToInt64Value(evt.FundAmount)
                };
                _rpcClient.CreateOrUpdateFundAccount(request);
                _log.Info($"Fund changes successfully for account {evt.AccountId}");
            }
            catch (RpcException ex)
            {
                _log.Error($"Failed to change fund value for account {evt.AccountId}", ex);
            }
        }
        private void OnCampaignBillingInfoChanged(CampaignBillingInfoChanged evt)
        {
            try
            {
                var accountTrack = _repository.GetAccountTrackInfo(evt.CampaignId, AccountType.Campaign);
                var request = new CreateOrUpdateBudgetAccountRequest
                {
                    AccountKey = evt.GetAccountKey(),
                    MinUnitPrice = Utilities.ToInt64Value(evt.MinimumUnitPrice),
                    PacingPolicy = (BudgetPacingPolicies)((int)evt.Pacing),
                    PreviousSpent = Utilities.ToInt64Value(accountTrack.LastSpend),
                    NewBudget = Utilities.GetNewBudget(evt.Budget, evt.DailyBudget, accountTrack.LastSpend),
                    BudgetExpiryEpoch = evt.DailyBudget.HasValue ? Utilities.ToInt64ValueEndOfDay(DateTime.UtcNow) : Utilities.ToInt64Value(evt.EndDate)
                };
                _rpcClient.CreateOrUpdateBudgetAccount(request);
                _log.Info($"Billing info created/updated successfully for campaign {evt.CampaignId}");
            }
            catch (Exception ex)
            {
                if (ex is RpcException && (ex as RpcException).Status.StatusCode == StatusCode.NotFound)
                    _log.Error($"Parent account not found for campaign {evt.CampaignId}", ex);
                else
                    _log.Error($"Failed to update billing info for campaign {evt.CampaignId}", ex);
           }
           
        }
        private void OnAdGroupBillingInfoChanged(AdGroupBillingInfoChanged evt)
        {
            try
            {
                var accountTrack = _repository.GetAccountTrackInfo(evt.AdGroupId, AccountType.AdGroup);
                var request = new CreateOrUpdateBudgetAccountRequest
                {
                    AccountKey = evt.GetAccountKey(),
                    MinUnitPrice = Utilities.ToInt64Value(evt.MinimumUnitPrice),
                    PacingPolicy = (BudgetPacingPolicies)((int)evt.Pacing),
                    PreviousSpent = Utilities.ToInt64Value(accountTrack.LastSpend),
                    NewBudget = Utilities.GetNewBudget(evt.Budget,evt.DailyBudget,accountTrack.LastSpend),
                    BudgetExpiryEpoch = evt.DailyBudget.HasValue ? Utilities.ToInt64ValueEndOfDay(DateTime.UtcNow) : Utilities.ToInt64Value(evt.EndDate)
                };
                _rpcClient.CreateOrUpdateBudgetAccount(request);
                _log.Info($"Billing info created/updated successfully for adgroup {evt.AdGroupId}");
            }
            catch (Exception ex)
            {
                if (ex is RpcException && (ex as RpcException).Status.StatusCode == StatusCode.NotFound)
                    _log.Error($"Parent account not found for adgroup {evt.AdGroupId}", ex);
                else
                    _log.Error($"Failed to create/update billing info for adgroup {evt.AdGroupId}", ex);
            }
        }
        public async Task<bool> JoinAdGroup(AdGroup adGroup)
        {
            try
            {
                var accountTrack =_repository.GetAccountTrackInfo(adGroup.Id, AccountType.AdGroup);
                _repository.DeleteAccountTrackInfo(adGroup.Id, AccountType.AdGroup);
                var request = new CreateOrUpdateBudgetAccountRequest
                {
                    AccountKey = adGroup.GetAccountKey(),
                    MinUnitPrice = Utilities.ToInt64Value(adGroup.MinimumUnitPrice),
                    PacingPolicy = (BudgetPacingPolicies)((int)adGroup.Pacing),
                    PreviousSpent = Utilities.ToInt64Value(accountTrack.LastSpend),
                    NewBudget = Utilities.GetNewBudget(adGroup.Budget, adGroup.DailyBudget, accountTrack.LastSpend),
                    BudgetExpiryEpoch = adGroup.DailyBudget.HasValue ? Utilities.ToInt64ValueEndOfDay(DateTime.UtcNow) : Utilities.ToInt64Value(adGroup.EndDate)
                };
                await _rpcClient.CreateOrUpdateBudgetAccountAsync(request);
                _log.Info($"Adgroup joined successfully (AdgroupId : {adGroup.Id})");
                return true;
            }
            catch (Exception ex)
            {
                _log.Error($"Adgroup failed to be joined (AdgroupId : {adGroup.Id})", ex);
                if (ex is RpcException && (ex as RpcException).Status.StatusCode == StatusCode.InvalidArgument)
                    return HandleCampaignAccountNotFound(adGroup.Id, () => JoinAdGroup(adGroup).Wait());
                return false;
            }

        }
        private bool HandleFundAccountNotFound(int accountID, Action postAction)
        {
            Account fundAccount = GetAccount(accountID);
            try
            {
                var request = new CreateOrUpdateFundAccountRequest
                {
                    AccountKey = fundAccount.GetAccountKey(),
                    ExtraFund = Utilities.ToInt64Value(fundAccount.FundAmount)
                };
                _rpcClient.CreateOrUpdateFundAccount(request);
                _log.Info($"Fund account {accountID} created successfully");
                postAction();
                return true;
            }
            catch (RpcException ex)
            {
                _log.Error($"Failed to create fund account {accountID}", ex);
                return false;
            }
        }
        private bool HandleCampaignAccountNotFound(int campaignId, Action postAction)
        {
            Campaign account = null;
            try
            {
                account = GetCampaign(campaignId);
                var accountTrack = _repository.GetAccountTrackInfo(campaignId, AccountType.Campaign);
                _repository.DeleteAccountTrackInfo(campaignId, AccountType.Campaign);
                var request = new CreateOrUpdateBudgetAccountRequest
                {
                    AccountKey = account.GetAccountKey(),
                    MinUnitPrice = Utilities.ToInt64Value(account.MinimumUnitPrice),
                    PacingPolicy = (BudgetPacingPolicies)((int)account.Pacing),
                    PreviousSpent = Utilities.ToInt64Value(accountTrack.LastSpend),
                    NewBudget = Utilities.GetNewBudget(account.Budget, account.DailyBudget, accountTrack.LastSpend),
                    BudgetExpiryEpoch = account.DailyBudget.HasValue ? Utilities.ToInt64ValueEndOfDay(DateTime.UtcNow) : Utilities.ToInt64Value(account.EndDate)
                };

                _rpcClient.CreateOrUpdateBudgetAccountAsync(request);
                _log.Info($"Billing info created successfully for campaign {campaignId}");
                postAction();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error($"Failed to create billing info for campaign {campaignId}", ex);
               if (ex is RpcException && (ex as RpcException).Status.StatusCode == StatusCode.NotFound)
                    return HandleFundAccountNotFound(account.AccountId, () => HandleCampaignAccountNotFound(campaignId, postAction));
                return false;
            }
        }
        public async Task AdGroupLeft(int AccountId ,int CampaignId, int AdGroupId)
        {
            var request = new CloseAccountRequest
            {
                AccountKey = $"{AccountId}:{CampaignId}:{AdGroupId}"
            };
            try
            {
                await _rpcClient.CloseAccountAsync(request);
            }
            catch (RpcException ex)
            {
                _log.Error($"Failed to close account {request.AccountKey}", ex);
                throw new BillingControllerException($"Failed to close account {request.AccountKey}",ex);
            }
        }
        private void OnAccountClosed(AccountClosed evt)
        {
            try
            {
                var accountsTracks = new List<AccountTrackInfo>();
                var adGroupIds = new List<int>();
                AccountTrackInfo accountTrackInfo = null;
                foreach (var pair in evt.AccountsSpends)
                {
                    accountTrackInfo = new AccountTrackInfo
                    {
                        AccountID = Utilities.GetAccountID(pair.Key),
                        LastSpend = pair.Value,
                        SpendDate = evt.ClosedDate,
                        AccountType = Utilities.GetAccountType(pair.Key)
                    };
                    accountsTracks.Add(accountTrackInfo);
                    if (accountTrackInfo.AccountType == AccountType.AdGroup)
                        adGroupIds.Add(accountTrackInfo.AccountID);
                }
                _repository.PersistAccountTrackInfos(accountsTracks);
                TriggerUpdateAdStatusEvents(adGroupIds, evt.ClosedReason);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
        private void TriggerUpdateAdStatusEvents(List<int> adGroupIds, AccountCloseReason closeReason)
        {
            if (closeReason == AccountCloseReason.OutOfFund && AdGroupInActive != null)
                AdGroupInActive(adGroupIds);
            else if (closeReason == AccountCloseReason.BudgetExpired && AdGroupExpired != null)
                AdGroupExpired(adGroupIds);
            else if (closeReason == AccountCloseReason.OutOfBudget && AdGroupBudgetConsumed != null && AdGroupDailyBudgetConsumed != null)
            {
                var budgetConsumedIds = new List<int>();
                var dailyBudgetConsumedIds = new List<int>();
                IList<AdGroup> adGroups = GetAdGroups(adGroupIds);
                foreach (var adGrp in adGroups)
                {
                    if (!adGrp.DailyBudget.HasValue)
                        budgetConsumedIds.Add(adGrp.Id);
                    else
                    {
                        var accountTrack = _repository.GetAccountTrackInfo(adGrp.Id, AccountType.AdGroup);
                        if (adGrp.Budget > accountTrack.LastSpend)
                            dailyBudgetConsumedIds.Add(adGrp.Id);
                        else
                            budgetConsumedIds.Add(adGrp.Id);
                    }

                }

                AdGroupDailyBudgetConsumed(dailyBudgetConsumedIds);
                AdGroupBudgetConsumed(budgetConsumedIds);
            }
        }
        private Account GetAccount(int accountID)
        {
            var account = _cache.Get<Account>(accountID);
            if (account == null)
            {
                account = _repository.GetAccount(accountID);
                _cache.Add(account);
            }
            return account;
        }
        private AdGroup GetAdGroup(int adGroupID)
        {
            var adGroup = _cache.Get<AdGroup>(adGroupID);
            if (adGroup == null)
            {
                 adGroup = _repository.GetAdGroup(adGroupID);
                _cache.Add(adGroup);
            }
            return adGroup;
        }
        private Campaign GetCampaign(int campaignID)
        {
            var campaign = _cache.Get<Campaign>(campaignID);
            if (campaign == null)
            {
                campaign = _repository.GetCampaign(campaignID);
                _cache.Add(campaign);
            }
            return campaign;
        }
        private IList<AdGroup> GetAdGroups(List<int> adGroupIds)
        {
            var adgroups = new List<AdGroup>();
            var notInCacheIds = new List<int>();
            foreach (var agGroupId in adGroupIds)
            {
                var account = _cache.Get<AdGroup>(agGroupId);
                if (account == null)
                    notInCacheIds.Add(agGroupId);
                else
                    adgroups.Add(account);
            }

            var dbAdGroups =_repository.GetAdGroups(notInCacheIds);
            adgroups.AddRange(dbAdGroups);
            return adgroups;
        }
        private decimal GetAvailableFund(int accountId)
        {
            var availableFund = _cache.GetAvailableFund(accountId);
            if (availableFund == 0)
            {
                 var request = new GetFundBalanceRequest { Key = accountId.ToString() };
                 var response = _rpcClient.GetFundBalance(request);
                availableFund = Utilities.ToDecimal(response.Balance);
                _cache.AddAvailableFund(accountId, availableFund);

            }
            return availableFund;
        }
        public bool AdIsValid(int adId, decimal bid)
        {
            try
            {
                var adGroupId = _repository.GetAdGroupId(adId);
                if (adGroupId < 0) return false;
                var adgroup = GetAdGroup(adGroupId);
                if (adgroup.DailyBudget.HasValue && adgroup.DailyBudget.Value < bid)
                    return false;
                if (adgroup.Budget.HasValue && adgroup.Budget.Value < bid)
                    return false;
                var availableFund = GetAvailableFund(adgroup.AccountId);
                if (availableFund < bid)
                    return false;
                var camaign = GetCampaign(adgroup.CampaignId);
                if (camaign.DailyBudget.HasValue && camaign.DailyBudget.Value < bid)
                    return false;
                if (camaign.Budget.HasValue && camaign.Budget.Value < bid)
                    return false;
                return true;
            }
            catch (RpcException ex)
            {
                _log.Error($"Failed to get balance for fund account", ex);
                throw new BillingControllerException("Failed to get balance for fund account", ex);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw new BillingControllerException("Unexpected error occured", ex);
            }
        }
    }
}

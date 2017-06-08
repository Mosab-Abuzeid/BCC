using DistEventBroker.PubSub;
using DistEventBroker.PubSub.Subscribtion;
using Noqoush.AdFalcon.Banker.Common;
using Noqoush.AdFalcon.Banker.Local;
using Noqoush.AdFalcon.EventDTOs;
using Noqoush.Framework;
using Noqoush.Framework.Logging;
using System;
using System.Collections.Generic;

namespace Noqoush.AdFalcon.Server.BillingController.Client
{
    public class BillingController
    {
        public event Action<IList<int>> AdGroupDeactivated;
        private static readonly BillingController _current = new BillingController();
        private static bool _isInitialized = false;
        private ILog _log;
        private ILocalBanker _localBanker;
        private static readonly object _lockObj = new object();

        private BillingController() { }
        public static BillingController Current
        {
            get
            {
                return _current;
            }
        }
        public BillingController Init(string loggerName)
        {
            if (_isInitialized) return this;
            lock (_lockObj)
            {
                if (_isInitialized) return this;
                if (string.IsNullOrEmpty(loggerName)) throw new ArgumentException("Parameter loggerName is null or empty");
                _log = LogManager.GetLogger(loggerName);
                try
                {
                    EventSubscribtionProxy.Current.SubscribeAsync<AccountClosed>("BillingController.Client", OnAccountClosed);
                    _log.Info("Subscription to events completed successfully");
                }
                catch (EventPubSubException ex)
                {
                    _log.Error("An error occured while subscribing to evens", ex);
                    throw new BillingControllerException("An error occured while subscribing to evens", ex);
                }
                _localBanker = IoC.Instance.Resolve<ILocalBanker>();
                _isInitialized = true;
            }
            return this;
        }
        private void OnAccountClosed(AccountClosed evt)
        {
            if (AdGroupDeactivated == null) return;
            var adgroupsIds = new List<int>();
            foreach (var pair in evt.AccountsSpends)
            {
                var adgroupId = Utilities.TryGetAdgroupId(pair.Key);
                if (adgroupId.HasValue)
                    adgroupsIds.Add(adgroupId.Value);
            }
            AdGroupDeactivated(adgroupsIds);
        }
        public bool AuthorizeBid(int AccountId, int CampaignId, int AdGroupId, decimal Amount, string BidId)
        {
            var accountKey = new AccountKey($"{AccountId.ToString()}:{CampaignId.ToString()}:{AdGroupId.ToString()}");
            var amount = new Amount() { Value = (long)Amount };
            return _localBanker.AuthorizeBid(accountKey, BidId, amount);
        }
        public bool CommitBid(int AccountId, int CampaignId, int AdGroupId, decimal Amount, string BidId)
        {
            var accountKey = new AccountKey($"{AccountId.ToString()}:{CampaignId.ToString()}:{AdGroupId.ToString()}");
            var amount = new Amount() { Value = (long)Amount };
            return _localBanker.CommitBid(accountKey, BidId, amount);
        }
    }
}

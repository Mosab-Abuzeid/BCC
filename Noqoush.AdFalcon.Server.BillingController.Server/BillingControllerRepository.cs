using MySql.Data.MySqlClient;
using Noqoush.AdFalcon.EventDTOs;
using Noqoush.AdFalcon.Server.BillingController.Server.Models;
using Noqoush.Framework.DB;
using Noqoush.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Noqoush.AdFalcon.Server.BillingController.Server
{
    internal class BillingControllerRepository : IBillingControllerRepository
    {
        private IDatabaseHelper _dbHelper;
        private int _insertionBatchSize;
        private string _billingControllerDb;

        public BillingControllerRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public void Init(string databaseName, int spendBatchSize)
        {
            if (string.IsNullOrEmpty(databaseName))
                throw new ArgumentException("Parameter databaseName is not a a valid connections string name");
            if (spendBatchSize < 1)
                throw new ArgumentOutOfRangeException("Parameter spendBatchSize must be greater than zero");
            _billingControllerDb = databaseName;
            _insertionBatchSize = spendBatchSize;
        }
        public AccountTrackInfo GetAccountTrackInfo(int accountID, AccountType accountType)
        {
            try
            {
                var paramters = new DBParameter[] {
                      new DBParameter($"@{Constants.AccountTrackInfos.AccountIDColumn}", accountID),
                      new DBParameter($"@{Constants.AccountTrackInfos.AccountTypeColumn}", accountType)
                };
                var results = _dbHelper.ExecuteQuery(Constants.AccountTrackInfos.selectQueryTemplate, ConvertToAccountTrackInfo, _billingControllerDb, paramters);
                var account = new AccountTrackInfo();
                if (results != null && results.Count == 1)
                    account = results[0];
                return account;
            }
            catch (MySqlException ex)
            {
                throw new BillingControllerException($"Failed to get AccountSummaryInfo for account {accountID}", ex);
            }
        }

        public void DeleteAccountTrackInfo(int accountID, AccountType accountType)
        {
            var paramters = new DBParameter[] {
                      new DBParameter($"@{Constants.AccountTrackInfos.AccountIDColumn}", accountID),
                      new DBParameter($"@{Constants.AccountTrackInfos.AccountTypeColumn}", accountType)
                };
            _dbHelper.ExecuteQuery(Constants.AccountTrackInfos.deleteQueryTemplate, _billingControllerDb, paramters);
        }

        private AccountTrackInfo ConvertToAccountTrackInfo(IDataReader reader)
        {
            return new AccountTrackInfo
            {
                AccountID = reader.SafeGetValue<int>(Constants.AccountTrackInfos.AccountIDColumn),
                AccountType = reader.SafeGetValue<AccountType>(Constants.AccountTrackInfos.AccountTypeColumn),
                LastSpend = reader.SafeGetValue<long>(Constants.AccountTrackInfos.LastSpendColumn),
                SpendDate = reader.SafeGetValue<DateTime>(Constants.AccountTrackInfos.SpendDateColumn)
            };
        }
        public void PersistAccountTrackInfos(IList<AccountTrackInfo> accounts)
        {
            if (accounts == null || accounts.Count < 1) return;
            try
            {
                var bulkInsertQueryBuilder = new StringBuilder(Constants.AccountTrackInfos.insertQueryTemplate);
                var hasMoreAccounts = true;
                var currentBacthSize = 0;
                for (int i = 0; i < accounts.Count; i++)
                {
                    bulkInsertQueryBuilder.Append(accounts[i].GetQueryValuesString());
                    currentBacthSize++;
                    hasMoreAccounts = i < accounts.Count - 1;
                    if (currentBacthSize < _insertionBatchSize && hasMoreAccounts)
                    {
                        bulkInsertQueryBuilder.Append(",");
                        continue;
                    }
                    bulkInsertQueryBuilder.Append(Constants.AccountTrackInfos.onDuplicateSegmant);
                    _dbHelper.ExecuteQuery(bulkInsertQueryBuilder.ToString(), _billingControllerDb);
                    //reset
                    if (hasMoreAccounts)
                    {
                        bulkInsertQueryBuilder = new StringBuilder(Constants.AccountTrackInfos.insertQueryTemplate);
                        currentBacthSize = 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new BillingControllerException("Failed to persist AccountSummaryInfos", ex);
            }
        }
        public Account GetAccount(int accountId)
        {
            var paramters = new DBParameter[] { new DBParameter($"@{Constants.AccountSummary.AccountIdColumn}", accountId) };
            var accounts = _dbHelper.ExecuteQuery(Constants.AccountSummary.selectQueryTemplate, ConvertToAccount, _billingControllerDb, paramters);
            if (accounts == null || accounts.Count == 0)
                return null;
            return accounts[0];
        }
        private Account ConvertToAccount(IDataReader reader)
        {
            return new Account
            {
                Id = reader.SafeGetValue<int>(Constants.AccountSummary.AccountIdColumn),
                FundAmount = reader.SafeGetValue<decimal>(Constants.AccountSummary.FundAmountAlias)
            };
        }
        public Campaign GetCampaign(int campaignId)
        {
            var paramters = new DBParameter[] { new DBParameter($"@{Constants.Campaigns.AccountIdColumn}", campaignId) };
            var accounts = _dbHelper.ExecuteQuery(Constants.Campaigns.selectQueryTemplate, ConvertToCampaign, _billingControllerDb, paramters);
            if (accounts == null || accounts.Count == 0)
                return null;
            return accounts[0];
        }
        private Campaign ConvertToCampaign(IDataReader reader)
        {
            return new Campaign
            {
                AccountId = reader.SafeGetValue<int>(Constants.Campaigns.AccountIdColumn),
                Id = reader.SafeGetValue<int>(Constants.Campaigns.CampaignIdColumn),
                EndDate = reader.SafeGetValue<DateTime?>(Constants.Campaigns.EndDateColumn),
                Budget = reader.SafeGetValue<decimal?>(Constants.Campaigns.BudgetColumn),
                DailyBudget = reader.SafeGetValue<decimal?>(Constants.Campaigns.DailyBudgetColumn),
                MinimumUnitPrice = reader.SafeGetValue<decimal?>(Constants.Campaigns.MinimumUnitPriceColumn),
                Pacing = reader.SafeGetValue<PacingPolicies>(Constants.Campaigns.PacingColumn)
            };
        }
        public IList<AdGroup> GetAdGroups(List<int> adGroupsIds)
        {
            var query = string.Format(Constants.Adgroups.selectMultiQueryTemplate, string.Join(",",adGroupsIds));
            var accounts = _dbHelper.ExecuteQuery(query, ConvertToAdGroup, _billingControllerDb);
            return accounts;
        }
        public AdGroup GetAdGroup(int adGroupId)
        {
            var paramters = new DBParameter[] { new DBParameter($"@{Constants.Adgroups.AdgroupIdColumn}", adGroupId) };
            var accounts = _dbHelper.ExecuteQuery(Constants.Adgroups.selectSingleQueryTemplate, ConvertToAdGroup, _billingControllerDb, paramters);
            if (accounts == null || accounts.Count == 0)
                return null;
            return accounts[0];
        }
        private AdGroup ConvertToAdGroup(IDataReader reader)
        {
            return new AdGroup
            {
                AccountId = reader.SafeGetValue<int>(Constants.Adgroups.AccountIdAlias),
                Id = reader.SafeGetValue<int>(Constants.Adgroups.CampaignIdColumn),
                CampaignId = reader.SafeGetValue<int>(Constants.Adgroups.AdgroupIdColumn),
                EndDate = reader.SafeGetValue<DateTime?>(Constants.Adgroups.EndDateColumn),
                Budget = reader.SafeGetValue<decimal?>(Constants.Adgroups.BudgetColumn),
                DailyBudget = reader.SafeGetValue<decimal?>(Constants.Adgroups.DailyBudgetColumn),
                MinimumUnitPrice = reader.SafeGetValue<decimal?>(Constants.Adgroups.MinimumUnitPriceColumn),
                Pacing = reader.SafeGetValue<PacingPolicies>(Constants.Adgroups.PacingColumn)
            };
        }

        public int GetAdGroupId(int adId)
        {
            var paramters = new DBParameter[] { new DBParameter($"@{Constants.Ads.AdIdColumn}", adId) };
            var adgroupId = _dbHelper.ExecuteScalarQuery(Constants.Ads.selectGroupIdQuery, _billingControllerDb, paramters);
            return Convert.ToInt32(adgroupId);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noqoush.AdFalcon.Server.BillingController.Server
{
    internal static class Constants
    {
        public static class Commons
        {
            public const string MasterBankerAddressCfg = "billingController.masterBankerAddress";
            public const string BillingControllerCacheName = "billingController.Cache";
        }
        public static class AccountTrackInfos {
            public const string EventSubscriberId = "BillingController.Server";
            public const string TableName = "bc_last_spend_track";
            public const string AccountIDColumn = "AccountID";
            public const string AccountTypeColumn = "AccountType";
            public const string SpendDateColumn = "SpendDate";
            public const string LastSpendColumn = "LastSpend";
            public const string DateFormat = "yyyy-MM-dd hh:mm:ss";

            public readonly static string[] billingAccountTableColumns = { AccountIDColumn, LastSpendColumn, AccountTypeColumn, SpendDateColumn };
            public readonly static string[] onDuplicateValues = new string[2];
            public readonly static string insertQueryTemplate = $"INSERT INTO {TableName} ({string.Join(",", billingAccountTableColumns)}) VALUES ";
            public readonly static string selectQueryTemplate = $@"Select {string.Join(",", billingAccountTableColumns)} From {TableName} 
                                                                Where {AccountIDColumn} = @{AccountIDColumn} and {AccountTypeColumn} = @{AccountTypeColumn}";
            public readonly static string onDuplicateSegmant;
            public readonly static string deleteQueryTemplate = $@"Delete from {TableName} Where {AccountIDColumn} = @{AccountIDColumn} 
                                                                      and {AccountTypeColumn} = @{AccountTypeColumn}";

            static AccountTrackInfos()
            {
                var index = 0;
                foreach (var col in billingAccountTableColumns)
                {
                    if (col == AccountIDColumn) continue;
                    onDuplicateValues[index] = $"{col} =  VALUES({col})";
                    index++;
                }
                onDuplicateSegmant = $" ON DUPLICATE KEY UPDATE {string.Join(",", onDuplicateValues)}";
            }
        }

        public static class AccountSummary
        {
            public const string TableName = "accountsummary";
            public const string AccountIdColumn = "AccountId";
            public const string FundsColumn = "Funds";
            public const string CreditColumn = "Credit";
            public const string FundAmountAlias = "FundAmount";

            public readonly static string selectQueryTemplate = $@"Select {AccountIdColumn}, ({FundsColumn} + {CreditColumn}) as {FundAmountAlias} from
                                                                     {TableName} where {AccountIdColumn} = @{AccountIdColumn}";
        }


        public static class Campaigns
        {
            public const string TableName = "campaigns";
            public const string AccountIdColumn = "AccountId";
            public const string CampaignIdColumn = "Id";
            public const string EndDateColumn = "EndDate";
            public const string BudgetColumn = "Budget";
            public const string DailyBudgetColumn = "DailyBudget";
            public const string PacingColumn = "Pacing";
            public const string MinimumUnitPriceColumn = "MinimumUnitPrice";

            public readonly static string[] campaignsTableColumns = { AccountIdColumn, CampaignIdColumn, EndDateColumn,
                                                                      BudgetColumn, DailyBudgetColumn, PacingColumn, MinimumUnitPriceColumn };
            public readonly static string selectQueryTemplate = $@"Select {string.Join(",", campaignsTableColumns)} from
                                                                     {TableName} where {CampaignIdColumn} = @{CampaignIdColumn}";
        }


        public static class Adgroups
        {
            public const string TableName = "Adgroups";
            public const string AccountIdAlias = "AccountId";
            public const string CampaignIdColumn = "CampaignId";
            public const string AdgroupIdColumn = "Id";
            public const string EndDateColumn = "EndDate";
            public const string BudgetColumn = "Budget";
            public const string DailyBudgetColumn = "DailyBudget";
            public const string PacingColumn = "Pacing";
            public const string MinimumUnitPriceColumn = "MinimumUnitPrice";

            public readonly static string[] adgroupsTableColumns = { AdgroupIdColumn, CampaignIdColumn, EndDateColumn,
                                                                      BudgetColumn, DailyBudgetColumn, PacingColumn, MinimumUnitPriceColumn };
            private readonly static string selectAccountIdQueryTemplate =  $@"Select {Campaigns.CampaignIdColumn} from {Campaigns.TableName} 
                                                                             where {CampaignIdColumn} = {CampaignIdColumn}";
            public readonly static string selectSingleQueryTemplate = $@"Select ({selectAccountIdQueryTemplate}) as {AccountIdAlias}, 
                                                 {string.Join(",", adgroupsTableColumns)} from {TableName} where {AdgroupIdColumn} = @{AdgroupIdColumn}";
            public readonly static string selectMultiQueryTemplate = $@"Select ({selectAccountIdQueryTemplate}) as {AccountIdAlias}, 
                                                 {string.Join(",", adgroupsTableColumns)} from {TableName} where {AdgroupIdColumn} in {{0}}";
        }

        public static class Ads
        {
            public const string TableName = "Ads";
            public const string AdgroupIdColumn = "AdGroupId";
            public const string AdIdColumn = "Id";
            public readonly static string selectGroupIdQuery = $"Select {AdgroupIdColumn} from {TableName} where {AdIdColumn} = @{AdIdColumn}";
        }
    }
}

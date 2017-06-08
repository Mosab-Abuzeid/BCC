using Noqoush.AdFalcon.Server.BillingController.Server.Models;
using System.Collections.Generic;

namespace Noqoush.AdFalcon.Server.BillingController.Server
{
    internal interface IBillingControllerRepository
    {
        void Init(string databaseName, int spendBatchSize);
        AccountTrackInfo GetAccountTrackInfo(int accountID, AccountType accountType);
        void PersistAccountTrackInfos(IList<AccountTrackInfo> accounts);
        Account GetAccount(int accountId);
        Campaign GetCampaign(int campaignId);
        AdGroup GetAdGroup(int adGroupId);
        IList<AdGroup> GetAdGroups(List<int> adGroupsIds);
        int GetAdGroupId(int adId);
        void DeleteAccountTrackInfo(int accountID, AccountType accountType);
    }
}
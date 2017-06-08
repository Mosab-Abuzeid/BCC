using System;

namespace Noqoush.AdFalcon.Server.BillingController.Server.Models
{

    public enum AccountType { Campaign, AdGroup }
    public class AccountTrackInfo
    {
        public int AccountID { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime SpendDate { get; set; }
        public decimal LastSpend { get; set; }
    }
}

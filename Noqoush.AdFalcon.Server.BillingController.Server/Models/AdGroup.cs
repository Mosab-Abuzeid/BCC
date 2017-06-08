using Noqoush.AdFalcon.EventDTOs;
using System;

namespace Noqoush.AdFalcon.Server.BillingController.Server.Models
{
    public class AdGroup: IBillingEntity
    {
        public  int Id { get; set; }
        public int AccountId { get; set; }
        public  int CampaignId { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public decimal? DailyBudget { get; set; }
        public PacingPolicies Pacing { get; set; }
        public decimal? MinimumUnitPrice { get; set; }
        public string GetAccountKey()
        {
            return $"{AccountId}:{CampaignId}:{Id}";
        }
    }
}

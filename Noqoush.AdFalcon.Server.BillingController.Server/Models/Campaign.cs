using Noqoush.AdFalcon.EventDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noqoush.AdFalcon.Server.BillingController.Server.Models
{
    internal class Campaign : IBillingEntity
    {
        public int AccountId { get; set; }
        public int Id { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public decimal? DailyBudget { get; set; }
        public PacingPolicies Pacing { get; set; }
        public decimal? MinimumUnitPrice { get; set; }

        public string GetAccountKey()
        {
            return $"{AccountId}:{Id}";
        }
    }
}

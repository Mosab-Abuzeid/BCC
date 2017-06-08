using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noqoush.AdFalcon.Server.BillingController.Server.Models
{
    internal interface IBillingEntity
    {
        int Id { get; set; }
        string GetAccountKey();
    }
}

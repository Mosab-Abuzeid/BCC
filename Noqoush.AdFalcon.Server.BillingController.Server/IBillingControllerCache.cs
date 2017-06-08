using Noqoush.AdFalcon.Server.BillingController.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noqoush.AdFalcon.Server.BillingController.Server
{
    internal interface IBillingControllerCache
    {
        bool Add<T>(T billingEntity) where T : IBillingEntity;
        bool AddAvailableFund(int accountId, decimal value);
        decimal GetAvailableFund(int accountId);
        T Get<T>(int billingEntityId) where T : IBillingEntity;
        bool Remove<T>(int billingEntityId) where T : IBillingEntity;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noqoush.AdFalcon.Server.BillingController.Client
{
    internal static class Utilities
    {
        public static int? TryGetAdgroupId(string accountKey)
        {
            if (accountKey.Split(':').Length <= 3)
                return null;
            var accountIds = accountKey.Split(':');
            var id = 0;
            int.TryParse(accountIds[accountIds.Length - 1], out id);
            return id;
        }
    }
}

using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Noqoush.AdFalcon.EventDTOs;
using Noqoush.AdFalcon.Server.BillingController.Server.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noqoush.AdFalcon.Server.BillingController.Server
{
    internal static class Utilities
    {
  
        public static Int64Value GetNewBudget(decimal? budget, decimal? dailyBudget , decimal lastSpend)
        {
            if (dailyBudget.HasValue)
                return ToInt64Value(lastSpend + dailyBudget.Value);
            else
                return ToInt64Value(budget);
        }

        public static AccountType GetAccountType(string accountKey)
        {
           return accountKey.Split(':').Length > 2 ? AccountType.AdGroup : AccountType.Campaign;
        }
        public static int GetAccountID(string accountKey)
        {
            var accountIds = accountKey.Split(':');
            var id = 0;
            int.TryParse(accountIds[accountIds.Length - 1], out id);
            return id;
        }

        public static Int64Value ToInt64Value(decimal value)
        {
            return new Int64Value { Value = (long)(value * (decimal)1E6) };
        }
        public static decimal ToDecimal(Int64Value value)
        {
            if (value == null) return 0;
            return value.Value/(decimal)1E6;
        }
        public static Int64Value ToInt64Value(decimal? value)
        {
            if (!value.HasValue) return null;
            return ToInt64Value(value.Value);
        }

        public static Int64Value ToInt64Value(DateTime value)
        {
            return new Int64Value { Value = (long)(value - DateTime.UtcNow).TotalSeconds };
        }

        public static Int64Value ToInt64ValueEndOfDay(DateTime value)
        {
            return new Int64Value { Value = (long)(value - DateTime.UtcNow.Date.AddDays(1)).TotalSeconds };
        }

        public static Int64Value ToInt64Value(this DateTime? value)
        {
            if (!value.HasValue) return null;
            return ToInt64Value(value.Value);
        }

        public static Int64Value ToInt64ValueEndOfDay(DateTime? value)
        {
            if (!value.HasValue) return null;
            return ToInt64ValueEndOfDay(value.Value);
        }

        public static string GetQueryValuesString(this AccountTrackInfo account)
        {
            var valuesStr = $@"('{account.AccountID}',{account.LastSpend.ToString()},
                                 {((int)account.AccountType).ToString()}, '{account.SpendDate.ToString(Constants.AccountTrackInfos.DateFormat)}')";
            return valuesStr;
        }

        public static T SafeGetValue<T>(this IDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal)) return default(T);
            return (T)reader.GetValue(ordinal);
        }
    }

}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistEventBroker.PubSub.Subscribtion;
using Noqoush.AdFalcon.EventDTOs;

namespace BillingController.Test
{
    using BC = Noqoush.AdFalcon.Server.BillingController.Server.BillingController;
    class Program
    {
        static void Main(string[] args)
        {
            DropCreateTables();
           // EventSubscribtionProxy.Current.Init("dummy", "localhost:50052", "dasda");
            BC.Current.Init("mysql", 1000, "billingController.general");
        }


        public static void DropCreateTables()
        {
            var accountTrack = @"CREATE TABLE {0} (
                       Id int(11) NOT NULL AUTO_INCREMENT,
                       AccountID int(11) NOT NULL,
                       AccountType int(11) NOT NULL,
                       SpendDate DateTime NOT NULL,
                       LastSpend decimal(21,12) NOT NULL,
                       PRIMARY KEY (Id),
                       UNIQUE KEY (AccountID, AccountType)
                     )";
            var accountsummary = @"CREATE TABLE {0} (
                       Id int(11) NOT NULL AUTO_INCREMENT,
                       AccountID int(11) NOT NULL,
                       AccountType int(11) NOT NULL,
                       SpendDate DateTime NOT NULL,
                       LastSpend decimal(21, 12) NOT NULL,
                       PRIMARY KEY(Id),
                       UNIQUE KEY(AccountID, AccountType)
                     )";
            using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysql"].ConnectionString))
            {
                connection.Open();
                MySqlHelper.ExecuteNonQuery(connection, string.Format("DROP TABLE if exists {0}", "bc_last_spend_track"));
                MySqlHelper.ExecuteNonQuery(connection, string.Format(accountTrack, "bc_last_spend_track"));
            }

        }
    }
}

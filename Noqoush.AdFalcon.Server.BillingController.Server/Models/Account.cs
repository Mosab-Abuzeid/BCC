namespace Noqoush.AdFalcon.Server.BillingController.Server.Models
{
    internal class Account: IBillingEntity
    {
        public int Id { get; set; }
        public decimal FundAmount { get; set; }
        public string GetAccountKey()
        {
            return Id.ToString();
        }
    }
}

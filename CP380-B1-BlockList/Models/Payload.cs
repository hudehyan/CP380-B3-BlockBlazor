
namespace CP380_B1_BlockList.Models
{
    public enum TransactionTypes
    {
        BUY, SELL, GRANT
    }

    public class Payload
    {
        public Payload(string user, TransactionTypes transactionType, int amount, string item)
        {
            this.user = user;
            this.transactiontype = transactionType;
            this.item = item;
            this.amount = amount;
        }

        public string user { get; set; }

        public TransactionTypes transactiontype { get; set; }
        
        public int amount { get; set; }
        public string item { get; set; }


    }
}

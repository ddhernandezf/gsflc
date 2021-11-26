namespace Transporte.Model.Operation
{
    public class TransactionDetail
    {
        public long? id { get; set; }
        public long transaction { get; set; }
        public decimal quantity { get; set; }
        public string description { get; set; }
        public decimal unitPrice { get; set; }
        public decimal totalPrice { get; set; }
    }
}

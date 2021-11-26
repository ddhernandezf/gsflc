namespace Transporte.Model.Report
{
    public class Item
    {
        public long id { get; set; }
        public Header transaction { get; set; }
        public decimal quantity { get; set; }
        public string description { get; set; }
        public decimal unitPrice { get; set; }
        public decimal totalPrice { get; set; }
    }
}

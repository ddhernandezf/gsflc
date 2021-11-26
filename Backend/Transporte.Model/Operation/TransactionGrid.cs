using System;
using Transporte.Model.Catalog.Vehicles;

namespace Transporte.Model.Operation
{
    public class TransactionGrid
    {
        public long id { get; set; }
        public TransactionType type { get; set; }
        public Vehicle vehicle { get; set; }
        public Movement movement { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime transactionDate { get; set; }
        public string userName { get; set; }
        public byte userId { get; set; }
        public decimal total { get; set; }
        public decimal valueTotal => total == 0 ? 0 : (type.id == 1 ? total : (total * -1));
    }
}

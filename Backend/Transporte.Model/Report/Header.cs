using System;
using Transporte.Model.Catalog.Vehicles;
using Transporte.Model.Operation;

namespace Transporte.Model.Report
{
    public class Header
    {
        public long id { get; set; }
        public Vehicle vehicle { get; set; }
        public TransactionType type { get; set; }
        public Option option { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime transactionDate { get; set; }
        public decimal total { get; set; }
    }
}

using System;
using Transporte.Model.Catalog.Expenses;
using Transporte.Model.Catalog.Services;
using Transporte.Model.Catalog.Vehicles;

namespace Transporte.Model.Operation
{
    public class Transaction
    {
        public long? id { get; set; }
        public Vehicle vehicle { get; set; }
        public Service service { get; set; }
        public Expense expense { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime transactionDate { get; set; }
        public string userName { get; set; }
        public byte userId { get; set; }
        public decimal total { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            TransactionDetail = new HashSet<TransactionDetail>();
        }

        public long Id { get; set; }
        public byte Vehicle { get; set; }
        public short? Service { get; set; }
        public short? Expense { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public byte User { get; set; }
        public decimal? Total { get; set; }

        public virtual Expense ExpenseNavigation { get; set; }
        public virtual Service ServiceNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
        public virtual Vehicle VehicleNavigation { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetail { get; set; }
    }
}

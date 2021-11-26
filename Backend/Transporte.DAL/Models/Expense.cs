using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class Expense
    {
        public Expense()
        {
            Transaction = new HashSet<Transaction>();
        }

        public short Id { get; set; }
        public byte Type { get; set; }
        public string Name { get; set; }

        public virtual ExpenseType TypeNavigation { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}

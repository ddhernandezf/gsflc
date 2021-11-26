using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class ExpenseType
    {
        public ExpenseType()
        {
            Expense = new HashSet<Expense>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Expense> Expense { get; set; }
    }
}

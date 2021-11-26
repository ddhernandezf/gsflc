using System.Collections.Generic;
using Transporte.Model.Catalog.Abstract;

namespace Transporte.Model.Catalog.Expenses
{
    public class ExpenseGrouped : ByteCatalog
    {
        public List<Expense> expenses { get; set; }
    }
}

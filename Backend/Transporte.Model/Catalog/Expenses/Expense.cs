namespace Transporte.Model.Catalog.Expenses
{
    public class Expense
    {
        public short? id { get; set; }
        public string name { get; set; }
        public ExpenseType expenseType { get; set; }
    }
}

using System.Collections.Generic;

namespace Transporte.Model.Report
{
    public class Balance
    {
        public decimal serviceTotal { get; set; }
        public decimal expenseTotal { get; set; }
        public decimal total => serviceTotal - expenseTotal;
        public List<Item> data { get; set; }
        public List<ResumeChart> resumeChart { get; set; }
    }
}

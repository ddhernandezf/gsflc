using System;
using System.Collections.Generic;

namespace Transporte.Model.Report
{
    public class BalanceFilter
    {
        public List<string> vehicles { get; set; }
        public List<string> services { get; set; }
        public List<string> expenses { get; set; }
        public bool dateRange { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool monthAndYear { get; set; }
        public byte? month { get; set; }
        public short? year { get; set; }
    }
}

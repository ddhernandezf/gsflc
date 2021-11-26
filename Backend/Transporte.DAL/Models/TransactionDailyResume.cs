using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class TransactionDailyResume
    {
        public byte Day { get; set; }
        public byte Month { get; set; }
        public short Year { get; set; }
        public byte Vehicle { get; set; }
        public decimal Total { get; set; }

        public virtual Vehicle VehicleNavigation { get; set; }
    }
}

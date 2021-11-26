using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class TransactionDetail
    {
        public long Id { get; set; }
        public long Transaction { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Transaction TransactionNavigation { get; set; }
    }
}

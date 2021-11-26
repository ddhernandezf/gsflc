using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class Service
    {
        public Service()
        {
            Transaction = new HashSet<Transaction>();
        }

        public short Id { get; set; }
        public byte Type { get; set; }
        public string Name { get; set; }

        public virtual ServiceType TypeNavigation { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}

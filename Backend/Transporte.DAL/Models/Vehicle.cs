using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Transaction = new HashSet<Transaction>();
            TransactionDailyResume = new HashSet<TransactionDailyResume>();
        }

        public byte Id { get; set; }
        public byte Type { get; set; }
        public string RegistrationType { get; set; }
        public string Registration { get; set; }
        public byte Brand { get; set; }
        public byte Model { get; set; }
        public short Year { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public virtual Brand BrandNavigation { get; set; }
        public virtual BrandModel ModelNavigation { get; set; }
        public virtual RegistrationType RegistrationTypeNavigation { get; set; }
        public virtual VehicleType TypeNavigation { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
        public virtual ICollection<TransactionDailyResume> TransactionDailyResume { get; set; }
    }
}

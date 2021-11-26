using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class ServiceType
    {
        public ServiceType()
        {
            Service = new HashSet<Service>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Service> Service { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class RegistrationType
    {
        public RegistrationType()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}

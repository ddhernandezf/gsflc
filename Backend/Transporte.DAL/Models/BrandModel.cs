using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class BrandModel
    {
        public BrandModel()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        public byte Id { get; set; }
        public byte Brand { get; set; }
        public string Name { get; set; }

        public virtual Brand BrandNavigation { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}

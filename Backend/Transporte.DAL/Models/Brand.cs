using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class Brand
    {
        public Brand()
        {
            BrandModel = new HashSet<BrandModel>();
            Vehicle = new HashSet<Vehicle>();
        }

        public byte Id { get; set; }
        public byte Type { get; set; }
        public string Name { get; set; }

        public virtual VehicleType TypeNavigation { get; set; }
        public virtual ICollection<BrandModel> BrandModel { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Brand = new HashSet<Brand>();
            Vehicle = new HashSet<Vehicle>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public bool? CanService { get; set; }
        public bool? CanExpense { get; set; }

        public virtual ICollection<Brand> Brand { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}

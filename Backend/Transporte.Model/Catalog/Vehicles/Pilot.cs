using System;

namespace Transporte.Model.Catalog.Vehicles
{
    public class Pilot
    {
        public byte? id { get; set; }
        public bool isMale { get; set; }
        public DateTime? hiringDate { get; set; }
        public DateTime bornDate { get; set; }
        public string completeName { get; set; }
    }
}

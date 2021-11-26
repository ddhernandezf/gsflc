using Transporte.Model.Catalog.Abstract;

namespace Transporte.Model.Catalog.Vehicles
{
    public class VehicleType : ByteCatalog
    {
        public bool? canService { get; set; }
        public bool? canExpense { get; set; }
    }
}

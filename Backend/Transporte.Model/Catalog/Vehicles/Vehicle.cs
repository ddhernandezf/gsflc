namespace Transporte.Model.Catalog.Vehicles
{
    public class Vehicle
    {
        public byte? id { get; set; }
        public VehicleType vehicleType { get; set; }
        public RegistrationType registrationType { get; set; }
        public string registration { get; set; }
        public Brand brand { get; set; }
        public BrandModel brandModel { get; set; }
        public short year { get; set; }
        public string name { get; set; }
        public bool active { get; set; }

        public string text => $"{brand.name} {brandModel.name} {registrationType.id} {registration}: {name}";
    }
}

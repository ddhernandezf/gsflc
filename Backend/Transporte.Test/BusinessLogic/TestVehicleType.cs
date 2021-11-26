using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTypeBL = Transporte.BL.Catalog.Vehicles.VehicleType;

namespace Transporte.Test.BusinessLogic
{
    [TestClass]
    public class TestVehicleType
    {
        private string ConnectionString => "Server=DESKTOP-NDTELRU; Database=Transporte; User Id=sa; Password=Letmein1.;";
        private VehicleTypeBL bl => new VehicleTypeBL(this.ConnectionString);

        [TestMethod]
        public void AA_Get()
        {
            Assert.AreEqual(bl.Get().Count, 2);
        }
    }
}

using System.Collections.Generic;

namespace Transporte.Model.Catalog.Vehicles
{
    public class BrandModelGrouped : Brand
    {
        public List<BrandModel> models { get; set; }
    }
}

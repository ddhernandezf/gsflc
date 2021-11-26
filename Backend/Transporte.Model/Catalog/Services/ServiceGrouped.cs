using System.Collections.Generic;
using Transporte.Model.Catalog.Abstract;

namespace Transporte.Model.Catalog.Services
{
    public class ServiceGrouped : ByteCatalog
    {
        public List<Service> services { get; set; }
    }
}

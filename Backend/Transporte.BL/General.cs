using System.Collections.Generic;
using Transporte.Model.Catalog;
using Transporte.Model.Operation;

namespace Transporte.BL
{
    public class General : LogicBase
    {
        public List<Month> GetMonths()
        {
            return new List<Month>
            {
                new Month() { id = 1, name = "Enero"},
                new Month() { id = 2, name = "Febrero"},
                new Month() { id = 3, name = "Marzo"},
                new Month() { id = 4, name = "Abril"},
                new Month() { id = 5, name = "Mayo"},
                new Month() { id = 6, name = "Junio"},
                new Month() { id = 7, name = "Julio"},
                new Month() { id = 8, name = "Agosto"},
                new Month() { id = 9, name = "Septiembre"},
                new Month() { id = 10, name = "Octubre"},
                new Month() { id = 11, name = "Noviembre"},
                new Month() { id = 12, name = "Diciembre"},
            };
        }

        public List<DocType> GetDocTypes()
        {
            return new List<DocType>
            {
                new DocType() { id = "PDF", name = "PDF"},
                new DocType() { id = "XLS", name = "Excel"}
            };
        }

        public List<TransactionType> GetTransactionType()
        {
            return new List<TransactionType>
            {
                new TransactionType() { id = 1, name = "Servicio"},
                new TransactionType() { id = 2, name = "Gasto"}
            };
        }
    }
}

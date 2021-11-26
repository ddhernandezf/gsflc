using Microsoft.AspNetCore.Mvc;
using Transporte.Model.Security;
using Transporte.Security;

namespace Transporte.API
{
    [AccessControl]
    public class CustomController : ControllerBase
    {
        public string userName => BasicAuthentication.GetUserName(HttpContext.Request.Headers["Authorization"]);
        public Login login => BasicAuthentication.GetLogin(HttpContext.Request.Headers["Authorization"]);

        public string GetMonthName(byte montNumber)
        {
            switch (montNumber)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return null;
            }
        }
    }
}

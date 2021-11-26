using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Transporte.BL;
using Transporte.BL.Security;
using Transporte.Security;

namespace Transporte.API
{
    public class AccessControl : Attribute, IAuthorizationFilter
    {
        private Response response { get; } = new Response();

        public void OnAuthorization(AuthorizationFilterContext filter)
        {
            try
            {
                string token = filter.HttpContext.Request.Headers["Token"].ToString();
                string auth = filter.HttpContext.Request.Headers["Authorization"];
                string ip = filter.HttpContext.Connection.RemoteIpAddress.ToString();
                ControllerActionDescriptor descriptor = filter.ActionDescriptor as ControllerActionDescriptor;


                if (token.IsNull() || !new User(Tool.configuration).ValidateKey(Guid.Parse(token)))
                    throw new Exception("Token no reconocido");

                if (auth.IsNull() || !BasicAuthentication.Authenticate(auth))
                    throw new Exception("Niveles de seguridad no superados");

                string userName = BasicAuthentication.GetUserName(auth);
                Log.DataToLog(userName, descriptor?.ControllerName, descriptor?.ActionName, new { ip });
            }
            catch (Exception ex)
            {
                filter.Result = response.Unauthorized(ex.CustomResponse());
            }
        }
    }
}

using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using RoleBL = Transporte.BL.Security.Role;

namespace Transporte.API.Controllers.Security
{
    [Route("Seguridad/Rol")]
    [ApiController]
    public class RoleController : CustomController
    {
        private RoleBL bl { get; } = new RoleBL(Tool.configuration);

        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.Get());
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }
    }
}
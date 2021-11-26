using Microsoft.AspNetCore.Mvc;
using Transporte.BL;

namespace Transporte.API.Controllers
{
    [Route("Secure/Test")]
    [ApiController]
    public class SecureAliveController : CustomController
    {
        [HttpGet()]
        public IActionResult Get()
        {
            string msg = $"Hello {userName} this is the IP {Tool.configuration.connectionString.transport}";
            return Ok(new { message = msg });
        }
    }
}
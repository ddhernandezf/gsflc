using Microsoft.AspNetCore.Mvc;
using Transporte.BL;

namespace Transporte.API.Controllers
{
    [Route("Test")]
    [ApiController]
    public class AliveController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get() { return Ok(new { message = "Hello, i am api" }); }
    }
}
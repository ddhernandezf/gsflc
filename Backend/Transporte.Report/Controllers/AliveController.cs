using Microsoft.AspNetCore.Mvc;

namespace Transporte.Report.Controllers
{
    [Route("Test")]
    [ApiController]
    public class AliveController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get() { return Ok(new { message = "Hello, i am report" }); }
    }
}
using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using Transporte.Model.Report;
using BalanceBL = Transporte.BL.Report.Balance;

namespace Transporte.API.Controllers.Report
{
    [Route("Reportes/Balance")]
    [ApiController]
    public class BalanceController : CustomController
    {
        private BalanceBL bl { get; } = new BalanceBL(Tool.configuration);

        [HttpPost("Opcion/{option}")]
        public IActionResult General(BalanceOptions option, [FromBody] BalanceFilter filter)
        {
            try
            {
                return Ok(bl.General(filter, option));
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
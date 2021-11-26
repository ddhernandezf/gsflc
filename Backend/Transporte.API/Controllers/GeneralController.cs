using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using GeneralBL = Transporte.BL.General;

namespace Transporte.API.Controllers
{
    [Route("General")]
    [ApiController]
    public class GeneralController : CustomController
    {
        private GeneralBL bl { get; } = new GeneralBL();

        [HttpGet("Meses")]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetMonths());
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpGet("TipoDocumento")]
        public IActionResult GetDocs()
        {
            try
            {
                return Ok(bl.GetDocTypes());
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpGet("TipoTransaccion")]
        public IActionResult GetTransactionType()
        {
            try
            {
                return Ok(bl.GetTransactionType());
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
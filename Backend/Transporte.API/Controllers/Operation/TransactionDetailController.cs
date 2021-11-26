using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using Transporte.Model.Operation;
using TransactionDetailBL = Transporte.BL.Operation.TransactionDetail;

namespace Transporte.API.Controllers.Operation
{
    [Route("Operaciones/TransaccionDetalle")]
    [ApiController]
    public class TransactionDetailController : CustomController
    {
        private TransactionDetailBL bl { get; } = new TransactionDetailBL(Tool.configuration);

        [HttpGet("Transaccion/{transactionId}")]
        public IActionResult Get(long transactionId)
        {
            try
            {
                return Ok(bl.Get(transactionId));
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpPost()]
        public IActionResult Add([FromBody] TransactionDetail model)
        {
            try
            {
                return Ok(bl.Add(model));
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpPut()]
        public IActionResult Update([FromBody] TransactionDetail model)
        {
            try
            {
                return Ok(bl.Update(model));
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpDelete("Id/{detailId}")]
        public IActionResult Delete(long detailId)
        {
            try
            {
                bl.Delete(detailId);

                return Ok();
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
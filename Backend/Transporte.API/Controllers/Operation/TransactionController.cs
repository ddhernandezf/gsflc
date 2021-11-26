using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using Transporte.Model.Operation;
using TransactionBL = Transporte.BL.Operation.Transaction;
using UserBL = Transporte.BL.Security.User;

namespace Transporte.API.Controllers.Operation
{
    [Route("Operaciones/Transacciones")]
    [ApiController]
    public class TransactionController : CustomController
    {
        private TransactionBL bl { get; } = new TransactionBL(Tool.configuration);
        private UserBL blUser { get; } = new UserBL(Tool.configuration);

        [HttpGet("Anio/{year}/Mes/{month}/CodigoVehiculo/{vehicleId}")]
        public IActionResult Get(int year, int month, byte vehicleId)
        {
            try
            {
                return Ok(bl.Get(year, month, vehicleId));
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpPost()]
        public IActionResult Add([FromBody] Transaction model)
        {
            try
            {
                model.userName = userName;
                model.userId = blUser.GetUserId(userName);

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
        public IActionResult Update([FromBody] Transaction model)
        {
            try
            {
                model.userName = userName;
                model.userId = blUser.GetUserId(userName);

                return Ok(bl.Update(model));
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpDelete("Id/{transactionId}")]
        public IActionResult Delete(long transactionId)
        {
            try
            {
                bl.Delete(transactionId);

                return Ok();
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpGet("VerificarDetalle/Transaccion/{transactionId}")]
        public IActionResult VerifyDetail(long transactionId)
        {
            try
            {
                return Ok(bl.VerifyDetail(transactionId));
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
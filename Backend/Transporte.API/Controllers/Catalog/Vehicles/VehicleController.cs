using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using Transporte.Model.Report;
using VehicleBL = Transporte.BL.Catalog.Vehicles.Vehicle;
using VehicleModel = Transporte.Model.Catalog.Vehicles.Vehicle;

namespace Transporte.API.Controllers.Catalog.Vehicles
{
    [Route("Catalogo/Vehiculos/Vehiculo")]
    [ApiController]
    public class VehicleController : CustomController
    {
        private VehicleBL bl { get; } = new VehicleBL(Tool.configuration);

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

        [HttpGet("Agrupado")]
        public IActionResult GetGrouped()
        {
            try
            {
                return Ok(bl.GetGrouped());
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpGet("Agrupado/Opcion/{option}")]
        public IActionResult GetGrouped(BalanceOptions option)
        {
            try
            {
                return Ok(bl.GetGrouped(option));
            }
            catch (Exception ex)
            {
                string error = ex.CustomResponse();
                Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                return BadRequest(error);
            }
        }

        [HttpPost()]
        public IActionResult Add([FromBody] VehicleModel model)
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
        public IActionResult Update([FromBody] VehicleModel model)
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

        [HttpDelete("Id/{vehicleId}")]
        public IActionResult Delete(byte vehicleId)
        {
            try
            {
                bl.Delete(vehicleId);

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
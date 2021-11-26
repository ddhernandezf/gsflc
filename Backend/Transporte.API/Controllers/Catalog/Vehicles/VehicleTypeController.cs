using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using VehicleTypeBL = Transporte.BL.Catalog.Vehicles.VehicleType;
using VehicleTypeModel = Transporte.Model.Catalog.Vehicles.VehicleType;

namespace Transporte.API.Controllers.Catalog.Vehicles
{
    [Route("Catalogo/Vehiculos/Tipo")]
    [ApiController]
    public class VehicleTypeController : CustomController
    {
        private VehicleTypeBL bl { get; } = new VehicleTypeBL(Tool.configuration);

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

        [HttpPost()]
        public IActionResult Add([FromBody] VehicleTypeModel model)
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
        public IActionResult Update([FromBody] VehicleTypeModel model)
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

        [HttpDelete("Id/{typeId}")]
        public IActionResult Delete(byte typeId)
        {
            try
            {
                bl.Delete(typeId);

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
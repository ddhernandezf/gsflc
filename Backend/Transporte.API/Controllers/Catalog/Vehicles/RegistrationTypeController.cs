using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using Transporte.Model.Catalog.Vehicles;
using RegistrationTypeBL = Transporte.BL.Catalog.Vehicles.RegistrationType;

namespace Transporte.API.Controllers.Catalog.Vehicles
{
    [Route("Catalogo/Vehiculos/Matricula")]
    [ApiController]
    public class RegistrationTypeController : CustomController
    {
        private RegistrationTypeBL bl { get; } = new RegistrationTypeBL(Tool.configuration);

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
        public IActionResult Add([FromBody] RegistrationType model)
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
        public IActionResult Update([FromBody] RegistrationType model)
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

        [HttpDelete("Id/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                bl.Delete(id);

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
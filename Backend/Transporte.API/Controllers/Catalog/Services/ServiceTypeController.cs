using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using ServiceTypeBL = Transporte.BL.Catalog.Services.ServiceType;
using ServiceTypeModel = Transporte.Model.Catalog.Services.ServiceType;

namespace Transporte.API.Controllers.Catalog.Services
{
    [Route("Catalogo/Servicios/Tipo")]
    [ApiController]
    public class ServiceTypeController : CustomController
    {
        private ServiceTypeBL bl { get; } = new ServiceTypeBL(Tool.configuration);

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
        public IActionResult Add([FromBody] ServiceTypeModel model)
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
        public IActionResult Update([FromBody] ServiceTypeModel model)
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

        [HttpDelete("Id/{serviceId}")]
        public IActionResult Delete(byte serviceId)
        {
            try
            {
                bl.Delete(serviceId);

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
using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using ServiceBL = Transporte.BL.Catalog.Services.Service;
using ServiceModel = Transporte.Model.Catalog.Services.Service;

namespace Transporte.API.Controllers.Catalog.Services
{
    [Route("Catalogo/Servicios/Servicio")]
    [ApiController]
    public class ServiceController : CustomController
    {
        private ServiceBL bl { get; } = new ServiceBL(Tool.configuration);

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

        [HttpPost()]
        public IActionResult Add([FromBody] ServiceModel model)
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
        public IActionResult Update([FromBody] ServiceModel model)
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
        public IActionResult Delete(short serviceId)
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
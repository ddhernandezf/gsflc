using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Transporte.BL;
using BrandBL = Transporte.BL.Catalog.Vehicles.Brand;
using BrandMdl = Transporte.Model.Catalog.Vehicles.Brand;

namespace Transporte.API.Controllers.Catalog.Vehicles
{
    [Route("Catalogo/Vehiculos/Marca")]
    [ApiController]
    public class BrandController : CustomController
    {
        private BrandBL bl { get; } = new BrandBL(Tool.configuration);

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
        public IActionResult Add([FromBody] BrandMdl model)
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
        public IActionResult Update([FromBody] BrandMdl model)
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

        [HttpDelete("Id/{brandId}")]
        public IActionResult Delete(byte brandId)
        {
            try
            {
                bl.Delete(brandId);

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
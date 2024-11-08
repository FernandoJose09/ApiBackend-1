using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Newtonsoft.Json;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Hosting;
using reportesApi.Models.Compras;

namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class Inv_TiposMovimientoController: ControllerBase
    {
   
        private readonly Inv_TiposMovimientoService _Inv_TiposMovimientoService;
        private readonly ILogger<Inv_TiposMovimientoController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public Inv_TiposMovimientoController(Inv_TiposMovimientoService Inv_TiposMovimientoService, ILogger<Inv_TiposMovimientoController> logger, IJwtAuthenticationService authService) {
            _Inv_TiposMovimientoService = Inv_TiposMovimientoService;
            _logger = logger;
       
            _authService = authService;
        

            
            
        }


        [HttpPost("InsertInv_TiposMovimientoModel")]
        public IActionResult InsertInv_TiposMovimiento([FromBody] InsertInv_TiposMovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {   
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _Inv_TiposMovimientoService.InsertInv_TiposMovimiento(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

      [HttpGet("GetInv_TiposMovimiento")]
        public IActionResult GetInv_TiposMovimiento()
        {
            var objectResponse = Helper.GetStructResponse();
             var resultado = _Inv_TiposMovimientoService.GetInv_TiposMovimiento();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Existencia cargados exitosamente";
               
               
               

                // Llamando a la función y recibiendo los dos valores.
               
                 objectResponse.response = resultado;
            }

            catch (System.Exception ex)
            {
              
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateInv_TiposMovimiento")]
        public IActionResult UpdateInv_TiposMovimiento([FromBody] UpdateInv_TiposMovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _Inv_TiposMovimientoService.UpdateInv_TiposMovimiento(req);

                ;

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteInv_TiposMovimiento/{id}")]
        public IActionResult DeleteInv_TiposMovimiento([FromRoute] int id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";
                _Inv_TiposMovimientoService.DeleteInv_TiposMovimiento(id);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}
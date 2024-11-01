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
    public class Inv_MovimientosController: ControllerBase
    {
   
        private readonly Inv_MovimientosService _Inv_MovimientosService;
        private readonly ILogger<Inv_MovimientosController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public Inv_MovimientosController(Inv_MovimientosService Inv_MovimientosService, ILogger<Inv_MovimientosController> logger, IJwtAuthenticationService authService) {
            _Inv_MovimientosService = Inv_MovimientosService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }


        [HttpPost("InsertInv_MovimientosModel")]
        public IActionResult InsertInv_Movimientos([FromBody] InsertInv_MovimientosModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {   
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _Inv_MovimientosService.InsertInv_Movimientos(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

      [HttpGet("GetInv_Movimientos")]
        public IActionResult GetInv_Movimientos()
        {
            var objectResponse = Helper.GetStructResponse();
             var resultado = _Inv_MovimientosService.GetInv_Movimientos();

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

        [HttpPut("UpdateInv_Movimientos")]
        public IActionResult UpdateInv_Movimientos([FromBody] UpdateInv_MovimientosModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _Inv_MovimientosService.UpdateInv_Movimientos(req);

                ;

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteInv_Movimientos/{id}")]
        public IActionResult DeleteInv_Movimientos([FromRoute] int id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";
                _Inv_MovimientosService.DeleteInv_Movimientos(id);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}
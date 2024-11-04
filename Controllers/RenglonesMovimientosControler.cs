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
    public class RenglonesMovimientoController: ControllerBase
    {
   
        private readonly Inv_RenglonesMovimientoService _Inv_RenglonesMovimientoService;
        private readonly ILogger<RenglonesMovimientoController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public RenglonesMovimientoController(Inv_RenglonesMovimientoService Inv_RenglonesMovimientoService, ILogger<RenglonesMovimientoController> logger, IJwtAuthenticationService authService) {
            _Inv_RenglonesMovimientoService = Inv_RenglonesMovimientoService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }


        [HttpPost("InsertInv_RenglonesMovimientos")]
        public IActionResult InsertInv_RenglonesMovimeinto([FromBody] InsertInv_RenglonesMovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {   
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _Inv_RenglonesMovimientoService.InsertInv_RenglonesMovimiento(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

    //   [HttpGet("GetInv_RenglonesMovimiento")]
    //     public IActionResult GetInv_RenglonesMovimiento([FromQuery] int IdMovimiento)
    //     {
    //         var objectResponse = Helper.GetStructResponse();
            

    //         try
    //         {
    //             objectResponse.StatusCode = (int)HttpStatusCode.OK;
    //             objectResponse.success = true;
    //             objectResponse.message = "Existencia cargados exitosamente";
    //             var resultado = _Inv_RenglonesMovimientoService.GetInv_RenglonesMovimiento(IdMovimiento);
               
               

    //             // Llamando a la función y recibiendo los dos valores.
               
    //              objectResponse.response = resultado;
    //         }

    //         catch (System.Exception ex)
    //         {
    //           objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
    //           objectResponse.success = false;
    //             objectResponse.message = ex.Message;
    //         }

    //         return new JsonResult(objectResponse);
    //     }
                 [HttpGet("GetInv_RenglonesMovimiento")]
        public IActionResult GetDetalleReceta([FromQuery] int IdMovimiento)
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Data cargado exitosamente";
                var resultado = _Inv_RenglonesMovimientoService.GetInv_RenglonesMovimiento(IdMovimiento);
               
               

                // Llamando a la función y recibiendo los dos valores.
               
                 objectResponse.response = resultado;
            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }


        [HttpPut("UpdateInv_RenglonesMovimiento")]
        public IActionResult UpdateInv_RenglonesMovimiento([FromBody] UpdateInv_RenglonesMovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _Inv_RenglonesMovimientoService.UpdateInv_RenglonesMovimiento(req);

                ;

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteInv_RenglonesMovimiento/{id}")]
        public IActionResult DeleteInv_Movimientos([FromRoute] int id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";
                _Inv_RenglonesMovimientoService.DeleteInv_RenglonesMovimiento(id);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}
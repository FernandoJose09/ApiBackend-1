using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Models;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using MyNamespace.Services;

namespace reportesApi.Controllers
{
    [Route("api")]
    public class TraspasoController : ControllerBase
    {
        private readonly TraspasoService _TraspasoService;
        private readonly ILogger<TraspasoController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TraspasoController(TraspasoService traspasoService, ILogger<TraspasoController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _TraspasoService = traspasoService;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        // Estructura común para las respuestas
        private object CreateResponse(int statusCode, bool success, string message, object response = null)
        {
            return new
            {
                StatusCode = statusCode,
                success = success,
                message = message,
                response = response
            };
        }

        [HttpPost("InsertTraspaso")]
        public IActionResult InsertTraspaso([FromBody] InsertTraspasoModel req)
        {
            try
            {
                var result = _TraspasoService.InsertTraspaso(req);
                var objectResponse = CreateResponse((int)HttpStatusCode.OK, true, "Traspaso insertado con éxito", result);
                return new JsonResult(objectResponse);
            }
            catch (System.Exception ex)
            {
                var objectResponse = CreateResponse((int)HttpStatusCode.InternalServerError, false, ex.Message);
                return new JsonResult(objectResponse);
            }
        }

        [HttpGet("GetTraspaso")]
        public IActionResult GetTraspaso([FromQuery] int IdTipoMovimiento)
        {
            try
            {
                var result = _TraspasoService.UpdateTraspaso(req.Id, req.Estatus, req.Comentarios, req.CantidadTotalMovimiento);
                var objectResponse = CreateResponse((int)HttpStatusCode.OK, true, "Traspasos cargados exitosamente", resultado);
                return new JsonResult(objectResponse);
            }
            catch (System.Exception ex)
            {
                var objectResponse = CreateResponse((int)HttpStatusCode.InternalServerError, false, ex.Message);
                return new JsonResult(objectResponse);
            }
        }

   [HttpPut("UpdateTraspaso")]
public IActionResult UpdateTraspaso([FromBody] UpdateTraspasoModel req)
{
    try
    {
        _TraspasoService.UpdateTraspaso(req.Id, req.Estatus, req.Comentarios, req.CantidadTotalMovimiento);  // No se asigna nada
        var objectResponse = CreateResponse((int)HttpStatusCode.OK, true, "Traspaso actualizado con éxito");
        return new JsonResult(objectResponse);
    }
    catch (System.Exception ex)
    {
        var objectResponse = CreateResponse((int)HttpStatusCode.InternalServerError, false, ex.Message);
        return new JsonResult(objectResponse);
    }
}


        [HttpDelete("DeleteTraspaso/{id}")]
        public IActionResult DeleteTraspaso([FromRoute] int id)
        {
            try
            {
                _TraspasoService.DeleteTraspaso(id);
                var objectResponse = CreateResponse((int)HttpStatusCode.OK, true, "Traspaso eliminado con éxito");
                return new JsonResult(objectResponse);
            }
            catch (System.Exception ex)
            {
                var objectResponse = CreateResponse((int)HttpStatusCode.InternalServerError, false, ex.Message);
                return new JsonResult(objectResponse);
            }
        }
    }
}

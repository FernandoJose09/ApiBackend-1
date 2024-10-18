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
using System.Data; // Agrega esta línea
using ClosedXML.Excel;


namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class DetalleEntradaController: ControllerBase
    {
   
        private readonly DetalleEntradaService _DetalleEntradaService;
        private readonly ILogger<DetalleEntradaController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public DetalleEntradaController(DetalleEntradaService DetalleEntradaService, ILogger<DetalleEntradaController> logger, IJwtAuthenticationService authService) {
            _DetalleEntradaService = DetalleEntradaService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }


        [HttpPost("InsertDetalleEntrada")]
        public IActionResult InsertDetalleEntrada([FromBody] InsertDetalleEntradaModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {   
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _DetalleEntradaService.InsertDetalleEntrada(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetDetalleEntradas")]
        public IActionResult GetDetalleEntrada([FromQuery] int IdEntrada)
        {
            var objectResponse = Helper.GetStructResponse();

    try
    {
        var resultado = _DetalleEntradaService.GetDetalleEntrada(IdEntrada);

        if (resultado == null || resultado.Count == 0)
        {
            objectResponse.StatusCode = (int)HttpStatusCode.NoContent;
            objectResponse.success = false;
            objectResponse.message = "No se encontraron detalles de la entrada.";
            return new JsonResult(objectResponse);
        }

        // Crear el archivo Excel
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("DetalleEntrada");

            // Agregar encabezados
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "IdEntrada";
            worksheet.Cell(1, 3).Value = "Insumo";
            worksheet.Cell(1, 4).Value = "DescripcionInsumo";
            worksheet.Cell(1, 5).Value = "Cantidad";
            worksheet.Cell(1, 6).Value = "Costo";
            worksheet.Cell(1, 7).Value = "Estatus";
            worksheet.Cell(1, 8).Value = "UsuarioRegistra";
            // Aplicar estilos (Color de fondo y color de texto)
            var headerRange = worksheet.Range("A1:H1"); // Seleccionamos el rango de encabezados
            headerRange.Style.Fill.BackgroundColor = XLColor.Green; // Color de fondo azul claro
            headerRange.Style.Font.FontColor = XLColor.White; // Texto blanco
            headerRange.Style.Font.Bold = true; // Texto en negrita

            // Agregar datos
            int row = 2; // Comenzamos en la segunda fila
            foreach (var item in resultado)
            {
                worksheet.Cell(row, 1).Value = item.Id;
                worksheet.Cell(row, 2).Value = item.IdEntrada;
                worksheet.Cell(row, 3).Value = item.Insumo;
                worksheet.Cell(row, 4).Value = item.DescripcionInsumo;
                worksheet.Cell(row, 5).Value = item.Cantidad;
                worksheet.Cell(row, 6).Value = item.Costo;
                worksheet.Cell(row, 7).Value = item.Estatus;
                worksheet.Cell(row, 8).Value = item.UsuarioRegistra;
                var dataRange = worksheet.Range($"A{row}:H{row}");
                if (row % 2 == 0) // Alternar color de fondo para filas pares
                {
                    dataRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                }

                
                row++;
            }

            // Guardar en un MemoryStream
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0;
                string excelName = $"DetalleEntrada-{System.DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
    }
    catch (Exception ex)
    {
        objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
        objectResponse.success = false;
        objectResponse.message = ex.Message;
        return new JsonResult(objectResponse);
    }
        }


        [HttpPut("UpdateDetalleEntrada")]
        public IActionResult UpdateDetalleEntrada([FromBody] UpdateDetalleEntradaModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _DetalleEntradaService.UpdateDetalleEntrada(req);

                ;

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteDetalleEntrada/{id}")]
        public IActionResult DeleteDetalleEntrada([FromRoute] int id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                _DetalleEntradaService.DeleteDetalleEntrada(id);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}
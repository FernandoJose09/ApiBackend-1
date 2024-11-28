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
using ClosedXML.Excel;

namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class TraspasoController: ControllerBase
    {

   
        private readonly TraspasoService _TraspasoService;
        private readonly ILogger<TraspasoController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public TraspasoController(TraspasoService TraspasoService, ILogger<TraspasoController> logger, IJwtAuthenticationService authService) {
            _TraspasoService = TraspasoService;
            _logger = logger;
       
            _authService = authService;
            

            
            
        }


        [HttpPost("InsertTraspaso")]
        public IActionResult InsertTraspaso([FromBody] InsertTraspasoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {   
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _TraspasoService.InsertTraspaso(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }



        [HttpPut("UpdateTraspaso")]
        public IActionResult UpdateTraspaso([FromBody] UpdateTraspasoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _TraspasoService.UpdateTraspaso(req);

                

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteTraspaso/{id}")]
        public IActionResult DeleteTraspaso([FromRoute] int id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";
                _TraspasoService.DeleteTraspaso(id);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
        [HttpGet("GetTraspasoOrigen")]
        public IActionResult GetTraspaso(int? IdAlmacen = null, DateTime? fechainicial = null, DateTime? fechaFinal = null, int? tipoMovimiento = null)
            {
               var objectResponse = Helper.GetStructResponse();

    try
    {
        var resultado = _TraspasoService.GetTraspaso(IdAlmacen, fechainicial, fechaFinal, tipoMovimiento);

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
            var worksheet = workbook.Worksheets.Add("Transferencias");

            // Agregar encabezados   
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "IdAlmacenOrigen";
            worksheet.Cell(1, 3).Value = "IdAlmacenDestino";
            worksheet.Cell(1, 4).Value = "Insumo";
            worksheet.Cell(1, 5).Value = "DescripcionInsumo";
            worksheet.Cell(1, 6).Value = "CantidadTotal";
            worksheet.Cell(1, 7).Value = "TipoMovimiento";
            worksheet.Cell(1, 8).Value = "Estatus";
            worksheet.Cell(1, 9).Value = "Fecha_registra";
            worksheet.Cell(1, 10).Value = "UsuarioRegistra";
            worksheet.Cell(1, 11).Value = "Comentarios";
           

            // Aplicar estilos (Color de fondo y color de texto)
            var headerRange = worksheet.Range("A1:K1"); // Seleccionamos el rango de encabezados
            headerRange.Style.Fill.BackgroundColor = XLColor.BananaMania; // Color de fondo azul claro
            headerRange.Style.Font.FontColor = XLColor.White; // Texto blanco
            headerRange.Style.Font.Bold = true; // Texto en negrita

            // Agregar datos
            int row = 2; // Comenzamos en la segunda fila
            foreach (var item in resultado)
            {
                worksheet.Cell(row, 1).Value = item.Id;
                worksheet.Cell(row, 2).Value = item.IdAlmacenOrigen;
                worksheet.Cell(row, 3).Value = item.IdAlmacenDestino;
                worksheet.Cell(row, 4).Value = item.Insumos;
                worksheet.Cell(row, 5).Value = item.DescripcionInsumo;
                worksheet.Cell(row, 6).Value = item.CantidadTotal;
                worksheet.Cell(row, 7).Value = item.TipoMovimiento;
                worksheet.Cell(row, 8).Value = item.Estatus;
                worksheet.Cell(row, 9).Value = item.FechaRegistra;
                worksheet.Cell(row, 10).Value = item.UsuarioRegistra;
                worksheet.Cell(row, 11).Value = item.Comentarios;
                var dataRange = worksheet.Range($"A{row}:K{row}");
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
                string excelName = $"TraspasoOrigen-{System.DateTime.Now:yyyyMMddHHmmss}.xlsx";
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

      [HttpGet("GetTraspasoDestino")]
        public IActionResult GetTraspaso2(int? IdAlmacen = null, DateTime? fechainicial = null, DateTime? fechaFinal = null, int? tipoMovimiento = null)
            {
               var objectResponse = Helper.GetStructResponse();

    try
    {
        var resultado = _TraspasoService.GetTraspaso2(IdAlmacen, fechainicial, fechaFinal, tipoMovimiento);

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
            var worksheet = workbook.Worksheets.Add("Transferencias");

            // Agregar encabezados   
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "IdAlmacenOrigen";
            worksheet.Cell(1, 3).Value = "IdAlmacenDestino";
            worksheet.Cell(1, 4).Value = "Insumo";
            worksheet.Cell(1, 5).Value = "DescripcionInsumo";
            worksheet.Cell(1, 6).Value = "CantidadTotal";
            worksheet.Cell(1, 7).Value = "TipoMovimiento";
            worksheet.Cell(1, 8).Value = "Estatus";
            worksheet.Cell(1, 9).Value = "Fecha_registra";
            worksheet.Cell(1, 10).Value = "UsuarioRegistra";
            worksheet.Cell(1, 11).Value = "Comentarios";
           

            // Aplicar estilos (Color de fondo y color de texto)
            var headerRange = worksheet.Range("A1:K1"); // Seleccionamos el rango de encabezados
            headerRange.Style.Fill.BackgroundColor = XLColor.Amber; // Color de fondo azul claro
            headerRange.Style.Font.FontColor = XLColor.White; // Texto blanco
            headerRange.Style.Font.Bold = true; // Texto en negrita

            // Agregar datos
            int row = 2; // Comenzamos en la segunda fila
            foreach (var item in resultado)
            {
                worksheet.Cell(row, 1).Value = item.Id;
                worksheet.Cell(row, 2).Value = item.IdAlmacenOrigen;
                worksheet.Cell(row, 3).Value = item.IdAlmacenDestino;
                worksheet.Cell(row, 4).Value = item.Insumos;
                worksheet.Cell(row, 5).Value = item.DescripcionInsumo;
                worksheet.Cell(row, 6).Value = item.CantidadTotal;
                worksheet.Cell(row, 7).Value = item.TipoMovimiento;
                worksheet.Cell(row, 8).Value = item.Estatus;
                worksheet.Cell(row, 9).Value = item.FechaRegistra;
                worksheet.Cell(row, 10).Value = item.UsuarioRegistra;
                worksheet.Cell(row, 11).Value = item.Comentarios;
                var dataRange = worksheet.Range($"A{row}:K{row}");
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
                string excelName = $"TraspasoDestino-{System.DateTime.Now:yyyyMMddHHmmss}.xlsx";
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
}
}
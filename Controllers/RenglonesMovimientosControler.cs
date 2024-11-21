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
        [HttpGet("ExportInv_RenglonesMovimientoToExcel")]
        public IActionResult ExportInv_RenglonesMovimientoToExcel([FromQuery] int IdMovimiento)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
        
                var data = _Inv_RenglonesMovimientoService.GetInv_RenglonesMovimiento(IdMovimiento);

        
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("RenglonesMovimiento");

            
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "IdMovimiento";
                    worksheet.Cells[1, 3].Value = "Insumo";
                    worksheet.Cells[1, 4].Value = "DescripcionInsumo";
                    worksheet.Cells[1, 5].Value = "Cantidad";
                    worksheet.Cells[1, 6].Value = "Costo";
                    worksheet.Cells[1, 7].Value = "Estatus";
                    worksheet.Cells[1, 8].Value = "FechaRegistro";
                    worksheet.Cells[1, 9].Value = "usuarioRegistra";
                    worksheet.Cells[1, 9].Value = "Fechainicial";

            
                    int row = 2;
                    foreach (var item in data)
                    {
                        worksheet.Cells[row, 1].Value = item.Id;
                        worksheet.Cells[row, 2].Value = item.IdMovimiento;       
                        worksheet.Cells[row, 3].Value = item.Insumo;
                        worksheet.Cells[row, 4].Value = item.DescripcionInsumo; 
                        worksheet.Cells[row, 5].Value = item.Cantidad;  
                        worksheet.Cells[row, 6].Value = item.Costo;
                        worksheet.Cells[row, 7].Value = item.Estatus;
                        worksheet.Cells[row, 8].Value = item.FechaRegistro;
                        worksheet.Cells[row, 9].Value = item.Usuario_registra;   
                        worksheet.Cells[row, 10].Value = item.Fechainicial;
                        row++;
                    }

            
                    using (var range = worksheet.Cells[1, 1, 1, 6])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    }

            
                    var excelBytes = package.GetAsByteArray();

            
                    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RenglonesMovimiento.xlsx");
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
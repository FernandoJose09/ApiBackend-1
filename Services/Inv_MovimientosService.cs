using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using reportesApi.Models.Compras;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
namespace reportesApi.Services
{
    public class Inv_MovimientosService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public Inv_MovimientosService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetInv_MovimientosModel> GetInv_Movimientos(int IdTipoMovimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@IdTipoMovimiento", SqlDbType = SqlDbType.Int, Value = IdTipoMovimiento });

            List<GetInv_MovimientosModel> lista = new List<GetInv_MovimientosModel>();
            try
            {
                
                DataSet ds = dac.Fill("sp_get_Movimientos", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetInv_MovimientosModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdTipoMovimiento = int.Parse(dataRow["IdTipoMovimiento"].ToString()),
                        IdAlmacen = int.Parse(dataRow["IdAlmacen"].ToString()),
                        Fecha = dataRow["Fecha"].ToString(),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
                        IdUsuario = int.Parse(dataRow["IdUsuario"].ToString()),

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return lista;
        }

        public string InsertInv_Movimientos(InsertInv_MovimientosModel Inv_Movimientos)
        {
           int IdTipoMovimiento;
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@IdTipoMovimiento", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_Movimientos.IdTipoMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacen", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_Movimientos.IdAlmacen});
            parametros.Add(new SqlParameter { ParameterName = "@IdUsuario", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_Movimientos.IdUsuario });

             try 
            {
                DataSet ds = dac.Fill("sp_insert_Movimientos", parametros);
                IdTipoMovimiento = ds.Tables[0].AsEnumerable().Select(dataRow => int.Parse(dataRow["IdTipoMovimiento"].ToString())).ToList()[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return IdTipoMovimiento.ToString();
        }

        public string UpdateInv_Movimientos(UpdateInv_MovimientosModel Inv_Movimientos)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;


            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_Movimientos.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdTipoMovimiento", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_Movimientos.IdTipoMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacen", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_Movimientos.IdAlmacen});
            parametros.Add(new SqlParameter { ParameterName = "@IdUsuario", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_Movimientos.IdUsuario });


            try
            {
                DataSet ds = dac.Fill("sp_update_Movimientos", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteInv_Movimientos(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("sp_delete_Movimientos", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
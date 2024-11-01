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
    public class ExistenciasService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public ExistenciasService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetExistenciasModel> GetExistencias()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            GetExistenciasModel ordencompra = new GetExistenciasModel();

            List<GetExistenciasModel> lista = new List<GetExistenciasModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("sp_get_Existencias", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetExistenciasModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Fecha = dataRow["Fecha"].ToString(),
                        Insumo = dataRow["Insumo"].ToString(),
                        IdAlmacen = int.Parse(dataRow["IdAlmacen"].ToString()),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
                        FechaRegistro = dataRow["FechaRegistro"].ToString(),
                        Cantidad = decimal.Parse(dataRow["Cantidad"].ToString()),
                        Usuario_registra = int.Parse(dataRow["Usuario_registra"].ToString()),

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public string InsertExistencias(InsertExistenciasModel existencias)
        {
           
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.Int, Value = existencias.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = existencias.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacen", SqlDbType = System.Data.SqlDbType.Int, Value = existencias.IdAlmacen});
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = existencias.Usuario_registra });

             try 
            {
                DataSet ds = dac.Fill("sp_insert_Existencias", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return mensaje;
        }

        public string UpdateExistencias(UpdateExistenciasModel existencias)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;


            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = existencias.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.Int, Value = existencias.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = existencias.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacen", SqlDbType = System.Data.SqlDbType.Int, Value = existencias.IdAlmacen});
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = existencias.Usuario_registra });


            try
            {
                DataSet ds = dac.Fill("sp_update_Existencias", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteExistencias(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("sp_delete_Existencias", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
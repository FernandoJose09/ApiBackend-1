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
    public class Inv_TiposMovimientoService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public Inv_TiposMovimientoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetInv_TiposMovimientoModel> GetInv_TiposMovimiento()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            GetInv_TiposMovimientoModel Inv_TiposMovimiento = new GetInv_TiposMovimientoModel();

            List<GetInv_TiposMovimientoModel> lista = new List<GetInv_TiposMovimientoModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("sp_get_TiposMovimiento", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetInv_TiposMovimientoModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Nombre = dataRow["Nombre"].ToString(),
                        EntradaSalida = int.Parse(dataRow["EntradaSalida"].ToString()),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
                        FechaRegistro = dataRow["FechaRegistro"].ToString(),
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

        public string InsertInv_TiposMovimiento(InsertInv_TiposMovimientoModel Inv_TiposMovimiento)
        {
           
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = Inv_TiposMovimiento.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@EntradaSalida", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_TiposMovimiento.EntradaSalida});
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_TiposMovimiento.Usuario_registra });

             try 
            {
                DataSet ds = dac.Fill("sp_insert_TiposMovimiento", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return mensaje;
        }

        public string UpdateInv_TiposMovimiento(UpdateInv_TiposMovimientoModel Inv_TiposMovimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;


            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_TiposMovimiento.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = Inv_TiposMovimiento.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@EntradaSalida", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_TiposMovimiento.EntradaSalida});
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = Inv_TiposMovimiento.Usuario_registra });


            try
            {
                DataSet ds = dac.Fill("sp_update_TiposMovimiento", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteInv_TiposMovimiento(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("sp_delete_TiposMovimiento", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
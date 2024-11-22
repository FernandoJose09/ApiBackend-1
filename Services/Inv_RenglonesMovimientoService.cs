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
    public class Inv_RenglonesMovimientoService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public Inv_RenglonesMovimientoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

              public List<GetInv_RenglonesMovimientoModel> GetInv_RenglonesMovimiento(DateTime? Fechainicial = null, DateTime? Fechafinal = null)
        {

            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "@Fechainicial", SqlDbType = SqlDbType.Date, Value = Fechainicial });
            parametros.Add(new SqlParameter { ParameterName = "@Fechafinal", SqlDbType = SqlDbType.Date, Value = Fechafinal });

            
            

            List<GetInv_RenglonesMovimientoModel> lista = new List<GetInv_RenglonesMovimientoModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_RenglonesMovimiento", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetInv_RenglonesMovimientoModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdMovimiento = int.Parse(dataRow["IdMovimiento"].ToString()),
                        Insumo = dataRow["Insumo"].ToString(),
                        DescripcionInsumo = dataRow["DescripcionInsumo"].ToString(),
                        Cantidad = decimal.Parse(dataRow["Cantidad"].ToString()),
                        Costo = decimal.Parse(dataRow["Costo"].ToString()),
                        FechaRegistro = dataRow["FechaRegistro"].ToString(),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
                        Usuario_registra = dataRow["Usuario_registra"].ToString(),
                        
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

        public string InsertInv_RenglonesMovimiento(InsertInv_RenglonesMovimientoModel rm)
        {
           int IdMovimiento;
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@IdMovimiento", SqlDbType = System.Data.SqlDbType.Int, Value = rm.IdMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.VarChar, Value = rm.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = rm.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = System.Data.SqlDbType.Decimal, Value = rm.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = rm.Usuario_registra });
             try 
            {
                DataSet ds = dac.Fill("sp_insert_RenglonesMovimiento", parametros);
                IdMovimiento = ds.Tables[0].AsEnumerable().Select(dataRow => int.Parse(dataRow["IdMovimiento"].ToString())).ToList()[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return IdMovimiento.ToString();
        }

        public string UpdateInv_RenglonesMovimiento(UpdateInv_RenglonesMovimientoModel rm)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;


            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = rm.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdMovimiento", SqlDbType = System.Data.SqlDbType.Int, Value = rm.IdMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.VarChar, Value = rm.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = rm.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = System.Data.SqlDbType.Decimal, Value = rm.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = rm.Usuario_registra });
            try
            {
                DataSet ds = dac.Fill("sp_update_RenglonesMovimiento", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteInv_RenglonesMovimiento(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("sp_delete_RenglonesMovimiento", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
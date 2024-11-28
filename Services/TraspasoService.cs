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
    public class TraspasoService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public TraspasoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetTraspasoModel> GetTraspaso(int? IdAlmacen = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null, int? tipoMovimiento = null)
{
    ConexionDataAccess dac = new ConexionDataAccess(connection);
    List<GetTraspasoModel> lista = new List<GetTraspasoModel>();
    parametros = new ArrayList
    {
        new SqlParameter { ParameterName = "@Fechainicial", SqlDbType = SqlDbType.DateTime, Value = (object)fechaInicial ?? DBNull.Value },
        new SqlParameter { ParameterName = "@Fechafinal", SqlDbType = SqlDbType.DateTime, Value = (object)fechaFinal ?? DBNull.Value },
        new SqlParameter { ParameterName = "@IdAlmacenOrigen", SqlDbType = SqlDbType.Int, Value = (object)IdAlmacen ?? DBNull.Value },
        new SqlParameter { ParameterName = "@TipoMovimiento", SqlDbType = SqlDbType.Int, Value = (object)tipoMovimiento ?? DBNull.Value }
    };

    try
    {
        DataSet ds = dac.Fill("sp_get_Traspaso", parametros);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lista = ds.Tables[0].AsEnumerable()
                .Select(dataRow => new GetTraspasoModel
                {
                    Id = int.Parse(dataRow["Id"].ToString()),
                    IdAlmacenOrigen = int.Parse(dataRow["IdAlmacenOrigen"].ToString()),
                    IdAlmacenDestino = int.Parse(dataRow["IdAlmacenDestino"].ToString()),
                    UsuarioRegistra = dataRow["UsuarioRegistra"].ToString(),
                    Estatus = int.Parse(dataRow["Estatus"].ToString()),
                    TipoMovimiento = int.Parse(dataRow["TipoMovimiento"].ToString()),
                    CantidadTotal = decimal.Parse(dataRow["CantidadTotal"].ToString()),
                    Comentarios = dataRow["Comentarios"].ToString(),
                    FechaRegistra = dataRow["FechaRegistra"].ToString(), 
                    Insumos = int.Parse(dataRow["Insumos"].ToString()),
                    DescripcionInsumo = dataRow["DescripcionInsumo"].ToString()
                })
                .ToList();
        }
    }
    catch (Exception ex)
    {
        // Maneja la excepción apropiadamente en lugar de lanzarla directamente
        throw new Exception("Error al obtener los datos de traspaso: " + ex.Message, ex);
    }

    return lista;
}


  public List<GetTraspasoModel> GetTraspaso2(int? IdAlmacen = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null, int? tipoMovimiento = null)
{
    ConexionDataAccess dac = new ConexionDataAccess(connection);
    List<GetTraspasoModel> lista = new List<GetTraspasoModel>();
    parametros = new ArrayList
    {
        new SqlParameter { ParameterName = "@Fechainicial", SqlDbType = SqlDbType.DateTime, Value = (object)fechaInicial ?? DBNull.Value },
        new SqlParameter { ParameterName = "@Fechafinal", SqlDbType = SqlDbType.DateTime, Value = (object)fechaFinal ?? DBNull.Value },
        new SqlParameter { ParameterName = "@IdAlmacenDestino", SqlDbType = SqlDbType.Int, Value = (object)IdAlmacen ?? DBNull.Value },
        new SqlParameter { ParameterName = "@TipoMovimiento", SqlDbType = SqlDbType.Int, Value = (object)tipoMovimiento ?? DBNull.Value }
    };

    try
    {
        DataSet ds = dac.Fill("sp_get_Traspaso2", parametros);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lista = ds.Tables[0].AsEnumerable()
                .Select(dataRow => new GetTraspasoModel
                {
                    Id = int.Parse(dataRow["Id"].ToString()),
                    IdAlmacenOrigen = int.Parse(dataRow["IdAlmacenOrigen"].ToString()),
                    IdAlmacenDestino = int.Parse(dataRow["IdAlmacenDestino"].ToString()),
                    UsuarioRegistra = dataRow["UsuarioRegistra"].ToString(),
                    Estatus = int.Parse(dataRow["Estatus"].ToString()),
                    TipoMovimiento = int.Parse(dataRow["TipoMovimiento"].ToString()),
                    CantidadTotal = decimal.Parse(dataRow["CantidadTotal"].ToString()),
                    Comentarios = dataRow["Comentarios"].ToString(),
                    FechaRegistra = dataRow["FechaRegistra"].ToString(), 
                    Insumos = int.Parse(dataRow["Insumos"].ToString()),
                    DescripcionInsumo = dataRow["DescripcionInsumo"].ToString()
                })
                .ToList();
        }
    }
    catch (Exception ex)
    {
        // Maneja la excepción apropiadamente en lugar de lanzarla directamente
        throw new Exception("Error al obtener los datos de traspaso: " + ex.Message, ex);
    }

    return lista;
}

       public string InsertTraspaso(InsertTraspasoModel Traspaso) 
        {
             ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string Mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacenOrigen", SqlDbType = SqlDbType.Int, Value = Traspaso.IdAlmacenOrigen });
            parametros.Add(new SqlParameter { ParameterName = "@Insumos", SqlDbType = SqlDbType.Int, Value = Traspaso.Insumos });
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacenDestino", SqlDbType = SqlDbType.Int, Value = Traspaso.IdAlmacenDestino });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.VarChar, Value = Traspaso.UsuarioRegistra });
            parametros.Add(new SqlParameter { ParameterName = "@TipoMovimiento", SqlDbType = SqlDbType.Int, Value = Traspaso.TipoMovimimiento });
            parametros.Add(new SqlParameter { ParameterName = "@CantidadTotal", SqlDbType = SqlDbType.Decimal, Value = Traspaso.CantidadTotal });
            parametros.Add(new SqlParameter { ParameterName = "@Comentarios", SqlDbType = SqlDbType.VarChar, Value = Traspaso.Comentarios });

             try 
            {
                DataSet ds = dac.Fill("sp_insert_Traspaso", parametros);
                Mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return Mensaje;
        
        }



        public string UpdateTraspaso(UpdateTraspasoModel Traspaso)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = Traspaso.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Comentarios", SqlDbType = System.Data.SqlDbType.VarChar, Value = Traspaso.Comentarios });
            parametros.Add(new SqlParameter { ParameterName = "@CantidadTotal", SqlDbType = System.Data.SqlDbType.Decimal, Value = Traspaso.CantidadTotal });
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacenOrigen", SqlDbType = System.Data.SqlDbType.Int, Value = Traspaso.IdAlmacenOrigen});
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacenDestino", SqlDbType = System.Data.SqlDbType.Int, Value = Traspaso.IdAlmacenDestino });
            parametros.Add(new SqlParameter { ParameterName = "@TipoMovimiento", SqlDbType = System.Data.SqlDbType.Int, Value = Traspaso.TipoMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@Insumos", SqlDbType = System.Data.SqlDbType.Int, Value = Traspaso.Insumos });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = System.Data.SqlDbType.VarChar, Value = Traspaso.UsuarioRegistra });
           

            try
            {
                DataSet ds = dac.Fill("sp_update_Traspaso", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteTraspaso(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("sp_delete_Traspaso", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
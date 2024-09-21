// using System;
// using System.Collections;
// using System.Data;
// using System.Data.SqlClient;
// using reportesApi.DataContext;
// using reportesApi.Models;
// using System.Collections.Generic;
// using reportesApi.Models.Compras;
// using OfficeOpenXml;
// using Microsoft.AspNetCore.Hosting;
// using System.IO;
// using Microsoft.AspNetCore.Mvc;
// using System.Drawing.Printing;
// using System.Linq;
// using System.Text;
// namespace reportesApi.Services
// {
//     public class GrupoService
//     {
//         private  string connection;
//         private readonly IWebHostEnvironment _webHostEnvironment;
//         private ArrayList parametros = new ArrayList();


//         public GrupoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
//         {
//              connection = settings.ConnectionString;

//              _webHostEnvironment = webHostEnvironment;
             
//         }

//         public List<GetGrupoModel> GetGrupos()
//         {
//             ConexionDataAccess dac = new ConexionDataAccess(connection);
//             GetGrupoModel persona = new GetGrupoModel();

//             List<GetGrupoModel> lista = new List<GetGrupoModel>();
//             try
//             {
//                 parametros = new ArrayList();
//                 DataSet ds = dac.Fill("sp_get_grupos", parametros);
//                 if (ds.Tables[0].Rows.Count > 0)
//                 {

//                   lista = ds.Tables[0].AsEnumerable()
//                     .Select(dataRow => new GetGrupoModel {
//                         Id = int.Parse(dataRow["Id"].ToString()),
//                         Nombre = dataRow["Nombre"].ToString(),
//                         Clave = dataRow["Clave"].ToString(),
//                         Estatus = dataRow["Estatus"].ToString(),
//                         UsuarioRegistra = dataRow["UsuarioRegistra"].ToString(),
//                         FechaRegistro= dataRow["FechaRegistro"].ToString()
//                     }).ToList();
//                 }
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//             return lista;
//         }

//         public string InsertGrupo(InsertGrupoModel Grupo)
//         {
//             ConexionDataAccess dac = new ConexionDataAccess(connection);
//             parametros = new ArrayList();
//             string mensaje;

//             parametros.Add(new SqlParameter { ParameterName = "@pNombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = Grupo.Nombre });
//             parametros.Add(new SqlParameter { ParameterName = "@pClave", SqlDbType = System.Data.SqlDbType.VarChar, Value = Grupo.Clave});
//             parametros.Add(new SqlParameter { ParameterName = "@pUsuarioRegistra", SqlDbType = System.Data.SqlDbType.Int, Value = 1 });

//             try
//             {
//                 DataSet ds = dac.Fill("sp_insert_grupo", parametros);
//                 mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//             return mensaje;
//         }

//         public string UpdateGrupo(UpdateGrupoModel Grupo)
//         {
//             ConexionDataAccess dac = new ConexionDataAccess(connection);
//             parametros = new ArrayList();
//             string mensaje;


//             parametros.Add(new SqlParameter { ParameterName = "@pId", SqlDbType = System.Data.SqlDbType.VarChar, Value = Grupo.Id });
//             parametros.Add(new SqlParameter { ParameterName = "@pNombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = Grupo.Nombre });
//             parametros.Add(new SqlParameter { ParameterName = "@pClave", SqlDbType = System.Data.SqlDbType.VarChar, Value = Grupo.Clave });
//             parametros.Add(new SqlParameter { ParameterName = "@pUsuarioRegistra", SqlDbType = System.Data.SqlDbType.Int, Value = 1});

//             try
//             {
//                 DataSet ds = dac.Fill("sp_update_grupos", parametros);
//                 mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }

//             return mensaje;
//         }

//         public void DeleteGrupo(int id)
//         {
//             ConexionDataAccess dac = new ConexionDataAccess(connection);
//             parametros = new ArrayList();
//             parametros.Add(new SqlParameter { ParameterName = "@pId", SqlDbType = SqlDbType.Int, Value = id });
//             parametros.Add(new SqlParameter { ParameterName = "@pUsuarioRegistra", SqlDbType = System.Data.SqlDbType.Int, Value = 1});


//             try
//             {
//                 dac.ExecuteNonQuery("sp_delete_Grupos", parametros);
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//         }
//     }
// }
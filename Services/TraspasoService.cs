using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using reportesApi.Models;

namespace MyNamespace.Services
{
    public class TraspasoService
    {
        private readonly string _connectionString;

        
        public TraspasoService(string connectionString)
        {
            _connectionString = connectionString;
        }

        
        public int InsertTraspaso(InsertTraspasoModel traspaso)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("sp_insert_Traspaso", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                
                command.Parameters.AddWithValue("@IdAlmacenOrigen", traspaso.IdAlmacenOrigen);
                command.Parameters.AddWithValue("@IdAlmacenDestino", traspaso.IdAlmacenDestino);
                command.Parameters.AddWithValue("@FechaRegistra", traspaso.FechaRegistra);
                command.Parameters.AddWithValue("@UsuarioRegistra", traspaso.UsuarioRegistra);
                command.Parameters.AddWithValue("@Estatus", traspaso.Estatus); 
                command.Parameters.AddWithValue("@EntradaSalida", traspaso.EntradaSalida); 
                command.Parameters.AddWithValue("@TipoMovimiento", traspaso.TipoMovimiento);
                command.Parameters.AddWithValue("@CantidadTotalMovimiento", traspaso.CantidadTotalMovimiento);
                command.Parameters.AddWithValue("@Comentarios", traspaso.Comentarios ?? (object)DBNull.Value); 

                connection.Open();

                
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        
        public List<GetTraspasoModel> GetTraspaso()
        {
            List<GetTraspasoModel> traspasos = new List<GetTraspasoModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("sp_get_Traspasos", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        traspasos.Add(new GetTraspasoModel
                        {
                            Id = Convert.ToInt32(reader["IdTraspaso"]),
                            IdAlmacenOrigen = Convert.ToInt32(reader["IdAlmacenOrigen"]),
                            IdAlmacenDestino = Convert.ToInt32(reader["IdAlmacenDestino"]),
                            FechaRegistra = Convert.ToDateTime(reader["FechaRegistra"]),
                            UsuarioRegistra = reader["UsuarioRegistra"].ToString(),
                            Estatus = reader["Estatus"].ToString(),
                            EntradaSalida = reader["EntradaSalida"].ToString(),
                            TipoMovimiento = reader["TipoMovimiento"].ToString(),
                            CantidadTotalMovimiento = Convert.ToDecimal(reader["CantidadTotalMovimiento"]),
                            Comentarios = reader["Comentarios"].ToString(),
                            FechaActualizacion = reader["FechaActualizacion"] as DateTime? 
                        });
                    }
                }
            }
            return traspasos;
        }

        
        public void UpdateTraspaso(int id, string estatus, string comentarios, decimal cantidadTotal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("sp_update_Traspaso", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                
                command.Parameters.AddWithValue("@IdTraspaso", id);
                command.Parameters.AddWithValue("@Estatus", estatus); 
                command.Parameters.AddWithValue("@Comentarios", comentarios ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CantidadTotalMovimiento", cantidadTotal);

                connection.Open();

                
                command.ExecuteNonQuery();
            }
        }

        
        public void DeleteTraspaso(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("sp_delete_Traspaso", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                
                command.ExecuteNonQuery();
            }
        }
    }
}

using System;
namespace reportesApi.Models
{
    public class GetInv_MovimientosModel{
        public int Id { get; set; }
        public int IdTipoMovimiento { get; set; }
        public int IdAlmacen { get; set; }
        public string Fecha { get; set; }
        public int Estatus { get; set; }
        public int IdUsuario { get; set; }

    }

    public class InsertInv_MovimientosModel 
    {
        public int IdTipoMovimiento { get; set; }
        public int IdAlmacen { get; set; }
        public int IdUsuario { get; set; }
    }

    public class UpdateInv_MovimientosModel
    {
        public int Id { get; set;}
        public int IdTipoMovimiento { get; set; }
        public int IdAlmacen { get; set; }
        public int IdUsuario { get; set; }
    }

}
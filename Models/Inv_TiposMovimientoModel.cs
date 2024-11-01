using System;
namespace reportesApi.Models
{
    public class GetInv_TiposMovimientoModel{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int EntradaSalida{ get; set; }
        public int Estatus { get; set; }
        public string FechaRegistro { get; set; }
        public int Usuario_registra { get; set; }

    }

    public class InsertInv_TiposMovimientoModel 
    {
        public string Nombre{ get; set; }
        public int EntradaSalida { get; set; }
        public int Usuario_registra { get; set; }
    }

    public class UpdateInv_TiposMovimientoModel
    {
        public int Id { get; set;}
        public string Nombre { get; set; }
        public decimal EntradaSalida { get; set; }
        public int Usuario_registra { get; set; }
    }

}
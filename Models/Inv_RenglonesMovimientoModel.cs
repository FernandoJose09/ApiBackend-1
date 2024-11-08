using System;
namespace reportesApi.Models
{
    public class GetInv_RenglonesMovimientoModel{
        public int Id { get; set; }
        public int IdMovimiento { get; set; }
        public string Insumo { get; set; }
        public string DescripcionInsumo {get; set;}
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public int Estatus { get; set; }
        public string FechaRegistro { get; set; }
        public string Usuario_registra { get; set; }
        public string Fechafinal {get; set ;}
        public string Fechainicial {get; set;}

    }

    public class InsertInv_RenglonesMovimientoModel 
    {
        public int IdMovimiento { get; set; }
        public string Insumo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public int Usuario_registra { get; set; }
    }

    public class UpdateInv_RenglonesMovimientoModel
    {
        public int Id { get; set; }
        public int IdMovimiento { get; set;}
        public string Insumo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public int Usuario_registra { get; set; }
    }

}
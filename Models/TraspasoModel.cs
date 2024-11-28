using System;
namespace reportesApi.Models
{
    public class GetTraspasoModel{
        public int Id { get; set; }
        public int IdAlmacenOrigen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public string UsuarioRegistra { get; set; }
        public int Estatus { get; set; }
        public int TipoMovimiento { get; set; }
        public decimal CantidadTotal { get; set; }
        public string Comentarios { get; set; }
        public string FechaRegistra { get; set; }
        public int Insumos { get; set; }
        public string DescripcionInsumo {get; set;}
       

    }

    public class InsertTraspasoModel 
    {
        public int IdAlmacenOrigen { get; set; }
        public int Insumos { get; set; }
        public int IdAlmacenDestino { get; set; }
        public string UsuarioRegistra { get; set; }
        public int TipoMovimimiento { get; set; }
        public decimal CantidadTotal { get; set; }
        public string Comentarios { get; set; }
        
    }

    public class UpdateTraspasoModel
    {
        public int Id { get; set;}
        public string Comentarios { get; set; }
        public decimal CantidadTotal { get; set; }
        public int IdAlmacenOrigen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public string TipoMovimiento { get; set; }
        public int Insumos { get; set; }
        public string UsuarioRegistra { get; set; }

    }

}
using System;
namespace reportesApi.Models
{
    public class GetTraspasoModel
    {
        public int Id { get; set; }
        public int IdAlmacenOrigen { get; set; }
        public string NombreAlmacenOrigen { get; set; } 
        public int IdAlmacenDestino { get; set; }
        public string NombreAlmacenDestino { get; set; }
        public string UsuarioRegistra { get; set; }
        public string Estatus { get; set; }
        public string EntradaSalida { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal CantidadTotalMovimiento { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaRegistra { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    public class InsertTraspasoModel 
    {
        public int IdAlmacenOrigen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public string UsuarioRegistra { get; set; }
        public DateTime FechaRegistra {get; set;}
        public string Estatus { get; set; } 
        public string EntradaSalida { get; set; } 
        public string TipoMovimiento { get; set; } 
        public decimal CantidadTotalMovimiento { get; set; }
        public string Comentarios { get; set; }

    }

    public class UpdateTraspasoModel
    {
        public int Id { get; set; } 
        public int IdAlmacenOrigen { get; set; } 
        public int IdAlmacenDestino { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string UsuarioRegistra { get; set; }
        public string Estatus { get; set; }
        public string EntradaSalida { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal CantidadTotalMovimiento { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }

}
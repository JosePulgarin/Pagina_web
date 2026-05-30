using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class PromocionesEspecialesClase
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descuento { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        [JsonIgnore]
        [NotMapped] public List<PromocionesServiciosClase>? PromocionesServiciosClase { get; set; }
    }
}
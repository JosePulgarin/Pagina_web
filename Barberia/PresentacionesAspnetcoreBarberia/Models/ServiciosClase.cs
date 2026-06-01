using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class ServiciosClase
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Costo { get; set; }
        public int Tiempo { get; set; }
        public string? Nota { get; set; }

        [JsonIgnore]
        [NotMapped] public List<PromocionesServiciosClase>? PromocionesServiciosClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ReservasServiciosClase>? ReservasServiciosClase { get; set; }
    }
}

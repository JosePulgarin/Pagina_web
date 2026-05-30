using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace LibreriaBarberia.Entidades
{
    public class Servicios
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Costo { get; set; }
        public int Tiempo { get; set; }
        public string? Nota { get; set; }
        [JsonIgnore]
        [NotMapped] public List<PromocionesServicios>? PromocionesServicios { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ReservasServicios>? ReservasServicios { get; set; }
    }
}

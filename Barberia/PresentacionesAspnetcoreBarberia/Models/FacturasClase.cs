using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class FacturasClase
    {
        [Key] public int Id { get; set; }
        public string? NumeroFactura { get; set; }
        public decimal MontoSubTotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public int IdReserva { get; set; }
        public int IdMetodo { get; set; }
        [JsonIgnore]
        [ForeignKey("IdReserva")] public ReservasClase? ReservasClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdMetodo")] public MetodosPagoClase? MetodosPagoClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ComisionesClase>? ComisionesClase { get; set; }
    }
}
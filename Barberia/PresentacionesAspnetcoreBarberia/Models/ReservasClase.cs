using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class ReservasClase
    {
        [Key] public int Id { get; set; }
        public string? Recordatorio { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Estado { get; set; }
        public string? Notas { get; set; }
        public int IdAgenda { get; set; }
        public int IdCliente { get; set; }
        [JsonIgnore]
        [ForeignKey("IdAgenda")] public AgendasClase? AgendasClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdCliente")] public ClientesClase? ClientesClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ReseñasClientesClase>? ReseñasClientesClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ReservasServiciosClase>? ReservasServiciosClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<FacturasClase>? FacturasClase { get; set; }
    }
}
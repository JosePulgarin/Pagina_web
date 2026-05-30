using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class AgendasClase
    {
        [Key] public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string? Estado { get; set; } 
        public int IdBarbero { get; set; }

        [JsonIgnore] 
        [ForeignKey("IdBarbero")] public BarberosClase? BarberosClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ReservasClase>? ReservasClase { get; set; }
    }
}
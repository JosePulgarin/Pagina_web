using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace LibreriaBarberia.Entidades
{
    public class Agendas
    {
        [Key] public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string? Estado { get; set; } // Pendiente, Confirmada, Cancelada
        public int IdBarbero { get; set; }
        [JsonIgnore] // Para quitar el chorro de babas en Swagger
        [ForeignKey("IdBarbero")] public Barberos? Barberos { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Reservas>? Reservas { get; set; }
    }
}
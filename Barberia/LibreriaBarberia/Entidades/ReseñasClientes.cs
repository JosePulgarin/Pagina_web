using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibreriaBarberia.Entidades
{
    public class ReseñasClientes
    {
        [Key] public int Id { get; set; }
        public int Puntuacion { get; set; } // 1 a 5 (OJO, preguntar al profe la restricción)
        public string? Comentario { get; set; }
        public DateOnly FechaPublicacion { get; set; }
        public string? Etiquetas { get; set; }
        public int IdReserva { get; set; }
        [JsonIgnore]
        [ForeignKey("IdReserva")] public Reservas? Reservas { get; set; }
    }
}
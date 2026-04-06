using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class Agendas
    {
        [Key] public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string? Estado { get; set; } // Pendiente, Confirmada, Cancelada (OJO, preguntar al profe)
        public string? Telefono { get; set; }
        public int IdBarbero { get; set; }
        [ForeignKey("IdBarbero")] public Barberos? Barberos { get; set; }
        [NotMapped] public List<Reservas>? Reservas { get; set; }
    }
}
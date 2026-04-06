using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class ReservasServicios
    {
        [Key] public int Id { get; set; }
        public decimal Precio { get; set; }
        public string? Observacion { get; set; }
        public int IdServicio { get; set; }
        public int IdReserva { get; set; }
        [ForeignKey("IdServicio")] public Servicios? Servicios { get; set; }
        [ForeignKey("IdReserva")] public Reservas? Reservas { get; set; }
    }
}
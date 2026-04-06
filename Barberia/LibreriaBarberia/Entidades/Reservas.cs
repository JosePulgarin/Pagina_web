using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class Reservas
    {
        [Key] public int Id { get; set; }
        public string? Recordatorio { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Estado { get; set; }
        public string? Notas { get; set; }
        public int IdAgenda { get; set; }
        public int IdCliente { get; set; }
        [ForeignKey("IdAgenda")] public Agendas? Agendas { get; set; }
        [ForeignKey("IdCliente")] public Clientes? Clientes { get; set; }
        [NotMapped] public List<ReseñasClientes>? ReseñasClientes { get; set; }
        [NotMapped] public List<ReservasServicios>? ReservasServicios { get; set; }
        [NotMapped] public List<Facturas>? Facturas { get; set; }
    }
}
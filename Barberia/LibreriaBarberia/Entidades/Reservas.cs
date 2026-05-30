using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        [ForeignKey("IdAgenda")] public Agendas? Agendas { get; set; }
        [JsonIgnore]
        [ForeignKey("IdCliente")] public Clientes? Clientes { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ReseñasClientes>? ReseñasClientes { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ReservasServicios>? ReservasServicios { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Facturas>? Facturas { get; set; }
    }
}
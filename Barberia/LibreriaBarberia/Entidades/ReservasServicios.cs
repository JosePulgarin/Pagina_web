using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace LibreriaBarberia.Entidades
{
    public class ReservasServicios
    {
        [Key] public int Id { get; set; }
        public decimal Precio { get; set; }
        public string? Observacion { get; set; }
        public int IdServicio { get; set; }
        public int IdReserva { get; set; }
        [JsonIgnore]
        [ForeignKey("IdServicio")] public Servicios? Servicios { get; set; }
        [JsonIgnore]
        [ForeignKey("IdReserva")] public Reservas? Reservas { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class ReservasServiciosClase
    {
        [Key] public int Id { get; set; }
        public decimal Precio { get; set; }
        public string? Observacion { get; set; }
        public int IdServicio { get; set; }
        public int IdReserva { get; set; }
        [JsonIgnore]
        [ForeignKey("IdServicio")] public ServiciosClase? ServiciosClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdReserva")] public ReservasClase? ReservasClase { get; set; }
    }
}
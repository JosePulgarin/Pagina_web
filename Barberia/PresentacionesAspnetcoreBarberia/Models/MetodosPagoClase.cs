using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class MetodosPagoClase
    {
        [Key] public int Id { get; set; }
        public string? TipoMetodo { get; set; }
        public string? Banco { get; set; }
        public string? Moneda { get; set; }
        [JsonIgnore]
        [NotMapped] public List<FacturasClase>? FacturasClase { get; set; }
    }
}

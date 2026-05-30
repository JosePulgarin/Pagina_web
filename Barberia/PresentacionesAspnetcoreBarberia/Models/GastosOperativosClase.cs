using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class GastosOperativosClase
    {
        [Key] public int Id { get; set; }
        public string? Categoria { get; set; }
        public decimal Monto { get; set; }
        public DateOnly FechaPago { get; set; }
        public string? NumeroComprobante { get; set; }
        public int IdSede { get; set; }
        [JsonIgnore]
        [ForeignKey("IdSede")] public SedesClase? SedesClase { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class InventariosClase
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int CantidadActual { get; set; }
        public DateOnly FechaAbastecimiento { get; set; }
        public int IdSede { get; set; }

        [JsonIgnore]
        [ForeignKey("IdSede")] public SedesClase? SedesClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ProductosClase>? ProductosClase { get; set; }
    }
}
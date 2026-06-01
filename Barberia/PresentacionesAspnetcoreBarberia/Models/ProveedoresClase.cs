using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class ProveedoresClase
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ProductosClase>? ProductosClase { get; set; }
    }
}

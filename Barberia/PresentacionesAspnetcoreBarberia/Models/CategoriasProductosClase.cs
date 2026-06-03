using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class CategoriasProductosClase
    {
		[Key] public int Id { get; set; }
		public string? Nombre { get; set; }
		public string? Descripcion { get; set; }
		public bool AplicaImpuesto { get; set; }
		public bool Estado { get; set; }

        [JsonIgnore]
        [NotMapped] public List<ProductosClase>? ProductosClase { get; set; }
    }
}
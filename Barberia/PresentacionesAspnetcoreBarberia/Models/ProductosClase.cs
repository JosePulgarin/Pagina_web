using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class ProductosClase
    {
        [Key] public int Id { get; set; }
        public string? MarcaProducto { get; set; }
        public string? NombreArticulo { get; set; }
        public decimal Precio { get; set; }
        public int StockActual { get; set; }
        public int IdInventario { get; set; }
        public int IdProveedor { get; set; }
        public int IdCategoria { get; set; }
        [JsonIgnore]
        [ForeignKey("IdInventario")] public InventariosClase? InventariosClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdProveedor")] public ProveedoresClase? ProveedoresClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdCategoria")] public CategoriasProductosClase? CategoriasProductosClase { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace LibreriaBarberia.Entidades
{
    public class PromocionesServicios
    {
        [Key] public int Id { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal DescuentoFinde { get; set; }
        public int IdServicio { get; set; }
        public int IdPromocionEspecial { get; set; }
        [JsonIgnore]
        [ForeignKey("IdServicio")] public Servicios? Servicios { get; set; }
        [JsonIgnore]
        [ForeignKey("IdPromocionEspecial")] public PromocionesEspeciales? PromocionesEspeciales { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class PromocionesServiciosClase
    {
        [Key] public int Id { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal DescuentoFinde { get; set; }
        public int IdServicio { get; set; }
        public int IdPromocionEspecial { get; set; }
        [JsonIgnore]
        [ForeignKey("IdServicio")] public ServiciosClase? ServiciosClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdPromocionEspecial")] public PromocionesEspecialesClase? PromocionesEspecialesClase { get; set; }
    }
}
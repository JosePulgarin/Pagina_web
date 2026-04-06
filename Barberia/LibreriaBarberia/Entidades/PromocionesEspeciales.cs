using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class PromocionesEspeciales
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descuento { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        [NotMapped] public List<PromocionesServicios>? PromocionesServicios { get; set; }
    }
}
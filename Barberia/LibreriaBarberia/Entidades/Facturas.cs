using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class Facturas
    {
        [Key] public int Id { get; set; }
        public string? NumeroFactura { get; set; }
        public decimal MontoSubTotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public int IdReserva { get; set; }
        public int IdMetodo { get; set; }
        [ForeignKey("IdReserva")] public Reservas? Reservas { get; set; }
        [ForeignKey("IdMetodo")] public MetodosPago? MetodosPago { get; set; }
        [NotMapped] public List<Comisiones>? Comisiones { get; set; }
    }
}
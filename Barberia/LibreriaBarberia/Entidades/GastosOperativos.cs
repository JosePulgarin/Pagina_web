using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class GastosOperativos
    {
        [Key] public int Id { get; set; }
        public string? Categoria { get; set; }
        public decimal Monto { get; set; }
        public DateOnly FechaPago { get; set; }
        public string? NumeroComprobante { get; set; }
        public int IdSede { get; set; }
        [ForeignKey("IdSede")] public Sedes? Sedes { get; set; }
    }
}
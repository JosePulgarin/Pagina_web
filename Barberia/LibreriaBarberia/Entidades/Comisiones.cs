using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace LibreriaBarberia.Entidades
{
    public class Comisiones
    {
        [Key] public int Id { get; set; }
        public decimal PorcentajeAplicado { get; set; }
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public string? EstadoLiquidacion { get; set; } // Pendiente, Liquidada, Anulada (OJO, preguntar al profe)
        public int IdFactura { get; set; }
        public int IdBarbero { get; set; }
        [JsonIgnore]
        [ForeignKey("IdFactura")] public Facturas? Facturas { get; set; }
        [JsonIgnore]
        [ForeignKey("IdBarbero")] public Barberos? Barberos { get; set; }
    }
}
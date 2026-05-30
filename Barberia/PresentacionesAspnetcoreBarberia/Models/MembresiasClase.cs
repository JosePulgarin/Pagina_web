using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace PresentacionesAspnetcoreBarberia.Models
{
    public class MembresiasClase
    {
        [Key] public int Id { get; set; }
        public string? NombrePlan { get; set; }
        public decimal CostoMensual { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public int DiaVigencia { get; set; }

        [JsonIgnore]
        public List<ClientesClase>? ClientesClase { get; set; }

    }
}

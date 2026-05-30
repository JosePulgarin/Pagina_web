using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace LibreriaBarberia.Entidades
{
    public class HorariosLaborales
    {
        [Key] public int Id { get; set; }
        public string? Dia { get; set; }
        public TimeOnly HoraApertura { get; set; }
        public TimeOnly HoraCierre { get; set; }
        public bool DiaFestivo { get; set; }
        public int IdSede { get; set; }
        [JsonIgnore]
        [ForeignKey("IdSede")] public Sedes? Sedes { get; set; }
    }
}
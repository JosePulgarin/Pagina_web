using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("IdSede")] public Sedes? Sedes { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class HistoricosClase
    {
        [Key] public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Entidad { get; set; } //En que entidad ocurrió
        public string? Accion { get; set; }
        public DateTime? Fecha { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class Historicos
    {
        [Key] public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Entidad { get; set; } //En que entidad ocurrió
        public string? Accion { get; set; }
        public DateTime? Fecha { get; set; }
    }
}

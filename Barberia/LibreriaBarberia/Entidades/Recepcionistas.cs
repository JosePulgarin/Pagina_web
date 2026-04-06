using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class Recepcionistas
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string? Turno { get; set; }
        public string? Telefono { get; set; }
        public int IdUsuario { get; set; }
        public int IdSede { get; set; }
        [ForeignKey("IdUsuario")] public PerfilUsuarios? PerfilUsuarios { get; set; }
        [ForeignKey("IdSede")] public Sedes? Sedes { get; set; }
    }
}
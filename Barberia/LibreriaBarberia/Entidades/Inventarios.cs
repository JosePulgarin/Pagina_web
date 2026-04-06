using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class Inventarios
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int CantidadActual { get; set; }
        public DateOnly FechaAbastecimiento { get; set; }
        public int IdSede { get; set; }
        [ForeignKey("IdSede")] public Sedes? Sedes { get; set; }
        [NotMapped] public List<Productos>? Productos { get; set; }
    }
}
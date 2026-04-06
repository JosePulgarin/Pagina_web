using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class Proveedores
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        [NotMapped] public List<Productos>? Productos { get; set; }
    }
}

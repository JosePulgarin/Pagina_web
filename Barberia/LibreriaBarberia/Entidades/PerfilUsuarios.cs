using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaBarberia.Entidades
{
    public class PerfilUsuarios
    {
        [Key] public int Id { get; set; }
        public string? Correo { get; set; }
        public string? Contraseña { get; set; }
        public string? Rol { get; set; }
        public string? Estado { get; set; }

        [NotMapped] public List<Barberos>? Barberos { get; set; }
        [NotMapped] public List<Recepcionistas>? Recepcionistas { get; set; }
        [NotMapped] public List<Clientes>? Clientes { get; set; }
    }
}

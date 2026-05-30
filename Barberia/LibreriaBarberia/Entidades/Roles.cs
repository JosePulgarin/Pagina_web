using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibreriaBarberia.Entidades
{
    public class Roles
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Barberos>? Barberos { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Recepcionistas>? Recepcionistas { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Clientes>? Clientes { get; set; }
        [JsonIgnore]
        [NotMapped] public List<PerfilUsuarios>? PerfilUsuarios { get; set; }
    }
}


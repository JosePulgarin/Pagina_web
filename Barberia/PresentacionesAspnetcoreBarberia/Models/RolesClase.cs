using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class RolesClase
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        [JsonIgnore]
        [NotMapped] public List<BarberosClase>? BarberosClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<RecepcionistasClase>? RecepcionistasClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ClientesClase>? ClientesClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<PerfilUsuariosClase>? PerfilUsuariosClase { get; set; }
    }
}


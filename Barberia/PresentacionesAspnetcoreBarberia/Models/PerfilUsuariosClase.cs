using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace PresentacionesAspnetcoreBarberia.Models
{
    public class PerfilUsuariosClase
    {
        [Key] public int Id { get; set; }
        public string? Correo { get; set; }
        public string? Contraseña { get; set; }
        public string? Estado { get; set; }
        public int IdRol {  get; set; }

        [JsonIgnore]
        [ForeignKey("IdRol")] public RolesClase? RolesClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<BarberosClase>? BarberosClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<RecepcionistasClase>? RecepcionistasClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ClientesClase>? ClientesClase { get; set; }
    }
}

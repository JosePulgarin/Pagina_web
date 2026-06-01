using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace PresentacionesAspnetcoreBarberia.Models
{
    public class BarberosClase
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string? Especialidad { get; set; }
        public string? Biografia { get; set; }
        public int IdUsuario { get; set; }
        public int IdSede { get; set; }

        [JsonIgnore]
        [ForeignKey("IdUsuario")] public PerfilUsuariosClase? PerfilUsuariosClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdSede")] public SedesClase? SedesClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<AgendasClase>? AgendasClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ComisionesClase>? ComisionesClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<PortafoliosClase>? PortafoliosClase { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PresentacionesAspnetcoreBarberia.Models
{
    public class RecepcionistasClase
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string? Turno { get; set; }
        public string? Telefono { get; set; }
        public int IdUsuario { get; set; }
        public int IdSede { get; set; }
        [JsonIgnore]
        [ForeignKey("IdUsuario")] public PerfilUsuariosClase? PerfilUsuariosClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdSede")] public SedesClase? SedesClase { get; set; }
    }
}
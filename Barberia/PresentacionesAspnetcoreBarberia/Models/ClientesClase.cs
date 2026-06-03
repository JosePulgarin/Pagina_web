using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class ClientesClase
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
     
        public int IdUsuario { get; set; }
        public int IdSede { get; set; }
        public int IdMembresia { get; set; }
        [JsonIgnore]
        [ForeignKey("IdUsuario")] public PerfilUsuariosClase? PerfilUsuariosClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdSede")] public SedesClase? SedesClase { get; set; }
        [JsonIgnore]
        [ForeignKey("IdMembresia")] public MembresiasClase? MembresiasClase { get; set; }
        [JsonIgnore]
        [NotMapped] public List<ReservasClase>? ReservasClase { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace LibreriaBarberia.Entidades
{
    public class Barberos
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
        [ForeignKey("IdUsuario")] public PerfilUsuarios? PerfilUsuarios { get; set; }
        [JsonIgnore]
        [ForeignKey("IdSede")] public Sedes? Sedes { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Agendas>? Agendas { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Comisiones>? Comisiones { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Portafolios>? Portafolios { get; set; }
    }
}
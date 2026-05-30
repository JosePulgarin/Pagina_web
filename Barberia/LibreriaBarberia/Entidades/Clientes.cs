using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace LibreriaBarberia.Entidades
{
    public class Clientes
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
     
        public int IdUsuario { get; set; }
        public int IdSede { get; set; }
        public int IdMembresia { get; set; }
        [JsonIgnore]
        [ForeignKey("IdUsuario")] public PerfilUsuarios? PerfilUsuarios { get; set; }
        [JsonIgnore]
        [ForeignKey("IdSede")] public Sedes? Sedes { get; set; }
        [JsonIgnore]
        [ForeignKey("IdMembresia")] public Membresias? Membresias { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Reservas>? Reservas { get; set; }

    }
}

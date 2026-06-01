using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace LibreriaBarberia.Entidades
{
    public class PerfilUsuarios
    {
        [Key] public int Id { get; set; }
        public string? Correo { get; set; }
        public string? Contraseña { get; set; }
        public string? Estado { get; set; }
        public int IdRol {  get; set; }

        [JsonIgnore]
        [ForeignKey("IdRol")] public Roles? Roles { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Barberos>? Barberos { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Recepcionistas>? Recepcionistas { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Clientes>? Clientes { get; set; }
    }
}

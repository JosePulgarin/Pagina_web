using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
// Este namespace se utiliza para atributos relacionados con la serialización JSON, como [JsonIgnore] o [JsonProperty]. En este código, no se están utilizando atributos de este namespace, pero se incluye en caso de que quieras agregar atributos de serialización JSON en el futuro.
using System.ComponentModel.DataAnnotations.Schema;// This namespace is used for attributes that specify how classes and properties are mapped to database tables and columns. In this code, there are no attributes from this namespace being used, but it is included in case you want to add attributes like [Table] or [Column] in the future.
namespace LibreriaBarberia.Entidades
{
    public class Sedes
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Correo { get; set; }

        [JsonIgnore]
        [NotMapped] public List<Barberos>? Barberos { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Recepcionistas>? Recepcionistas { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Clientes>? Clientes { get; set; }
        [JsonIgnore]
        [NotMapped] public List<HorariosLaborales>? HorariosLaborales { get; set; }
        [JsonIgnore]
        [NotMapped] public List<GastosOperativos>? GastosOperativos { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Inventarios>? Inventarios { get; set; }
    }
}

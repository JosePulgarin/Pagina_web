using System.ComponentModel.DataAnnotations; // This namespace is used for data annotations, which are attributes that can be applied to classes and properties to specify validation rules, display formats, and other metadata. In this code, the [Key] attribute is used to indicate that the Id property is the primary key of the Sedes class.
using System.ComponentModel.DataAnnotations.Schema; // This namespace is used for attributes that specify how classes and properties are mapped to database tables and columns. In this code, there are no attributes from this namespace being used, but it is included in case you want to add attributes like [Table] or [Column] in the future.
namespace LibreriaBarberia.Entidades
{
    public class Sedes
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Correo { get; set; }
        [NotMapped] public List<Barberos>? Barberos { get; set; }
        [NotMapped] public List<Recepcionistas>? Recepcionistas { get; set; }
        [NotMapped] public List<Clientes>? Clientes { get; set; }
        [NotMapped] public List<HorariosLaborales>? HorariosLaborales { get; set; }
        [NotMapped] public List<GastosOperativos>? GastosOperativos { get; set; }
        [NotMapped] public List<Inventarios>? Inventarios { get; set; }
    }
}

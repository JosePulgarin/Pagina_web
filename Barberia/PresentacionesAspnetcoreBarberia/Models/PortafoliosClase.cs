using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace PresentacionesAspnetcoreBarberia.Models
{
    public class PortafoliosClase
    {
        [Key] public int Id { get; set; }
        public string? Ruta { get; set; }
        public string? TituloCorte { get; set; }
        public string? Descripcion { get; set; }
        public int IdBarbero { get; set; }
        [JsonIgnore]
        [ForeignKey("IdBarbero")] public BarberosClase? BarberosClase { get; set; }


    }
}

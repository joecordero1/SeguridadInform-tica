using SeguridadInformática.Models;
using System.ComponentModel.DataAnnotations;
namespace SeguridadInformática.Models;
    public class Activo
    {
        [Key]
        public int Id_Activo { get; set; }
        public string? Tipo { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<RiesgoPorActivo>? RiesgosPorActivo { get; set; }
}
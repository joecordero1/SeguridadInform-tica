using System.ComponentModel.DataAnnotations;
namespace SeguridadInformática.Models
{
    public class Riesgo
    {
        [Key]
        public int Id_Riesgo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Tipo { get; set; }

        public virtual ICollection<RiesgoPorActivo> RiesgosPorActivo { get; set; } // Colección para la relación
        public virtual ICollection<ControlPorRiesgo> ControlPorRiesgo { get; set; } // Colección para la relación
    }

}

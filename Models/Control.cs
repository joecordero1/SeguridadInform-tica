using System.ComponentModel.DataAnnotations;
namespace SeguridadInformática.Models
{
    public class Control
    {
        [Key]
        public int Id_Control { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Tipo { get; set; }
        public virtual ICollection<ControlPorRiesgo> ControlPorRiesgo { get; set; } // Colección para la relación
    }
}

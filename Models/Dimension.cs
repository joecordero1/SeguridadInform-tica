using System.ComponentModel.DataAnnotations;
namespace SeguridadInformática.Models
{
    public class Dimension
    {
        [Key]
        public int Id_Dimension { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}

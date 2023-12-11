using SeguridadInformática.Models;

namespace SeguridadInformática.Models
{
    public class DimensionPorRiesgo
    {
        public int Id_Riesgo { get; set; }
        public int Id_Dimension { get; set; }
        public List<Riesgo> Riesgos { get; set; } = new List<Riesgo>();
        public List<Dimension> Dimensiones { get; set; } = new List<Dimension>();
    }
}

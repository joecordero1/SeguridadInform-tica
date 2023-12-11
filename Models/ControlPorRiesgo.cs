using SeguridadInformática.Models;

namespace SeguridadInformática.Models
{
    public class ControlPorRiesgo
    {
        public int Id_Riesgo { get; set; }
        public int Id_Control { get; set; }
        public List<Riesgo> Riesgos { get; set; } = new List<Riesgo>();
        public List<Control> Controles { get; set; } = new List<Control>();
    }
}

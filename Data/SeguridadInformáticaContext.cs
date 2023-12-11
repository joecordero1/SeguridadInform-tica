using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeguridadInformática;
using SeguridadInformática.Models;

namespace SeguridadInformática.Data
{
    public class SeguridadInformáticaContext : DbContext
    {
        public SeguridadInformáticaContext (DbContextOptions<SeguridadInformáticaContext> options)
            : base(options)
        {
        }
        public DbSet<SeguridadInformática.Models.Activo> Activo { get; set; }
        public DbSet<SeguridadInformática.Models.Control> Control { get; set; }
        public DbSet<SeguridadInformática.Models.Dimension> Dimension { get; set; }
        public DbSet<SeguridadInformática.Models.Riesgo> Riesgo { get; set; }

        public DbSet<RiesgoPorActivo> RiesgoPorActivo { get; set; }
        public IEnumerable<object> DimensionPorRiesgo { get; internal set; }
        public IEnumerable<object> ControlPorRiesgo { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la clave primaria compuesta para RiesgoPorActivo
            modelBuilder.Entity<RiesgoPorActivo>()
                .HasKey(rpa => new { rpa.Id_Activo, rpa.Id_Riesgo });

            // Si necesitas configuraciones adicionales para las claves foráneas:
            modelBuilder.Entity<RiesgoPorActivo>()
                .HasOne(rpa => rpa.Activo)
                .WithMany(a => a.RiesgosPorActivo)
                .HasForeignKey(rpa => rpa.Id_Activo);

            modelBuilder.Entity<RiesgoPorActivo>()
                .HasOne(rpa => rpa.Riesgo)
                .WithMany(r => r.RiesgosPorActivo)
                .HasForeignKey(rpa => rpa.Id_Riesgo);

            // Configuraciones adicionales...
        }


    }
}

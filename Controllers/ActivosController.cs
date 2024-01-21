using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeguridadInformática.Data;
using SeguridadInformática.Models;

namespace SeguridadInformática.Controllers
{
    public class ActivosController : Controller
    {
        private readonly SeguridadInformáticaContext _context;

        public ActivosController(SeguridadInformáticaContext context)
        {
            _context = context;
        }

        // GET: Activos
        public async Task<IActionResult> Index()
        {
            var activos = await _context.Activo
                .Include(a => a.RiesgosPorActivo)
                    .ThenInclude(rpa => rpa.Riesgo)
                        .ThenInclude(r => r.ControlPorRiesgo)
                            .ThenInclude(cpr => cpr.Control)
                .ToListAsync();

            return View(activos);
        }



        // GET: Activos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Activo == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo
                .FirstOrDefaultAsync(m => m.Id_Activo == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // GET: Activos/Create
        public IActionResult Create()
        {
            // Crear una lista de selección para los tipos de activos.
            ViewBag.TipoSelectList = new SelectList(new List<string> { "D", "KEYS", "S", "SW", "HW", "COM", "MEDIA", "AUX", "L", "P", "D.LOG","D.CONF" });
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.TipoSelectList = new SelectList(new List<string> { "Building", "Local" });
            return View();
        
        }

        // POST: Activos/Create
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Nombre")] Activo activo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activo);
                await _context.SaveChangesAsync();

                if (activo.Tipo == "HW")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 17, 33, 34, 35, 40, 41, 45, 52, 53, 54, 55, 56 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2,3,7,8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }else if (activo.Tipo == "MEDIA")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 14, 15, 16, 17, 25, 28, 29, 30, 33, 35, 41, 45, 49,50,51,52,55,56 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "AUX")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 33, 35, 41, 45, 52, 53, 55, 56 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "L")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 1, 2, 3, 4, 5, 6, 15, 25, 26, 28, 29, 30, 41, 45, 49, 50, 51, 56, 57 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "D")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 16, 17, 25, 26, 28, 29, 30, 39, 40, 45, 49, 50, 51 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "KEYS")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 16, 17, 25, 26, 28, 29, 30, 39, 40, 45, 49, 50, 51 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "S")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 16, 17, 22, 23, 25, 26, 28, 29, 30, 34, 39, 40, 41, 43, 44, 45, 47, 49, 50, 51, 53, 54 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "SW")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 9, 16, 17, 21, 22, 23, 25, 26, 28, 29, 30, 31, 32, 39, 40, 41, 42, 43, 44, 45, 49, 50, 51 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "D.LOG")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 18, 37, 38, 47 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "D.CONF")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 19 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }
                else if (activo.Tipo == "P")
                {
                    // IDs de los riesgos para activos de tipo "HW"
                    int[] riesgosIds = new[] { 20, 30, 36, 58, 59, 60 };
                    foreach (var riesgoId in riesgosIds)
                    {
                        var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                        if (riesgo != null)
                        {
                            _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                            // Asignar controles específicos para el riesgo con Id 1
                            if (riesgo.Tipo == "[N] Desastres Naturales")
                            {
                                int[] controlesIds = new[] { 83, 92, 102, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[I] De origen industrial")
                            {
                                int[] controlesIds = new[] { 53, 55, 63, 66, 89, 90, 95, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[E] Errores y fallos no intencionados")
                            {
                                int[] controlesIds = new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                            if (riesgo.Tipo == "[A] Ataques intencionados")
                            {
                                int[] controlesIds = new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 };
                                foreach (var controlId in controlesIds)
                                {
                                    var control = await _context.Control.FindAsync(controlId);
                                    if (control != null)
                                    {
                                        _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                }

                return RedirectToAction(nameof(Index));
            }

            return View(activo);
        }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Nombre")] Activo activo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activo);
                await _context.SaveChangesAsync();

                var controlesAsignados = new HashSet<(int Id_Riesgo, int Id_Control)>();
                var riesgosIds = activo.Tipo switch
                {
                    "HW" => new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 17, 33, 34, 35, 40, 41, 45, 52, 53, 54, 55, 56 },
                    "MEDIA" => new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 14, 15, 16, 17, 25, 28, 29, 30, 33, 35, 41, 45, 49, 50, 51, 52, 55, 56 },
                    "AUX" => new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 33, 35, 41, 45, 52, 53, 55, 56 },
                    "L" => new[] { 1, 2, 3, 4, 5, 6, 15, 25, 26, 28, 29, 30, 41, 45, 49, 50, 51, 56, 57 },
                    "D" => new[] { 16, 17, 25, 26, 28, 29, 30, 39, 40, 45, 49, 50, 51 },
                    "KEYS" => new[] { 16, 17, 25, 26, 28, 29, 30, 39, 40, 45, 49, 50, 51 },
                    "S" => new[] { 16, 17, 22, 23, 25, 26, 28, 29, 30, 34, 39, 40, 41, 43, 44, 45, 47, 49, 50, 51, 53, 54 },
                    "SW" => new[] { 9, 16, 17, 21, 22, 23, 25, 26, 28, 29, 30, 31, 32, 39, 40, 41, 42, 43, 44, 45, 49, 50, 51 },
                    "D.LOG" => new[] { 18, 37, 38, 47 },
                    "D.CONF" => new[] { 19 },
                    "P" => new[] { 20, 30, 36, 58, 59, 60 },
                    // ... agregar más tipos de activos aquí ...
                    _ => Array.Empty<int>()
                };

                foreach (var riesgoId in riesgosIds)
                {
                    var riesgo = await _context.Riesgo.FindAsync(riesgoId);
                    if (riesgo != null)
                    {
                        _context.RiesgoPorActivo.Add(new RiesgoPorActivo { Id_Activo = activo.Id_Activo, Id_Riesgo = riesgo.Id_Riesgo });

                        var controlesIds = riesgo.Tipo switch
                        {
                            "[N] Desastres Naturales" => new[] { 83, 92, 102, 103 },
                            "[I] De origen industrial" => new[] { 53, 55, 63, 66, 89, 90, 95, 103 },
                            "[E] Errores y fallos no intencionados" => new[] { 5, 16, 19, 20, 44, 48, 35, 95, 100, 103 },
                            "[A] Ataques intencionados" => new[] { 2, 3, 7, 8, 12, 14, 63, 66, 75, 76, 91, 95, 103, 106 },
                            _ => Array.Empty<int>()
                        };

                        foreach (var controlId in controlesIds)
                        {
                            if (controlesAsignados.Add((riesgo.Id_Riesgo, controlId)))
                            {
                                var control = await _context.Control.FindAsync(controlId);
                                if (control != null)
                                {
                                    _context.ControlPorRiesgo.Add(new ControlPorRiesgo { Id_Riesgo = riesgo.Id_Riesgo, Id_Control = control.Id_Control });
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(activo);
        }



        // GET: Activos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Activo == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }
            return View(activo);
        }

        // POST: Activos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Activo,Tipo,Nombre")] Activo activo)
        {
            if (id != activo.Id_Activo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivoExists(activo.Id_Activo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(activo);
        }

        // GET: Activos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Activo == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo
                .FirstOrDefaultAsync(m => m.Id_Activo == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // POST: Activos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Activo == null)
            {
                return Problem("Entity set 'SeguridadInformáticaContext.Activo'  is null.");
            }
            var activo = await _context.Activo.FindAsync(id);
            if (activo != null)
            {
                _context.Activo.Remove(activo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivoExists(int id)
        {
          return (_context.Activo?.Any(e => e.Id_Activo == id)).GetValueOrDefault();
        }

    }
}

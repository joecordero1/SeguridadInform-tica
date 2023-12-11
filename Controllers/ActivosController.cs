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
            return View();
        }

        // POST: Activos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Nombre")] Activo activo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activo);
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

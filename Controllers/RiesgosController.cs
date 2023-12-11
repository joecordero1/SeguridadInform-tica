using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeguridadInformática.Models;
using SeguridadInformática.Data;

namespace SeguridadInformática.Controllers
{
    public class RiesgosController : Controller
    {
        private readonly SeguridadInformáticaContext _context;

        public RiesgosController(SeguridadInformáticaContext context)
        {
            _context = context;
        }

        // GET: Riesgos
        public async Task<IActionResult> Index()
        {
              return _context.Riesgo != null ? 
                          View(await _context.Riesgo.ToListAsync()) :
                          Problem("Entity set 'SeguridadInformáticaContext.Riesgo'  is null.");
        }

        // GET: Riesgos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Riesgo == null)
            {
                return NotFound();
            }

            var riesgo = await _context.Riesgo
                .FirstOrDefaultAsync(m => m.Id_Riesgo == id);
            if (riesgo == null)
            {
                return NotFound();
            }

            return View(riesgo);
        }

        // GET: Riesgos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Riesgos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Riesgo,Nombre,Descripcion,Tipo")] Riesgo riesgo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riesgo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(riesgo);
        }

        // GET: Riesgos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Riesgo == null)
            {
                return NotFound();
            }

            var riesgo = await _context.Riesgo.FindAsync(id);
            if (riesgo == null)
            {
                return NotFound();
            }
            return View(riesgo);
        }

        // POST: Riesgos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Riesgo,Nombre,Descripcion,Tipo")] Riesgo riesgo)
        {
            if (id != riesgo.Id_Riesgo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riesgo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiesgoExists(riesgo.Id_Riesgo))
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
            return View(riesgo);
        }

        // GET: Riesgos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Riesgo == null)
            {
                return NotFound();
            }

            var riesgo = await _context.Riesgo
                .FirstOrDefaultAsync(m => m.Id_Riesgo == id);
            if (riesgo == null)
            {
                return NotFound();
            }

            return View(riesgo);
        }

        // POST: Riesgos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Riesgo == null)
            {
                return Problem("Entity set 'SeguridadInformáticaContext.Riesgo'  is null.");
            }
            var riesgo = await _context.Riesgo.FindAsync(id);
            if (riesgo != null)
            {
                _context.Riesgo.Remove(riesgo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiesgoExists(int id)
        {
          return (_context.Riesgo?.Any(e => e.Id_Riesgo == id)).GetValueOrDefault();
        }
    }
}

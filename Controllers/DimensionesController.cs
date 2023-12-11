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
    public class DimensionesController : Controller
    {
        private readonly SeguridadInformáticaContext _context;

        public DimensionesController(SeguridadInformáticaContext context)
        {
            _context = context;
        }

        // GET: Dimensiones
        public async Task<IActionResult> Index()
        {
              return _context.Dimension != null ? 
                          View(await _context.Dimension.ToListAsync()) :
                          Problem("Entity set 'SeguridadInformáticaContext.Dimension'  is null.");
        }

        // GET: Dimensiones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dimension == null)
            {
                return NotFound();
            }

            var dimension = await _context.Dimension
                .FirstOrDefaultAsync(m => m.Id_Dimension == id);
            if (dimension == null)
            {
                return NotFound();
            }

            return View(dimension);
        }

        // GET: Dimensiones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dimensiones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Dimension,Nombre,Descripcion")] Dimension dimension)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dimension);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dimension);
        }

        // GET: Dimensiones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dimension == null)
            {
                return NotFound();
            }

            var dimension = await _context.Dimension.FindAsync(id);
            if (dimension == null)
            {
                return NotFound();
            }
            return View(dimension);
        }

        // POST: Dimensiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Dimension,Nombre,Descripcion")] Dimension dimension)
        {
            if (id != dimension.Id_Dimension)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dimension);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DimensionExists(dimension.Id_Dimension))
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
            return View(dimension);
        }

        // GET: Dimensiones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dimension == null)
            {
                return NotFound();
            }

            var dimension = await _context.Dimension
                .FirstOrDefaultAsync(m => m.Id_Dimension == id);
            if (dimension == null)
            {
                return NotFound();
            }

            return View(dimension);
        }

        // POST: Dimensiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dimension == null)
            {
                return Problem("Entity set 'SeguridadInformáticaContext.Dimension'  is null.");
            }
            var dimension = await _context.Dimension.FindAsync(id);
            if (dimension != null)
            {
                _context.Dimension.Remove(dimension);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DimensionExists(int id)
        {
          return (_context.Dimension?.Any(e => e.Id_Dimension == id)).GetValueOrDefault();
        }
    }
}

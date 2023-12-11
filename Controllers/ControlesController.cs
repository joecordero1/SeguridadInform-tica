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
    public class ControlesController : Controller
    {
        private readonly SeguridadInformáticaContext _context;

        public ControlesController(SeguridadInformáticaContext context)
        {
            _context = context;
        }

        // GET: Controles
        public async Task<IActionResult> Index()
        {
              return _context.Control != null ? 
                          View(await _context.Control.ToListAsync()) :
                          Problem("Entity set 'SeguridadInformáticaContext.Control'  is null.");
        }

        // GET: Controles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Control == null)
            {
                return NotFound();
            }

            var control = await _context.Control
                .FirstOrDefaultAsync(m => m.Id_Control == id);
            if (control == null)
            {
                return NotFound();
            }

            return View(control);
        }

        // GET: Controles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Controles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Control,Nombre,Descripcion,Tipo")] Control control)
        {
            if (ModelState.IsValid)
            {
                _context.Add(control);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(control);
        }

        // GET: Controles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Control == null)
            {
                return NotFound();
            }

            var control = await _context.Control.FindAsync(id);
            if (control == null)
            {
                return NotFound();
            }
            return View(control);
        }

        // POST: Controles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Control,Nombre,Descripcion,Tipo")] Control control)
        {
            if (id != control.Id_Control)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(control);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlExists(control.Id_Control))
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
            return View(control);
        }

        // GET: Controles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Control == null)
            {
                return NotFound();
            }

            var control = await _context.Control
                .FirstOrDefaultAsync(m => m.Id_Control == id);
            if (control == null)
            {
                return NotFound();
            }

            return View(control);
        }

        // POST: Controles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Control == null)
            {
                return Problem("Entity set 'SeguridadInformáticaContext.Control'  is null.");
            }
            var control = await _context.Control.FindAsync(id);
            if (control != null)
            {
                _context.Control.Remove(control);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControlExists(int id)
        {
          return (_context.Control?.Any(e => e.Id_Control == id)).GetValueOrDefault();
        }
    }
}

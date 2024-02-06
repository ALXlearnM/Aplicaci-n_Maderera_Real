using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Models;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pret16MermaController : Controller
    {
        private readonly EagleContext _context;

        public Pret16MermaController(EagleContext context)
        {
            _context = context;
        }

        // GET: Pret16Merma
        public async Task<IActionResult> ListadoMer()
        {
            var eagleContext = _context.Pret16Mermas.Include(p => p.IdExtraccionNavigation).Include(p => p.IdProduccionNavigation);
            return View(await eagleContext.ToListAsync());
        }

        // GET: Pret16Merma/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pret16Mermas == null)
            {
                return NotFound();
            }

            var pret16Merma = await _context.Pret16Mermas
                .Include(p => p.IdExtraccionNavigation)
                .Include(p => p.IdProduccionNavigation)
                .FirstOrDefaultAsync(m => m.IdMerma == id);
            if (pret16Merma == null)
            {
                return NotFound();
            }

            return View(pret16Merma);
        }

        // GET: Pret16Merma/Create
        public IActionResult Merma()
        {
            ViewData["IdExtraccion"] = new SelectList(_context.Pret07Extraccions, "IdExtraccion", "IdExtraccion");
            ViewData["IdProduccion"] = new SelectList(_context.Pret14Produccions, "IdProduccion", "IdProduccion");
            return View();
        }

        // POST: Pret16Merma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMerma,IdProduccion,IdExtraccion,FechaMerma,IdEstado,TxtEstado")] Pret16Merma pret16Merma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pret16Merma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdExtraccion"] = new SelectList(_context.Pret07Extraccions, "IdExtraccion", "IdExtraccion", pret16Merma.IdExtraccion);
            ViewData["IdProduccion"] = new SelectList(_context.Pret14Produccions, "IdProduccion", "IdProduccion", pret16Merma.IdProduccion);
            return View(pret16Merma);
        }

        // GET: Pret16Merma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pret16Mermas == null)
            {
                return NotFound();
            }

            var pret16Merma = await _context.Pret16Mermas.FindAsync(id);
            if (pret16Merma == null)
            {
                return NotFound();
            }
            ViewData["IdExtraccion"] = new SelectList(_context.Pret07Extraccions, "IdExtraccion", "IdExtraccion", pret16Merma.IdExtraccion);
            ViewData["IdProduccion"] = new SelectList(_context.Pret14Produccions, "IdProduccion", "IdProduccion", pret16Merma.IdProduccion);
            return View(pret16Merma);
        }

        // POST: Pret16Merma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMerma,IdProduccion,IdExtraccion,FechaMerma,IdEstado,TxtEstado")] Pret16Merma pret16Merma)
        {
            if (id != pret16Merma.IdMerma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pret16Merma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pret16MermaExists(pret16Merma.IdMerma))
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
            ViewData["IdExtraccion"] = new SelectList(_context.Pret07Extraccions, "IdExtraccion", "IdExtraccion", pret16Merma.IdExtraccion);
            ViewData["IdProduccion"] = new SelectList(_context.Pret14Produccions, "IdProduccion", "IdProduccion", pret16Merma.IdProduccion);
            return View(pret16Merma);
        }

        // GET: Pret16Merma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pret16Mermas == null)
            {
                return NotFound();
            }

            var pret16Merma = await _context.Pret16Mermas
                .Include(p => p.IdExtraccionNavigation)
                .Include(p => p.IdProduccionNavigation)
                .FirstOrDefaultAsync(m => m.IdMerma == id);
            if (pret16Merma == null)
            {
                return NotFound();
            }

            return View(pret16Merma);
        }

        // POST: Pret16Merma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pret16Mermas == null)
            {
                return Problem("Entity set 'EagleContext.Pret16Mermas'  is null.");
            }
            var pret16Merma = await _context.Pret16Mermas.FindAsync(id);
            if (pret16Merma != null)
            {
                _context.Pret16Mermas.Remove(pret16Merma);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pret16MermaExists(int id)
        {
          return (_context.Pret16Mermas?.Any(e => e.IdMerma == id)).GetValueOrDefault();
        }
    }
}

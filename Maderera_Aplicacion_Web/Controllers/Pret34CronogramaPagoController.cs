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
    public class Pret34CronogramaPagoController : Controller
    {
        private readonly EagleContext _context;

        public Pret34CronogramaPagoController(EagleContext context)
        {
            _context = context;
        }

        // GET: Pret34CronogramaPago
        public async Task<IActionResult> Index()
        {
            var eagleContext = _context.Pret34CronogramaPagos.Include(p => p.IdPrestamoNavigation).Include(p => p.IdUsuarioNavigation).Include(p => p.IdUsuariomodNavigation);
            return View(await eagleContext.ToListAsync());
        }

        // GET: Pret34CronogramaPago/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Pret34CronogramaPagos == null)
            {
                return NotFound();
            }

            var pret34CronogramaPago = await _context.Pret34CronogramaPagos
                .Include(p => p.IdPrestamoNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .Include(p => p.IdUsuariomodNavigation)
                .FirstOrDefaultAsync(m => m.IdCronograma == id);
            if (pret34CronogramaPago == null)
            {
                return NotFound();
            }

            return View(pret34CronogramaPago);
        }

        // GET: Pret34CronogramaPago/Create
        public IActionResult Create()
        {
            ViewData["IdPrestamo"] = new SelectList(_context.Pret33Prestamos, "IdPrestamo", "IdPrestamo");
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Pret34CronogramaPago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCronograma,IdPrestamo,FechaPago,SaldoInicial,Amortizacion,Interes,Cuota,FechaCreacion,IdEstado,TxtEstado,IdUsuario,TxtUsuario,IdUsuariomod,TxtUsuariomod,FechaModificacion")] Pret34CronogramaPago pret34CronogramaPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pret34CronogramaPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPrestamo"] = new SelectList(_context.Pret33Prestamos, "IdPrestamo", "IdPrestamo", pret34CronogramaPago.IdPrestamo);
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret34CronogramaPago.IdUsuario);
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret34CronogramaPago.IdUsuariomod);
            return View(pret34CronogramaPago);
        }

        // GET: Pret34CronogramaPago/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Pret34CronogramaPagos == null)
            {
                return NotFound();
            }

            var pret34CronogramaPago = await _context.Pret34CronogramaPagos.FindAsync(id);
            if (pret34CronogramaPago == null)
            {
                return NotFound();
            }
            ViewData["IdPrestamo"] = new SelectList(_context.Pret33Prestamos, "IdPrestamo", "IdPrestamo", pret34CronogramaPago.IdPrestamo);
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret34CronogramaPago.IdUsuario);
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret34CronogramaPago.IdUsuariomod);
            return View(pret34CronogramaPago);
        }

        // POST: Pret34CronogramaPago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCronograma,IdPrestamo,FechaPago,SaldoInicial,Amortizacion,Interes,Cuota,FechaCreacion,IdEstado,TxtEstado,IdUsuario,TxtUsuario,IdUsuariomod,TxtUsuariomod,FechaModificacion")] Pret34CronogramaPago pret34CronogramaPago)
        {
            if (id != pret34CronogramaPago.IdCronograma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pret34CronogramaPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pret34CronogramaPagoExists(pret34CronogramaPago.IdCronograma))
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
            ViewData["IdPrestamo"] = new SelectList(_context.Pret33Prestamos, "IdPrestamo", "IdPrestamo", pret34CronogramaPago.IdPrestamo);
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret34CronogramaPago.IdUsuario);
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret34CronogramaPago.IdUsuariomod);
            return View(pret34CronogramaPago);
        }

        // GET: Pret34CronogramaPago/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Pret34CronogramaPagos == null)
            {
                return NotFound();
            }

            var pret34CronogramaPago = await _context.Pret34CronogramaPagos
                .Include(p => p.IdPrestamoNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .Include(p => p.IdUsuariomodNavigation)
                .FirstOrDefaultAsync(m => m.IdCronograma == id);
            if (pret34CronogramaPago == null)
            {
                return NotFound();
            }

            return View(pret34CronogramaPago);
        }

        // POST: Pret34CronogramaPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Pret34CronogramaPagos == null)
            {
                return Problem("Entity set 'EagleContext.Pret34CronogramaPagos'  is null.");
            }
            var pret34CronogramaPago = await _context.Pret34CronogramaPagos.FindAsync(id);
            if (pret34CronogramaPago != null)
            {
                _context.Pret34CronogramaPagos.Remove(pret34CronogramaPago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pret34CronogramaPagoExists(long id)
        {
          return (_context.Pret34CronogramaPagos?.Any(e => e.IdCronograma == id)).GetValueOrDefault();
        }
    }
}

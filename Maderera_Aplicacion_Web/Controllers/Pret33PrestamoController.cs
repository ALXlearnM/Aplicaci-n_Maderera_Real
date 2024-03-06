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
    public class Pret33PrestamoController : Controller
    {
        private readonly EagleContext _context;

        public Pret33PrestamoController(EagleContext context)
        {
            _context = context;
        }

        // GET: Pret33Prestamo
        public async Task<IActionResult> Index()
        {
            var eagleContext = _context.Pret33Prestamos.Include(p => p.IdAutorizadorNavigation).Include(p => p.IdEmpleadoNavigation).Include(p => p.IdMotivoNavigation).Include(p => p.IdTipoPlazoNavigation).Include(p => p.IdTipoPrestamoNavigation).Include(p => p.IdUsuarioNavigation).Include(p => p.IdUsuariomodNavigation);
            return View(await eagleContext.ToListAsync());
        }

        // GET: Pret33Prestamo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Pret33Prestamos == null)
            {
                return NotFound();
            }

            var pret33Prestamo = await _context.Pret33Prestamos
                .Include(p => p.IdAutorizadorNavigation)
                .Include(p => p.IdEmpleadoNavigation)
                .Include(p => p.IdMotivoNavigation)
                .Include(p => p.IdTipoPlazoNavigation)
                .Include(p => p.IdTipoPrestamoNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .Include(p => p.IdUsuariomodNavigation)
                .FirstOrDefaultAsync(m => m.IdPrestamo == id);
            if (pret33Prestamo == null)
            {
                return NotFound();
            }

            return View(pret33Prestamo);
        }

        // GET: Pret33Prestamo/Create
        public IActionResult Create()
        {
            ViewData["IdAutorizador"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado");
            ViewData["IdEmpleado"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado");
            ViewData["IdMotivo"] = new SelectList(_context.Pret32Motivos, "IdMotivo", "IdMotivo");
            ViewData["IdTipoPlazo"] = new SelectList(_context.Pret31TipoPlazos, "IdTipoPlazo", "IdTipoPlazo");
            ViewData["IdTipoPrestamo"] = new SelectList(_context.Pret30TipoPrestamos, "IdTipoPrestamo", "IdTipoPrestamo");
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Pret33Prestamo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrestamo,IdEmpleado,IdAutorizador,MontoTotal,IdTipoPrestamo,TxtObservacion,FechaAprobPrestamo,Plazo,FechaVtoProg,NroCuotasGracia,IdTipoPlazo,Posteo,IdMotivo,CuotaDoble,MontoTea,MontoTcea,FechaDesembolso,FechaPrimPago,Comisiones,FechaCreacion,IdEstado,TxtEstado,IdUsuario,TxtUsuario,IdUsuariomod,TxtUsuariomod,FechaModificacion")] Pret33Prestamo pret33Prestamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pret33Prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutorizador"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado", pret33Prestamo.IdAutorizador);
            ViewData["IdEmpleado"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado", pret33Prestamo.IdEmpleado);
            ViewData["IdMotivo"] = new SelectList(_context.Pret32Motivos, "IdMotivo", "IdMotivo", pret33Prestamo.IdMotivo);
            ViewData["IdTipoPlazo"] = new SelectList(_context.Pret31TipoPlazos, "IdTipoPlazo", "IdTipoPlazo", pret33Prestamo.IdTipoPlazo);
            ViewData["IdTipoPrestamo"] = new SelectList(_context.Pret30TipoPrestamos, "IdTipoPrestamo", "IdTipoPrestamo", pret33Prestamo.IdTipoPrestamo);
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret33Prestamo.IdUsuario);
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret33Prestamo.IdUsuariomod);
            return View(pret33Prestamo);
        }

        // GET: Pret33Prestamo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Pret33Prestamos == null)
            {
                return NotFound();
            }

            var pret33Prestamo = await _context.Pret33Prestamos.FindAsync(id);
            if (pret33Prestamo == null)
            {
                return NotFound();
            }
            ViewData["IdAutorizador"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado", pret33Prestamo.IdAutorizador);
            ViewData["IdEmpleado"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado", pret33Prestamo.IdEmpleado);
            ViewData["IdMotivo"] = new SelectList(_context.Pret32Motivos, "IdMotivo", "IdMotivo", pret33Prestamo.IdMotivo);
            ViewData["IdTipoPlazo"] = new SelectList(_context.Pret31TipoPlazos, "IdTipoPlazo", "IdTipoPlazo", pret33Prestamo.IdTipoPlazo);
            ViewData["IdTipoPrestamo"] = new SelectList(_context.Pret30TipoPrestamos, "IdTipoPrestamo", "IdTipoPrestamo", pret33Prestamo.IdTipoPrestamo);
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret33Prestamo.IdUsuario);
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret33Prestamo.IdUsuariomod);
            return View(pret33Prestamo);
        }

        // POST: Pret33Prestamo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdPrestamo,IdEmpleado,IdAutorizador,MontoTotal,IdTipoPrestamo,TxtObservacion,FechaAprobPrestamo,Plazo,FechaVtoProg,NroCuotasGracia,IdTipoPlazo,Posteo,IdMotivo,CuotaDoble,MontoTea,MontoTcea,FechaDesembolso,FechaPrimPago,Comisiones,FechaCreacion,IdEstado,TxtEstado,IdUsuario,TxtUsuario,IdUsuariomod,TxtUsuariomod,FechaModificacion")] Pret33Prestamo pret33Prestamo)
        {
            if (id != pret33Prestamo.IdPrestamo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pret33Prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pret33PrestamoExists(pret33Prestamo.IdPrestamo))
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
            ViewData["IdAutorizador"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado", pret33Prestamo.IdAutorizador);
            ViewData["IdEmpleado"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado", pret33Prestamo.IdEmpleado);
            ViewData["IdMotivo"] = new SelectList(_context.Pret32Motivos, "IdMotivo", "IdMotivo", pret33Prestamo.IdMotivo);
            ViewData["IdTipoPlazo"] = new SelectList(_context.Pret31TipoPlazos, "IdTipoPlazo", "IdTipoPlazo", pret33Prestamo.IdTipoPlazo);
            ViewData["IdTipoPrestamo"] = new SelectList(_context.Pret30TipoPrestamos, "IdTipoPrestamo", "IdTipoPrestamo", pret33Prestamo.IdTipoPrestamo);
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret33Prestamo.IdUsuario);
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", pret33Prestamo.IdUsuariomod);
            return View(pret33Prestamo);
        }

        // GET: Pret33Prestamo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Pret33Prestamos == null)
            {
                return NotFound();
            }

            var pret33Prestamo = await _context.Pret33Prestamos
                .Include(p => p.IdAutorizadorNavigation)
                .Include(p => p.IdEmpleadoNavigation)
                .Include(p => p.IdMotivoNavigation)
                .Include(p => p.IdTipoPlazoNavigation)
                .Include(p => p.IdTipoPrestamoNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .Include(p => p.IdUsuariomodNavigation)
                .FirstOrDefaultAsync(m => m.IdPrestamo == id);
            if (pret33Prestamo == null)
            {
                return NotFound();
            }

            return View(pret33Prestamo);
        }

        // POST: Pret33Prestamo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Pret33Prestamos == null)
            {
                return Problem("Entity set 'EagleContext.Pret33Prestamos'  is null.");
            }
            var pret33Prestamo = await _context.Pret33Prestamos.FindAsync(id);
            if (pret33Prestamo != null)
            {
                _context.Pret33Prestamos.Remove(pret33Prestamo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pret33PrestamoExists(long id)
        {
          return (_context.Pret33Prestamos?.Any(e => e.IdPrestamo == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Models;

namespace Aplicacion_Maderera.Controllers
{
    public class Tnst02CompRecibidoDtlController : Controller
    {
        private readonly EagleContext _context;

        public Tnst02CompRecibidoDtlController(EagleContext context)
        {
            _context = context;
        }

        // GET: Tnst02CompRecibidoDtl
        public async Task<IActionResult> Index()
        {
            var eagleContext = _context.Tnst02CompRecibidoDtls.Include(t => t.IdCompRecibidoNavigation).Include(t => t.IdProductoNavigation).Include(t => t.IdRazonNavigation).Include(t => t.IdUmNavigation);
            return View(await eagleContext.ToListAsync());
        }

        // GET: Tnst02CompRecibidoDtl/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Tnst02CompRecibidoDtls == null)
            {
                return NotFound();
            }

            var tnst02CompRecibidoDtl = await _context.Tnst02CompRecibidoDtls
                .Include(t => t.IdCompRecibidoNavigation)
                .Include(t => t.IdProductoNavigation)
                .Include(t => t.IdRazonNavigation)
                .Include(t => t.IdUmNavigation)
                .FirstOrDefaultAsync(m => m.IdCompRecibidoDtl == id);
            if (tnst02CompRecibidoDtl == null)
            {
                return NotFound();
            }

            return View(tnst02CompRecibidoDtl);
        }

        // GET: Tnst02CompRecibidoDtl/Create
        public IActionResult Create()
        {
            ViewData["IdCompRecibido"] = new SelectList(_context.Tnst01CompRecibidos, "IdCompRecibido", "IdCompRecibido");
            ViewData["IdProducto"] = new SelectList(_context.Prot09Productos, "IdProducto", "IdProducto");
            ViewData["IdRazon"] = new SelectList(_context.Mstt05Razons, "IdRazon", "IdRazon");
            ViewData["IdUm"] = new SelectList(_context.Sntt06UnidadMedida, "IdUm", "IdUm");
            return View();
        }

        // POST: Tnst02CompRecibidoDtl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompRecibidoDtl,IdProducto,IdCompRecibido,TxtProducto,Cantidad,Peso,PorDscto,MtoDsctoSinTax,MtoDsctoConTax,PunitSinTax,PunitConTax,TaxPorTot,TaxMtoTot,TaxPor01,TaxPor02,TaxPor03,TaxPor04,TaxPor05,TaxPor06,TaxPor07,TaxPor08,TaxMto01,TaxMto02,TaxMto03,TaxMto04,TaxMto05,TaxMto06,TaxMto07,TaxMto08,MtoVtaSinTax,MtoVtaConTax,TxtObserv,IdEstado,TxtEstado,IdUm,IdRazon")] Tnst02CompRecibidoDtl tnst02CompRecibidoDtl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tnst02CompRecibidoDtl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompRecibido"] = new SelectList(_context.Tnst01CompRecibidos, "IdCompRecibido", "IdCompRecibido", tnst02CompRecibidoDtl.IdCompRecibido);
            ViewData["IdProducto"] = new SelectList(_context.Prot09Productos, "IdProducto", "IdProducto", tnst02CompRecibidoDtl.IdProducto);
            ViewData["IdRazon"] = new SelectList(_context.Mstt05Razons, "IdRazon", "IdRazon", tnst02CompRecibidoDtl.IdRazon);
            ViewData["IdUm"] = new SelectList(_context.Sntt06UnidadMedida, "IdUm", "IdUm", tnst02CompRecibidoDtl.IdUm);
            return View(tnst02CompRecibidoDtl);
        }

        // GET: Tnst02CompRecibidoDtl/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Tnst02CompRecibidoDtls == null)
            {
                return NotFound();
            }

            var tnst02CompRecibidoDtl = await _context.Tnst02CompRecibidoDtls.FindAsync(id);
            if (tnst02CompRecibidoDtl == null)
            {
                return NotFound();
            }
            ViewData["IdCompRecibido"] = new SelectList(_context.Tnst01CompRecibidos, "IdCompRecibido", "IdCompRecibido", tnst02CompRecibidoDtl.IdCompRecibido);
            ViewData["IdProducto"] = new SelectList(_context.Prot09Productos, "IdProducto", "IdProducto", tnst02CompRecibidoDtl.IdProducto);
            ViewData["IdRazon"] = new SelectList(_context.Mstt05Razons, "IdRazon", "IdRazon", tnst02CompRecibidoDtl.IdRazon);
            ViewData["IdUm"] = new SelectList(_context.Sntt06UnidadMedida, "IdUm", "IdUm", tnst02CompRecibidoDtl.IdUm);
            return View(tnst02CompRecibidoDtl);
        }

        // POST: Tnst02CompRecibidoDtl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCompRecibidoDtl,IdProducto,IdCompRecibido,TxtProducto,Cantidad,Peso,PorDscto,MtoDsctoSinTax,MtoDsctoConTax,PunitSinTax,PunitConTax,TaxPorTot,TaxMtoTot,TaxPor01,TaxPor02,TaxPor03,TaxPor04,TaxPor05,TaxPor06,TaxPor07,TaxPor08,TaxMto01,TaxMto02,TaxMto03,TaxMto04,TaxMto05,TaxMto06,TaxMto07,TaxMto08,MtoVtaSinTax,MtoVtaConTax,TxtObserv,IdEstado,TxtEstado,IdUm,IdRazon")] Tnst02CompRecibidoDtl tnst02CompRecibidoDtl)
        {
            if (id != tnst02CompRecibidoDtl.IdCompRecibidoDtl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tnst02CompRecibidoDtl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tnst02CompRecibidoDtlExists(tnst02CompRecibidoDtl.IdCompRecibidoDtl))
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
            ViewData["IdCompRecibido"] = new SelectList(_context.Tnst01CompRecibidos, "IdCompRecibido", "IdCompRecibido", tnst02CompRecibidoDtl.IdCompRecibido);
            ViewData["IdProducto"] = new SelectList(_context.Prot09Productos, "IdProducto", "IdProducto", tnst02CompRecibidoDtl.IdProducto);
            ViewData["IdRazon"] = new SelectList(_context.Mstt05Razons, "IdRazon", "IdRazon", tnst02CompRecibidoDtl.IdRazon);
            ViewData["IdUm"] = new SelectList(_context.Sntt06UnidadMedida, "IdUm", "IdUm", tnst02CompRecibidoDtl.IdUm);
            return View(tnst02CompRecibidoDtl);
        }

        // GET: Tnst02CompRecibidoDtl/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Tnst02CompRecibidoDtls == null)
            {
                return NotFound();
            }

            var tnst02CompRecibidoDtl = await _context.Tnst02CompRecibidoDtls
                .Include(t => t.IdCompRecibidoNavigation)
                .Include(t => t.IdProductoNavigation)
                .Include(t => t.IdRazonNavigation)
                .Include(t => t.IdUmNavigation)
                .FirstOrDefaultAsync(m => m.IdCompRecibidoDtl == id);
            if (tnst02CompRecibidoDtl == null)
            {
                return NotFound();
            }

            return View(tnst02CompRecibidoDtl);
        }

        // POST: Tnst02CompRecibidoDtl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Tnst02CompRecibidoDtls == null)
            {
                return Problem("Entity set 'EagleContext.Tnst02CompRecibidoDtls'  is null.");
            }
            var tnst02CompRecibidoDtl = await _context.Tnst02CompRecibidoDtls.FindAsync(id);
            if (tnst02CompRecibidoDtl != null)
            {
                _context.Tnst02CompRecibidoDtls.Remove(tnst02CompRecibidoDtl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Tnst02CompRecibidoDtlExists(long id)
        {
            return (_context.Tnst02CompRecibidoDtls?.Any(e => e.IdCompRecibidoDtl == id)).GetValueOrDefault();
        }
    }
}

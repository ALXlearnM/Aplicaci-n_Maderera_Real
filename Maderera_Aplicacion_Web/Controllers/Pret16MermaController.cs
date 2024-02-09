using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;

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
            var eagleContext = _context.Pret16Mermas.Include(p => p.IdPredioNavigation).
                Include(p => p.IdCampanaNavigation)
                .Include(p => p.IdProduccionNavigation);
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
                .Include(p => p.IdCampanaNavigation)
                .Include(p => p.IdPredioNavigation)
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

            return View();
        }



    }
}

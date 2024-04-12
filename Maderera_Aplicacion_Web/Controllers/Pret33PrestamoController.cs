using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Models;
using Maderera_Aplicacion_Web.Data.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using System.Numerics;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pret33PrestamoController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;
        private readonly long idusuario;
        private readonly string txtusuario;
        private readonly DateTime fechaHoy;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static long? IdTemporal { get; set; }
        private static long? IdTemporalCrono { get; set; }
        private static long? IdTemporalrefinancia { get; set; }

        public Pret33PrestamoController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pret33Prestamo
        public async Task<IActionResult> ListadoPrestamo()
        {
            var eagleContext = _context.Pret33Prestamos.Include(p => p.IdTiporazonAnulacionNavigation).Include(p => p.IdAutorizadorNavigation).Include(p => p.IdEmpleadoNavigation).Include(p => p.IdMotivoNavigation).Include(p => p.IdTipoPlazoNavigation).Include(p => p.IdTipoPrestamoNavigation).Include(p => p.IdUsuarioNavigation).Include(p => p.IdUsuariomodNavigation);
            ViewData["Idtiporazon"] = new SelectList(_context.Pret35TipoRazonAnulacions, "IdTiporazonAnulacion", "TxtDesc");
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
        [HttpGet]
        // GET: Pret33Prestamo/Create
        public IActionResult Prestamo()
        {
            ViewData["Idtiporazon"] = new SelectList(_context.Pret35TipoRazonAnulacions, "IdTiporazonAnulacion", "TxtDesc");
            IdTemporal = null;
            IdTemporalrefinancia = null;
            IdTemporalCrono = null;
            ViewBag.EmpleadoP = _context.Pert04Empleados
                    .Include(e => e.IdNacionalidadNavigation)
                    .Include(e => e.IdDistNavigation)
                        .ThenInclude(d => d.IdProvNavigation)
                            .ThenInclude(p => p.IdDptoNavigation)
                    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        Idempleado = e.IdEmpleado,
                        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                        cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                        telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                    .ToList();
            ViewData["IdEmpleado"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado");
            ViewData["IdMotivo"] = new SelectList(_context.Pret32Motivos, "IdMotivo", "TxtMotivo");
            ViewData["IdTipoPlazo"] = new SelectList(_context.Pret31TipoPlazos, "IdTipoPlazo", "TxtTipoPlazo");
            ViewData["IdTipoPrestamo"] = new SelectList(_context.Pret30TipoPrestamos, "IdTipoPrestamo", "TxtTipoPrestamo");
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }
        [HttpGet]
        public IActionResult EditarPrestamo(long idprestamo)
        {
            ViewData["Idtiporazon"] = new SelectList(_context.Pret35TipoRazonAnulacions, "IdTiporazonAnulacion", "TxtDesc");
            IdTemporal = null;
            IdTemporalCrono = null;
            ViewBag.EmpleadoP = _context.Pert04Empleados
                    .Include(e => e.IdNacionalidadNavigation)
                    .Include(e => e.IdDistNavigation)
                        .ThenInclude(d => d.IdProvNavigation)
                            .ThenInclude(p => p.IdDptoNavigation)
                    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        Idempleado = e.IdEmpleado,
                        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                        cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                        telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                    .ToList();
            ViewData["IdEmpleado"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado");
            ViewData["IdMotivo"] = new SelectList(_context.Pret32Motivos, "IdMotivo", "TxtMotivo");
            ViewData["IdTipoPlazo"] = new SelectList(_context.Pret31TipoPlazos, "IdTipoPlazo", "TxtTipoPlazo");
            ViewData["IdTipoPrestamo"] = new SelectList(_context.Pret30TipoPrestamos, "IdTipoPrestamo", "TxtTipoPrestamo");
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            var prestamo = _context.Pret33Prestamos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuariomodNavigation)
            .Where(c => c.IdPrestamo == idprestamo)
            .Select(campanaTA => new
            {
                all = campanaTA,
                CampanaTA = campanaTA,
                empleado = campanaTA.IdEmpleadoNavigation.TxtPriNom + " " + campanaTA.IdEmpleadoNavigation.TxtApePat,
                montop = campanaTA.MontoPrestamo,
                montoc = campanaTA.MontoCuota,
                tea = campanaTA.MontoTea,
                comisiones = campanaTA.Comisiones,
                tcea = campanaTA.MontoTcea,
                plazo = campanaTA.Plazo,
                montot = campanaTA.MontoTotal,

            })
            .FirstOrDefault();

            var prestamovista = _context.Pret33Prestamos
            .Where(c => c.IdPrestamo == idprestamo)
            .FirstOrDefault();
            if (prestamo != null)
            {

                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(prestamo, settings);

                IdTemporal = prestamovista?.IdPrestamo;
                ViewBag.Prestamo = campanaserialized;
                ViewBag.tipo = 1;
                return View("Prestamo", prestamovista);
            }
            else
            {
                return NotFound(); // O redirigir a otra página
            }
        }

        [HttpGet]
        public IActionResult RefinanciarPrestamo()
        {
            try
            {
                ViewData["Idtiporazon"] = new SelectList(_context.Pret35TipoRazonAnulacions, "IdTiporazonAnulacion", "TxtDesc");
                IdTemporalrefinancia = IdTemporal;
                IdTemporal = null;
                IdTemporalCrono = null;
                ViewBag.EmpleadoP = _context.Pert04Empleados
                        .Include(e => e.IdNacionalidadNavigation)
                        .Include(e => e.IdDistNavigation)
                            .ThenInclude(d => d.IdProvNavigation)
                                .ThenInclude(p => p.IdDptoNavigation)
                        .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                        .Select(e => new
                        {
                            Idempleado = e.IdEmpleado,
                            NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                            cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                            telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                            Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                            ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                        })
                        .ToList();
                ViewData["IdEmpleado"] = new SelectList(_context.Pert04Empleados, "IdEmpleado", "IdEmpleado");
                ViewData["IdMotivo"] = new SelectList(_context.Pret32Motivos, "IdMotivo", "TxtMotivo");
                ViewData["IdTipoPlazo"] = new SelectList(_context.Pret31TipoPlazos, "IdTipoPlazo", "TxtTipoPlazo");
                ViewData["IdTipoPrestamo"] = new SelectList(_context.Pret30TipoPrestamos, "IdTipoPrestamo", "TxtTipoPrestamo");
                ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
                ViewData["IdUsuariomod"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
                var prestamo = _context.Pret33Prestamos
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.IdUsuariomodNavigation)
                .Where(c => c.IdPrestamo == IdTemporalrefinancia)
                .Select(campanaTA => new
                {
                    all = campanaTA,
                    CampanaTA = campanaTA,
                    empleado = campanaTA.IdEmpleadoNavigation.TxtPriNom + " " + campanaTA.IdEmpleadoNavigation.TxtApePat,
                    montop = campanaTA.MontoPrestamo,
                    montoc = campanaTA.MontoCuota,
                    tea= campanaTA.MontoTea,
                    comisiones= campanaTA.Comisiones,
                    tcea= campanaTA.MontoTcea,
                    plazo= campanaTA.Plazo,
                    montot= campanaTA.MontoTotal,

                })
                .FirstOrDefault();

                var prestamovista = _context.Pret33Prestamos
                .Where(c => c.IdPrestamo == IdTemporalrefinancia)
                .FirstOrDefault();
                if (prestamo != null)
                {

                    var settings = new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    };

                    var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(prestamo, settings);

                    ViewBag.Prestamo = campanaserialized;
                    ViewBag.monto = (prestamo.CampanaTA.MontoPrestamo) - _context.Pret34CronogramaPagos.Where(p => p.IdPrestamo == IdTemporalrefinancia && p.IdEstado == 8).Sum(p => p.Cuota);
                    ViewBag.tipo = 2;
                    ViewBag.countdtl = (prestamo.CampanaTA.Plazo)-_context.Pret34CronogramaPagos.Where(p => p.IdPrestamo == IdTemporalrefinancia && p.IdEstado == 8).Count();
                    return View("Prestamo", prestamovista);
                }
                else
                {
                    return NotFound(); // O redirigir a otra página
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult RecargarEmpleadoP()
        {
            try
            {
                var updatedEmpleado = _context.Pert04Empleados
                    .Include(e => e.IdNacionalidadNavigation)
                    .Include(e => e.IdDistNavigation)
                        .ThenInclude(d => d.IdProvNavigation)
                            .ThenInclude(p => p.IdDptoNavigation)
                    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        Idempleado = e.IdEmpleado,
                        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                        cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                        telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                    .ToList();

                return Json(updatedEmpleado);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el inversionista: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult CerrarPrestamo()
        {
            IdTemporal = null;
            IdTemporalCrono = null;
            IdTemporalrefinancia= null;
            var redirectUrl = Url.Action("ListadoPrestamo", "Pret33Prestamo");
            var response = new { redirectUrl };
            return Json(response);
        }


        [HttpGet]
        public IActionResult CerrarCronograma()
        {
            IdTemporalCrono = null;
            var redirectUrl = Url.Action("ListadoCrono", "Pret33Prestamo");
            var response = new { redirectUrl };
            return Json(response);
        }
        [HttpGet]
        public IActionResult CalcularPrestamo(double interes, double monto, int plazo, int tipoplazo)
        {
            try
            {
                double cuota = 0;

                switch (tipoplazo)
                {
                    case 1:
                        {
                            if (interes == 0)
                            {
                                cuota = monto/plazo;
                            }
                            else { 
                            var interesc = Math.Pow(1 + ((double)(interes / 100) * 1 / 12), plazo);
                            cuota = (monto * (interes / 1200) * interesc) / (interesc - 1);
                            }
                        }
                        break;
                    case 2:
                        {
                            if (interes == 0)
                            {
                                cuota = monto / plazo;
                            }
                            else
                            {
                                var interesc = Math.Pow(1 + ((double)(interes / 100) * 1 / 52), plazo);
                                cuota = (monto * (interes / 5200) * interesc) / (interesc - 1);
                            }
                        }
                        break;
                    case 3:
                        {
                            if (interes == 0)
                            {
                                cuota = monto / plazo;
                            }
                            else
                            {
                                var interesc = Math.Pow(1 + ((double)(interes / 100) * 1 / 24), plazo);
                                cuota = (monto * (interes / 2400) * interesc) / (interesc - 1);
                            }

                        }
                        break;
                }

                var montoT = cuota * plazo;
                var response = new { cuota, montoT };
                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(new { errores = new List<string> { ex.Message, ex.StackTrace } });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearPrestamo(int idempleado, int idtipoprestamo, int idtipoplazo, int idtipomotivo, decimal tea,
            string observacion, decimal montoprestamo, int plazo, int cuotasgracia, decimal comisiones, decimal tcea,
            DateTime Fechaaprob, DateTime Fechavto, DateTime Fechades, DateTime Fechaprimp,
            bool estado, bool cuotadoble, bool post)
        {
            try
            {
                if (IdTemporalrefinancia != null)
                {
                    var prestamorefinanciado = _context.Pret33Prestamos
                .Where(c => c.IdPrestamo == IdTemporalrefinancia)
                .FirstOrDefault();
                    prestamorefinanciado.IdEstado = 5;
                    prestamorefinanciado.TxtEstado= "ANULADO";
                    var existingdtlprestamo= _context.Pret34CronogramaPagos
                .Where(c => c.IdPrestamo == IdTemporalrefinancia && c.IdEstado!= 8)
                .ToList();
                    foreach (Pret34CronogramaPago unidad in existingdtlprestamo) { 
                    unidad.IdEstado= 5;
                    unidad.TxtEstado= "ANULADO";

                    }
                    _context.SaveChanges();
                    IdTemporalrefinancia = null;
                }
                DateTime fechavencimiento = Fechaprimp;
                decimal montoinicial = 0;
                decimal montointeres = 0;
                decimal montoamortizacion = 0;
                decimal saldoinicial = 0;
                var existingPrestamo = _context.Pret33Prestamos.AsNoTracking()
           .FirstOrDefault(c => c.IdPrestamo == IdTemporal);
                decimal cuota = 0;
                decimal interess = 0;
                switch (idtipoplazo)
                {
                    case 1:
                        {
                            if (tcea==0)
                            {
                                cuota = montoprestamo / plazo;
                            }
                            else {
                                var interesc = Math.Pow(1 + ((double)(tcea / 100) * 1 / 12), plazo);
                                cuota = (montoprestamo * (tcea / 1200) * (decimal)interesc) / ((decimal)interesc - 1);
                                interess = (tcea / 1200);
                            }
                        }
                        break;
                    case 2:
                        {
                            if (tcea == 0)
                            {
                                cuota = montoprestamo / plazo;
                            }
                            else
                            {
                                var interesc = Math.Pow(1 + ((double)(tcea / 100) * 1 / 52), plazo);
                                cuota = (montoprestamo * (tcea / 5200) * (decimal)interesc) / ((decimal)interesc - 1);
                                interess = (tcea / 5200);
                            }
                        }
                        break;
                    case 3:
                        {
                            if (tcea == 0)
                            {
                                cuota = montoprestamo / plazo;
                            }
                            else
                            {
                                var interesc = Math.Pow(1 + ((double)(tcea / 100) * 1 / 24), plazo);
                                cuota = (montoprestamo * (tcea / 2400) * (decimal)interesc) / ((decimal)interesc - 1);
                                interess = (tcea / 2400);
                            }
                        }
                        break;
                }
                if (existingPrestamo != null)
                {
                    existingPrestamo.IdEmpleado = idempleado;
                    existingPrestamo.IdAutorizador = _context.Pert01Usuarios.Where(p => p.IdUsuario == idusuario).Select(p => p.IdEmpleado).FirstOrDefault();
                    existingPrestamo.IdTipoPrestamo = idtipoprestamo;
                    existingPrestamo.IdTipoPlazo = idtipoplazo;
                    existingPrestamo.IdMotivo = idtipomotivo;
                    existingPrestamo.MontoTea = tea;
                    existingPrestamo.TxtObservacion = observacion;
                    existingPrestamo.MontoPrestamo = montoprestamo;
                    existingPrestamo.MontoTotal = cuota * plazo;
                    existingPrestamo.Plazo = plazo;
                    existingPrestamo.MontoCuota = cuota;
                    existingPrestamo.NroCuotasGracia = cuotasgracia;
                    existingPrestamo.Comisiones = comisiones;
                    existingPrestamo.MontoTcea = tcea;
                    existingPrestamo.FechaAprobPrestamo = Fechaaprob;
                    existingPrestamo.FechaVtoProg = Fechavto;
                    existingPrestamo.FechaDesembolso = Fechades;
                    existingPrestamo.FechaPrimPago = Fechaprimp;
                    existingPrestamo.CuotaDoble = cuotadoble;
                    //existingPrestamo.Posteo = post;
                    if (existingPrestamo.IdEstado == 3)
                    {
                        existingPrestamo.IdEstado = estado == true ? 3 : 6;
                        existingPrestamo.TxtEstado = estado == true ? "BORRADOR" : "PRESTADO";
                    }
                    existingPrestamo.IdUsuariomod = idusuario;
                    existingPrestamo.TxtUsuariomod = txtusuario;
                    existingPrestamo.FechaModificacion = fechaHoy;
                    _context.Pret33Prestamos.Update(existingPrestamo);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingPrestamo.IdPrestamo;

                    var existingCronogramaPrestamo = _context.Pret34CronogramaPagos.Where(P => P.IdPrestamo == IdTemporal).ToList();
                    //invalidar anteriores cronogramas
                    foreach (Pret34CronogramaPago item in existingCronogramaPrestamo)
                    {
                        item.IdEstado = 5;
                        item.TxtEstado = "ANULADO";
                    }
                    //CREACION DE CRONOGRAMA
                    for (int i = 1; i <= plazo; i++)
                    {

                        if (i == 1)
                        {
                            montoinicial = montoprestamo;
                            montointeres = montoinicial * interess;
                            montoamortizacion = cuota - (montointeres);
                            saldoinicial = montoinicial - montoamortizacion;

                        }
                        else
                        {
                            switch (idtipoplazo)
                            {
                                case 1:
                                    fechavencimiento = fechavencimiento.AddMonths(1);
                                    break;
                                case 2:
                                    fechavencimiento = fechavencimiento.AddDays(7);
                                    break;
                                case 3:
                                    fechavencimiento = fechavencimiento.AddDays(15);
                                    break;
                            }
                            montointeres = saldoinicial * interess;
                            montoamortizacion = cuota - (montointeres);
                            saldoinicial = saldoinicial - montoamortizacion;

                        }


                        var Pret34Cronograma = new Pret34CronogramaPago
                        {
                            IdPrestamo = IdTemporal,
                            FechaPago = null,
                            SaldoInicial = saldoinicial,
                            Amortizacion = montoamortizacion,
                            Interes = montointeres,
                            Cuota = cuota,
                            FechaVencimiento = fechavencimiento,
                            //Posteo = post,
                            FechaCreacion = fechaHoy,
                            IdEstado = estado == true ? 3 : 7,
                            TxtEstado = estado == true ? "BORRADOR" : "PENDIENTE",
                            IdUsuario = idusuario,
                            TxtUsuario = txtusuario,
                        };
                        _context.Pret34CronogramaPagos.Add(Pret34Cronograma);
                        await _context.SaveChangesAsync();
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    //CREACION DE PRESTAMO
                    var Pret33Prestamo = new Pret33Prestamo
                    {
                        IdEmpleado = idempleado,
                        IdAutorizador = _context.Pert01Usuarios.Where(p => p.IdUsuario == idusuario).Select(p => p.IdEmpleado).FirstOrDefault(),
                        IdTipoPrestamo = idtipoprestamo,
                        IdTipoPlazo = idtipoplazo,
                        IdMotivo = idtipomotivo,
                        MontoTea = tea,
                        TxtObservacion = observacion,
                        MontoPrestamo = montoprestamo,
                        MontoTotal = cuota * plazo,
                        Plazo = plazo,
                        MontoCuota = cuota,
                        NroCuotasGracia = cuotasgracia,
                        Comisiones = comisiones,
                        MontoTcea = tcea,
                        FechaAprobPrestamo = Fechaaprob,
                        FechaVtoProg = Fechavto,
                        FechaDesembolso = Fechades,
                        FechaPrimPago = Fechaprimp,
                        CuotaDoble = cuotadoble,
                        Post=0,
                        FechaCreacion = fechaHoy,
                        IdEstado = estado == true ? 3 : 6,
                        TxtEstado = estado == true ? "BORRADOR" : "PRESTADO",
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                    };

                    _context.Pret33Prestamos.Add(Pret33Prestamo);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret33Prestamo.IdPrestamo;
                    //CREACION DE CRONOGRAMA
                    for (int i = 1; i <= plazo; i++)
                    {

                        if (i == 1)
                        {
                            montoinicial = montoprestamo;
                            montointeres = montoinicial * interess;
                            montoamortizacion = cuota - (montointeres);
                            saldoinicial = montoinicial - montoamortizacion;
                        }
                        else
                        {
                            switch (idtipoplazo)
                            {
                                case 1:
                                    fechavencimiento = fechavencimiento.AddMonths(1);
                                    break;
                                case 2:
                                    fechavencimiento = fechavencimiento.AddDays(7);
                                    break;
                                case 3:
                                    fechavencimiento = fechavencimiento.AddDays(15);
                                    break;
                            }
                            montointeres = saldoinicial * interess;
                            montoamortizacion = cuota - (montointeres);
                            saldoinicial = saldoinicial - montoamortizacion;

                        }


                        var Pret34Cronograma = new Pret34CronogramaPago
                        {
                            IdPrestamo = IdTemporal,
                            FechaPago = null,
                            SaldoInicial = saldoinicial,
                            Amortizacion = montoamortizacion,
                            Interes = montointeres,
                            Cuota = cuota,
                            FechaVencimiento = fechavencimiento,
                            //Posteo = post,
                            FechaCreacion = fechaHoy,
                            IdEstado = estado == true ? 3 : 7,
                            TxtEstado = estado == true ? "BORRADOR" : "PENDIENTE",
                            IdUsuario = idusuario,
                            TxtUsuario = txtusuario,
                        };
                        _context.Pret34CronogramaPagos.Add(Pret34Cronograma);
                        await _context.SaveChangesAsync();
                    }

                }
                return Json(new { mensaje = "Guardado correctamente" });

            }

            catch (Exception ex)
            {
                return Json(new { errores = new List<string> { ex.Message, ex.StackTrace } });
            }

        }

        [HttpPost]
        public async Task<IActionResult> CrearPrestamoycerrar(int idempleado, int idtipoprestamo, int idtipoplazo, int idtipomotivo, decimal tea,
            string observacion, decimal montoprestamo, int plazo, int cuotasgracia, decimal comisiones, decimal tcea,
            DateTime Fechaaprob, DateTime Fechavto, DateTime Fechades, DateTime Fechaprimp,
            bool estado, bool cuotadoble/*, bool post*/)
        {
            try
            {
                if (IdTemporalrefinancia != null)
                {
                    var prestamorefinanciado = _context.Pret33Prestamos
                .Where(c => c.IdPrestamo == IdTemporalrefinancia)
                .FirstOrDefault();
                    prestamorefinanciado.IdEstado = 5;
                    prestamorefinanciado.TxtEstado = "ANULADO";
                    var existingdtlprestamo = _context.Pret34CronogramaPagos
                .Where(c => c.IdPrestamo == IdTemporalrefinancia && c.IdEstado != 8)
                .ToList();
                    foreach (Pret34CronogramaPago unidad in existingdtlprestamo)
                    {
                        unidad.IdEstado = 5;
                        unidad.TxtEstado = "ANULADO";

                    }
                    _context.SaveChanges();
                    IdTemporalrefinancia = null;
                }
                DateTime fechavencimiento = Fechaprimp;
                decimal montoinicial = 0;
                decimal montointeres = 0;
                decimal montoamortizacion = 0;
                decimal saldoinicial = 0;
                var existingPrestamo = _context.Pret33Prestamos.AsNoTracking()
           .FirstOrDefault(c => c.IdPrestamo == IdTemporal);
                decimal cuota = 0;
                decimal interess = 0;
                switch (idtipoplazo)
                {
                    case 1:
                        {
                            if (tcea == 0)
                            {
                                cuota = montoprestamo / plazo;
                            }
                            else
                            {
                                var interesc = Math.Pow(1 + ((double)(tcea / 100) * 1 / 12), plazo);
                                cuota = (montoprestamo * (tcea / 1200) * (decimal)interesc) / ((decimal)interesc - 1);
                                interess = (tcea / 1200);
                            }
                        }
                        break;
                    case 2:
                        {
                            if (tcea == 0)
                            {
                                cuota = montoprestamo / plazo;
                            }
                            else
                            {
                                var interesc = Math.Pow(1 + ((double)(tcea / 100) * 1 / 52), plazo);
                                cuota = (montoprestamo * (tcea / 5200) * (decimal)interesc) / ((decimal)interesc - 1);
                                interess = (tcea / 5200);
                            }
                        }
                        break;
                    case 3:
                        {
                            if (tcea == 0)
                            {
                                cuota = montoprestamo / plazo;
                            }
                            else
                            {
                                var interesc = Math.Pow(1 + ((double)(tcea / 100) * 1 / 24), plazo);
                                cuota = (montoprestamo * (tcea / 2400) * (decimal)interesc) / ((decimal)interesc - 1);
                                interess = (tcea / 2400);
                            }
                        }
                        break;
                }
                if (existingPrestamo != null)
                {
                    existingPrestamo.IdEmpleado = idempleado;
                    existingPrestamo.IdAutorizador = _context.Pert01Usuarios.Where(p => p.IdUsuario == idusuario).Select(p => p.IdEmpleado).FirstOrDefault();
                    existingPrestamo.IdTipoPrestamo = idtipoprestamo;
                    existingPrestamo.IdTipoPlazo = idtipoplazo;
                    existingPrestamo.IdMotivo = idtipomotivo;
                    existingPrestamo.MontoTea = tea;
                    existingPrestamo.TxtObservacion = observacion;
                    existingPrestamo.MontoPrestamo = montoprestamo;
                    existingPrestamo.MontoTotal = cuota * plazo;
                    existingPrestamo.Plazo = plazo;
                    existingPrestamo.MontoCuota = cuota;
                    existingPrestamo.NroCuotasGracia = cuotasgracia;
                    existingPrestamo.Comisiones = comisiones;
                    existingPrestamo.MontoTcea = tcea;
                    existingPrestamo.FechaAprobPrestamo = Fechaaprob;
                    existingPrestamo.FechaVtoProg = Fechavto;
                    existingPrestamo.FechaDesembolso = Fechades;
                    existingPrestamo.FechaPrimPago = Fechaprimp;
                    existingPrestamo.CuotaDoble = cuotadoble;
                    //existingPrestamo.Posteo = post;
                    if (existingPrestamo.IdEstado == 3)
                    {
                        existingPrestamo.IdEstado = estado == true ? 3 : 6;
                        existingPrestamo.TxtEstado = estado == true ? "BORRADOR" : "PRESTADO";
                    }
                    existingPrestamo.IdUsuariomod = idusuario;
                    existingPrestamo.TxtUsuariomod = txtusuario;
                    existingPrestamo.FechaModificacion = fechaHoy;
                    _context.Pret33Prestamos.Update(existingPrestamo);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingPrestamo.IdPrestamo;

                    var existingCronogramaPrestamo = _context.Pret34CronogramaPagos.Where(P => P.IdPrestamo == IdTemporal).ToList();
                    //invalidar anteriores cronogramas
                    foreach (Pret34CronogramaPago item in existingCronogramaPrestamo)
                    {
                        item.IdEstado = 5;
                        item.TxtEstado = "ANULADO";
                    }
                    //CREACION DE CRONOGRAMA
                    for (int i = 1; i <= plazo; i++)
                    {

                        if (i == 1)
                        {
                            montoinicial = montoprestamo;
                            montointeres = montoinicial * interess;
                            montoamortizacion = cuota - (montointeres);
                            saldoinicial = montoinicial - montoamortizacion;

                        }
                        else
                        {
                            switch (idtipoplazo)
                            {
                                case 1:
                                    fechavencimiento = fechavencimiento.AddMonths(1);
                                    break;
                                case 2:
                                    fechavencimiento = fechavencimiento.AddDays(7);
                                    break;
                                case 3:
                                    fechavencimiento = fechavencimiento.AddDays(15);
                                    break;
                            }
                            montointeres = saldoinicial * interess;
                            montoamortizacion = cuota - (montointeres);
                            saldoinicial = saldoinicial - montoamortizacion;

                        }


                        var Pret34Cronograma = new Pret34CronogramaPago
                        {
                            IdPrestamo = IdTemporal,
                            FechaPago = null,
                            SaldoInicial = saldoinicial,
                            Amortizacion = montoamortizacion,
                            Interes = montointeres,
                            Cuota = cuota,
                            FechaVencimiento = fechavencimiento,
                            //Posteo = post,
                            FechaCreacion = fechaHoy,
                            IdEstado = estado == true ? 3 : 7,
                            TxtEstado = estado == true ? "BORRADOR" : "PENDIENTE",
                            IdUsuario = idusuario,
                            TxtUsuario = txtusuario,
                        };
                        _context.Pret34CronogramaPagos.Add(Pret34Cronograma);
                        await _context.SaveChangesAsync();
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    //CREACION DE PRESTAMO
                    var Pret33Prestamo = new Pret33Prestamo
                    {
                        IdEmpleado = idempleado,
                        IdAutorizador = _context.Pert01Usuarios.Where(p => p.IdUsuario == idusuario).Select(p => p.IdEmpleado).FirstOrDefault(),
                        IdTipoPrestamo = idtipoprestamo,
                        IdTipoPlazo = idtipoplazo,
                        IdMotivo = idtipomotivo,
                        MontoTea = tea,
                        TxtObservacion = observacion,
                        MontoPrestamo = montoprestamo,
                        MontoTotal = cuota * plazo,
                        Plazo = plazo,
                        MontoCuota = cuota,
                        NroCuotasGracia = cuotasgracia,
                        Comisiones = comisiones,
                        MontoTcea = tcea,
                        FechaAprobPrestamo = Fechaaprob,
                        FechaVtoProg = Fechavto,
                        FechaDesembolso = Fechades,
                        FechaPrimPago = Fechaprimp,
                        CuotaDoble = cuotadoble,
                        Post=0,
                        FechaCreacion = fechaHoy,
                        IdEstado = estado == true ? 3 : 6,
                        TxtEstado = estado == true ? "BORRADOR" : "PRESTADO",
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                    };

                    _context.Pret33Prestamos.Add(Pret33Prestamo);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret33Prestamo.IdPrestamo;
                    //CREACION DE CRONOGRAMA
                    for (int i = 1; i <= plazo; i++)
                    {

                        if (i == 1)
                        {
                            montoinicial = montoprestamo;
                            montointeres = montoinicial * interess;
                            montoamortizacion = cuota - (montointeres);
                            saldoinicial = montoinicial - montoamortizacion;
                        }
                        else
                        {
                            switch (idtipoplazo)
                            {
                                case 1:
                                    fechavencimiento = fechavencimiento.AddMonths(1);
                                    break;
                                case 2:
                                    fechavencimiento = fechavencimiento.AddDays(7);
                                    break;
                                case 3:
                                    fechavencimiento = fechavencimiento.AddDays(15);
                                    break;
                            }
                            montointeres = saldoinicial * interess;
                            montoamortizacion = cuota - (montointeres);
                            saldoinicial = saldoinicial - montoamortizacion;

                        }


                        var Pret34Cronograma = new Pret34CronogramaPago
                        {
                            IdPrestamo = IdTemporal,
                            FechaPago = null,
                            SaldoInicial = saldoinicial,
                            Amortizacion = montoamortizacion,
                            Interes = montointeres,
                            Cuota = cuota,
                            FechaVencimiento = fechavencimiento,
                            //Posteo = post,
                            FechaCreacion = fechaHoy,
                            IdEstado = estado == true ? 3 : 7,
                            TxtEstado = estado == true ? "BORRADOR" : "PENDIENTE",
                            IdUsuario = idusuario,
                            TxtUsuario = txtusuario,
                        };
                        _context.Pret34CronogramaPagos.Add(Pret34Cronograma);
                        await _context.SaveChangesAsync();
                    }

                }
                IdTemporal = null;
                IdTemporalCrono = null;
                return Json(new { redirectUrl = Url.Action("ListadoPrestamo", "Pret33Prestamo") });

            }

            catch (Exception ex)
            {
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }
        public IActionResult CargarMood()
        {
            if (IdTemporal != null)
            {
                var estado = _context.Pret33Prestamos.Where(t => t.IdPrestamo == IdTemporal).Select(t => t.IdEstado).FirstOrDefault();
                var estadoName = _context.Pret33Prestamos.Where(t => t.IdPrestamo == IdTemporal).Select(t => t.TxtEstado).FirstOrDefault();
                var post = _context.Pret33Prestamos.Where(t => t.IdPrestamo == IdTemporal).Select(t => t.Post).FirstOrDefault();
                var cuotadoble = _context.Pret33Prestamos.Where(t => t.IdPrestamo == IdTemporal).Select(t => t.CuotaDoble).FirstOrDefault();
                var response = new
                {
                    id = estado,
                    name = estadoName,
                    poste = post,
                    cuotad = cuotadoble
                };
                return Json(response);

            }
            else
            {
                return Json(new { success = true });

            }



        }

        [HttpPost]
        public async Task<IActionResult> EliminarPrestamo(long id)
        {
            try
            {
                var existingPrestamo = _context.Pret33Prestamos.Where(p => p.IdPrestamo == id).FirstOrDefault();

                if (existingPrestamo != null)
                {
                    var existingDtlPrestamo = _context.Pret34CronogramaPagos.Where(c => c.IdPrestamo == id).ToList();
                    foreach (Pret34CronogramaPago unidad in existingDtlPrestamo)
                    {
                        _context.Pret34CronogramaPagos.Remove(unidad);

                    }
                    _context.Pret33Prestamos.Remove(existingPrestamo);
                    await _context.SaveChangesAsync();
                }

                return Json(new { redirectUrl = Url.Action("ListadoPrestamo", "Pret33Prestamo") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AnularPrestamo(long id,int? idtiporazon,string txttiporazon)
        {
            try
            {
                var existingprestamo = _context.Pret33Prestamos.Where(c => c.IdPrestamo == id).ToList();
                foreach (Pret33Prestamo prestamo in existingprestamo)
                {
                    prestamo.IdEstado = 5;
                    prestamo.TxtEstado = "ANULADO";
                    prestamo.IdTiporazonAnulacion= idtiporazon;
                    prestamo.TxtRazon=txttiporazon;
                }
                var existingDtlPrestamo = _context.Pret34CronogramaPagos.Where(c => c.IdPrestamo == id).ToList();
                foreach (Pret34CronogramaPago unidad in existingDtlPrestamo)
                {
                    unidad.IdEstado = 5;
                    unidad.TxtEstado = "ANULADO";
                }
                await _context.SaveChangesAsync();


                return Json(new { redirectUrl = Url.Action("ListadoPrestamo", "Pret33Prestamo") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AnularPrestamopro(int? idtiporazon, string txttiporazon)
        {
            try
            {
                long? id =IdTemporal;
                var existingprestamo = _context.Pret33Prestamos.Where(c => c.IdPrestamo == id).ToList();
                foreach (Pret33Prestamo prestamo in existingprestamo)
                {
                    prestamo.IdEstado = 5;
                    prestamo.TxtEstado = "ANULADO";
                    prestamo.IdTiporazonAnulacion = idtiporazon;
                    prestamo.TxtRazon = txttiporazon;
                }
                var existingDtlPrestamo = _context.Pret34CronogramaPagos.Where(c => c.IdPrestamo == id).ToList();
                foreach (Pret34CronogramaPago unidad in existingDtlPrestamo)
                {
                    unidad.IdEstado = 5;
                    unidad.TxtEstado= "ANULADO";
                }
                await _context.SaveChangesAsync();


                return Json(new { redirectUrl = Url.Action("ListadoPrestamo", "Pret33Prestamo") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult VolverPrestamo()
        {
            try
            {

                return Json(new { redirectUrl = Url.Action("EditarPrestamo", "Pret33Prestamo", new { idprestamo = IdTemporal }) });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        //CRONOGRAMA

        public IActionResult Cronograma()
        {
            IdTemporalCrono = null;
            ViewBag.ListadoPers = _context.Pret34CronogramaPagos.Where(P => (P.IdEstado == 3 ||P.IdEstado == 7 || P.IdEstado == 8) && P.IdPrestamo==IdTemporal).ToList();
            return View();
        }
        public async Task<IActionResult> ListadoCrono()
        {
            var eagleContext = _context.Pret34CronogramaPagos.Include(p => p.IdPrestamoNavigation).Include(p => p.IdUsuarioNavigation).Include(p => p.IdUsuariomodNavigation).Where(p => p.IdPrestamo == IdTemporal);
            return View(await eagleContext.ToListAsync());
        }
        public IActionResult cargaprestamo()
        {
            decimal? amortizacion = 0;
            var prestamo = _context.Pret33Prestamos.Where(p => p.IdPrestamo == IdTemporal && (p.IdEstado == 6 || p.IdEstado == 3)).FirstOrDefault();
            var existingCronoPrestamo = _context.Pret34CronogramaPagos.Where(p => p.IdPrestamo == IdTemporal).Count();
            if (existingCronoPrestamo > 0)
            {
                amortizacion = prestamo.MontoPrestamo - prestamo.MontoCuota;
            }
            else
            {
                amortizacion = prestamo.MontoPrestamo - prestamo.MontoCuota;
            }

            var response = new { cuota = prestamo.MontoCuota, amortizacion = amortizacion };
            return Json(response);
        }

    }


}

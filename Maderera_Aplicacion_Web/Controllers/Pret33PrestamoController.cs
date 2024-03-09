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
        public IActionResult Prestamo()
        {
            IdTemporal = 0;
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
            IdTemporal = null;
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
                return View("Prestamo", prestamovista);
            }
            else
            {
                return NotFound(); // O redirigir a otra página
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
            var redirectUrl = Url.Action("ListadoPrestamo", "Pret33Prestamo");
            var response = new { redirectUrl };
            return Json(response);
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

        [HttpPost]
        public async Task<IActionResult> CrearPrestamo(int idempleado, int idtipoprestamo, int idtipoplazo, int idtipomotivo, decimal tea,
            string observacion, decimal montoprestamo, int plazo, decimal montocuota, int cuotasgracia, decimal comisiones, decimal tcea,
            DateTime Fechaaprob, DateTime Fechavto, DateTime Fechades, DateTime Fechaprimp,
            bool estado, bool cuotadoble, bool post)
        {
            try
            {
                var existingPrestamo = _context.Pret33Prestamos.AsNoTracking()
           .FirstOrDefault(c => c.IdPrestamo == IdTemporal);

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
                    existingPrestamo.MontoTotal = montoprestamo * (1 + (tcea / 100));
                    existingPrestamo.Plazo = plazo;
                    existingPrestamo.MontoCuota = montocuota;
                    existingPrestamo.NroCuotasGracia = cuotasgracia;
                    existingPrestamo.Comisiones = comisiones;
                    existingPrestamo.MontoTcea = tcea;//Preguntar al ingeniero si guardarlo entre 100 o como un decimal porcentuado
                    existingPrestamo.FechaAprobPrestamo = Fechaaprob;
                    existingPrestamo.FechaVtoProg = Fechavto;
                    existingPrestamo.FechaDesembolso = Fechades;
                    existingPrestamo.FechaPrimPago = Fechaprimp;
                    existingPrestamo.CuotaDoble = cuotadoble;
                    existingPrestamo.Posteo = post;
                    if (existingPrestamo.IdEstado == 3)
                    {
                        existingPrestamo.IdEstado = estado == true ? 3 : 6;
                        existingPrestamo.TxtEstado = estado == true ? "BORRADOR" : "PRESTADO";
                    }
                    existingPrestamo.TxtUsuariomod = txtusuario;
                    existingPrestamo.FechaModificacion = fechaHoy;
                    _context.Pret33Prestamos.Update(existingPrestamo);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingPrestamo.IdPrestamo;

                    await _context.SaveChangesAsync();
                }
                else
                {

                    var Pret33Prestamo = new Pret33Prestamo
                    {
                        IdEmpleado = idempleado,
                        IdAutorizador = _context.Pert01Usuarios.Where(p => p.IdUsuario == idusuario).Select(p => p.IdEmpleado).FirstOrDefault(),
                        IdTipoPrestamo = idtipoplazo,
                        IdTipoPlazo = idtipoplazo,
                        IdMotivo = idtipomotivo,
                        MontoTea = tea,
                        TxtObservacion = observacion,
                        MontoPrestamo = montoprestamo,
                        MontoTotal = montoprestamo * (1 + (tcea / 100)),
                        Plazo = plazo,
                        MontoCuota = montocuota,
                        NroCuotasGracia = cuotasgracia,
                        Comisiones = comisiones,
                        MontoTcea = tcea,
                        FechaAprobPrestamo = Fechaaprob,
                        FechaVtoProg = Fechavto,
                        FechaDesembolso = Fechades,
                        FechaPrimPago = Fechaprimp,
                        CuotaDoble = cuotadoble,
                        Posteo = post,
                        FechaCreacion = fechaHoy,
                        IdEstado = estado == true ? 3 : 6,
                        TxtEstado = estado == true ? "BORRADOR" : "PRESTADO",
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                    };

                    _context.Pret33Prestamos.Add(Pret33Prestamo);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret33Prestamo.IdPrestamo;



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
            string observacion, decimal montoprestamo, int plazo, decimal montocuota, int cuotasgracia, decimal comisiones, decimal tcea,
            DateTime Fechaaprob, DateTime Fechavto, DateTime Fechades, DateTime Fechaprimp,
            bool estado, bool cuotadoble, bool post)
        {
            try
            {

                var existingPrestamo = _context.Pret33Prestamos.AsNoTracking()
           .FirstOrDefault(c => c.IdPrestamo == IdTemporal);

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
                    existingPrestamo.MontoTotal = montoprestamo * (1 + (tcea / 100));
                    existingPrestamo.Plazo = plazo;
                    existingPrestamo.MontoCuota = montocuota;
                    existingPrestamo.NroCuotasGracia = cuotasgracia;
                    existingPrestamo.Comisiones = comisiones;
                    existingPrestamo.MontoTcea = tcea;
                    existingPrestamo.FechaAprobPrestamo = Fechaaprob;
                    existingPrestamo.FechaVtoProg = Fechavto;
                    existingPrestamo.FechaDesembolso = Fechades;
                    existingPrestamo.FechaPrimPago = Fechaprimp;
                    existingPrestamo.CuotaDoble = cuotadoble;
                    existingPrestamo.Posteo = post;
                    if (existingPrestamo.IdEstado == 3)
                    {
                        existingPrestamo.IdEstado = estado == true ? 3 : 6;
                        existingPrestamo.TxtEstado = estado == true ? "BORRADOR" : "PRESTADO";
                    }
                    existingPrestamo.TxtUsuariomod = txtusuario;
                    existingPrestamo.FechaModificacion = fechaHoy;
                    _context.Pret33Prestamos.Update(existingPrestamo);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingPrestamo.IdPrestamo;

                    await _context.SaveChangesAsync();
                }
                else
                {

                    var Pret33Prestamo = new Pret33Prestamo
                    {
                        IdEmpleado = idempleado,
                        IdAutorizador = _context.Pert01Usuarios.Where(p => p.IdUsuario == idusuario).Select(p => p.IdEmpleado).FirstOrDefault(),
                        IdTipoPrestamo = idtipoplazo,
                        IdTipoPlazo = idtipoplazo,
                        IdMotivo = idtipomotivo,
                        MontoTea = tea,
                        TxtObservacion = observacion,
                        MontoPrestamo = montoprestamo,
                        MontoTotal = montoprestamo * (1 + (tcea / 100)),
                        Plazo = plazo,
                        MontoCuota = montocuota,
                        NroCuotasGracia = cuotasgracia,
                        Comisiones = comisiones,
                        MontoTcea = tcea,
                        FechaAprobPrestamo = Fechaaprob,
                        FechaVtoProg = Fechavto,
                        FechaDesembolso = Fechades,
                        FechaPrimPago = Fechaprimp,
                        CuotaDoble = cuotadoble,
                        Posteo = post,
                        FechaCreacion = fechaHoy,
                        IdEstado = estado == true ? 3 : 6,
                        TxtEstado = estado == true ? "BORRADOR" : "PRESTADO",
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                    };

                    _context.Pret33Prestamos.Add(Pret33Prestamo);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret33Prestamo.IdPrestamo;



                }
                IdTemporal = null;
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
                var post = _context.Pret33Prestamos.Where(t => t.IdPrestamo == IdTemporal).Select(t => t.Posteo).FirstOrDefault();
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
        public async Task<IActionResult> AnularPrestamo(long id)
        {
            try
            {
                var existingprestamo = _context.Pret33Prestamos.Where(c => c.IdPrestamo== id).ToList();
                foreach (Pret33Prestamo prestamo in existingprestamo)
                {
                    prestamo.IdEstado = 5;
                    prestamo.TxtEstado = "ANULADO";
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

    }

}

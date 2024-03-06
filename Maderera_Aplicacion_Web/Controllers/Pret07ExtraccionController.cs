using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Maderera_Aplicacion_Web.Data.Interfaces;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using System.Linq.Expressions;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pret07ExtraccionController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;
        private readonly long idusuario;
        private readonly string txtusuario;
        private readonly DateTime fechaHoy;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static long? IdTemporal { get; set; }
        private static long? IdTemporalCampana { get; set; }
        private static int UmbralmaxNroarbol { get; set; }
        private static List<IdsCampanasSummary> ListadoCampanaTA { get; set; }
        private int? sumarreglo = 0;

        public Pret07ExtraccionController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult ListadoExtraccion()
        {

            ViewBag.ListadoExtraccion = _context.Pret07Extraccions.Include(p => p.IdCampanaNavigation).ThenInclude(p => p.IdPredioNavigation).Where(e => e.IdEstado == 1);
            return View();
        }
        [HttpGet]
        public IActionResult ObtenerProvincias(int departamentoId)
        {

            var provincias = _context.Sntt32Provincia
                .Where(p => p.IdDpto == departamentoId)
                .Select(p => new { idProv = p.IdProv, txtDesc = p.TxtDesc })
                .ToList();

            return Json(provincias);
        }

        [HttpGet]
        public IActionResult ObtenerDistritos(int provinciaId)
        {

            var distritos = _context.Sntt33Distritos
                .Where(d => d.IdProv == provinciaId)
                .Select(d => new { idDist = d.IdDist, txtDesc = d.TxtDesc })
                .ToList();

            return Json(distritos);
        }

        public async Task<IActionResult> Details(long? IdExtraccion)
        {

            var pret07Extraccion = await _context.Pret07Extraccions
                .Include(p => p.IdCampanaNavigation)
                .FirstOrDefaultAsync(m => m.IdExtraccion == IdExtraccion);

            return View(pret07Extraccion);
        }

        [HttpGet]

        public IActionResult Recargarcampext()
        {
            try
            {

                sumarreglo = ListadoCampanaTA == null ? 0 : ListadoCampanaTA.Sum(lc => lc.sumNroArboles);

                var updatedCampext = _context.Pret02Campanas
                    .Where(c => sumarreglo + _context.Pret08ExtraccionDtls
                        .Where(item =>
                            item.IdEstado == 1 &&
                            c.IdCampana == item.IdExtraccionNavigation.IdCampana &&
                            (IdTemporal == null || IdTemporal != item.IdExtraccion)
                        )
                        .AsEnumerable() // Forzar la evaluación en memoria
                        .Sum(item => item.NroArboles)
                    != c.NroArboles
                    )
                    .Select(c => new
                    {
                        id = c.IdCampana,
                        codigo = c.CodigoCampana,
                        nrositio = c.IdPredioNavigation.UnidadCatastral,
                        fechaini = c.FechaInicio
                    })
                    .ToList();



                return Json(updatedCampext);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar la Campan a Extraccion: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult SeleccionarCampExt(long? idtCampana)
        {
            try
            {
                IdTemporalCampana = idtCampana == null ? IdTemporalCampana : idtCampana;
                return Json(IdTemporalCampana); // Devuelve el valor como JSON
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la Campan a Extraccion: " + ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet]
        public IActionResult ObtenerCampanas([FromQuery] string? idsCampanasJson)
        {
            try
            {
                List<CampanhaArbol> idsCampanas = new List<CampanhaArbol>();

                if (!string.IsNullOrEmpty(idsCampanasJson))
                {
                    idsCampanas = JsonConvert.DeserializeObject<List<CampanhaArbol>>(idsCampanasJson);
                }

                ListadoCampanaTA = idsCampanas
                    .Where(id => id.idTipoArbol != null)
                    .GroupBy(id => id.idTipoArbol)
                    .Select(group => new IdsCampanasSummary
                    {
                        idTipoArbol = group.Key,
                        sumNroArboles = group.Sum(x => x.nroArboles)
                    })
                    .ToList();
                Recargarcampext();
                var campanas_tipo_Arbol = _context.Pret04CampanaTipoArbols
                    .Where(p => p.IdCampana == IdTemporalCampana && p.IdEstado == 1)
                    .Select(p => new { idta = p.IdTipoarbol, txtdesc = p.TxtTipoarbol, nroArboles = p.NroArboles })
                    .ToList();

                var campañasDisponibles = campanas_tipo_Arbol
                .Where(c =>
                    !ListadoCampanaTA.Any(id =>
                        id.idTipoArbol == c.idta &&
                        (id.sumNroArboles + _context.Pret08ExtraccionDtls
                            .Where(item =>
                                item.IdExtraccionNavigation.IdCampana == IdTemporalCampana &&
                                item.IdEstado == 1 && item.IdTipoArbol == c.idta &&
                                (IdTemporal == null || IdTemporal != item.IdExtraccion)
                            )
                            .Sum(item => item.NroArboles)
                        ) == c.nroArboles
                    )
                )
                .ToList();



                return Json(campañasDisponibles);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las campañas: " + ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }



        [HttpGet]
        public IActionResult CargarDatosCampana(long? taid)
        {
            try
            {
                var campanas_tipo_Arbol = _context.Pret04CampanaTipoArbols
                  .Where(p => p.IdTipoarbol == taid && p.IdCampana == IdTemporalCampana && p.IdEstado == 1)
                  .ToList()  // Trae los datos de la base de datos a memoria
                  .Select(p => new
                  {
                      txtdesc = p.TxtTipoarbol,
                      nroarbol = p.NroArboles - ((ListadoCampanaTA.Count > 0 ? (ListadoCampanaTA
                                      .Where(item => item.idTipoArbol == p.IdTipoarbol)
                                      .Sum(item => item.sumNroArboles)) : 0) + (_context.Pret08ExtraccionDtls
                                      .Where(item => item.IdTipoArbol == p.IdTipoarbol && item.IdExtraccionNavigation.IdCampana == IdTemporalCampana && item.IdEstado == 1)
                                      .Sum(item => item.NroArboles)))
                  })
                  .FirstOrDefault();
                if (IdTemporal != null)
                {
                    campanas_tipo_Arbol = _context.Pret04CampanaTipoArbols
                   .Where(p => p.IdTipoarbol == taid && p.IdCampana == IdTemporalCampana && p.IdEstado == 1)
                   .ToList()  // Trae los datos de la base de datos a memoria
                   .Select(p => new
                   {
                       txtdesc = p.TxtTipoarbol,
                       nroarbol = p.NroArboles - ((ListadoCampanaTA.Count > 0 ? (ListadoCampanaTA
                                       .Where(item => item.idTipoArbol == p.IdTipoarbol)
                                       .Sum(item => item.sumNroArboles)) : 0) + (_context.Pret08ExtraccionDtls
                                       .Where(item => item.IdTipoArbol == p.IdTipoarbol && item.IdExtraccion != IdTemporal && item.IdExtraccionNavigation.IdCampana == IdTemporalCampana && item.IdEstado == 1)
                                       .Sum(item => item.NroArboles)))
                   })
                   .FirstOrDefault();
                }
                UmbralmaxNroarbol = campanas_tipo_Arbol?.nroarbol ?? 0; // Si campanas_tipo_Arbol es null, asigna 0

                return Json(campanas_tipo_Arbol);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public string GenerarIdUnico()
        {
            Guid uniqueId = Guid.NewGuid();
            return uniqueId.ToString();
        }
        [HttpGet]
        public IActionResult Tipoarbol(int idtipoarbol)
        {
            try
            {
                var TipoArbol = _context.Pret06TipoArbols.FirstOrDefault(p => p.IdTipoarbol == idtipoarbol && p.IdEstado == 1);
                if (TipoArbol != null)
                {
                    var idunico = GenerarIdUnico();

                    var campanata = new
                    {
                        IdTipoArbol = TipoArbol.IdTipoarbol,
                        Nombretipoarbol = TipoArbol.Txtdesc,
                        IdUnico = idunico
                    };

                    return Json(campanata);
                }
                else
                {
                    return Json(null); // Producto no encontrado
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult removeIdTemporalCampana()
        {
            IdTemporalCampana = null;
            ListadoCampanaTA = null;
            sumarreglo = null;
            return Json(new { success = true });
        }
        [HttpGet]
        public IActionResult CerrarCampanaExt()
        {
            var redirectUrl = Url.Action("ListadoExtraccion", "Pret07Extraccion");
            var response = new { redirectUrl };
            IdTemporalCampana = null;
            ListadoCampanaTA = null;
            sumarreglo = null;
            return Json(response);
        }
        [HttpGet]
        public IActionResult Recargarempext([FromQuery] List<long> listaIds)
        {
            try
            {
                List<long> ids = listaIds ?? new List<long>();

                var EmpleadosExtraccion = _context.Pert04Empleados
            .Include(e => e.IdNacionalidadNavigation)
            .Include(e => e.IdDistNavigation)
                .ThenInclude(d => d.IdProvNavigation)
                    .ThenInclude(p => p.IdDptoNavigation)
            .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdEstado == 1)
            .Where(e => !ids.Contains(e.IdEmpleado))  // Excluye los IDs proporcionados
            .Select(e => new
            {
                Idempleado = e.IdEmpleado,
                NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
            })
            .ToList();

                return Json(EmpleadosExtraccion);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el empleado de Extracción: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult CargarDatosEmpleado(long idEmpleado)
        {
            var campanas_tipo_Arbol = _context.Pert04Empleados
                .Where(p => p.IdEmpleado == idEmpleado && p.IdEstado == 1)
                .Select(e => new
                {
                    txtnombre = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                    cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                    condicion = e.IdCondicionLaboralNavigation.TxtDesc,
                    nrodoc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc,
                    telefono = e.Celular1
                })
                .ToList().FirstOrDefault();

            return Json(campanas_tipo_Arbol);


        }
        [HttpGet]
        public IActionResult EmpleadoExt(long empleadoID)
        {
            var campanas_tipo_Arbol = _context.Pert04Empleados
                .Where(p => p.IdEmpleado == empleadoID && p.IdEstado == 1)
                .Select(e => new
                {
                    txtnombre = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                    cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                    condicion = e.IdCondicionLaboralNavigation.TxtDesc,
                    nrodoc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc,
                    telefono = e.Celular1
                })
                .ToList().FirstOrDefault();

            return Json(campanas_tipo_Arbol);


        }
        [HttpGet]
        public IActionResult Extraccion()
        {
            try
            {
                var sumarreglo = ListadoCampanaTA == null ? 0 : ListadoCampanaTA.Sum(lc => lc.sumNroArboles);

                ViewBag.CampanaExt = _context.Pret02Campanas
                    .Where(c => sumarreglo + _context.Pret08ExtraccionDtls
                        .Where(item =>
                            item.IdEstado == 1 &&
                            c.IdCampana == item.IdExtraccionNavigation.IdCampana &&
                            (IdTemporal == null || IdTemporal != item.IdExtraccion)
                        )
                        .AsEnumerable() // Forzar la evaluación en memoria
                        .Sum(item => item.NroArboles)
                    != c.NroArboles
                    )
                    .Select(c => new
                    {
                        id = c.IdCampana,
                        codigo = c.CodigoCampana,
                        nrositio = c.IdPredioNavigation.UnidadCatastral,
                        fechaini = c.FechaInicio
                    })
                    .ToList();

                var empleadosextraccion = _context.Pert04Empleados
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
                        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                    .ToList();

                ViewBag.EmpExt = empleadosextraccion;
                IdTemporal = null;
                return View();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult EditarExtraccion(long idextraccion)
        {


            var sumarreglo = ListadoCampanaTA == null ? 0 : ListadoCampanaTA.Sum(lc => lc.sumNroArboles);

            ViewBag.CampanaExt = _context.Pret02Campanas
                .Where(c => sumarreglo + _context.Pret08ExtraccionDtls
                    .Where(item =>
                        item.IdEstado == 1 &&
                        c.IdCampana == item.IdExtraccionNavigation.IdCampana &&
                        (IdTemporal == null || IdTemporal != item.IdExtraccion)
                    )
                    .AsEnumerable() // Forzar la evaluación en memoria
                    .Sum(item => item.NroArboles)
                != c.NroArboles
                )
                .Select(c => new
                {
                    id = c.IdCampana,
                    codigo = c.CodigoCampana,
                    nrositio = c.IdPredioNavigation.UnidadCatastral,
                    fechaini = c.FechaInicio
                })
                .ToList();


            var empleadosextraccion = _context.Pert04Empleados
                .Include(e => e.IdNacionalidadNavigation)
                .Include(e => e.IdDistNavigation)
                    .ThenInclude(d => d.IdProvNavigation)
                        .ThenInclude(p => p.IdDptoNavigation)
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdEstado == 1)
                .Select(e => new
                {
                    Idempleado = e.IdEmpleado,
                    NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                    cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                    telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                    Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                    Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                })
                .ToList();

            ViewBag.EmpExt = empleadosextraccion;

            var extraccionvista = _context.Pret07Extraccions
            .Where(c => c.IdExtraccion == idextraccion && c.IdEstado == 1)
            .FirstOrDefault();



            var Extraccion = _context.Pret07Extraccions
            .Include(t => t.IdCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdExtraccion == idextraccion && c.IdEstado == 1)
            .Select(campanaTA => new
            {
                CampanaTA = campanaTA,
                codigo = campanaTA.IdCampanaNavigation.CodigoCampana,
            })
            .FirstOrDefault();

            if (Extraccion != null)
            {
                var detallesextraccion = _context.Pret08ExtraccionDtls
                .Where(detalle => detalle.IdExtraccion == idextraccion && detalle.IdEstado == 1)

                .ToList();
                var Empextraccion = _context.Pret19ExtraccionEmpleados
                .Where(detalle => detalle.IdExtraccion == idextraccion && detalle.IdEstado == 1)
                .Select(
                    n => new
                    {
                        idempleado = n.IdEmpleadoEx,
                        txtempleado = n.IdEmpleadoExNavigation.TxtPriNom == null ? n.IdEmpleadoExNavigation.TxtRznSocial : $"{n.IdEmpleadoExNavigation.TxtPriNom} {n.IdEmpleadoExNavigation.TxtApePat}",
                        nrodoc = String.IsNullOrEmpty(n.IdEmpleadoExNavigation.NroRuc) ? n.IdEmpleadoExNavigation.NroDoc : n.IdEmpleadoExNavigation.NroRuc,
                        condicion = n.IdEmpleadoExNavigation.IdCondicionLaboralNavigation.TxtDesc,
                        categoria = n.IdEmpleadoExNavigation.IdCategoriaEmpNavigation.TxtNombre,

                    })
                .ToList();
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(Extraccion, settings);
                var detallesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(detallesextraccion, settings);
                var empleadosSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Empextraccion, settings);

                IdTemporal = extraccionvista?.IdExtraccion;
                IdTemporalCampana = extraccionvista?.IdCampana;
                ViewBag.Extraccion = campanaserialized;
                ViewBag.Detalles = detallesSerialized;
                ViewBag.Empleados = empleadosSerialized;
                return View("Extraccion", extraccionvista);
            }
            else
            {
                return NotFound(); // O redirigir a otra página
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearExtraccion(DateTime FechaExtraccion, double altarbtot, int nroarboltot,
            int nrotrozastot, double diamtot, string? comentarioExt, string arregloextraccionTA, string arregloempleadosextraccion)
        {
            try
            {

                var campanatipoarbolseleccionadosext = JsonConvert.DeserializeObject<List<TAExtraccion>>(arregloextraccionTA);
                var empleadosseleccionadosext = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadosextraccion);

                var existingExtraccion = _context.Pret07Extraccions.AsNoTracking()
           .FirstOrDefault(c => c.IdExtraccion == IdTemporal);

                if (existingExtraccion != null)
                {
                    existingExtraccion.IdCampana = (long)IdTemporalCampana;
                    existingExtraccion.FechaExtraccion = FechaExtraccion;
                    existingExtraccion.AltArbolProTotal = altarbtot;
                    existingExtraccion.NroArbolesTotal = nroarboltot;
                    existingExtraccion.NroTrozosTotal = nrotrozastot;
                    existingExtraccion.DiamProTotal = diamtot;
                    existingExtraccion.Comentario = comentarioExt;
                    existingExtraccion.IdUsuarioModificador = idusuario;
                    existingExtraccion.TxtUsuarioModificador = txtusuario;
                    existingExtraccion.FechaModificacion = fechaHoy;
                    _context.Pret07Extraccions.Update(existingExtraccion);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingExtraccion.IdExtraccion;

                    var idcampanaTAExtEnArreglo = campanatipoarbolseleccionadosext.Select(p => p.idunico).ToList();


                    foreach (TAExtraccion campanaTAExt in campanatipoarbolseleccionadosext)
                    {
                        var existinExtDtl = _context.Pret08ExtraccionDtls
                           .FirstOrDefault(d => d.IdExtraccion == IdTemporal && d.CodigoExtraccion == campanaTAExt.idunico);

                        if (existinExtDtl != null)
                        {
                            existinExtDtl.IdTipoArbol = campanaTAExt.idTipoArbol;
                            existinExtDtl.TxtTipoArbol = campanaTAExt.nombreTipoarbol;
                            existinExtDtl.NroArboles = campanaTAExt.numarbol;
                            existinExtDtl.NroTrozos = campanaTAExt.nrotrozas;
                            existinExtDtl.DiamPro = campanaTAExt.diametro;
                            existinExtDtl.AltArbolPro = campanaTAExt.altura;
                            existinExtDtl.Comentario = campanaTAExt.comentario;
                            existinExtDtl.IdEstado = 1;
                            existinExtDtl.TxtEstado = "ACTIVO";

                            _context.Pret08ExtraccionDtls.Update(existinExtDtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    foreach (TAExtraccion campanaTA in campanatipoarbolseleccionadosext)
                    {
                        var existingExtraccionDtl = _context.Pret08ExtraccionDtls
                           .FirstOrDefault(d => d.IdExtraccion == IdTemporal && d.CodigoExtraccion == campanaTA.idunico);

                        if (existingExtraccionDtl == null)
                        {
                            var Pret08ExtDtll = new Pret08ExtraccionDtl
                            {
                                IdExtraccion = (long)(IdTemporal),
                                IdTipoArbol = campanaTA.idTipoArbol,
                                NroTrozos = campanaTA.nrotrozas,
                                NroArboles = campanaTA.numarbol,
                                Comentario = campanaTA.comentario,
                                DiamPro = campanaTA.diametro,
                                AltArbolPro = campanaTA.altura,
                                CodigoExtraccion = campanaTA.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret08ExtraccionDtls.Add(Pret08ExtDtll);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtdtll = _context.Pret08ExtraccionDtls
                        .Where(d => d.IdExtraccion == IdTemporal && !idcampanaTAExtEnArreglo.Contains(d.CodigoExtraccion))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtdtll)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();


                    var idempleadoExtEnArreglo = empleadosseleccionadosext.Select(p => p.idempleado).ToList();
                    foreach (EmpleadoExt empleadoExt in empleadosseleccionadosext)
                    {
                        var existinExtEmp = _context.Pret19ExtraccionEmpleados
                           .FirstOrDefault(d => d.IdExtraccion == IdTemporal && d.IdEmpleadoEx == empleadoExt.idempleado);

                        if (existinExtEmp != null)
                        {
                            existinExtEmp.IdEmpleadoEx = empleadoExt.idempleado;
                            existinExtEmp.IdEstado = 1;
                            existinExtEmp.TxtEstado = "ACTIVO";

                            _context.Pret19ExtraccionEmpleados.Update(existinExtEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadosext)
                    {
                        var existingExtraccionEmp = _context.Pret19ExtraccionEmpleados
                           .FirstOrDefault(d => d.IdExtraccion == IdTemporal && d.IdEmpleadoEx == EmpleadoExt.idempleado);

                        if (existingExtraccionEmp == null)
                        {
                            var Pret19ExtEmp = new Pret19ExtraccionEmpleado
                            {
                                IdExtraccion = (long)(IdTemporal),
                                IdEmpleadoEx = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret19ExtraccionEmpleados.Add(Pret19ExtEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret19ExtraccionEmpleados
                        .Where(d => d.IdExtraccion == IdTemporal && !idempleadoExtEnArreglo.Contains((long)d.IdEmpleadoEx))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtemp)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cantidad = _context.Pret07Extraccions.Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nroext = txt_serie + "-" + txt_numero;
                    var Pret07Extraccion = new Pret07Extraccion
                    {
                        IdCampana = (long)IdTemporalCampana,
                        FechaExtraccion = FechaExtraccion,
                        NroArbolesTotal = nroarboltot,
                        NroTrozosTotal = nrotrozastot,
                        DiamProTotal = diamtot,
                        AltArbolProTotal = altarbtot,
                        Comentario = comentarioExt,
                        TxtSerie = txt_serie,
                        TxtNumero = txt_numero,
                        NroExtraccion = nroext,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = 1,
                        TxtEstado = "ACTIVO",
                    };

                    _context.Pret07Extraccions.Add(Pret07Extraccion);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret07Extraccion.IdExtraccion;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        foreach (TAExtraccion campanaTAExt in campanatipoarbolseleccionadosext)
                        {

                            var CampanaDtlExt = new Pret08ExtraccionDtl
                            {
                                IdExtraccion = (long)(IdTemporal),
                                IdTipoArbol = campanaTAExt.idTipoArbol,
                                TxtTipoArbol = campanaTAExt.nombreTipoarbol,
                                NroArboles = campanaTAExt.numarbol,
                                NroTrozos = campanaTAExt.nrotrozas,
                                DiamPro = campanaTAExt.diametro,

                                AltArbolPro = campanaTAExt.altura,
                                Comentario = campanaTAExt.comentario,
                                CodigoExtraccion = campanaTAExt.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret08ExtraccionDtls.Add(CampanaDtlExt);
                            await _context.SaveChangesAsync();


                        }
                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadosext)
                        {

                            var EmpleadoExt = new Pret19ExtraccionEmpleado
                            {
                                IdExtraccion = (long)(IdTemporal),
                                IdEmpleadoEx = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret19ExtraccionEmpleados.Add(EmpleadoExt);
                            await _context.SaveChangesAsync();


                        }

                    }



                }
                return Json(new { mensaje = "Guardado correctamente" });

            }

            catch (Exception ex)
            {
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CrearExtraccionycerrar(DateTime FechaExtraccion, double altarbtot, int nroarboltot,
            int nrotrozastot, double diamtot, string? comentarioExt, string arregloextraccionTA, string arregloempleadosextraccion)
        {
            try
            {

                var campanatipoarbolseleccionadosext = JsonConvert.DeserializeObject<List<TAExtraccion>>(arregloextraccionTA);
                var empleadosseleccionadosext = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadosextraccion);

                var existingExtraccion = _context.Pret07Extraccions.AsNoTracking()
           .FirstOrDefault(c => c.IdExtraccion == IdTemporal);

                if (existingExtraccion != null)
                {
                    existingExtraccion.IdCampana = (long)IdTemporalCampana;
                    existingExtraccion.FechaExtraccion = FechaExtraccion;
                    existingExtraccion.AltArbolProTotal = altarbtot;
                    existingExtraccion.NroArbolesTotal = nroarboltot;
                    existingExtraccion.NroTrozosTotal = nrotrozastot;
                    existingExtraccion.DiamProTotal = diamtot;
                    existingExtraccion.Comentario = comentarioExt;
                    existingExtraccion.IdUsuarioModificador = idusuario;
                    existingExtraccion.TxtUsuarioModificador = txtusuario;
                    existingExtraccion.FechaModificacion = fechaHoy;
                    _context.Pret07Extraccions.Update(existingExtraccion);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingExtraccion.IdExtraccion;

                    var idcampanaTAExtEnArreglo = campanatipoarbolseleccionadosext.Select(p => p.idunico).ToList();


                    foreach (TAExtraccion campanaTAExt in campanatipoarbolseleccionadosext)
                    {
                        var existinExtDtl = _context.Pret08ExtraccionDtls
                           .FirstOrDefault(d => d.IdExtraccion == IdTemporal && d.CodigoExtraccion == campanaTAExt.idunico);

                        if (existinExtDtl != null)
                        {
                            existinExtDtl.IdTipoArbol = campanaTAExt.idTipoArbol;
                            existinExtDtl.TxtTipoArbol = campanaTAExt.nombreTipoarbol;
                            existinExtDtl.NroArboles = campanaTAExt.numarbol;
                            existinExtDtl.NroTrozos = campanaTAExt.nrotrozas;
                            existinExtDtl.DiamPro = campanaTAExt.diametro;
                            existinExtDtl.AltArbolPro = campanaTAExt.altura;
                            existinExtDtl.Comentario = campanaTAExt.comentario;
                            existinExtDtl.IdEstado = 1;
                            existinExtDtl.TxtEstado = "ACTIVO";

                            _context.Pret08ExtraccionDtls.Update(existinExtDtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    foreach (TAExtraccion campanaTA in campanatipoarbolseleccionadosext)
                    {
                        var existingExtraccionDtl = _context.Pret08ExtraccionDtls
                           .FirstOrDefault(d => d.IdExtraccion == IdTemporal && d.CodigoExtraccion == campanaTA.idunico);

                        if (existingExtraccionDtl == null)
                        {
                            var Pret08ExtDtll = new Pret08ExtraccionDtl
                            {
                                IdExtraccion = (long)(IdTemporal),
                                IdTipoArbol = campanaTA.idTipoArbol,
                                NroTrozos = campanaTA.nrotrozas,
                                NroArboles = campanaTA.numarbol,
                                Comentario = campanaTA.comentario,
                                DiamPro = campanaTA.diametro,
                                AltArbolPro = campanaTA.altura,
                                CodigoExtraccion = campanaTA.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret08ExtraccionDtls.Add(Pret08ExtDtll);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtdtll = _context.Pret08ExtraccionDtls
                        .Where(d => d.IdExtraccion == IdTemporal && !idcampanaTAExtEnArreglo.Contains(d.CodigoExtraccion))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtdtll)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();


                    var idempleadoExtEnArreglo = empleadosseleccionadosext.Select(p => p.idempleado).ToList();
                    foreach (EmpleadoExt empleadoExt in empleadosseleccionadosext)
                    {
                        var existinExtEmp = _context.Pret19ExtraccionEmpleados
                           .FirstOrDefault(d => d.IdExtraccion == IdTemporal && d.IdEmpleadoEx == empleadoExt.idempleado);

                        if (existinExtEmp != null)
                        {
                            existinExtEmp.IdEmpleadoEx = empleadoExt.idempleado;
                            existinExtEmp.IdEstado = 1;
                            existinExtEmp.TxtEstado = "ACTIVO";

                            _context.Pret19ExtraccionEmpleados.Update(existinExtEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadosext)
                    {
                        var existingExtraccionEmp = _context.Pret19ExtraccionEmpleados
                           .FirstOrDefault(d => d.IdExtraccion == IdTemporal && d.IdEmpleadoEx == EmpleadoExt.idempleado);

                        if (existingExtraccionEmp == null)
                        {
                            var Pret19ExtEmp = new Pret19ExtraccionEmpleado
                            {
                                IdExtraccion = (long)(IdTemporal),
                                IdEmpleadoEx = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret19ExtraccionEmpleados.Add(Pret19ExtEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret19ExtraccionEmpleados
                        .Where(d => d.IdExtraccion == IdTemporal && !idempleadoExtEnArreglo.Contains((long)d.IdEmpleadoEx))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtemp)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cantidad = _context.Pret07Extraccions.Where(e => e.IdEstado == 1).Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nroext = txt_serie + "-" + txt_numero;
                    var Pret07Extraccion = new Pret07Extraccion
                    {
                        IdCampana = (long)IdTemporalCampana,
                        FechaExtraccion = FechaExtraccion,
                        NroArbolesTotal = nroarboltot,
                        NroTrozosTotal = nrotrozastot,
                        DiamProTotal = diamtot,
                        AltArbolProTotal = altarbtot,
                        Comentario = comentarioExt,
                        TxtSerie = txt_serie,
                        TxtNumero = txt_numero,
                        NroExtraccion = nroext,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = 1,
                        TxtEstado = "ACTIVO",
                    };

                    _context.Pret07Extraccions.Add(Pret07Extraccion);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret07Extraccion.IdExtraccion;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        foreach (TAExtraccion campanaTAExt in campanatipoarbolseleccionadosext)
                        {

                            var CampanaDtlExt = new Pret08ExtraccionDtl
                            {
                                IdExtraccion = (long)(IdTemporal),
                                IdTipoArbol = campanaTAExt.idTipoArbol,
                                TxtTipoArbol = campanaTAExt.nombreTipoarbol,
                                NroArboles = campanaTAExt.numarbol,
                                NroTrozos = campanaTAExt.nrotrozas,
                                DiamPro = campanaTAExt.diametro,

                                AltArbolPro = campanaTAExt.altura,
                                Comentario = campanaTAExt.comentario,
                                CodigoExtraccion = campanaTAExt.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret08ExtraccionDtls.Add(CampanaDtlExt);
                            await _context.SaveChangesAsync();


                        }
                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadosext)
                        {

                            var EmpleadoExt = new Pret19ExtraccionEmpleado
                            {
                                IdExtraccion = (long)(IdTemporal),
                                IdEmpleadoEx = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret19ExtraccionEmpleados.Add(EmpleadoExt);
                            await _context.SaveChangesAsync();


                        }

                    }



                }
                IdTemporal = null;
                IdTemporalCampana = null;
                ListadoCampanaTA = null;
                return Json(new { redirectUrl = Url.Action("ListadoExtraccion", "Pret07Extraccion") });

            }

            catch (Exception ex)
            {
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }


        [HttpPost]
        public async Task<IActionResult> EliminarExtraccion(long id)
        {
            try
            {
                //ELIMINACION DE MERMA Y PRODUCCION

                var existingProduccion = _context.Pret14Produccions.Where(p => p.IdExtraccion == id).ToList();

                if (existingProduccion != null)
                {
                    foreach (Pret14Produccion produccion in existingProduccion)
                    {
                        long idprod = produccion.IdProduccion;
                        var existingMerma = _context.Pret16Mermas.Where(p => p.IdProduccion == idprod).ToList();
                        if (existingMerma != null)
                        {
                            foreach (Pret16Merma merma in existingMerma)
                            {
                                long idmerma = merma.IdMerma;
                                var existingEmpleadoMer = _context.Pret24MermaEmpleados.Where(p => p.IdMerma == idmerma).ToList();

                                if (existingEmpleadoMer != null)
                                {
                                    foreach (Pret24MermaEmpleado empleado in existingEmpleadoMer)
                                    {
                                        //empleado.IdEstado = 0;
                                        //empleado.TxtEstado = "INACTIVO";
                                        _context.Pret24MermaEmpleados.Remove(empleado);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                await _context.SaveChangesAsync();
                                var existingDtllMer = _context.Pret17MermaDtls.Where(p => p.IdMerma == idmerma).ToList();

                                if (existingDtllMer != null)
                                {
                                    foreach (Pret17MermaDtl Detalle in existingDtllMer)
                                    {
                                        //Detalle.IdEstado = 2;
                                        //Detalle.TxtEstado = "INACTIVO";
                                        _context.Pret17MermaDtls.Remove(Detalle);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                _context.Pret16Mermas.Remove(merma);
                                await _context.SaveChangesAsync();
                            }
                        }
                        await _context.SaveChangesAsync();
                        var existingEmpleado = _context.Pret20ProduccionEmpleados.Where(p => p.IdProduccion == idprod).ToList();
                        foreach (Pret20ProduccionEmpleado empleado in existingEmpleado)
                        {
                            //empleado.IdEstado = 0;
                            //empleado.TxtEstado = "INACTIVO";
                            _context.Pret20ProduccionEmpleados.Remove(empleado);
                            await _context.SaveChangesAsync();
                        }
                        await _context.SaveChangesAsync();
                        var existingDtll = _context.Pret15ProduccionDtls.Where(p => p.IdProduccion == idprod).ToList();

                        if (existingDtll != null)
                        {
                            foreach (Pret15ProduccionDtl Detalle in existingDtll)
                            {
                                //Detalle.IdEstado = 2;
                                //Detalle.TxtEstado = "INACTIVO";
                                _context.Pret15ProduccionDtls.Remove(Detalle);
                                await _context.SaveChangesAsync();
                            }
                        }
                        _context.Pret14Produccions.Remove(produccion);
                        await _context.SaveChangesAsync();
                    }
                }
                //ELIMINACIÓN DE ENVIO Y RECEPCIÓN
                var existingEnvio = _context.Pret10Envios.Where(p => p.IdExtraccion == id).ToList();
                if (existingEnvio != null)
                {
                    foreach (Pret10Envio envio in existingEnvio)
                    {
                        long idenv = envio.IdEnvio;
                        var existingRecepcion = _context.Pret11Recepcions.Where(p => p.IdEnvio == idenv).ToList();

                        if (existingRecepcion != null)
                        {
                            foreach (Pret11Recepcion recepcion in existingRecepcion)
                            {
                                long idrec = recepcion.IdRecepcion;
                                var existingArchivorec = _context.Pret17Archivos.Where(c => c.IdRecepcion == idrec).ToList();
                                if (existingArchivorec != null)
                                {
                                    foreach (Pret17Archivo archivoReg in existingArchivorec)
                                    {
                                        //archivoReg.IdEstado = 2;
                                        //archivoReg.TxtEstado = "INACTIVO";
                                        string rutaArchivoEliminar = Path.Combine(_webHostEnvironment.WebRootPath, archivoReg.RutaArchivo);

                                        if (System.IO.File.Exists(rutaArchivoEliminar))
                                        {
                                            System.IO.File.Delete(rutaArchivoEliminar);
                                        }
                                        _context.Pret17Archivos.Remove(archivoReg);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                var existingEmpleadorec = _context.Pret22RecepcionEmpleados.Where(p => p.IdRecepcion == idrec).ToList();


                                foreach (Pret22RecepcionEmpleado empleado in existingEmpleadorec)
                                {
                                    //empleado.IdEstado = 0;
                                    //empleado.TxtEstado = "INACTIVO";
                                    _context.Pret22RecepcionEmpleados.Remove(empleado);
                                    await _context.SaveChangesAsync();
                                }
                                await _context.SaveChangesAsync();
                                var existingDtllrec = _context.Pret12RecepcionDtls.Where(p => p.IdRecepcion == idrec).ToList();

                                if (existingDtllrec != null)
                                {
                                    foreach (Pret12RecepcionDtl Detalle in existingDtllrec)
                                    {
                                        //Detalle.IdEstado = 2;
                                        //Detalle.TxtEstado = "INACTIVO";
                                        _context.Pret12RecepcionDtls.Remove(Detalle);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                _context.Pret11Recepcions.Remove(recepcion);
                                await _context.SaveChangesAsync();
                            }
                        }
                        await _context.SaveChangesAsync();

                        var existingArchivo = _context.Pret17Archivos.Where(c => c.IdEnvio == idenv).ToList();
                        if (existingArchivo != null)
                        {
                            foreach (Pret17Archivo archivoReg in existingArchivo)
                            {
                                //archivoReg.IdEstado = 2;
                                //archivoReg.TxtEstado = "INACTIVO";
                                string rutaArchivoEliminar = Path.Combine(_webHostEnvironment.WebRootPath, archivoReg.RutaArchivo);

                                if (System.IO.File.Exists(rutaArchivoEliminar))
                                {
                                    System.IO.File.Delete(rutaArchivoEliminar);
                                }
                                _context.Pret17Archivos.Remove(archivoReg);
                                await _context.SaveChangesAsync();
                            }
                        }
                        var existingEmpleado = _context.Pret21EnvioEmpleados.Where(p => p.IdEnvio == idenv).ToList();


                        foreach (Pret21EnvioEmpleado empleado in existingEmpleado)
                        {
                            //empleado.IdEstado = 0;
                            //empleado.TxtEstado = "INACTIVO";
                            _context.Pret21EnvioEmpleados.Remove(empleado);
                            await _context.SaveChangesAsync();
                        }
                        await _context.SaveChangesAsync();
                        var existingDtll = _context.Pret13EnvioDtls.Where(p => p.IdEnvio == idenv).ToList();

                        if (existingDtll != null)
                        {
                            foreach (Pret13EnvioDtl Detalle in existingDtll)
                            {
                                //Detalle.IdEstado = 2;
                                //Detalle.TxtEstado = "INACTIVO";
                                _context.Pret13EnvioDtls.Remove(Detalle);
                                await _context.SaveChangesAsync();
                            }
                        }
                        _context.Pret10Envios.Remove(envio);
                        await _context.SaveChangesAsync();
                    }
                }

                await _context.SaveChangesAsync();
                //ELIMINAR EXTRACCIÓN
                var existingExtraccion = _context.Pret07Extraccions.FirstOrDefault(c => c.IdExtraccion == id);

                if (existingExtraccion != null)
                {


                    var existingExtraccionDtlls = _context.Pret08ExtraccionDtls.Where(c => c.IdExtraccion == id);

                    foreach (var detalle in existingExtraccionDtlls)
                    {
                        _context.Pret08ExtraccionDtls.Remove(detalle);
                    }

                    var existingExtraccionEmps = _context.Pret19ExtraccionEmpleados.Where(c => c.IdExtraccion == id);

                    foreach (var empleado in existingExtraccionEmps)
                    {
                        _context.Pret19ExtraccionEmpleados.Remove(empleado);
                    }
                    _context.Pret07Extraccions.Remove(existingExtraccion);

                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("ListadoExtraccion", "Pret07Extraccion") });
            }

            catch (Exception ex)
            {
                return Json(ex);
            }

        }
        [HttpGet]
        public ActionResult GetVarUmbralnroarbol()
        {
            return Content(UmbralmaxNroarbol.ToString(), "text/plain");
        }

    }
}
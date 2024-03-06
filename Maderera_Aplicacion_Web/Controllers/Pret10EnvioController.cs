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
using Newtonsoft.Json;
using Microsoft.AspNetCore.StaticFiles;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pret10EnvioController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;
        private readonly long idusuario;
        private readonly string txtusuario;
        private readonly DateTime fechaHoy;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static long? IdTemporal { get; set; }
        private static long? IdTemporalExtraccion { get; set; }
        private static int UmbralmaxNroarbol { get; set; }
        private int? sumarreglo = 0;
        private static List<IdsTrozasSummary> ListadoExtraccionTA { get; set; }
        public Pret10EnvioController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pret10Envio
        public IActionResult ListadoEnv()
        {
            var eagleContext = _context.Pret10Envios.Include(p => p.IdExtraccionNavigation).ThenInclude(p => p.IdCampanaNavigation).ThenInclude(p => p.IdPredioNavigation).Include(p => p.IdLocationNavigation).Include(p => p.IdLocationToNavigation).Include(p => p.IdUsuarioModificadorNavigation).Include(p => p.IdUsuarioNavigation)
                .Where(p => p.IdEstado != 2).ToList();
            return View(eagleContext);
        }

        // GET: Pret10Envio/Details/5
        public async Task<IActionResult> Details(long? IdEnvio)
        {

            var pret10Envio = await _context.Pret10Envios
                .Include(p => p.IdExtraccionNavigation)
                .Include(p => p.IdLocationNavigation)
                .Include(p => p.IdLocationToNavigation)
                .Include(p => p.IdUsuarioModificadorNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEnvio == IdEnvio);


            return View(pret10Envio);
        }
        // GET: Pret10Envio/Create
        public IActionResult Envio()
        {
            IdTemporal = null;
            IdTemporalExtraccion = null;
            var cantidad = _context.Pret10Envios.Count() + 1;
            var txt_serie = "001";
            var txt_numero = cantidad.ToString().PadLeft(7, '0');
            string nroenv = txt_serie + "-" + txt_numero;
            ViewBag.NroEnv = nroenv;
            var Location = _context.Mstt08Locations
           .Include(p => p.IdTipoLocationNavigation)
           .Include(e => e.IdDistNavigation)
               .ThenInclude(d => d.IdProvNavigation)
                   .ThenInclude(p => p.IdDptoNavigation)

           .Where(e => e.TxtDireccion1 != null && e.IdDist != null && e.TxtDireccion1 != "" && e.IdEstado == 1)
           .Select(e => new
           {
               IdLocation = e.IdLocation,
               NombreCompleto = e.TxtDesc,
               FechaN = e.FechaNegocio,
               Tipol = e.IdTipoLocationNavigation.TxtDesc,
               //Telefono = !string.IsNullOrEmpty(e.Fono1) ? e.Fono1: (!string.IsNullOrEmpty(e.Fono2) ? e.Fono2: ""),
               Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
               Ruc = e.NroRuc
           })
           .ToList();

            ViewBag.Location = Location;
            ViewBag.Locationto = Location;
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
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                    .ToList();

            ViewBag.EmpExt = empleadosextraccion;
            var sumarreglo = ListadoExtraccionTA == null ? 0 : ListadoExtraccionTA.Sum(lc => lc.sumNroTrozas);
            ViewBag.Extraccionenv = _context.Pret07Extraccions
                   .Where(c => !_context.Pret10Envios
            .Any(envio =>
                envio.IdExtraccion == c.IdExtraccion &&
                envio.IdEstado == 3
            ) && sumarreglo + _context.Pret13EnvioDtls
                       .Where(item =>
                           item.IdEstado == 1 &&
                           c.IdExtraccion == item.IdEnvioNavigation.IdExtraccion &&
                           (IdTemporal == null || IdTemporal != item.IdEnvio)
                       )
                       .AsEnumerable() // Forzar la evaluación en memoria
                       .Sum(item => item.NroTrozos)
                   != c.NroTrozosTotal
                   )
                   .Select(c => new
                   {
                       id = c.IdExtraccion,
                       codigo = c.NroExtraccion,
                       nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                       fechaini = c.FechaExtraccion
                   })
                   .ToList();

            return View();
        }
        [HttpGet]
        public IActionResult SeleccionarExtEnv(long? idExtraccion)
        {
            try
            {

                IdTemporalExtraccion = idExtraccion == null ? IdTemporalExtraccion : idExtraccion;
                var CodCampana = _context.Pret07Extraccions.Where(e => e.IdExtraccion == IdTemporalExtraccion).Select(e => e.IdCampanaNavigation.CodigoCampana).FirstOrDefault();
                var UnCatastral = _context.Pret07Extraccions.Where(e => e.IdExtraccion == IdTemporalExtraccion).Select(e => e.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral).FirstOrDefault();
                var response = new
                {
                    CodCampana,
                    UnCatastral
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la Envio del envío: " + ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }
        [HttpGet]
        public IActionResult ObtenerExtraccion([FromQuery] string? idsExtraccionesJson)
        {
            try
            {
                List<trozasarbol> idsCampanas = new List<trozasarbol>();

                if (!string.IsNullOrEmpty(idsExtraccionesJson))
                {
                    idsCampanas = JsonConvert.DeserializeObject<List<trozasarbol>>(idsExtraccionesJson);
                }

                ListadoExtraccionTA = idsCampanas
                    .Where(id => id.idTipoArbol != null)
                    .GroupBy(id => id.idTipoArbol)
                    .Select(group => new IdsTrozasSummary
                    {
                        idTipoArbol = group.Key,
                        sumNroTrozas = group.Sum(x => x.nroTrozas)
                    })
                    .ToList();
                Recargarextenv();
                var campanas_tipo_Arbol = _context.Pret08ExtraccionDtls
                    .Where(p => p.IdExtraccion == IdTemporalExtraccion && p.IdEstado == 1)
                    .Select(p => new { idta = p.IdTipoArbol, txtdesc = p.TxtTipoArbol, nrotrozas = p.NroTrozos })
                    .ToList();

                var campañasDisponibles = campanas_tipo_Arbol
                .Where(c =>
                    !ListadoExtraccionTA.Any(id =>
                        id.idTipoArbol == c.idta &&
                        (id.sumNroTrozas + _context.Pret13EnvioDtls
                            .Where(item =>
                                item.IdEnvioNavigation.IdExtraccion == IdTemporalExtraccion &&
                                item.IdEstado == 1 && item.IdTipoArbol == c.idta &&
                                (IdTemporal == null || IdTemporal != item.IdEnvio)
                            )
                            .Sum(item => item.NroTrozos)
                        ) == c.nrotrozas
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
        public IActionResult CargarDatosExtraccion(long? taid)
        {
            try
            {
                var campanas_tipo_Arbol = _context.Pret08ExtraccionDtls
                  .Where(p => p.IdTipoArbol == taid && p.IdExtraccion == IdTemporalExtraccion && p.IdEstado == 1)
                  .ToList()  // Trae los datos de la base de datos a memoria
                  .Select(p => new
                  {
                      txtdesc = p.TxtTipoArbol,
                      nrotrozas = p.NroTrozos - ((ListadoExtraccionTA.Count > 0 ? (ListadoExtraccionTA
                                      .Where(item => item.idTipoArbol == p.IdTipoArbol)
                                      .Sum(item => item.sumNroTrozas)) : 0) + (_context.Pret13EnvioDtls
                                      .Where(item => item.IdTipoArbol == p.IdTipoArbol && item.IdEnvioNavigation.IdExtraccion == IdTemporalExtraccion && item.IdEstado == 1)
                                      .Sum(item => item.NroTrozos)))
                  })
                  .FirstOrDefault();
                if (IdTemporal != null)
                {
                    campanas_tipo_Arbol = _context.Pret08ExtraccionDtls
                   .Where(p => p.IdTipoArbol == taid && p.IdExtraccion == IdTemporalExtraccion && p.IdEstado == 1)
                   .ToList()  // Trae los datos de la base de datos a memoria
                   .Select(p => new
                   {
                       txtdesc = p.TxtTipoArbol,
                       nrotrozas = p.NroTrozos - ((ListadoExtraccionTA.Count > 0 ? (ListadoExtraccionTA
                                       .Where(item => item.idTipoArbol == p.IdTipoArbol)
                                       .Sum(item => item.sumNroTrozas)) : 0) + (_context.Pret13EnvioDtls
                                       .Where(item => item.IdTipoArbol == p.IdTipoArbol && item.IdEnvio != IdTemporal && item.IdEnvioNavigation.IdExtraccion == IdTemporalExtraccion && item.IdEstado == 1)
                                       .Sum(item => item.NroTrozos)))
                   })
                   .FirstOrDefault();
                }
                UmbralmaxNroarbol = campanas_tipo_Arbol?.nrotrozas ?? 0; // Si campanas_tipo_Arbol es null, asigna 0

                return Json(campanas_tipo_Arbol);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult removeIdTemporalExtraccion()
        {
            IdTemporalExtraccion = null;
            ListadoExtraccionTA = null;
            sumarreglo = null;
            return Json(new { success = true });
        }
        [HttpGet]
        public IActionResult CerrarExtraccionEnv()
        {
            var redirectUrl = Url.Action("ListadoEnv", "Pret10Envio");
            var response = new { redirectUrl };
            IdTemporal = null;
            IdTemporalExtraccion = null;
            ListadoExtraccionTA = null;
            sumarreglo = null;
            return Json(response);
        }
        [HttpGet]

        public IActionResult Recargarextenv()
        {
            try
            {

                var sumarreglo = ListadoExtraccionTA == null ? 0 : ListadoExtraccionTA.Sum(lc => lc.sumNroTrozas);
                var updatedExtenv = _context.Pret07Extraccions
                   .Where(c => !_context.Pret10Envios
            .Any(envio =>
                envio.IdExtraccion == c.IdExtraccion && envio.IdExtraccion == IdTemporalExtraccion &&
                envio.IdEstado == 3
            ) && sumarreglo + _context.Pret13EnvioDtls
                       .Where(item =>
                           item.IdEstado == 1 &&
                           c.IdExtraccion == item.IdEnvioNavigation.IdExtraccion &&
                           (IdTemporal == null || IdTemporal != item.IdEnvio)
                       )
                       .AsEnumerable() // Forzar la evaluación en memoria
                       .Sum(item => item.NroTrozos)
                   != c.NroTrozosTotal
                   )
                   .Select(c => new
                   {
                       id = c.IdExtraccion,
                       codigo = c.NroExtraccion,
                       nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                       fechaini = c.FechaExtraccion
                   })
                   .ToList();


                return Json(updatedExtenv);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar la Campan a Envio: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult RecargarLocalizacion(int? id)
        {
            try
            {
                Console.WriteLine($"ID Recibido: {id}");

                if (id != null)
                {
                    var UpdateLocation = _context.Mstt08Locations
                        .Include(p => p.IdTipoLocationNavigation)
                        .Include(e => e.IdDistNavigation)
                            .ThenInclude(d => d.IdProvNavigation)
                                .ThenInclude(p => p.IdDptoNavigation)

                        .Where(e => e.TxtDireccion1 != null && e.IdDist != null && e.TxtDireccion1 != "" && e.IdLocation != id && e.IdEstado == 1)
                        .Select(e => new
                        {
                            IdLocation = e.IdLocation,
                            NombreCompleto = e.TxtDesc,
                            FechaN = e.FechaNegocio,
                            Tipol = e.IdTipoLocationNavigation.TxtDesc,
                            Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                            Ruc = e.NroRuc
                        })
                        .ToList();

                    Console.WriteLine($"Datos Recibidos para ID: {id}");
                    // Retorna una respuesta exitosa
                    return Json(UpdateLocation);
                }
                else
                {
                    var UpdateLocation = _context.Mstt08Locations
                        .Include(p => p.IdTipoLocationNavigation)
                        .Include(e => e.IdDistNavigation)
                            .ThenInclude(d => d.IdProvNavigation)
                                .ThenInclude(p => p.IdDptoNavigation)

                    .Where(e => e.TxtDireccion1 != null && e.IdDist != null && e.TxtDireccion1 != "" && e.IdEstado == 1)
                    .Select(e => new
                    {
                        IdLocation = e.IdLocation,
                        NombreCompleto = e.TxtDesc,
                        FechaN = e.FechaNegocio,
                        Tipol = e.IdTipoLocationNavigation.TxtDesc,
                        //Telefono = !string.IsNullOrEmpty(e.Fono1) ? e.Fono1: (!string.IsNullOrEmpty(e.Fono2) ? e.Fono2: ""),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        Ruc = e.NroRuc
                    })
                    .ToList();

                    Console.WriteLine("Datos Recibidos para Todas las Localizaciones");
                    // Retorna una respuesta exitosa
                    return Json(UpdateLocation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el autorizador: {ex.Message}");
                return BadRequest("Error al actualizar el autorizador: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Recargarempenv([FromQuery] List<long> listaIds)
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
                Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
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
                    telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3)
                })
                .ToList().FirstOrDefault();

            return Json(campanas_tipo_Arbol);


        }
        [HttpGet]
        public IActionResult EmpleadoEnv(long empleadoID)
        {
            var campanas_tipo_Arbol = _context.Pert04Empleados
                .Where(p => p.IdEmpleado == empleadoID && p.IdEstado == 1)
                .Select(e => new
                {
                    txtnombre = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                    cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                    condicion = e.IdCondicionLaboralNavigation.TxtDesc,
                    nrodoc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc,
                    telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3)
                })
                .ToList().FirstOrDefault();

            return Json(campanas_tipo_Arbol);


        }
        [HttpGet]
        public IActionResult EditarEnvio(long idenvio)
        {
            var Envio = _context.Pret10Envios
            .Include(t => t.IdExtraccionNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdEnvio == idenvio && c.IdEstado != 2 && c.IdEstado != 5)
            .Select(campanaTA => new
            {
                all = campanaTA,
                CampanaTA = campanaTA,
                codigo = campanaTA.IdExtraccionNavigation.NroExtraccion,
                location = campanaTA.IdLocationNavigation.TxtDesc,
                locationto = campanaTA.IdLocationToNavigation.TxtDesc,
            })
            .FirstOrDefault();
            ViewBag.CodCam = _context.Pret10Envios.Include(e => e.IdExtraccionNavigation).ThenInclude(e => e.IdCampanaNavigation).ThenInclude(e => e.IdPredioNavigation).Where(e => e.IdEnvio == idenvio).Select(e => e.IdExtraccionNavigation.IdCampanaNavigation.CodigoCampana).FirstOrDefault();
            ViewBag.UnidCat = _context.Pret10Envios.Include(e => e.IdExtraccionNavigation).ThenInclude(e => e.IdCampanaNavigation).ThenInclude(e => e.IdPredioNavigation).Where(e => e.IdEnvio == idenvio).Select(e => e.IdExtraccionNavigation.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral).FirstOrDefault();

            var sumarreglo = ListadoExtraccionTA == null ? 0 : ListadoExtraccionTA.Sum(lc => lc.sumNroTrozas);
            ViewBag.Extraccionenv = _context.Pret07Extraccions
                   .Where(c => !_context.Pret10Envios
            .Any(envio =>
                envio.IdExtraccion == c.IdExtraccion && envio.IdExtraccion != Envio.all.IdExtraccion &&
                envio.IdEstado == 3
            ) && sumarreglo + _context.Pret13EnvioDtls
                       .Where(item =>
                           item.IdEstado == 1 &&
                           c.IdExtraccion == item.IdEnvioNavigation.IdExtraccion &&
                           (IdTemporal == null || IdTemporal != item.IdEnvio)
                       )
                       .AsEnumerable() // Forzar la evaluación en memoria
                       .Sum(item => item.NroTrozos)
                   != c.NroTrozosTotal
                   )
                   .Select(c => new
                   {
                       id = c.IdExtraccion,
                       codigo = c.NroExtraccion,
                       nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                       fechaini = c.FechaExtraccion
                   })
                   .ToList();
            var Location = _context.Mstt08Locations
               .Include(p => p.IdTipoLocationNavigation)
               .Include(e => e.IdDistNavigation)
                   .ThenInclude(d => d.IdProvNavigation)
                       .ThenInclude(p => p.IdDptoNavigation)

               .Where(e => e.TxtDireccion1 != null && e.IdDist != null && e.TxtDireccion1 != "" && e.IdEstado == 1)
               .Select(e => new
               {
                   IdLocation = e.IdLocation,
                   NombreCompleto = e.TxtDesc,
                   FechaN = e.FechaNegocio,
                   Tipol = e.IdTipoLocationNavigation.TxtDesc,
                   //Telefono = !string.IsNullOrEmpty(e.Fono1) ? e.Fono1: (!string.IsNullOrEmpty(e.Fono2) ? e.Fono2: ""),
                   Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                   Ruc = e.NroRuc
               })
               .ToList();

            ViewBag.Location = Location;
            ViewBag.Locationto = Location;
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
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                    .ToList();

            ViewBag.EmpExt = empleadosextraccion;
            var enviovista = _context.Pret10Envios
            .Where(c => c.IdEnvio == idenvio && c.IdEstado != 2 && c.IdEstado != 5)
            .FirstOrDefault();
            if (Envio != null)
            {
                var archivos = _context.Pret17Archivos
               .Where(a => a.IdEstado == 1 && a.IdEnvio == idenvio && a.IdPredio == null)
               .ToList();

                var proveedorTipoContenido = new FileExtensionContentTypeProvider();

                var archivosParaVista = archivos.Select(archivo =>
                {
                    // Obtener la ruta física del archivo directamente desde la base de datos
                    string rutaFisica = archivo.RutaArchivo;

                    byte[] fileBytes = System.IO.File.ReadAllBytes(rutaFisica);

                    // Obtén el nombre del archivo
                    string fileName = archivo.NombreArchivo;

                    // Obtén el tipo de contenido basándote en la extensión del archivo
                    string contentType;
                    if (proveedorTipoContenido.TryGetContentType(fileName, out contentType))
                    {
                        // Se encontró el tipo de contenido basándote en la extensión
                        contentType = contentType ?? "application/octet-stream"; // Asegurar que no sea nulo
                    }
                    else
                    {
                        // Tipo de contenido predeterminado si no se encuentra la extensión
                        contentType = "application/octet-stream";
                    }

                    // Devuelve el archivo al cliente
                    return File(fileBytes, contentType, fileName);
                }).ToList();

                var detallesenvio = _context.Pret13EnvioDtls
                .Where(detalle => detalle.IdEnvio == idenvio && detalle.IdEstado == 1)

                .ToList();
                var Empenvio = _context.Pret21EnvioEmpleados
                .Where(detalle => detalle.IdEnvio == idenvio && detalle.IdEstado == 1)
                .Select(
                    n => new
                    {
                        idempleado = n.IdEmpleadoEnv,
                        txtempleado = n.IdEmpleadoEnvNavigation.TxtPriNom == null ? n.IdEmpleadoEnvNavigation.TxtRznSocial : $"{n.IdEmpleadoEnvNavigation.TxtPriNom} {n.IdEmpleadoEnvNavigation.TxtApePat}",
                        nrodoc = String.IsNullOrEmpty(n.IdEmpleadoEnvNavigation.NroRuc) ? n.IdEmpleadoEnvNavigation.NroDoc : n.IdEmpleadoEnvNavigation.NroRuc,
                        condicion = n.IdEmpleadoEnvNavigation.IdCondicionLaboralNavigation.TxtDesc,
                        telefono = !string.IsNullOrEmpty(n.IdEmpleadoEnvNavigation.Celular1) ? n.IdEmpleadoEnvNavigation.Celular1 : (!string.IsNullOrEmpty(n.IdEmpleadoEnvNavigation.Celular2) ? n.IdEmpleadoEnvNavigation.Celular2 : n.IdEmpleadoEnvNavigation.Celular3),
                        categoria = n.IdEmpleadoEnvNavigation.IdCategoriaEmpNavigation.TxtNombre,

                    })
                .ToList();
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(Envio, settings);
                var detallesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(detallesenvio, settings);
                var empleadosSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Empenvio, settings);
                var archivosserialized = Newtonsoft.Json.JsonConvert.SerializeObject(archivosParaVista, settings);

                IdTemporal = enviovista?.IdEnvio;
                IdTemporalExtraccion = enviovista?.IdExtraccion;
                ViewBag.Envio = campanaserialized;
                ViewBag.Detalles = detallesSerialized;
                ViewBag.Empleados = empleadosSerialized;
                ViewBag.ArchivosParaVista = archivosParaVista;
                return View("Envio", enviovista);
            }
            else
            {
                return NotFound(); // O redirigir a otra página
            }
        }
        [HttpGet]
        public ActionResult GetVarUmbralnroarbolenv()
        {
            return Content(UmbralmaxNroarbol.ToString(), "text/plain");
        }


        [HttpGet]
        private bool Pret10EnvioExists(long id)
        {
            return (_context.Pret10Envios?.Any(e => e.IdEnvio == id)).GetValueOrDefault();
        }
        [HttpPost]
        public async Task<IActionResult> CrearEnvio(DateTime FechaEnvio, int idlocation, int idlocationto,
        string? nroplaca, string? nroguia, string? nroguiat, string? comentarioEnv, string arregloenvioTA,
        string arregloempleadosenvio, List<IFormFile> archivos, string tipoEnv, bool check)
        {
            try
            {
                var campanatipoarbolseleccionadosenv = JsonConvert.DeserializeObject<List<TAEnvio>>(arregloenvioTA);
                var empleadosseleccionadosenv = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadosenvio);
                int sumaNroTrozas = campanatipoarbolseleccionadosenv.Sum(e => e.nrotrozas);
                var existingEnvio = _context.Pret10Envios.AsNoTracking()
           .FirstOrDefault(c => c.IdEnvio == IdTemporal);

                if (existingEnvio != null)
                {

                    existingEnvio.IdExtraccion = (long)IdTemporalExtraccion;
                    existingEnvio.FechaEnvio = FechaEnvio;
                    existingEnvio.Comentario = comentarioEnv;
                    existingEnvio.NroPlaca = nroplaca;
                    existingEnvio.NroGuia = nroguia;
                    existingEnvio.NroGuiaTransp = nroguiat;
                    existingEnvio.IdLocation = idlocation;
                    existingEnvio.IdLocationTo = idlocationto;
                    existingEnvio.TipoEnvio = tipoEnv;
                    existingEnvio.IdUsuarioModificador = idusuario;
                    if (existingEnvio.IdEstado == 3)
                    {
                        existingEnvio.IdEstado = check == true ? 3 : 4;
                        existingEnvio.TxtEstado = check == true ? "BORRADOR" : "ENVIADO";
                    }
                    existingEnvio.TxtUsuarioModificador = txtusuario;
                    existingEnvio.FechaModificacion = fechaHoy;
                    existingEnvio.EnvioCant = sumaNroTrozas;
                    existingEnvio.IdTipoComp = 63;
                    _context.Pret10Envios.Update(existingEnvio);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingEnvio.IdEnvio;

                    var idcampanaTAEnvEnArreglo = campanatipoarbolseleccionadosenv.Select(p => p.idunico).ToList();

                    //DETALLE
                    //ACTUALIZAR
                    foreach (TAEnvio campanaTAEnv in campanatipoarbolseleccionadosenv)
                    {
                        var existinEnvDtl = _context.Pret13EnvioDtls
                           .FirstOrDefault(d => d.IdEnvio == IdTemporal && d.CodigoExtraccion == campanaTAEnv.idunico);

                        if (existinEnvDtl != null)
                        {
                            existinEnvDtl.IdTipoArbol = campanaTAEnv.idTipoArbol;
                            existinEnvDtl.TxtTipoArbol = campanaTAEnv.nombreTipoarbol;
                            existinEnvDtl.NroTrozos = campanaTAEnv.nrotrozas;
                            existinEnvDtl.DiamPro = campanaTAEnv.diametro;
                            existinEnvDtl.AltArbolPro = campanaTAEnv.altura;
                            existinEnvDtl.Comentario = campanaTAEnv.comentario;
                            existinEnvDtl.IdEstado = 1;
                            existinEnvDtl.TxtEstado = "ACTIVO";

                            _context.Pret13EnvioDtls.Update(existinEnvDtl);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (TAEnvio campanaTA in campanatipoarbolseleccionadosenv)
                    {
                        var existingEnvDtl = _context.Pret13EnvioDtls
                           .FirstOrDefault(d => d.IdEnvio == IdTemporal && d.CodigoExtraccion == campanaTA.idunico);

                        if (existingEnvDtl == null)
                        {
                            var Pret13EnvDtl = new Pret13EnvioDtl
                            {
                                IdEnvio = (long)(IdTemporal),
                                IdTipoArbol = campanaTA.idTipoArbol,
                                NroTrozos = campanaTA.nrotrozas,
                                Comentario = campanaTA.comentario,
                                DiamPro = campanaTA.diametro,
                                AltArbolPro = campanaTA.altura,
                                CodigoExtraccion = campanaTA.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret13EnvioDtls.Add(Pret13EnvDtl);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CAMBIAR ESTADO
                    var registrosParaActualizarEnvDtl = _context.Pret13EnvioDtls
                        .Where(d => d.IdEnvio == IdTemporal && !idcampanaTAEnvEnArreglo.Contains(d.CodigoExtraccion))
                        .ToList();

                    foreach (var registro in registrosParaActualizarEnvDtl)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();

                    //EMPLEADO
                    //ACTUALIZAR
                    var idempleadoExtEnArreglo = empleadosseleccionadosenv.Select(p => p.idempleado).ToList();
                    foreach (EmpleadoExt empleadoEnv in empleadosseleccionadosenv)
                    {
                        var existinEnvEmp = _context.Pret21EnvioEmpleados
                           .FirstOrDefault(d => d.IdEnvio == IdTemporal && d.IdEmpleadoEnv == empleadoEnv.idempleado);

                        if (existinEnvEmp != null)
                        {
                            existinEnvEmp.IdEmpleadoEnv = empleadoEnv.idempleado;
                            existinEnvEmp.IdEstado = 1;
                            existinEnvEmp.TxtEstado = "ACTIVO";

                            _context.Pret21EnvioEmpleados.Update(existinEnvEmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    var idarchivosenArreglo = archivos.Select(p => p.FileName).ToList();
                    //CREAR
                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadosenv)
                    {
                        var existingEnvioEmp = _context.Pret21EnvioEmpleados
                           .FirstOrDefault(d => d.IdEnvio == IdTemporal && d.IdEmpleadoEnv == EmpleadoExt.idempleado);

                        if (existingEnvioEmp == null)
                        {
                            var Pret21EnvEmp = new Pret21EnvioEmpleado
                            {
                                IdEnvio = (long)(IdTemporal),
                                IdEmpleadoEnv = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret21EnvioEmpleados.Add(Pret21EnvEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret21EnvioEmpleados
                        .Where(d => d.IdEnvio == IdTemporal && !idempleadoExtEnArreglo.Contains((long)d.IdEmpleadoEnv))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtemp)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    //Archivo
                    //Agregar
                    foreach (var archivo in archivos)
                    {
                        var existingArchivo = _context.Pret17Archivos
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdEnvio == IdTemporal);
                        if (existingArchivo == null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "envio", IdTemporal.ToString());


                                // Verificar si la carpeta no existe y, en caso afirmativo, crearla
                                if (!Directory.Exists(rutaAlmacenamiento))
                                {
                                    Directory.CreateDirectory(rutaAlmacenamiento);
                                }

                                // Crear un nombre único para el archivo
                                var nombreArchivo = archivo.FileName;

                                // Construir la ruta completa del archivo
                                var rutaCompleta = Path.Combine(rutaAlmacenamiento, nombreArchivo);

                                // Guardar el archivo en la ruta
                                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                                {
                                    await archivo.CopyToAsync(stream);
                                }
                                var pret17Archivo = new Pret17Archivo
                                {
                                    NombreArchivo = archivo.FileName,
                                    RutaArchivo = rutaCompleta,
                                    TipoArchivo = archivo.ContentType,
                                    TamanoArchivo = (int)archivo.Length,
                                    FechaCargaArchivo = DateTime.Now,
                                    IdEnvio = existingEnvio.IdEnvio,
                                    IdTipoDir = 3,
                                    IdEstado = 1,
                                    TxtEstado = "ACTIVO",
                                };

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Add(pret17Archivo);
                                await _context.SaveChangesAsync();

                            }
                        }
                    }
                    //Actualizar
                    foreach (var archivo in archivos)
                    {
                        var existingArchivo = _context.Pret17Archivos
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdEnvio == IdTemporal);
                        if (existingArchivo != null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "envio", IdTemporal.ToString());


                                // Verificar si la carpeta no existe y, en caso afirmativo, crearla
                                if (!Directory.Exists(rutaAlmacenamiento))
                                {
                                    Directory.CreateDirectory(rutaAlmacenamiento);
                                }

                                // Crear un nombre único para el archivo
                                var nombreArchivo = archivo.FileName;

                                // Construir la ruta completa del archivo
                                var rutaCompleta = Path.Combine(rutaAlmacenamiento, nombreArchivo);

                                // Guardar el archivo en la ruta
                                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                                {
                                    await archivo.CopyToAsync(stream);
                                }

                                existingArchivo.NombreArchivo = archivo.FileName;
                                existingArchivo.RutaArchivo = rutaCompleta;
                                existingArchivo.TipoArchivo = archivo.ContentType;
                                existingArchivo.TamanoArchivo = (int)archivo.Length;
                                existingArchivo.FechaCargaArchivo = DateTime.Now;
                                existingArchivo.IdEstado = 1;
                                existingArchivo.TxtEstado = "ACTIVO";
                                existingArchivo.IdEnvio = existingEnvio.IdEnvio;
                                existingArchivo.IdTipoDir = 3;

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Update(existingArchivo);
                                await _context.SaveChangesAsync();

                            }
                        }
                    }

                    var registrosParaActualizar = _context.Pret17Archivos
                       .Where(d => d.IdEnvio == IdTemporal && !idarchivosenArreglo.Contains(d.NombreArchivo))
                       .ToList();
                    //Actuaizar Estado
                    foreach (var registro in registrosParaActualizar)
                    {
                        // Actualizar el campo IdEstado y TxtEstado para marcar como inactivo
                        //registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        //registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                        string rutaArchivoEliminar = Path.Combine(_webHostEnvironment.WebRootPath, registro.RutaArchivo);

                        if (System.IO.File.Exists(rutaArchivoEliminar))
                        {
                            System.IO.File.Delete(rutaArchivoEliminar);
                        }
                        _context.Pret17Archivos.Remove(registro);
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cantidad = _context.Pret10Envios./*Where(e => e.IdEstado != 5 && e.IdEstado != 2).*/Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nroenv = txt_serie + "-" + txt_numero;
                    var Pret10Envio = new Pret10Envio
                    {
                        IdExtraccion = (long)IdTemporalExtraccion,
                        FechaEnvio = FechaEnvio,
                        Comentario = comentarioEnv,
                        TxtSerie = txt_serie,
                        TxtNro = txt_numero,
                        NroGuia = nroguia,
                        NroPlaca = nroplaca,
                        NroGuiaTransp = nroguiat,
                        IdLocation = idlocation,
                        IdLocationTo = idlocationto,
                        NroEnvio = nroenv,
                        TipoEnvio = tipoEnv,
                        IdTipoComp = 63,
                        EnvioCant = sumaNroTrozas,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check == true ? 3 : 4,
                        TxtEstado = check == true ? "BORRADOR" : "ENVIADO",
                    };

                    _context.Pret10Envios.Add(Pret10Envio);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret10Envio.IdEnvio;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        foreach (TAEnvio campanaTAExt in campanatipoarbolseleccionadosenv)
                        {

                            var EnvioDtl = new Pret13EnvioDtl
                            {
                                IdEnvio = (long)(IdTemporal),
                                IdTipoArbol = campanaTAExt.idTipoArbol,
                                TxtTipoArbol = campanaTAExt.nombreTipoarbol,
                                NroTrozos = campanaTAExt.nrotrozas,
                                DiamPro = campanaTAExt.diametro,

                                AltArbolPro = campanaTAExt.altura,
                                Comentario = campanaTAExt.comentario,
                                CodigoExtraccion = campanaTAExt.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret13EnvioDtls.Add(EnvioDtl);
                            await _context.SaveChangesAsync();


                        }
                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadosenv)
                        {

                            var EmpleadoEnv = new Pret21EnvioEmpleado
                            {
                                IdEnvio = (long)(IdTemporal),
                                IdEmpleadoEnv = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret21EnvioEmpleados.Add(EmpleadoEnv);
                            await _context.SaveChangesAsync();


                        }
                        foreach (var archivo in archivos)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "envio", IdTemporal.ToString());


                                // Verificar si la carpeta no existe y, en caso afirmativo, crearla
                                if (!Directory.Exists(rutaAlmacenamiento))
                                {
                                    Directory.CreateDirectory(rutaAlmacenamiento);
                                }


                                // Crear un nombre único para el archivo
                                var nombreArchivo = archivo.FileName;

                                // Construir la ruta completa del archivo
                                var rutaCompleta = Path.Combine(rutaAlmacenamiento, nombreArchivo);

                                // Guardar el archivo en la ruta
                                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                                {
                                    await archivo.CopyToAsync(stream);
                                }
                                var pret17Archivo = new Pret17Archivo
                                {
                                    NombreArchivo = archivo.FileName,
                                    RutaArchivo = rutaCompleta,
                                    TipoArchivo = archivo.ContentType,
                                    TamanoArchivo = (int)archivo.Length,
                                    FechaCargaArchivo = DateTime.Now,
                                    IdEnvio = Pret10Envio.IdEnvio,
                                    IdTipoDir = 3,
                                    IdEstado = 1,
                                    TxtEstado = "ACTIVO",
                                };

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Add(pret17Archivo);
                                await _context.SaveChangesAsync();

                            }
                        }

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
        public async Task<IActionResult> CrearEnvioycerrar(DateTime FechaEnvio, int idlocation, int idlocationto,
        string? nroplaca, string? nroguia, string? nroguiat, string? comentarioEnv, string arregloenvioTA,
        string arregloempleadosenvio, List<IFormFile> archivos, string tipoEnv, bool check)
        {
            try
            {

                var campanatipoarbolseleccionadosenv = JsonConvert.DeserializeObject<List<TAEnvio>>(arregloenvioTA);
                var empleadosseleccionadosenv = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadosenvio);
                int sumaNroTrozas = campanatipoarbolseleccionadosenv.Sum(e => e.nrotrozas);
                var existingEnvio = _context.Pret10Envios.AsNoTracking()
           .FirstOrDefault(c => c.IdEnvio == IdTemporal);

                if (existingEnvio != null)
                {

                    existingEnvio.IdExtraccion = (long)IdTemporalExtraccion;
                    existingEnvio.FechaEnvio = FechaEnvio;
                    existingEnvio.Comentario = comentarioEnv;
                    existingEnvio.NroPlaca = nroplaca;
                    existingEnvio.NroGuia = nroguia;
                    existingEnvio.NroGuiaTransp = nroguiat;
                    existingEnvio.IdLocation = idlocation;
                    existingEnvio.IdLocationTo = idlocationto;
                    existingEnvio.TipoEnvio = tipoEnv;
                    existingEnvio.IdUsuarioModificador = idusuario;
                    if (existingEnvio.IdEstado == 3)
                    {
                        existingEnvio.IdEstado = check == true ? 3 : 4;
                        existingEnvio.TxtEstado = check == true ? "BORRADOR" : "ENVIADO";
                    }
                    existingEnvio.TxtUsuarioModificador = txtusuario;
                    existingEnvio.FechaModificacion = fechaHoy;
                    existingEnvio.EnvioCant = sumaNroTrozas;
                    existingEnvio.IdTipoComp = 63;
                    _context.Pret10Envios.Update(existingEnvio);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingEnvio.IdEnvio;

                    var idcampanaTAEnvEnArreglo = campanatipoarbolseleccionadosenv.Select(p => p.idunico).ToList();

                    //DETALLE
                    //ACTUALIZAR
                    foreach (TAEnvio campanaTAEnv in campanatipoarbolseleccionadosenv)
                    {
                        var existinEnvDtl = _context.Pret13EnvioDtls
                           .FirstOrDefault(d => d.IdEnvio == IdTemporal && d.CodigoExtraccion == campanaTAEnv.idunico);

                        if (existinEnvDtl != null)
                        {
                            existinEnvDtl.IdTipoArbol = campanaTAEnv.idTipoArbol;
                            existinEnvDtl.TxtTipoArbol = campanaTAEnv.nombreTipoarbol;
                            existinEnvDtl.NroTrozos = campanaTAEnv.nrotrozas;
                            existinEnvDtl.DiamPro = campanaTAEnv.diametro;
                            existinEnvDtl.AltArbolPro = campanaTAEnv.altura;
                            existinEnvDtl.Comentario = campanaTAEnv.comentario;
                            existinEnvDtl.IdEstado = 1;
                            existinEnvDtl.TxtEstado = "ACTIVO";

                            _context.Pret13EnvioDtls.Update(existinEnvDtl);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (TAEnvio campanaTA in campanatipoarbolseleccionadosenv)
                    {
                        var existingEnvDtl = _context.Pret13EnvioDtls
                           .FirstOrDefault(d => d.IdEnvio == IdTemporal && d.CodigoExtraccion == campanaTA.idunico);

                        if (existingEnvDtl == null)
                        {
                            var Pret13EnvDtl = new Pret13EnvioDtl
                            {
                                IdEnvio = (long)(IdTemporal),
                                IdTipoArbol = campanaTA.idTipoArbol,
                                NroTrozos = campanaTA.nrotrozas,
                                Comentario = campanaTA.comentario,
                                DiamPro = campanaTA.diametro,
                                AltArbolPro = campanaTA.altura,
                                CodigoExtraccion = campanaTA.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret13EnvioDtls.Add(Pret13EnvDtl);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CAMBIAR ESTADO
                    var registrosParaActualizarEnvDtl = _context.Pret13EnvioDtls
                        .Where(d => d.IdEnvio == IdTemporal && !idcampanaTAEnvEnArreglo.Contains(d.CodigoExtraccion))
                        .ToList();

                    foreach (var registro in registrosParaActualizarEnvDtl)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();

                    //EMPLEADO
                    //ACTUALIZAR
                    var idempleadoExtEnArreglo = empleadosseleccionadosenv.Select(p => p.idempleado).ToList();
                    foreach (EmpleadoExt empleadoEnv in empleadosseleccionadosenv)
                    {
                        var existinEnvEmp = _context.Pret21EnvioEmpleados
                           .FirstOrDefault(d => d.IdEnvio == IdTemporal && d.IdEmpleadoEnv == empleadoEnv.idempleado);

                        if (existinEnvEmp != null)
                        {
                            existinEnvEmp.IdEmpleadoEnv = empleadoEnv.idempleado;
                            existinEnvEmp.IdEstado = 1;
                            existinEnvEmp.TxtEstado = "ACTIVO";

                            _context.Pret21EnvioEmpleados.Update(existinEnvEmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    var idarchivosenArreglo = archivos.Select(p => p.FileName).ToList();
                    //CREAR
                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadosenv)
                    {
                        var existingEnvioEmp = _context.Pret21EnvioEmpleados
                           .FirstOrDefault(d => d.IdEnvio == IdTemporal && d.IdEmpleadoEnv == EmpleadoExt.idempleado);

                        if (existingEnvioEmp == null)
                        {
                            var Pret21EnvEmp = new Pret21EnvioEmpleado
                            {
                                IdEnvio = (long)(IdTemporal),
                                IdEmpleadoEnv = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret21EnvioEmpleados.Add(Pret21EnvEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret21EnvioEmpleados
                        .Where(d => d.IdEnvio == IdTemporal && !idempleadoExtEnArreglo.Contains((long)d.IdEmpleadoEnv))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtemp)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }


                    //Agregar
                    foreach (var archivo in archivos)
                    {
                        var existingArchivo = _context.Pret17Archivos
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdEnvio == IdTemporal);
                        if (existingArchivo == null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "envio", IdTemporal.ToString());


                                // Verificar si la carpeta no existe y, en caso afirmativo, crearla
                                if (!Directory.Exists(rutaAlmacenamiento))
                                {
                                    Directory.CreateDirectory(rutaAlmacenamiento);
                                }

                                // Crear un nombre único para el archivo
                                var nombreArchivo = archivo.FileName;

                                // Construir la ruta completa del archivo
                                var rutaCompleta = Path.Combine(rutaAlmacenamiento, nombreArchivo);

                                // Guardar el archivo en la ruta
                                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                                {
                                    await archivo.CopyToAsync(stream);
                                }
                                var pret17Archivo = new Pret17Archivo
                                {
                                    NombreArchivo = archivo.FileName,
                                    RutaArchivo = rutaCompleta,
                                    TipoArchivo = archivo.ContentType,
                                    TamanoArchivo = (int)archivo.Length,
                                    FechaCargaArchivo = DateTime.Now,
                                    IdEnvio = existingEnvio.IdEnvio,
                                    IdTipoDir = 3,
                                    IdEstado = 1,
                                    TxtEstado = "ACTIVO",
                                };

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Add(pret17Archivo);
                                await _context.SaveChangesAsync();

                            }
                        }
                    }
                    //Actualizar
                    foreach (var archivo in archivos)
                    {
                        var existingArchivo = _context.Pret17Archivos
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdEnvio == IdTemporal);
                        if (existingArchivo != null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "envio", IdTemporal.ToString());


                                // Verificar si la carpeta no existe y, en caso afirmativo, crearla
                                if (!Directory.Exists(rutaAlmacenamiento))
                                {
                                    Directory.CreateDirectory(rutaAlmacenamiento);
                                }

                                // Crear un nombre único para el archivo
                                var nombreArchivo = archivo.FileName;

                                // Construir la ruta completa del archivo
                                var rutaCompleta = Path.Combine(rutaAlmacenamiento, nombreArchivo);

                                // Guardar el archivo en la ruta
                                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                                {
                                    await archivo.CopyToAsync(stream);
                                }

                                existingArchivo.NombreArchivo = archivo.FileName;
                                existingArchivo.RutaArchivo = rutaCompleta;
                                existingArchivo.TipoArchivo = archivo.ContentType;
                                existingArchivo.TamanoArchivo = (int)archivo.Length;
                                existingArchivo.FechaCargaArchivo = DateTime.Now;
                                existingArchivo.IdEstado = 1;
                                existingArchivo.TxtEstado = "ACTIVO";
                                existingArchivo.IdEnvio = existingEnvio.IdEnvio;
                                existingArchivo.IdTipoDir = 3;

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Update(existingArchivo);
                                await _context.SaveChangesAsync();

                            }
                        }
                    }

                    var registrosParaActualizar = _context.Pret17Archivos
                       .Where(d => d.IdEnvio == IdTemporal && !idarchivosenArreglo.Contains(d.NombreArchivo))
                       .ToList();
                    //Actuaizar Estado
                    foreach (var registro in registrosParaActualizar)
                    {
                        // Actualizar el campo IdEstado y TxtEstado para marcar como inactivo
                        //registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        //registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                        string rutaArchivoEliminar = Path.Combine(_webHostEnvironment.WebRootPath, registro.RutaArchivo);

                        if (System.IO.File.Exists(rutaArchivoEliminar))
                        {
                            System.IO.File.Delete(rutaArchivoEliminar);
                        }
                        _context.Pret17Archivos.Remove(registro);
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cantidad = _context.Pret10Envios./*Where(e => e.IdEstado != 5 && e.IdEstado != 2).*/Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nroenv = txt_serie + "-" + txt_numero;
                    var Pret10Envio = new Pret10Envio
                    {
                        IdExtraccion = (long)IdTemporalExtraccion,
                        FechaEnvio = FechaEnvio,
                        Comentario = comentarioEnv,
                        TxtSerie = txt_serie,
                        TxtNro = txt_numero,
                        NroGuia = nroguia,
                        NroPlaca = nroplaca,
                        NroGuiaTransp = nroguiat,
                        IdLocation = idlocation,
                        IdLocationTo = idlocationto,
                        NroEnvio = nroenv,
                        TipoEnvio = tipoEnv,
                        IdTipoComp = 63,
                        EnvioCant = sumaNroTrozas,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check == true ? 3 : 4,
                        TxtEstado = check == true ? "BORRADOR" : "ENVIADO",
                    };

                    _context.Pret10Envios.Add(Pret10Envio);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret10Envio.IdEnvio;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        foreach (TAEnvio campanaTAExt in campanatipoarbolseleccionadosenv)
                        {

                            var EnvioDtl = new Pret13EnvioDtl
                            {
                                IdEnvio = (long)(IdTemporal),
                                IdTipoArbol = campanaTAExt.idTipoArbol,
                                TxtTipoArbol = campanaTAExt.nombreTipoarbol,
                                NroTrozos = campanaTAExt.nrotrozas,
                                DiamPro = campanaTAExt.diametro,

                                AltArbolPro = campanaTAExt.altura,
                                Comentario = campanaTAExt.comentario,
                                CodigoExtraccion = campanaTAExt.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret13EnvioDtls.Add(EnvioDtl);
                            await _context.SaveChangesAsync();


                        }
                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadosenv)
                        {

                            var EmpleadoEnv = new Pret21EnvioEmpleado
                            {
                                IdEnvio = (long)(IdTemporal),
                                IdEmpleadoEnv = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret21EnvioEmpleados.Add(EmpleadoEnv);
                            await _context.SaveChangesAsync();


                        }
                        foreach (var archivo in archivos)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "envio", IdTemporal.ToString());


                                // Verificar si la carpeta no existe y, en caso afirmativo, crearla
                                if (!Directory.Exists(rutaAlmacenamiento))
                                {
                                    Directory.CreateDirectory(rutaAlmacenamiento);
                                }


                                // Crear un nombre único para el archivo
                                var nombreArchivo = archivo.FileName;

                                // Construir la ruta completa del archivo
                                var rutaCompleta = Path.Combine(rutaAlmacenamiento, nombreArchivo);

                                // Guardar el archivo en la ruta
                                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                                {
                                    await archivo.CopyToAsync(stream);
                                }
                                var pret17Archivo = new Pret17Archivo
                                {
                                    NombreArchivo = archivo.FileName,
                                    RutaArchivo = rutaCompleta,
                                    TipoArchivo = archivo.ContentType,
                                    TamanoArchivo = (int)archivo.Length,
                                    FechaCargaArchivo = DateTime.Now,
                                    IdEnvio = Pret10Envio.IdEnvio,
                                    IdTipoDir = 3,
                                    IdEstado = 1,
                                    TxtEstado = "ACTIVO",
                                };

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Add(pret17Archivo);
                                await _context.SaveChangesAsync();

                            }
                        }

                    }



                }
                IdTemporal = null;
                IdTemporalExtraccion = null;
                ListadoExtraccionTA = null;
                return Json(new { redirectUrl = Url.Action("ListadoEnv", "Pret10Envio") });

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
                var estado = _context.Pret10Envios.Where(t => t.IdEnvio == IdTemporal).Select(t => t.IdEstado).FirstOrDefault();
                var estadoName = _context.Pret10Envios.Where(t => t.IdEnvio == IdTemporal).Select(t => t.TxtEstado).FirstOrDefault();
                var response = new
                {
                    id = estado,
                    name = estadoName
                };
                return Json(response);

            }
            else
            {
                return Json(new { success = true });

            }



        }
        [HttpPost]
        public async Task<IActionResult> EliminarEnvio(long id)
        {
            try
            {
                var existingEnvio = _context.Pret10Envios.Where(p => p.IdEnvio == id).FirstOrDefault();

                if (existingEnvio != null)
                {
                    var existingRecepcion = _context.Pret11Recepcions.Where(p => p.IdEnvio == id).ToList();

                    if (existingRecepcion != null)
                    {
                        foreach (Pret11Recepcion recepcion in existingRecepcion)
                        {
                            long idrec = recepcion.IdRecepcion;
                            var existingArchivoREC = _context.Pret17Archivos.Where(c => c.IdRecepcion == idrec).ToList();
                            if (existingArchivoREC != null)
                            {
                                foreach (Pret17Archivo archivoReg in existingArchivoREC)
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
                            var existingEmpleadoREC = _context.Pret22RecepcionEmpleados.Where(p => p.IdRecepcion == idrec).ToList();

                            if (existingEmpleadoREC != null)
                            {
                                foreach (Pret22RecepcionEmpleado empleado in existingEmpleadoREC)
                                {
                                    //empleado.IdEstado = 0;
                                    //empleado.TxtEstado = "INACTIVO";
                                    _context.Pret22RecepcionEmpleados.Remove(empleado);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            await _context.SaveChangesAsync();
                            var existingDtllREC = _context.Pret12RecepcionDtls.Where(p => p.IdRecepcion == idrec).ToList();

                            if (existingDtllREC != null)
                            {
                                foreach (Pret12RecepcionDtl Detalle in existingDtllREC)
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
                    var existingArchivo = _context.Pret17Archivos.Where(c => c.IdEnvio == id).ToList();
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
                    var existingEmpleado = _context.Pret21EnvioEmpleados.Where(p => p.IdEnvio == id).ToList();


                    foreach (Pret21EnvioEmpleado empleado in existingEmpleado)
                    {
                        //empleado.IdEstado = 0;
                        //empleado.TxtEstado = "INACTIVO";
                        _context.Pret21EnvioEmpleados.Remove(empleado);
                        await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                    var existingDtll = _context.Pret13EnvioDtls.Where(p => p.IdEnvio == id).ToList();

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
                    _context.Pret10Envios.Remove(existingEnvio);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("ListadoEnv", "Pret10Envio") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AnularEnvio(long id)
        {
            try
            {
                var existingRecepcion = _context.Pret11Recepcions.Where(p => p.IdEnvio == id).ToList();

                if (existingRecepcion != null)
                {
                    foreach (Pret11Recepcion recepcion in existingRecepcion)
                    {
                        long idrec = recepcion.IdRecepcion;
                        var existingArchivo = _context.Pret17Archivos.Where(c => c.IdRecepcion == idrec).ToList();
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
                        var existingEmpleado = _context.Pret22RecepcionEmpleados.Where(p => p.IdRecepcion == idrec).ToList();


                        foreach (Pret22RecepcionEmpleado empleado in existingEmpleado)
                        {
                            empleado.IdEstado = 0;
                            empleado.TxtEstado = "INACTIVO";
                            _context.Pret22RecepcionEmpleados.Update(empleado);
                            await _context.SaveChangesAsync();
                        }
                        await _context.SaveChangesAsync();
                        var existingDtll = _context.Pret12RecepcionDtls.Where(p => p.IdRecepcion == idrec).ToList();

                        if (existingDtll != null)
                        {
                            foreach (Pret12RecepcionDtl Detalle in existingDtll)
                            {
                                Detalle.IdEstado = 2;
                                Detalle.TxtEstado = "INACTIVO";
                                _context.Pret12RecepcionDtls.Update(Detalle);
                                await _context.SaveChangesAsync();
                            }
                        }
                        recepcion.IdEstado = 5;
                        recepcion.TxtEstado = "ANULADO";

                        _context.Pret11Recepcions.Update(recepcion);
                        await _context.SaveChangesAsync();
                    }
                }
                await _context.SaveChangesAsync();


                var existingEnvio = _context.Pret10Envios.Where(p => p.IdEnvio == id).FirstOrDefault();

                if (existingEnvio != null)
                {
                    existingEnvio.IdEstado = 5;
                    existingEnvio.TxtEstado = "ANULADO";
                    var existingArchivo = _context.Pret17Archivos.Where(c => c.IdEnvio == id).ToList();
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
                    var existingEmpleado = _context.Pret21EnvioEmpleados.Where(p => p.IdEstado == 1 && p.IdEnvio == id).ToList();


                    foreach (Pret21EnvioEmpleado empleado in existingEmpleado)
                    {
                        empleado.IdEstado = 2;
                        empleado.TxtEstado = "INACTIVO";
                        //_context.Pret21EnvioEmpleados.Remove(empleado);
                        //await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                    var existingDtll = _context.Pret13EnvioDtls.Where(p => p.IdEstado == 1 && p.IdEnvio == id).ToList();

                    if (existingDtll != null)
                    {
                        foreach (Pret13EnvioDtl Detalle in existingDtll)
                        {
                            Detalle.IdEstado = 2;
                            Detalle.TxtEstado = "INACTIVO";
                            //_context.Pret13EnvioDtls.Remove(Detalle);
                            //await _context.SaveChangesAsync();
                        }
                    }
                    //_context.Pret10Envios.Remove(existingEnvio);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("ListadoEnv", "Pret10Envio") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }




    }
}

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
    public class Pret11RecepcionController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;
        private readonly long idusuario;
        private readonly string txtusuario;
        private readonly DateTime fechaHoy;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static long? IdTemporal { get; set; }
        private static long? IdTemporalEnvio { get; set; }
        private static int UmbralmaxNroarbol { get; set; }
        private int? sumarreglo = 0;
        private static List<IdsTrozasSummary> ListadoExtraccionTA { get; set; }
        public Pret11RecepcionController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pret11Recepcion
        public IActionResult ListadoRec()
        {
            var eagleContext = _context.Pret11Recepcions.Include(p => p.IdEnvioNavigation).Include(p => p.IdUsuarioModificadorNavigation).Include(p => p.IdUsuarioNavigation)
                .ToList();
            return View(eagleContext);
        }

        // GET: Pret11Recepcion/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Pret11Recepcions == null)
            {
                return NotFound();
            }

            var pret11Recepcion = await _context.Pret11Recepcions
                .Include(p => p.IdEnvioNavigation)
                .Include(p => p.IdUsuarioModificadorNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdRecepcion == id);
            if (pret11Recepcion == null)
            {
                return NotFound();
            }

            return View(pret11Recepcion);
        }

        // GET: Pret11Recepcion/Create
        [HttpGet]
        public IActionResult CerrarRecepcion()
        {
            var redirectUrl = Url.Action("ListadoRec", "Pret11Recepcion");
            var response = new { redirectUrl };
            return Json(response);
        }
        public IActionResult Recepcion()
        {
            IdTemporal = null;
            IdTemporalEnvio = null;
            var cantidad = _context.Pret11Recepcions.Count() + 1;
            var txt_serie = "001";
            var txt_numero = cantidad.ToString().PadLeft(7, '0');
            string nrorec = txt_serie + "-" + txt_numero;
            ViewBag.NroRec = nrorec;

            var empleadosrecepcion = _context.Pert04Empleados
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

            ViewBag.EmpRec = empleadosrecepcion;

            var sumarreglo = ListadoExtraccionTA == null ? 0 : ListadoExtraccionTA.Sum(lc => lc.sumNroTrozas);

            ViewBag.EnvioRecepcion = _context.Pret10Envios
                   .Where(c => c.IdEstado == 4 && sumarreglo + _context.Pret12RecepcionDtls
                       .Where(item =>
                           item.IdEstado == 1 &&
                           c.IdExtraccion == item.IdRecepcionNavigation.IdEnvio &&
                           (IdTemporal == null || IdTemporal != item.IdRecepcion)
                       )
                       .AsEnumerable() // Forzar la evaluación en memoria
                       .Sum(item => item.NroTrozos)
                   != c.EnvioCant
                   )
                   .Select(c => new
                   {
                       id = c.IdEnvio,
                       codigo = c.NroEnvio,
                       nrositio = c.IdExtraccionNavigation.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                       fechaini = c.FechaEnvio
                   })
                   .ToList();

            return View();
        }

        [HttpGet]
        public IActionResult EditarRecepcion(long idrecepcion)
        {
            var Recepcion = _context.Pret11Recepcions
            .Include(t => t.IdEnvioNavigation).ThenInclude(t => t.IdExtraccionNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdRecepcion == idrecepcion && c.IdEstado != 2 && c.IdEstado != 5)
            .Select(campanaTA => new
            {
                CampanaTA = campanaTA,
                codigo = campanaTA.IdEnvioNavigation.NroEnvio,
                location = campanaTA.IdEnvioNavigation.IdLocationNavigation.TxtDesc,
                locationto = campanaTA.IdEnvioNavigation.IdLocationToNavigation.TxtDesc,
                idEnvio = campanaTA.IdEnvio
            })
            .FirstOrDefault();

            ViewBag.CodCam = _context.Pret11Recepcions.Include(e => e.IdEnvioNavigation).ThenInclude(e => e.IdExtraccionNavigation).ThenInclude(e => e.IdCampanaNavigation).ThenInclude(e => e.IdPredioNavigation).Where(e => e.IdEnvio == idrecepcion).Select(e => e.IdEnvioNavigation.IdExtraccionNavigation.IdCampanaNavigation.CodigoCampana).FirstOrDefault();
            ViewBag.UnidCat = _context.Pret11Recepcions.Include(e => e.IdEnvioNavigation).ThenInclude(e => e.IdExtraccionNavigation).ThenInclude(e => e.IdCampanaNavigation).ThenInclude(e => e.IdPredioNavigation).Where(e => e.IdEnvio == idrecepcion).Select(e => e.IdEnvioNavigation.IdExtraccionNavigation.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral).FirstOrDefault();

            var sumarreglo = ListadoExtraccionTA == null ? 0 : ListadoExtraccionTA.Sum(lc => lc.sumNroTrozas);
            ViewBag.EnvioRecepcion = _context.Pret10Envios
                   .Where(c => c.IdEstado == 4 && sumarreglo + _context.Pret12RecepcionDtls
                       .Where(item =>
                           item.IdEstado == 1 &&
                           c.IdExtraccion == item.IdRecepcionNavigation.IdEnvio &&
                           (IdTemporal == null || IdTemporal != item.IdRecepcion)
                       )
                       .AsEnumerable() // Forzar la evaluación en memoria
                       .Sum(item => item.NroTrozos)
                   != c.EnvioCant
                   )
                   .Select(c => new
                   {
                       id = c.IdEnvio,
                       codigo = c.NroEnvio,
                       nrositio = c.IdExtraccionNavigation.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                       fechaini = c.FechaEnvio
                   })
                   .ToList();
            var empleadosRECEPCION = _context.Pert04Empleados
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

            ViewBag.EmpRec = empleadosRECEPCION;



            var recepcionvista = _context.Pret11Recepcions
            .Where(c => c.IdRecepcion == idrecepcion && c.IdEstado != 2 && c.IdEstado != 5)
            .FirstOrDefault();





            if (Recepcion != null)
            {
                var archivos = _context.Pret17Archivos
               .Where(a => a.IdEstado == 1 && a.IdRecepcion == idrecepcion)
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

                var detallesrecepcion = _context.Pret12RecepcionDtls
                .Where(detalle => detalle.IdRecepcion == idrecepcion && detalle.IdEstado == 1)

                .ToList();
                var Emprecepcion = _context.Pret22RecepcionEmpleados
                .Where(detalle => detalle.IdRecepcion == idrecepcion && detalle.IdEstado == 1)
                .Select(
                    n => new
                    {
                        idempleado = n.IdEmpleadoRec,
                        txtempleado = n.IdEmpleadoRecNavigation.TxtPriNom == null ? n.IdEmpleadoRecNavigation.TxtRznSocial : $"{n.IdEmpleadoRecNavigation.TxtPriNom} {n.IdEmpleadoRecNavigation.TxtApePat}",
                        nrodoc = String.IsNullOrEmpty(n.IdEmpleadoRecNavigation.NroRuc) ? n.IdEmpleadoRecNavigation.NroDoc : n.IdEmpleadoRecNavigation.NroRuc,
                        condicion = n.IdEmpleadoRecNavigation.IdCondicionLaboralNavigation.TxtDesc,
                        telefono = !string.IsNullOrEmpty(n.IdEmpleadoRecNavigation.Celular1) ? n.IdEmpleadoRecNavigation.Celular1 : (!string.IsNullOrEmpty(n.IdEmpleadoRecNavigation.Celular2) ? n.IdEmpleadoRecNavigation.Celular2 : n.IdEmpleadoRecNavigation.Celular3),
                        categoria = n.IdEmpleadoRecNavigation.IdCategoriaEmpNavigation.TxtNombre,

                    })
                .ToList();
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(Recepcion, settings);
                var detallesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(detallesrecepcion, settings);
                var empleadosSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Emprecepcion, settings);
                var archivosserialized = Newtonsoft.Json.JsonConvert.SerializeObject(archivosParaVista, settings);

                IdTemporal = recepcionvista?.IdRecepcion;
                IdTemporalEnvio = recepcionvista?.IdEnvio;
                ViewBag.Recepcion = campanaserialized;
                ViewBag.Detalles = detallesSerialized;
                ViewBag.Empleados = empleadosSerialized;
                ViewBag.ArchivosParaVista = archivosParaVista;
                return View("Recepcion", recepcionvista);
            }
            else
            {
                return NotFound(); // O redirigir a otra página
            }
        }
        [HttpGet]

        public IActionResult Recargarenvrec()
        {
            try
            {

                var sumarreglo = ListadoExtraccionTA == null ? 0 : ListadoExtraccionTA.Sum(lc => lc.sumNroTrozas);
                var updatedEnvrec = _context.Pret10Envios
                   .Where(c => c.IdEstado == 4 && sumarreglo + _context.Pret12RecepcionDtls
                       .Where(item =>
                           item.IdEstado == 1 &&
                           c.IdExtraccion == item.IdRecepcionNavigation.IdEnvio &&
                           (IdTemporal == null || IdTemporal != item.IdRecepcion)
                       )
                       .AsEnumerable() // Forzar la evaluación en memoria
                       .Sum(item => item.NroTrozos)
                   != c.EnvioCant
                   )
                   .Select(c => new
                   {
                       id = c.IdEnvio,
                       codigo = c.NroEnvio,
                       nrositio = c.IdExtraccionNavigation.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                       fechaini = c.FechaEnvio
                   })
                   .ToList();


                return Json(updatedEnvrec);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar la Campan a Recepcion: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult SeleccionarEnvRec(long? idEnvio)
        {
            try
            {

                IdTemporalEnvio = idEnvio == null ? IdTemporalEnvio : idEnvio;
                var response = _context.Pret10Envios
                    .Where(e => e.IdEnvio == IdTemporalEnvio && e.IdEstado == 4 || e.IdEstado == 6)
                    .Select(e => new
                    {
                        CodCampana = e.IdExtraccionNavigation.IdCampanaNavigation.CodigoCampana,
                        UnCatastral = e.IdExtraccionNavigation.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                        NomLoc = e.IdLocationNavigation.TxtDesc,
                        NomLto = e.IdLocationToNavigation.TxtDesc,
                        NroGuia = e.NroGuia,
                        NroGuiaTransp = e.NroGuiaTransp,
                        NroPlaca = e.NroPlaca,
                        detalle = _context.Pret13EnvioDtls.Where(ed => ed.IdEnvio == idEnvio && ed.IdEstado == 1)
                        .Select(ed => new
                        {
                            idun = ed.CodigoExtraccion,
                            idta = ed.IdTipoArbol,
                            ta = ed.TxtTipoArbol,
                            nt = ed.NroTrozos,
                            al = ed.AltArbolPro,
                            di = ed.DiamPro,
                            com = ed.Comentario

                        }).ToList()
                    })
                    .FirstOrDefault();

                return Json(response);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la Recepcion del envío: " + ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet]
        public IActionResult ObtenerEnvio([FromQuery] string? idsExtraccionesJson)
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
                Recargarenvrec();
                var campanas_tipo_Arbol = _context.Pret13EnvioDtls
                    .Where(p => p.IdEnvio == IdTemporalEnvio && p.IdEstado == 1 || p.IdEstado == 3)
                    .Select(p => new { idta = p.IdTipoArbol, txtdesc = p.TxtTipoArbol, nroArboles = p.NroArboles })
                    .ToList();

                var enviosDisponibles = campanas_tipo_Arbol
                .Where(c =>
                    !ListadoExtraccionTA.Any(id =>
                        id.idTipoArbol == c.idta &&
                        (id.sumNroTrozas + _context.Pret12RecepcionDtls
                            .Where(item =>
                                item.IdRecepcionNavigation.IdEnvio == IdTemporalEnvio &&
                                item.IdEstado == 1 || item.IdEstado == 3 && item.IdTipoArbol == c.idta &&
                                (IdTemporal == null || IdTemporal != item.IdRecepcion)
                            )
                            .Sum(item => item.NroArboles)
                        ) == c.nroArboles
                    )
                )
                .ToList();



                return Json(enviosDisponibles);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las campañas: " + ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet]
        public IActionResult Recargaremprec([FromQuery] List<long> listaIds)
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
        public IActionResult EmpleadoRec(long empleadoID)
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
        public IActionResult CerrarExtraccionRec()
        {
            var redirectUrl = Url.Action("ListadoRec", "Pret11Recepcion");
            var response = new { redirectUrl };
            IdTemporal = null;
            IdTemporalEnvio = null;
            ListadoExtraccionTA = null;
            sumarreglo = null;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRecepcion(DateTime FechaRecepcion, string? comentarioRec, string arreglorecepcionTA,
       string arregloempleadosrecepcion, List<IFormFile> archivos, bool check)
        {
            try
            {
                var campanatipoarbolseleccionadosrec = JsonConvert.DeserializeObject<List<TAEnvio>>(arreglorecepcionTA);
                var empleadosseleccionadosrec = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadosrecepcion);
                int sumaNroTrozas = campanatipoarbolseleccionadosrec.Sum(e => e.nrotrozas);
                var existingRecepcion = _context.Pret11Recepcions.AsNoTracking()
           .FirstOrDefault(c => c.IdRecepcion == IdTemporal);
                var existingEnvios = _context.Pret10Envios.Where(e => e.IdEnvio == IdTemporalEnvio).FirstOrDefault();

                if (existingRecepcion != null)
                {

                    existingRecepcion.IdEnvio = (long)IdTemporalEnvio;
                    existingRecepcion.FechaRecepcion = FechaRecepcion;
                    existingRecepcion.Comentario = comentarioRec;
                    existingRecepcion.NroPalaca = existingEnvios.NroPlaca;
                    existingRecepcion.NroGuia = existingEnvios.NroGuia;
                    existingRecepcion.NroGuiaTransp = existingEnvios.NroGuiaTransp;
                    existingRecepcion.IdLocation = existingEnvios.IdLocation;
                    existingRecepcion.IdLocationTo = existingEnvios.IdLocationTo;
                    existingRecepcion.RecepcionCant = sumaNroTrozas;
                    existingRecepcion.IdUsuarioModificador = idusuario;
                    if (existingRecepcion.IdEstado == 3)
                    {
                        existingRecepcion.IdEstado = check == true ? 3 : 4;
                        existingRecepcion.TxtEstado = check == true ? "BORRADOR" : "RECEPCIONADO";
                    }
                    existingRecepcion.TxtUsuarioModificador = txtusuario;
                    existingRecepcion.FechaModificacion = fechaHoy;
                    existingRecepcion.IdTipoComp = 63;
                    _context.Pret11Recepcions.Update(existingRecepcion);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingRecepcion.IdRecepcion;

                    var idcampanaTARecEnArreglo = campanatipoarbolseleccionadosrec.Select(p => p.idunico).ToList();

                    //DETALLE
                    //ACTUALIZAR
                    foreach (TAEnvio campanaTAEnv in campanatipoarbolseleccionadosrec)
                    {
                        var existinRecDtl = _context.Pret12RecepcionDtls
                           .FirstOrDefault(d => d.IdRecepcion == IdTemporal && d.CodigoRecepcion == campanaTAEnv.idunico);

                        if (existinRecDtl != null)
                        {
                            existinRecDtl.IdTipoArbol = campanaTAEnv.idTipoArbol;
                            existinRecDtl.TxtTipoArbol = campanaTAEnv.nombreTipoarbol;
                            existinRecDtl.NroTrozos = campanaTAEnv.nrotrozas;
                            existinRecDtl.DiamPro = campanaTAEnv.diametro;
                            existinRecDtl.AltArbolPro = campanaTAEnv.altura;
                            existinRecDtl.Comentario = campanaTAEnv.comentario;
                            existinRecDtl.IdEstado = 1;
                            existinRecDtl.TxtEstado = "ACTIVO";

                            _context.Pret12RecepcionDtls.Update(existinRecDtl);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (TAEnvio campanaTA in campanatipoarbolseleccionadosrec)
                    {
                        var existingRecDtl = _context.Pret12RecepcionDtls
                           .FirstOrDefault(d => d.IdRecepcion == IdTemporal && d.CodigoRecepcion == campanaTA.idunico);

                        if (existingRecDtl == null)
                        {
                            var Pret12RecDtl = new Pret12RecepcionDtl
                            {
                                IdRecepcion = (long)(IdTemporal),
                                IdTipoArbol = campanaTA.idTipoArbol,
                                NroTrozos = campanaTA.nrotrozas,
                                Comentario = campanaTA.comentario,
                                DiamPro = campanaTA.diametro,
                                AltArbolPro = campanaTA.altura,
                                CodigoRecepcion = campanaTA.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret12RecepcionDtls.Add(Pret12RecDtl);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CAMBIAR ESTADO
                    var registrosParaActualizarRecDtl = _context.Pret12RecepcionDtls
                        .Where(d => d.IdRecepcion == IdTemporal && !idcampanaTARecEnArreglo.Contains(d.CodigoRecepcion))
                        .ToList();

                    foreach (var registro in registrosParaActualizarRecDtl)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();



                    //EMPLEADO
                    //ACTUALIZAR
                    var idempleadoRecEnArreglo = empleadosseleccionadosrec.Select(p => p.idempleado).ToList();
                    foreach (EmpleadoExt empleadoRec in empleadosseleccionadosrec)
                    {
                        var existinRecEmp = _context.Pret22RecepcionEmpleados
                           .FirstOrDefault(d => d.IdRecepcion == IdTemporal && d.IdEmpleadoRec == empleadoRec.idempleado);

                        if (existinRecEmp != null)
                        {
                            existinRecEmp.IdEmpleadoRec = empleadoRec.idempleado;
                            existinRecEmp.IdEstado = 1;
                            existinRecEmp.TxtEstado = "ACTIVO";

                            _context.Pret22RecepcionEmpleados.Update(existinRecEmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadosrec)
                    {
                        var existingEnvioEmp = _context.Pret22RecepcionEmpleados
                           .FirstOrDefault(d => d.IdRecepcion == IdTemporal && d.IdEmpleadoRec == EmpleadoExt.idempleado);

                        if (existingEnvioEmp == null)
                        {
                            var Pret22RecEmp = new Pret22RecepcionEmpleado
                            {
                                IdRecepcion = (long)(IdTemporal),
                                IdEmpleadoRec = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret22RecepcionEmpleados.Add(Pret22RecEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret22RecepcionEmpleados
                        .Where(d => d.IdRecepcion == IdTemporal && !idempleadoRecEnArreglo.Contains((long)d.IdEmpleadoRec))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtemp)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    await _context.SaveChangesAsync();

                    //Archivo

                    var idarchivosenArreglo = archivos.Select(p => p.FileName).ToList();

                    //Agregar
                    foreach (var archivo in archivos)
                    {
                        var existingArchivo = _context.Pret17Archivos
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdRecepcion == IdTemporal);
                        if (existingArchivo == null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "recepcion", IdTemporal.ToString());


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
                                    IdRecepcion = existingRecepcion.IdRecepcion,
                                    IdTipoDir = 4,
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
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdRecepcion == IdTemporal);
                        if (existingArchivo != null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "recepcion", IdTemporal.ToString());


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
                                existingArchivo.IdRecepcion = existingRecepcion.IdRecepcion;
                                existingArchivo.IdTipoDir = 4;

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Update(existingArchivo);
                                await _context.SaveChangesAsync();

                            }
                        }
                    }

                    var registrosParaActualizar = _context.Pret17Archivos
                       .Where(d => d.IdRecepcion == IdTemporal && !idarchivosenArreglo.Contains(d.NombreArchivo))
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

                    if (existingRecepcion.IdEstado == 4)
                    {
                        existingEnvios.IdEstado = 4;
                        existingEnvios.TxtEstado = "RECEPCIONADO";

                    }
                }
                else
                {
                    var cantidad = _context.Pret11Recepcions./*Where(e => e.IdEstado != 5 && e.IdEstado != 2).*/Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nrorec = txt_serie + "-" + txt_numero;

                    var Pret11Recepcion = new Pret11Recepcion
                    {
                        IdEnvio = (long)IdTemporalEnvio,
                        FechaRecepcion = FechaRecepcion,
                        Comentario = comentarioRec,
                        TxtSerie = txt_serie,
                        TxtNro = txt_numero,
                        NroGuia = existingEnvios.NroGuia,
                        NroPalaca = existingEnvios.NroPlaca,
                        NroGuiaTransp = existingEnvios.NroGuiaTransp,
                        IdLocation = existingEnvios.IdLocation,
                        IdLocationTo = existingEnvios.IdLocationTo,
                        NroRecepcion = nrorec,
                        IdTipoComp = 64,
                        RecepcionCant = sumaNroTrozas,
                        Post=0,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check == true ? 3 : 4,
                        TxtEstado = check == true ? "BORRADOR" : "RECEPCIONADO",
                    };

                    _context.Pret11Recepcions.Add(Pret11Recepcion);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret11Recepcion.IdRecepcion;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {

                        foreach (TAEnvio campanaTAExt in campanatipoarbolseleccionadosrec)
                        {

                            var RecepcionDtl = new Pret12RecepcionDtl
                            {
                                IdRecepcion = (long)(IdTemporal),
                                IdTipoArbol = campanaTAExt.idTipoArbol,
                                TxtTipoArbol = campanaTAExt.nombreTipoarbol,
                                NroTrozos = campanaTAExt.nrotrozas,
                                DiamPro = campanaTAExt.diametro,

                                AltArbolPro = campanaTAExt.altura,
                                Comentario = campanaTAExt.comentario,
                                CodigoRecepcion = campanaTAExt.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret12RecepcionDtls.Add(RecepcionDtl);
                            await _context.SaveChangesAsync();


                        }

                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadosrec)
                        {

                            var EmpleadoEnv = new Pret22RecepcionEmpleado
                            {
                                IdRecepcion = (long)(IdTemporal),
                                IdEmpleadoRec = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret22RecepcionEmpleados.Add(EmpleadoEnv);
                            await _context.SaveChangesAsync();


                        }
                        foreach (var archivo in archivos)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "recepcion", IdTemporal.ToString());


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
                                    IdRecepcion = Pret11Recepcion.IdRecepcion,
                                    IdTipoDir = 4,
                                    IdEstado = 1,
                                    TxtEstado = "ACTIVO",
                                };

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Add(pret17Archivo);
                                await _context.SaveChangesAsync();

                            }
                        }


                        if (Pret11Recepcion.IdEstado == 4)
                        {
                            existingEnvios.IdEstado = 4;
                            existingEnvios.TxtEstado = "RECEPCIONADO";

                        }
                    }



                }
                await _context.SaveChangesAsync();
                return Json(new { mensaje = "Guardado correctamente" });

            }

            catch (Exception ex)
            {
                return Json(new { errores = new List<string> { ex.Message, ex.StackTrace } });
            }

        }

        [HttpPost]
        public async Task<IActionResult> CrearRecepcionycerrar(DateTime FechaRecepcion, string? comentarioRec, string arreglorecepcionTA,
       string arregloempleadosrecepcion, List<IFormFile> archivos, bool check)
        {
            try
            {
                var campanatipoarbolseleccionadosrec = JsonConvert.DeserializeObject<List<TAEnvio>>(arreglorecepcionTA);
                var empleadosseleccionadosrec = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadosrecepcion);
                int sumaNroTrozas = campanatipoarbolseleccionadosrec.Sum(e => e.nrotrozas);
                var existingRecepcion = _context.Pret11Recepcions.AsNoTracking()
           .FirstOrDefault(c => c.IdRecepcion == IdTemporal);
                var existingEnvios = _context.Pret10Envios.Where(e => e.IdEnvio == IdTemporalEnvio).FirstOrDefault();

                if (existingRecepcion != null)
                {

                    existingRecepcion.IdEnvio = (long)IdTemporalEnvio;
                    existingRecepcion.FechaRecepcion = FechaRecepcion;
                    existingRecepcion.Comentario = comentarioRec;
                    existingRecepcion.NroPalaca = existingEnvios.NroPlaca;
                    existingRecepcion.NroGuia = existingEnvios.NroGuia;
                    existingRecepcion.NroGuiaTransp = existingEnvios.NroGuiaTransp;
                    existingRecepcion.IdLocation = existingEnvios.IdLocation;
                    existingRecepcion.IdLocationTo = existingEnvios.IdLocationTo;
                    existingRecepcion.RecepcionCant = sumaNroTrozas;
                    existingRecepcion.IdUsuarioModificador = idusuario;
                    if (existingRecepcion.IdEstado == 3)
                    {
                        existingRecepcion.IdEstado = check == true ? 3 : 4;
                        existingRecepcion.TxtEstado = check == true ? "BORRADOR" : "RECEPCIONADO";
                    }
                    existingRecepcion.TxtUsuarioModificador = txtusuario;
                    existingRecepcion.FechaModificacion = fechaHoy;
                    existingRecepcion.IdTipoComp = 63;
                    _context.Pret11Recepcions.Update(existingRecepcion);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingRecepcion.IdRecepcion;

                    var idcampanaTARecEnArreglo = campanatipoarbolseleccionadosrec.Select(p => p.idunico).ToList();

                    //DETALLE
                    //ACTUALIZAR
                    foreach (TAEnvio campanaTAEnv in campanatipoarbolseleccionadosrec)
                    {
                        var existinRecDtl = _context.Pret12RecepcionDtls
                           .FirstOrDefault(d => d.IdRecepcion == IdTemporal && d.CodigoRecepcion == campanaTAEnv.idunico);

                        if (existinRecDtl != null)
                        {
                            existinRecDtl.IdTipoArbol = campanaTAEnv.idTipoArbol;
                            existinRecDtl.TxtTipoArbol = campanaTAEnv.nombreTipoarbol;
                            existinRecDtl.NroTrozos = campanaTAEnv.nrotrozas;
                            existinRecDtl.DiamPro = campanaTAEnv.diametro;
                            existinRecDtl.AltArbolPro = campanaTAEnv.altura;
                            existinRecDtl.Comentario = campanaTAEnv.comentario;
                            existinRecDtl.IdEstado = 1;
                            existinRecDtl.TxtEstado = "ACTIVO";

                            _context.Pret12RecepcionDtls.Update(existinRecDtl);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (TAEnvio campanaTA in campanatipoarbolseleccionadosrec)
                    {
                        var existingRecDtl = _context.Pret12RecepcionDtls
                           .FirstOrDefault(d => d.IdRecepcion == IdTemporal && d.CodigoRecepcion == campanaTA.idunico);

                        if (existingRecDtl == null)
                        {
                            var Pret12RecDtl = new Pret12RecepcionDtl
                            {
                                IdRecepcion = (long)(IdTemporal),
                                IdTipoArbol = campanaTA.idTipoArbol,
                                NroTrozos = campanaTA.nrotrozas,
                                Comentario = campanaTA.comentario,
                                DiamPro = campanaTA.diametro,
                                AltArbolPro = campanaTA.altura,
                                CodigoRecepcion = campanaTA.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret12RecepcionDtls.Add(Pret12RecDtl);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CAMBIAR ESTADO
                    var registrosParaActualizarRecDtl = _context.Pret12RecepcionDtls
                        .Where(d => d.IdRecepcion == IdTemporal && !idcampanaTARecEnArreglo.Contains(d.CodigoRecepcion))
                        .ToList();

                    foreach (var registro in registrosParaActualizarRecDtl)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();



                    //EMPLEADO
                    //ACTUALIZAR
                    var idempleadoRecEnArreglo = empleadosseleccionadosrec.Select(p => p.idempleado).ToList();
                    foreach (EmpleadoExt empleadoRec in empleadosseleccionadosrec)
                    {
                        var existinRecEmp = _context.Pret22RecepcionEmpleados
                           .FirstOrDefault(d => d.IdRecepcion == IdTemporal && d.IdEmpleadoRec == empleadoRec.idempleado);

                        if (existinRecEmp != null)
                        {
                            existinRecEmp.IdEmpleadoRec = empleadoRec.idempleado;
                            existinRecEmp.IdEstado = 1;
                            existinRecEmp.TxtEstado = "ACTIVO";

                            _context.Pret22RecepcionEmpleados.Update(existinRecEmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadosrec)
                    {
                        var existingEnvioEmp = _context.Pret22RecepcionEmpleados
                           .FirstOrDefault(d => d.IdRecepcion == IdTemporal && d.IdEmpleadoRec == EmpleadoExt.idempleado);

                        if (existingEnvioEmp == null)
                        {
                            var Pret22RecEmp = new Pret22RecepcionEmpleado
                            {
                                IdRecepcion = (long)(IdTemporal),
                                IdEmpleadoRec = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret22RecepcionEmpleados.Add(Pret22RecEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret22RecepcionEmpleados
                        .Where(d => d.IdRecepcion == IdTemporal && !idempleadoRecEnArreglo.Contains((long)d.IdEmpleadoRec))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtemp)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    await _context.SaveChangesAsync();

                    //Archivo

                    var idarchivosenArreglo = archivos.Select(p => p.FileName).ToList();

                    //Agregar
                    foreach (var archivo in archivos)
                    {
                        var existingArchivo = _context.Pret17Archivos
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdRecepcion == IdTemporal);
                        if (existingArchivo == null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "recepcion", IdTemporal.ToString());


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
                                    IdRecepcion = existingRecepcion.IdRecepcion,
                                    IdTipoDir = 4,
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
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdRecepcion == IdTemporal);
                        if (existingArchivo != null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "recepcion", IdTemporal.ToString());


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
                                existingArchivo.IdRecepcion = existingRecepcion.IdRecepcion;
                                existingArchivo.IdTipoDir = 4;

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Update(existingArchivo);
                                await _context.SaveChangesAsync();

                            }
                        }
                    }

                    var registrosParaActualizar = _context.Pret17Archivos
                       .Where(d => d.IdRecepcion == IdTemporal && !idarchivosenArreglo.Contains(d.NombreArchivo))
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

                    if (existingRecepcion.IdEstado == 4)
                    {
                        existingEnvios.IdEstado = 4;
                        existingEnvios.TxtEstado = "RECEPCIONADO";

                    }
                }
                else
                {
                    var cantidad = _context.Pret11Recepcions./*Where(e => e.IdEstado != 5 && e.IdEstado != 2).*/Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nrorec = txt_serie + "-" + txt_numero;

                    var Pret11Recepcion = new Pret11Recepcion
                    {
                        IdEnvio = (long)IdTemporalEnvio,
                        FechaRecepcion = FechaRecepcion,
                        Comentario = comentarioRec,
                        TxtSerie = txt_serie,
                        TxtNro = txt_numero,
                        NroGuia = existingEnvios.NroGuia,
                        NroPalaca = existingEnvios.NroPlaca,
                        NroGuiaTransp = existingEnvios.NroGuiaTransp,
                        IdLocation = existingEnvios.IdLocation,
                        IdLocationTo = existingEnvios.IdLocationTo,
                        NroRecepcion = nrorec,
                        IdTipoComp = 64,
                        RecepcionCant = sumaNroTrozas,
                        Post=0,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check == true ? 3 : 4,
                        TxtEstado = check == true ? "BORRADOR" : "RECEPCIONADO",
                    };

                    _context.Pret11Recepcions.Add(Pret11Recepcion);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret11Recepcion.IdRecepcion;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {

                        foreach (TAEnvio campanaTAExt in campanatipoarbolseleccionadosrec)
                        {

                            var RecepcionDtl = new Pret12RecepcionDtl
                            {
                                IdRecepcion = (long)(IdTemporal),
                                IdTipoArbol = campanaTAExt.idTipoArbol,
                                TxtTipoArbol = campanaTAExt.nombreTipoarbol,
                                NroTrozos = campanaTAExt.nrotrozas,
                                DiamPro = campanaTAExt.diametro,

                                AltArbolPro = campanaTAExt.altura,
                                Comentario = campanaTAExt.comentario,
                                CodigoRecepcion = campanaTAExt.idunico,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret12RecepcionDtls.Add(RecepcionDtl);
                            await _context.SaveChangesAsync();


                        }

                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadosrec)
                        {

                            var EmpleadoEnv = new Pret22RecepcionEmpleado
                            {
                                IdRecepcion = (long)(IdTemporal),
                                IdEmpleadoRec = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret22RecepcionEmpleados.Add(EmpleadoEnv);
                            await _context.SaveChangesAsync();


                        }
                        foreach (var archivo in archivos)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "recepcion", IdTemporal.ToString());


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
                                    IdRecepcion = Pret11Recepcion.IdRecepcion,
                                    IdTipoDir = 4,
                                    IdEstado = 1,
                                    TxtEstado = "ACTIVO",
                                };

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Add(pret17Archivo);
                                await _context.SaveChangesAsync();

                            }
                        }


                        if (Pret11Recepcion.IdEstado == 4)
                        {
                            existingEnvios.IdEstado = 4;
                            existingEnvios.TxtEstado = "RECEPCIONADO";

                        }
                    }



                }
                await _context.SaveChangesAsync();
                IdTemporal = null;
                IdTemporalEnvio = null;
                ListadoExtraccionTA = null;
                return Json(new { redirectUrl = Url.Action("ListadoRec", "Pret11Recepcion") });

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
                var estado = _context.Pret11Recepcions.Where(t => t.IdRecepcion == IdTemporal).Select(t => t.IdEstado).FirstOrDefault();
                var estadoName = _context.Pret11Recepcions.Where(t => t.IdRecepcion == IdTemporal).Select(t => t.TxtEstado).FirstOrDefault();
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







        [HttpGet]
        public IActionResult removeIdTemporalRecepcion()
        {
            IdTemporalEnvio = null;
            ListadoExtraccionTA = null;
            sumarreglo = null;
            return Json(new { success = true });
        }


        [HttpPost]
        public async Task<IActionResult> EliminarRecepcion(long id)
        {
            try
            {
                var existingRecepcion = _context.Pret11Recepcions.Where(p => p.IdEstado == 3 || p.IdEstado == 4 && p.IdRecepcion == id).FirstOrDefault();

                if (existingRecepcion != null)
                {

                    var existingArchivo = _context.Pret17Archivos.Where(c => c.IdRecepcion == id).ToList();
                    if (existingArchivo != null)
                    {
                        foreach (Pret17Archivo archivoReg in existingArchivo)
                        {
                            //archivoReg.IdEstado = 0;
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
                    var existingEmpleado = _context.Pret22RecepcionEmpleados.Where(p => p.IdRecepcion == id).ToList();


                    foreach (Pret22RecepcionEmpleado empleado in existingEmpleado)
                    {
                        //empleado.IdEstado = 0;
                        //empleado.TxtEstado = "INACTIVO";
                        _context.Pret22RecepcionEmpleados.Remove(empleado);
                        await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                    var existingDtll = _context.Pret12RecepcionDtls.Where(p => p.IdRecepcion == id).ToList();

                    if (existingDtll != null)
                    {
                        foreach (Pret12RecepcionDtl Detalle in existingDtll)
                        {
                            //Detalle.IdEstado = 0;
                            //Detalle.TxtEstado = "INACTIVO";
                            _context.Pret12RecepcionDtls.Remove(Detalle);
                            await _context.SaveChangesAsync();
                        }
                    }
                    _context.Pret11Recepcions.Remove(existingRecepcion);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("ListadoRec", "Pret11Recepcion") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AnularRecepcion(long id)
        {
            try
            {
                var existingRecepcion = _context.Pret11Recepcions.Where(p => p.IdEstado == 3 || p.IdEstado == 4 && p.IdRecepcion == id).FirstOrDefault();

                if (existingRecepcion != null)
                {
                    var existingEnvios = _context.Pret10Envios.Where(e => e.IdEnvio == existingRecepcion.IdEnvio).ToList();
                    foreach (Pret10Envio envio in existingEnvios)
                    {
                        envio.IdEstado = 2;
                        envio.TxtEstado = "ENVIADO";
                    }
                    existingRecepcion.IdEstado = 5;
                    existingRecepcion.TxtEstado = "ANULADO";
                    var existingArchivo = _context.Pret17Archivos.Where(c => c.IdRecepcion == id).ToList();
                    if (existingArchivo != null)
                    {
                        foreach (Pret17Archivo archivoReg in existingArchivo)
                        {
                            //archivoReg.IdEstado = 0;
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
                    var existingEmpleado = _context.Pret22RecepcionEmpleados.Where(p => p.IdEstado == 1 && p.IdRecepcion == id).ToList();


                    foreach (Pret22RecepcionEmpleado empleado in existingEmpleado)
                    {
                        empleado.IdEstado = 0;
                        empleado.TxtEstado = "INACTIVO";
                        //_context.Pret21EnvioEmpleados.Remove(empleado);
                        //await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();


                    var existingDtll = _context.Pret12RecepcionDtls.Where(p => p.IdEstado == 1 && p.IdRecepcion == id).ToList();

                    if (existingDtll != null)
                    {
                        foreach (Pret12RecepcionDtl Detalle in existingDtll)
                        {
                            Detalle.IdEstado = 0;
                            Detalle.TxtEstado = "INACTIVO";
                            //_context.Pret13EnvioDtls.Remove(Detalle);
                            //await _context.SaveChangesAsync();
                        }
                    }
                    //_context.Pret10Envios.Remove(existingEnvio);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("ListadoRec", "Pret11Recepcion") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Data.Interfaces;
using Maderera_Aplicacion_Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.StaticFiles;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pret14ProduccionController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;
        private readonly long idusuario;
        private readonly string txtusuario;
        private readonly DateTime fechaHoy;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static long? IdTemporal { get; set; }
        private static long? IdTemporalPredio { get; set; }
        private static long? IdTemporalCampana { get; set; }
        private static long? IdTemporalExtraccion { get; set; }
        private static long? IdTemporalIns { get; set; }

        public Pret14ProduccionController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pret14Produccion
        public async Task<IActionResult> ListadoPro()
        {
            var eagleContext = _context.Pret14Produccions.Include(p => p.IdExtraccionNavigation)
                .Include(p => p.IdPredioNavigation).Include(p => p.IdCampanaNavigation).Include(p => p.IdProductoInsNavigation).Include(p => p.IdUmInsNavigation)
                .Include(p => p.IdUsuarioModificadorNavigation)
                .Include(p => p.IdUsuarioNavigation);
            return View(await eagleContext.ToListAsync());
        }

        // GET: Pret14Produccion/Details/5
        public async Task<IActionResult> Details(long? IdProduccion)
        {
            var pret14Produccion = await _context.Pret14Produccions
                .Include(p => p.IdExtraccionNavigation)
                .Include(p => p.IdPredioNavigation)
                .Include(p => p.IdUsuarioModificadorNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdProduccion == IdProduccion);

            return View(pret14Produccion);
        }
        [HttpGet]
        public IActionResult Produccion()
        {
            IdTemporal = null;
            IdTemporalCampana = null;
            IdTemporalPredio = null;

            var empleadosventas = _context.Pert04Empleados
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
            ViewBag.EmpPro = empleadosventas;

            ViewBag.Extraccionpro = _context.Pret07Extraccions.Select(c => new
            {
                id = c.IdExtraccion,
                codigo = c.NroExtraccion,
                nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                fechaini = c.FechaExtraccion
            })
                   .ToList();

            var Predios = _context.Pret01Predios
                    .Where(e => e.IdEstado == 1)
                    .Select(e => new
                    {
                        id = e.IdPredio,
                        unidC = e.UnidadCatastral,
                        nroS = e.NroSitio,
                        fechA = e.FechaAdquisicion,
                        fechC = e.FechaCompra,

                    })
                    .ToList();

            ViewBag.Predios = Predios;
            ViewBag.Insumopro = _context.Prot09Productos.Where(c => c.IdTipoProd == 1002 && c.IdEstado == 1)
                .Select(p => new
                {
                    id = p.IdProducto,
                    nombre = p.TxtDesc,
                    um = p.IdUmNavigation.TxtDesc
                })
                .ToList();
            ViewBag.Productosp = _context.Prot09Productos
                .Where(p => p.MtoPvpuSinIgv != null)
                .Include(p => p.IdUmNavigation)
                .Select(e => new
                {
                    IdProducto = e.IdProducto,
                    NombreProducto = e.TxtDesc,
                    Monto_si = e.MtoPvpuSinIgv,
                    Monto_ci = e.MtoPvpuConIgv,
                    igv = e.PorImpto,
                    um = e.IdUmNavigation.TxtDesc,
                    dimension = e.LargoProd + "x" + e.AnchoProd + "x" + e.AlturaProd,
                    diametro = e.DiametroProd,
                    peso = e.PesoProd
                })
                .ToList();

            return View();
        }
        [HttpGet]
        public IActionResult Editarproduccion(long idproduccion)
        {
            IdTemporal = null;
            IdTemporalCampana = null;
            IdTemporalPredio = null;
            IdTemporalExtraccion = null;
            IdTemporalIns = null;
            var Produccion = _context.Pret14Produccions
            .Include(t => t.IdExtraccionNavigation)
            .Include(t => t.IdPredioNavigation)
            .Include(t => t.IdProductoInsNavigation)
            .Include(t => t.IdCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdProduccion == idproduccion)
            .Select(campanaTA => new
            {
                all = campanaTA,
                CampanaTA = campanaTA,
                ide = campanaTA.IdExtraccion,
                codigo = campanaTA.IdExtraccionNavigation.NroExtraccion,
                predio = campanaTA.IdPredioNavigation.UnidadCatastral,
                campana = campanaTA.IdCampanaNavigation.CodigoCampana,
                um = campanaTA.IdUmInsNavigation.TxtDesc,
                insumo = campanaTA.IdProductoInsNavigation.TxtDesc,
                idi = campanaTA.IdProductoIns,
                idc = campanaTA.IdCampana,
                idp = campanaTA.IdPredio
            })
            .FirstOrDefault();
            var empleadosventas = _context.Pert04Empleados
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
            ViewBag.Extraccionpro = _context.Pret07Extraccions.Select(c => new
            {
                id = c.IdExtraccion,
                codigo = c.NroExtraccion,
                nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                fechaini = c.FechaExtraccion
            })
                   .ToList();

            ViewBag.EmpPro = empleadosventas;
            var Predios = _context.Pret01Predios
                    .Where(e => e.IdEstado == 1)
                    .Select(e => new
                    {
                        id = e.IdPredio,
                        unidC = e.UnidadCatastral,
                        nroS = e.NroSitio,
                        fechA = e.FechaAdquisicion,
                        fechC = e.FechaCompra,

                    })
                    .ToList();

            ViewBag.Predios = Predios;
            ViewBag.Insumopro = _context.Prot09Productos.Where(c => c.IdTipoProd == 1002 && c.IdEstado == 1)
                .Select(p => new
                {
                    id = p.IdProducto,
                    nombre = p.TxtDesc,
                    um = p.IdUmNavigation.TxtDesc
                })
                .ToList();
            ViewBag.Productosp = _context.Prot09Productos
                .Where(p => p.MtoPvpuSinIgv != null)
                .Include(p => p.IdUmNavigation)
                .Select(e => new
                {
                    IdProducto = e.IdProducto,
                    NombreProducto = e.TxtDesc,
                    Monto_si = e.MtoPvpuSinIgv,
                    Monto_ci = e.MtoPvpuConIgv,
                    igv = e.PorImpto,
                    um = e.IdUmNavigation.TxtDesc,
                    dimension = e.LargoProd + "x" + e.AnchoProd + "x" + e.AlturaProd,
                    diametro = e.DiametroProd,
                    peso = e.PesoProd
                })
                .ToList();

            var produccionvista = _context.Pret14Produccions
            .Where(c => c.IdProduccion == idproduccion)
            .FirstOrDefault();
            if (Produccion != null)
            {

                var detallesproduccion = _context.Pret15ProduccionDtls
                .Where(detalle => detalle.IdProduccion == idproduccion && detalle.IdEstado == 1)
                .Select(p => new
                {
                    id = p.IdProductoPro,
                    np = p.TxtProPro,
                    cu = p.IdProductoProNavigation.MtoPvpuConIgv,
                    um = p.IdUmProNavigation.TxtDesc,
                    can = p.CantidadPro,
                    total = p.TotalProp,
                    obs = p.TxtComentario
                }).ToList();
                var Empproduccion = _context.Pret20ProduccionEmpleados
                .Where(detalle => detalle.IdProduccion == idproduccion && detalle.IdEstado == 1)
                .Select(
                    n => new
                    {
                        idempleado = n.IdEmpleadoPro,
                        txtempleado = n.IdEmpleadoProNavigation.TxtPriNom == null ? n.IdEmpleadoProNavigation.TxtRznSocial : $"{n.IdEmpleadoProNavigation.TxtPriNom} {n.IdEmpleadoProNavigation.TxtApePat}",
                        nrodoc = String.IsNullOrEmpty(n.IdEmpleadoProNavigation.NroRuc) ? n.IdEmpleadoProNavigation.NroDoc : n.IdEmpleadoProNavigation.NroRuc,
                        condicion = n.IdEmpleadoProNavigation.IdCondicionLaboralNavigation.TxtDesc,
                        telefono = !string.IsNullOrEmpty(n.IdEmpleadoProNavigation.Celular1) ? n.IdEmpleadoProNavigation.Celular1 : (!string.IsNullOrEmpty(n.IdEmpleadoProNavigation.Celular2) ? n.IdEmpleadoProNavigation.Celular2 : n.IdEmpleadoProNavigation.Celular3),
                        categoria = n.IdEmpleadoProNavigation.IdCategoriaEmpNavigation.TxtNombre,

                    })
                .ToList();
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(Produccion, settings);
                var detallesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(detallesproduccion, settings);
                var empleadosSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Empproduccion, settings);

                IdTemporal = produccionvista?.IdProduccion;
                IdTemporalExtraccion = produccionvista?.IdExtraccion;
                IdTemporalCampana = produccionvista?.IdCampana;
                IdTemporalPredio = produccionvista?.IdPredio;
                IdTemporalIns = produccionvista?.IdProductoIns;
                ViewBag.Produccion = campanaserialized;
                ViewBag.Detalles = detallesSerialized;
                ViewBag.Empleados = empleadosSerialized;
                return View("Produccion", produccionvista);
            }
            else
            {
                return NotFound(); // O redirigir a otra página
            }
        }

        [HttpGet]
        public IActionResult RecargarPredio()
        {
            try
            {
                var updatedPredios = _context.Pret01Predios
                    .Where(e => e.IdEstado == 1)
                    .Select(e => new
                    {
                        id = e.IdPredio,
                        unidC = e.UnidadCatastral,
                        nroS = e.NroSitio,
                        fechA = e.FechaAdquisicion,
                        fechC = e.FechaCompra,

                    })
                    .ToList();

                ViewBag.Predios = updatedPredios;

                return Json(updatedPredios);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el cliente: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult RecargarInsumo()
        {
            try
            {
                var updatedInsumo = _context.Prot09Productos.Where(c => c.IdTipoProd == 1002 && c.IdEstado == 1)
                .Select(p => new
                {
                    id = p.IdProducto,
                    nombre = p.TxtDesc,
                    um = p.IdUmNavigation.TxtDesc
                })
                .ToList();

                ViewBag.Insumopro = updatedInsumo;

                return Json(updatedInsumo);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el cliente: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult RecargarCampanas(long? IdPredio)
        {
            try
            {
                if (IdPredio == null)
                {
                    var updatedCampanas = _context.Pret02Campanas
                        .Where(e => e.IdEstado == 1 && e.IdPredio == IdTemporalPredio)
                        .Select(e => new
                        {
                            id = e.IdCampana,
                            codC = e.CodigoCampana,
                            fechI = e.FechaInicio,
                            area = e.Area

                        })
                        .ToList();

                    ViewBag.Predios = updatedCampanas;
                    return Json(updatedCampanas);

                }
                else
                {
                    var updatedCampanas = _context.Pret02Campanas
                        .Where(e => e.IdEstado == 1 && e.IdPredio == IdPredio)
                        .Select(e => new
                        {
                            id = e.IdCampana,
                            codC = e.CodigoCampana,
                            fechI = e.FechaInicio,
                            area = e.Area

                        })
                        .ToList();
                    IdTemporalPredio = IdPredio;
                    ViewBag.Predios = updatedCampanas;
                    return Json(updatedCampanas);

                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el cliente: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult RemovePredio()
        {
            IdTemporalPredio = null;
            IdTemporalCampana = null;
            return Json(new { success = true });
        }
        [HttpGet]
        public IActionResult RemoveCampana()
        {
            IdTemporalCampana = null;
            return Json(new { success = true });
        }
        public IActionResult SelecCampana(long idCampana)
        {
            IdTemporalCampana = idCampana;
            return Json(new { success = true });


        }
        [HttpGet]
        public IActionResult Recargaremppro([FromQuery] List<long> listaIds)
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
                    celular = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                })
                .ToList().FirstOrDefault();

            return Json(campanas_tipo_Arbol);


        }
        [HttpGet]
        public IActionResult EmpleadoPro(long empleadoID)
        {
            var campanas_tipo_Arbol = _context.Pert04Empleados
                .Where(p => p.IdEmpleado == empleadoID && p.IdEstado == 1)
                .Select(e => new
                {
                    txtnombre = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                    cargo = e.IdCategoriaEmpNavigation.TxtNombre,
                    condicion = e.IdCondicionLaboralNavigation.TxtDesc,
                    nrodoc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc,
                    Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                })
                .ToList().FirstOrDefault();

            return Json(campanas_tipo_Arbol);


        }
        [HttpGet]

        public IActionResult Recargarextpro()
        {
            try
            {

                var updatedExtenv = _context.Pret07Extraccions.Select(c => new
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
                return BadRequest("Error al actualizar la Campan a Produccion: " + ex.Message);
            }
        }
        [HttpGet]

        public IActionResult VerificarExt(long idext)
        {
            try
            {

                var updatedExtenv = _context.Pret07Extraccions.Where(c => c.IdExtraccion == idext).Include(c => c.IdCampanaNavigation).ThenInclude(c => c.IdPredioNavigation).Select(c => new
                {
                    id = c.IdExtraccion,
                    codigo = c.NroExtraccion,
                    nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                    fechaini = c.FechaExtraccion,
                    idC = c.IdCampana,
                    Idp = c.IdCampanaNavigation.IdPredio

                })
                   .FirstOrDefault();

                IdTemporalExtraccion = idext;
                IdTemporalCampana = updatedExtenv.idC;
                IdTemporalPredio = updatedExtenv.Idp;



                return Json(updatedExtenv);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar la Campan a Produccion: " + ex.Message);
            }
        }
        [HttpGet]

        public IActionResult VerificarIns(long idext)
        {
            try
            {

                IdTemporalIns = idext;


                return Json(IdTemporalIns);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar la Campan a Produccion: " + ex.Message);
            }
        }
        public IActionResult removeIdTemporalExtraccion()
        {
            IdTemporalExtraccion = null;
            IdTemporalPredio = null;
            IdTemporalCampana = null;
            return Json(new { success = true });
        }
        public IActionResult removeIdIns()
        {
            IdTemporalIns = null;
            return Json(new { success = true });
        }
        [HttpGet]
        public IActionResult RecargarProductop(List<long> productosSeleccionadosIds)
        {

            // Crear una consulta de subconjunto de productos excluyendo aquellos en la lista productosSeleccionadosIds
            var productosExcluidos = _context.Prot09Productos
                .Where(p => productosSeleccionadosIds.Contains(p.IdProducto)
               );

            // Filtrar productos que no están en la lista de seleccionados
            var productosFiltrados = _context.Prot09Productos
                .Except(productosExcluidos)
                .Where(p => p.MtoPvpuSinIgv != null)
                .Include(p => p.IdUmNavigation)
                .Select(e => new
                {
                    IdProducto = e.IdProducto,
                    NombreProducto = e.TxtDesc,
                    Monto_si = e.MtoPvpuSinIgv,
                    Monto_ci = e.MtoPvpuConIgv,
                    igv = e.PorImpto,
                    um = e.IdUmNavigation.TxtDesc,
                    dimension = e.LargoProd + "x" + e.AnchoProd + "x" + e.AlturaProd,
                    diametro = e.DiametroProd,
                    peso = e.PesoProd
                })
                .ToList();

            // Retorna una respuesta exitosa
            return Json(productosFiltrados);
        }

        [HttpGet]
        public IActionResult ProductoDatos(int idProducto, float cantidad)
        {
            try
            {
                var producto = _context.Prot09Productos.Include(p => p.IdUmNavigation).FirstOrDefault(p => p.IdProducto == idProducto && p.IdEstado == 1);
                if (producto != null)
                {
                    var productoDetalles = new
                    {
                        IdProducto = producto.IdProducto,
                        NombreProducto = producto.TxtDesc,
                        Monto_ci = producto.MtoPvpuConIgv,
                        um = producto.IdUmNavigation.TxtDesc,
                        total = ((cantidad) * ((float)(producto.MtoPvpuConIgv))),

                    };
                    // Retorna una respuesta exitosa
                    return Json(productoDetalles);
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
        public IActionResult CerrarExtraccionPro()
        {
            var redirectUrl = Url.Action("ListadoPro", "Pret14Produccion");
            var response = new { redirectUrl };
            IdTemporal = null;
            IdTemporalPredio = null;
            IdTemporalExtraccion = null;
            IdTemporalCampana = null;
            IdTemporalIns = null;
            return Json(response);
        }
        public IActionResult CargarMood()
        {
            if (IdTemporal != null)
            {
                var estado = _context.Pret14Produccions.Where(t => t.IdProduccion == IdTemporal).Select(t => t.IdEstado).FirstOrDefault();
                var estadoName = _context.Pret14Produccions.Where(t => t.IdProduccion == IdTemporal).Select(t => t.TxtEstado).FirstOrDefault();
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
        public async Task<IActionResult> CrearProduccion(DateTime FechaPro, string? comentarioPro, int cinsPro, string productosSeleccionado,
       string arregloempleadospro, string tipopro, bool check)
        {
            try
            {
                var productosseleccionados = JsonConvert.DeserializeObject<List<ProductoSp>>(productosSeleccionado);
                var empleadosseleccionadospro = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadospro);

                var existingProduccion = _context.Pret14Produccions.AsNoTracking()
           .FirstOrDefault(c => c.IdProduccion == IdTemporal);

                if (existingProduccion != null)
                {
                    var conjuntoproducto = _context.Prot09Productos.Where(p => p.IdProducto == IdTemporalIns).Select(p => new { p.IdUm, p.TxtDesc }).FirstOrDefault();

                    existingProduccion.IdExtraccion = IdTemporalExtraccion;
                    existingProduccion.IdPredio = (long)IdTemporalPredio;
                    existingProduccion.IdCampana = (long)IdTemporalCampana;
                    existingProduccion.IdProductoIns = (long)IdTemporalIns;
                    existingProduccion.ComentarioPro = comentarioPro;
                    existingProduccion.FechaProduccion = FechaPro;
                    existingProduccion.CantidadIns = cinsPro;
                    existingProduccion.TipoPro = tipopro;
                    existingProduccion.IdUsuarioModificador = idusuario;
                    existingProduccion.IdUmIns = (int)conjuntoproducto.IdUm;
                    existingProduccion.TxtProIns = conjuntoproducto.TxtDesc;
                    if (existingProduccion.IdEstado == 3)
                    {
                        existingProduccion.IdEstado = check == true ? 3 : 4;
                        existingProduccion.TxtEstado = check == true ? "BORRADOR" : "ACTIVO";
                    }
                    existingProduccion.TxtUsuarioModificador = txtusuario;
                    existingProduccion.FechaModificacion = fechaHoy;
                    existingProduccion.TipoPro = tipopro;

                    _context.Pret14Produccions.Update(existingProduccion);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingProduccion.IdProduccion;

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idProductosEnArreglo = productosseleccionados.Select(p => p.idProducto).ToList();

                    // Actualizar registros existentes
                    foreach (ProductoSp producto in productosseleccionados)
                    {
                        var existingProdDet = _context.Pret15ProduccionDtls
                           .FirstOrDefault(d => d.IdProduccion == IdTemporal && d.IdProductoPro == producto.idProducto);

                        if (existingProdDet != null)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            // Actualiza los valores
                            existingProdDet.IdProductoPro = producto.idProducto;
                            existingProdDet.TxtProPro = producto.nombreProducto;
                            existingProdDet.IdUmPro = idum;
                            existingProdDet.CantidadPro = producto.cantidad;
                            existingProdDet.TxtComentario = producto.observacion;
                            existingProdDet.TotalProp = (double)producto.total;
                            existingProdDet.IdEstado = 1;
                            existingProdDet.TxtEstado = "ACTIVO";

                            _context.Pret15ProduccionDtls.Update(existingProdDet);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (ProductoSp producto in productosseleccionados)
                    {
                        var existingproddtll = _context.Pret15ProduccionDtls
                           .FirstOrDefault(d => d.IdProduccion == IdTemporal && d.IdProductoPro == producto.idProducto);

                        if (existingproddtll == null)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            // Crea un nuevo registro
                            var pret15proddtl = new Pret15ProduccionDtl
                            {
                                IdProduccion = (long)(IdTemporal),
                                IdProductoPro = producto.idProducto,
                                TxtProPro = producto.nombreProducto,
                                IdUmPro = idum,
                                CantidadPro = producto.cantidad,
                                TotalProp = (double)producto.total,
                                TxtComentario = producto.observacion,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret15ProduccionDtls.Add(pret15proddtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminarProdtl = _context.Pret15ProduccionDtls
                        .Where(d => d.IdProduccion == IdTemporal && !idProductosEnArreglo.Contains((long)d.IdProductoPro))
                        .ToList();
                    foreach (var registro in registrosParaEliminarProdtl)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    //_context.Tnst05CompEmitidoDtls.RemoveRange(registrosParaEliminar);
                    await _context.SaveChangesAsync();

                    var idEmpleadosEnArreglo = empleadosseleccionadospro.Select(p => p.idempleado).ToList();



                    //EMPLEADO
                    //ACTUALIZAR
                    var idempleadoRecEnArreglo = empleadosseleccionadospro.Select(p => p.idempleado).ToList();
                    foreach (EmpleadoExt empleadoRec in empleadosseleccionadospro)
                    {
                        var existinProdEmp = _context.Pret20ProduccionEmpleados
                           .FirstOrDefault(d => d.IdProduccion == IdTemporal && d.IdEmpleadoPro == empleadoRec.idempleado);

                        if (existinProdEmp != null)
                        {
                            existinProdEmp.IdEmpleadoPro = empleadoRec.idempleado;
                            existinProdEmp.IdEstado = 1;
                            existinProdEmp.TxtEstado = "ACTIVO";

                            _context.Pret20ProduccionEmpleados.Update(existinProdEmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadospro)
                    {
                        var existingProEmp = _context.Pret20ProduccionEmpleados
                           .FirstOrDefault(d => d.IdProduccion == IdTemporal && d.IdEmpleadoPro == EmpleadoExt.idempleado);

                        if (existingProEmp == null)
                        {
                            var Pret22RecEmp = new Pret20ProduccionEmpleado
                            {
                                IdProduccion = (long)(IdTemporal),
                                IdEmpleadoPro = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret20ProduccionEmpleados.Add(Pret22RecEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret20ProduccionEmpleados
                        .Where(d => d.IdProduccion == IdTemporal && !idempleadoRecEnArreglo.Contains((long)d.IdEmpleadoPro))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtemp)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    await _context.SaveChangesAsync();
                    //if (existingProduccion.IdEstado == 4)
                    //{
                    //    existingEnvios.IdEstado = 6;
                    //    existingEnvios.TxtEstado = "RECEPCIONADO";

                    //}
                }
                else
                {
                    var cantidad = _context.Pret14Produccions./*Where(e => e.IdEstado != 5 && e.IdEstado != 2).*/Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nropro = txt_serie + "-" + txt_numero;
                    var conjuntoproducto = _context.Prot09Productos.Where(p => p.IdProducto == IdTemporalIns).Select(p => new { p.IdUm, p.TxtDesc }).FirstOrDefault();
                    var Pret14Produccion = new Pret14Produccion
                    {
                        IdExtraccion = IdTemporalExtraccion,
                        IdPredio = (long)IdTemporalPredio,
                        IdCampana = (long)IdTemporalCampana,
                        IdProductoIns = (long)IdTemporalIns,
                        NroPro = txt_numero,
                        ComentarioPro = comentarioPro,
                        FechaProduccion = FechaPro,
                        CantidadIns = cantidad,
                        IdUmIns = (int)conjuntoproducto.IdUm,
                        TxtProIns = conjuntoproducto.TxtDesc,
                        TipoPro = tipopro,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check == true ? 3 : 4,
                        TxtEstado = check == true ? "BORRADOR" : "ACTIVO",
                    };

                    _context.Pret14Produccions.Add(Pret14Produccion);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret14Produccion.IdProduccion;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {

                        foreach (ProductoSp producto in productosseleccionados)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            var Pret15ProDet = new Pret15ProduccionDtl
                            {
                                IdProduccion = (long)(IdTemporal),
                                IdProductoPro = producto.idProducto,
                                TxtProPro = producto.nombreProducto,
                                IdUmPro = idum,
                                CantidadPro = producto.cantidad,
                                TotalProp = (double)producto.total,
                                TxtComentario = producto.observacion,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret15ProduccionDtls.Add(Pret15ProDet);
                            await _context.SaveChangesAsync();


                        }
                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadospro)
                        {

                            var EmpleadoPro = new Pret20ProduccionEmpleado
                            {
                                IdProduccion = (long)(IdTemporal),
                                IdEmpleadoPro = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret20ProduccionEmpleados.Add(EmpleadoPro);
                            await _context.SaveChangesAsync();


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
        public async Task<IActionResult> CrearProduccionyCerrar(DateTime FechaPro, string? comentarioPro, int cinsPro, string productosSeleccionado,
      string arregloempleadospro, string tipopro, bool check)
        {
            try
            {
                var productosseleccionados = JsonConvert.DeserializeObject<List<ProductoSp>>(productosSeleccionado);
                var empleadosseleccionadospro = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadospro);

                var existingProduccion = _context.Pret14Produccions.AsNoTracking()
           .FirstOrDefault(c => c.IdProduccion == IdTemporal);

                if (existingProduccion != null)
                {
                    var conjuntoproducto = _context.Prot09Productos.Where(p => p.IdProducto == IdTemporalIns).Select(p => new { p.IdUm, p.TxtDesc }).FirstOrDefault();

                    existingProduccion.IdExtraccion = IdTemporalExtraccion;
                    existingProduccion.IdPredio = (long)IdTemporalPredio;
                    existingProduccion.IdCampana = (long)IdTemporalCampana;
                    existingProduccion.IdProductoIns = (long)IdTemporalIns;
                    existingProduccion.ComentarioPro = comentarioPro;
                    existingProduccion.FechaProduccion = FechaPro;
                    existingProduccion.CantidadIns = cinsPro;
                    existingProduccion.TipoPro = tipopro;
                    existingProduccion.IdUsuarioModificador = idusuario;
                    existingProduccion.IdUmIns = (int)conjuntoproducto.IdUm;
                    existingProduccion.TxtProIns = conjuntoproducto.TxtDesc;
                    if (existingProduccion.IdEstado == 3)
                    {
                        existingProduccion.IdEstado = check == true ? 3 : 4;
                        existingProduccion.TxtEstado = check == true ? "BORRADOR" : "ACTIVO";
                    }
                    existingProduccion.TxtUsuarioModificador = txtusuario;
                    existingProduccion.FechaModificacion = fechaHoy;
                    existingProduccion.TipoPro = tipopro;

                    _context.Pret14Produccions.Update(existingProduccion);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingProduccion.IdProduccion;

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idProductosEnArreglo = productosseleccionados.Select(p => p.idProducto).ToList();

                    // Actualizar registros existentes
                    foreach (ProductoSp producto in productosseleccionados)
                    {
                        var existingProdDet = _context.Pret15ProduccionDtls
                           .FirstOrDefault(d => d.IdProduccion == IdTemporal && d.IdProductoPro == producto.idProducto);

                        if (existingProdDet != null)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            // Actualiza los valores
                            existingProdDet.IdProductoPro = producto.idProducto;
                            existingProdDet.TxtProPro = producto.nombreProducto;
                            existingProdDet.IdUmPro = idum;
                            existingProdDet.CantidadPro = producto.cantidad;
                            existingProdDet.TxtComentario = producto.observacion;
                            existingProdDet.TotalProp = (double)producto.total;
                            existingProdDet.IdEstado = 1;
                            existingProdDet.TxtEstado = "ACTIVO";

                            _context.Pret15ProduccionDtls.Update(existingProdDet);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (ProductoSp producto in productosseleccionados)
                    {
                        var existingproddtll = _context.Pret15ProduccionDtls
                           .FirstOrDefault(d => d.IdProduccion == IdTemporal && d.IdProductoPro == producto.idProducto);

                        if (existingproddtll == null)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            // Crea un nuevo registro
                            var pret15proddtl = new Pret15ProduccionDtl
                            {
                                IdProduccion = (long)(IdTemporal),
                                IdProductoPro = producto.idProducto,
                                TxtProPro = producto.nombreProducto,
                                IdUmPro = idum,
                                CantidadPro = producto.cantidad,
                                TotalProp = (double)producto.total,
                                TxtComentario = producto.observacion,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret15ProduccionDtls.Add(pret15proddtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminarProdtl = _context.Pret15ProduccionDtls
                        .Where(d => d.IdProduccion == IdTemporal && !idProductosEnArreglo.Contains((long)d.IdProductoPro))
                        .ToList();
                    foreach (var registro in registrosParaEliminarProdtl)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    //_context.Tnst05CompEmitidoDtls.RemoveRange(registrosParaEliminar);
                    await _context.SaveChangesAsync();

                    var idEmpleadosEnArreglo = empleadosseleccionadospro.Select(p => p.idempleado).ToList();



                    //EMPLEADO
                    //ACTUALIZAR
                    var idempleadoRecEnArreglo = empleadosseleccionadospro.Select(p => p.idempleado).ToList();
                    foreach (EmpleadoExt empleadoRec in empleadosseleccionadospro)
                    {
                        var existinProdEmp = _context.Pret20ProduccionEmpleados
                           .FirstOrDefault(d => d.IdProduccion == IdTemporal && d.IdEmpleadoPro == empleadoRec.idempleado);

                        if (existinProdEmp != null)
                        {
                            existinProdEmp.IdEmpleadoPro = empleadoRec.idempleado;
                            existinProdEmp.IdEstado = 1;
                            existinProdEmp.TxtEstado = "ACTIVO";

                            _context.Pret20ProduccionEmpleados.Update(existinProdEmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadospro)
                    {
                        var existingProEmp = _context.Pret20ProduccionEmpleados
                           .FirstOrDefault(d => d.IdProduccion == IdTemporal && d.IdEmpleadoPro == EmpleadoExt.idempleado);

                        if (existingProEmp == null)
                        {
                            var Pret22RecEmp = new Pret20ProduccionEmpleado
                            {
                                IdProduccion = (long)(IdTemporal),
                                IdEmpleadoPro = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret20ProduccionEmpleados.Add(Pret22RecEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret20ProduccionEmpleados
                        .Where(d => d.IdProduccion == IdTemporal && !idempleadoRecEnArreglo.Contains((long)d.IdEmpleadoPro))
                        .ToList();

                    foreach (var registro in registrosParaActualizarExtemp)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    await _context.SaveChangesAsync();
                    //if (existingProduccion.IdEstado == 4)
                    //{
                    //    existingEnvios.IdEstado = 6;
                    //    existingEnvios.TxtEstado = "RECEPCIONADO";

                    //}
                }
                else
                {
                    var cantidad = _context.Pret14Produccions./*Where(e => e.IdEstado != 5 && e.IdEstado != 2).*/Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nropro = txt_serie + "-" + txt_numero;
                    var conjuntoproducto = _context.Prot09Productos.Where(p => p.IdProducto == IdTemporalIns).Select(p => new { p.IdUm, p.TxtDesc }).FirstOrDefault();
                    var Pret14Produccion = new Pret14Produccion
                    {
                        IdExtraccion = IdTemporalExtraccion,
                        IdPredio = (long)IdTemporalPredio,
                        IdCampana = (long)IdTemporalCampana,
                        IdProductoIns = (long)IdTemporalIns,
                        NroPro = txt_numero,
                        ComentarioPro = comentarioPro,
                        FechaProduccion = FechaPro,
                        CantidadIns = cantidad,
                        IdUmIns = (int)conjuntoproducto.IdUm,
                        TxtProIns = conjuntoproducto.TxtDesc,
                        TipoPro = tipopro,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check == true ? 3 : 4,
                        TxtEstado = check == true ? "BORRADOR" : "ACTIVO",
                    };

                    _context.Pret14Produccions.Add(Pret14Produccion);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret14Produccion.IdProduccion;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {

                        foreach (ProductoSp producto in productosseleccionados)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            var Pret15ProDet = new Pret15ProduccionDtl
                            {
                                IdProduccion = (long)(IdTemporal),
                                IdProductoPro = producto.idProducto,
                                TxtProPro = producto.nombreProducto,
                                IdUmPro = idum,
                                CantidadPro = producto.cantidad,
                                TotalProp = (double)producto.total,
                                TxtComentario = producto.observacion,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret15ProduccionDtls.Add(Pret15ProDet);
                            await _context.SaveChangesAsync();


                        }
                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadospro)
                        {

                            var EmpleadoPro = new Pret20ProduccionEmpleado
                            {
                                IdProduccion = (long)(IdTemporal),
                                IdEmpleadoPro = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret20ProduccionEmpleados.Add(EmpleadoPro);
                            await _context.SaveChangesAsync();


                        }

                    }



                }
                await _context.SaveChangesAsync();

                IdTemporal = null;
                IdTemporalPredio = null;
                IdTemporalCampana = null;
                IdTemporalExtraccion = null;
                IdTemporalIns = null;
                return Json(new { redirectUrl = Url.Action("ListadoPro", "Pret14Produccion") });

            }

            catch (Exception ex)
            {
                return Json(new { errores = new List<string> { ex.Message, ex.StackTrace } });
            }

        }
        [HttpPost]
        public async Task<IActionResult> EliminarProduccion(long id)
        {
            try
            {
                var existingProduccion = _context.Pret14Produccions.Where(p => p.IdProduccion == id).FirstOrDefault();

                if (existingProduccion != null)
                {
                    var existingMerma = _context.Pret16Mermas.Where(p => p.IdProduccion == id).ToList();

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
                    var existingEmpleado = _context.Pret20ProduccionEmpleados.Where(p => p.IdProduccion == id).ToList();


                    foreach (Pret20ProduccionEmpleado empleado in existingEmpleado)
                    {
                        //empleado.IdEstado = 0;
                        //empleado.TxtEstado = "INACTIVO";
                        _context.Pret20ProduccionEmpleados.Remove(empleado);
                        await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                    var existingDtll = _context.Pret15ProduccionDtls.Where(p => p.IdProduccion == id).ToList();

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
                    _context.Pret14Produccions.Remove(existingProduccion);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("ListadoPro", "Pret14Produccion") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AnularProduccion(long id)
        {
            try
            {
                var existingPro = _context.Pret14Produccions.Where(c => c.IdProduccion == id).ToList();
                foreach (Pret14Produccion Produccion in existingPro)
                {
                    Produccion.IdEstado = 5;
                    Produccion.TxtEstado = "ANULADO";
                }
                await _context.SaveChangesAsync();

                if (existingPro != null)
                {

                    var existingEmpleado = _context.Pret20ProduccionEmpleados.Where(p => p.IdEstado == 1 && p.IdProduccion == id).ToList();


                    foreach (Pret20ProduccionEmpleado empleado in existingEmpleado)
                    {
                        empleado.IdEstado = 2;
                        empleado.TxtEstado = "INACTIVO";
                        //_context.Pret21EnvioEmpleados.Remove(empleado);
                        //await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                    var existingDtll = _context.Pret15ProduccionDtls.Where(p => p.IdEstado == 1 && p.IdProduccion == id).ToList();

                    if (existingDtll != null)
                    {
                        foreach (Pret15ProduccionDtl Detalle in existingDtll)
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

                return Json(new { redirectUrl = Url.Action("ListadoPro", "Pret14Produccion") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }


    }
}

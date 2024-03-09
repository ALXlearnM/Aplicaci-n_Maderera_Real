using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Data.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Maderera_Aplicacion_Web.Models;
using Newtonsoft.Json;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pret16MermaController : Controller
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
        private static long? IdTemporalProduccion { get; set; }

        public Pret16MermaController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Details(int? idmerma)
        {

            var pret16Merma = await _context.Pret16Mermas
                .Include(p => p.IdCampanaNavigation)
                .Include(p => p.IdPredioNavigation)
                .Include(p => p.IdProduccionNavigation)
                .FirstOrDefaultAsync(m => m.IdMerma == idmerma);
            if (pret16Merma == null)
            {
                return NotFound();
            }

            return View(pret16Merma);
        }

        [HttpGet]
        public IActionResult Merma()
        {
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
            ViewBag.EmpMer = empleadosventas;
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
            ViewBag.Produccionmer = _context.Pret14Produccions.Select(c => new
            {
                id = c.IdProduccion,
                codigo = c.NroPro,
                nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                fechaini = c.FechaProduccion
            })
                   .ToList();
            ViewBag.Productosm = _context.Prot09Productos
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
        public IActionResult EditarMerma(long idmerma)
        {
            IdTemporal = null;
            IdTemporalCampana = null;
            IdTemporalPredio = null;
            IdTemporalProduccion = null;
            var merma = _context.Pret16Mermas
            .Include(t => t.IdProduccionNavigation)
            .Include(t => t.IdPredioNavigation)
            .Include(t => t.IdCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdMerma == idmerma)
            .Select(campanaTA => new
            {
                all = campanaTA,
                CampanaTA = campanaTA,
                idpro = campanaTA.IdProduccion,
                codigo = campanaTA.IdProduccionNavigation.NroPro,
                predio = campanaTA.IdPredioNavigation.UnidadCatastral,
                campana = campanaTA.IdCampanaNavigation.CodigoCampana,
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
            ViewBag.EmpMer = empleadosventas;
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
            ViewBag.Produccionmer = _context.Pret14Produccions.Select(c => new
            {
                id = c.IdProduccion,
                codigo = c.NroPro,
                nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                fechaini = c.FechaProduccion
            })
                   .ToList();
            ViewBag.Productosm = _context.Prot09Productos
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
            var mermavista = _context.Pret16Mermas
            .Where(c => c.IdMerma == idmerma)
            .FirstOrDefault();
            if (merma != null)
            {

                var detallesmerma = _context.Pret17MermaDtls
                .Where(detalle => detalle.IdMerma == idmerma && detalle.IdEstado == 1)
                .Select(p => new
                {
                    id = p.IdProductoMer,
                    np = p.TxtProMer,
                    cu = p.IdProductoMerNavigation.MtoPvpuConIgv,
                    um = p.IdUmMerNavigation.TxtDesc,
                    can = p.CantidadMer,
                    total = p.TotalMer,
                    obs = p.TxtComentario
                }).ToList();

                var Empmerma = _context.Pret24MermaEmpleados
                .Where(detalle => detalle.IdMerma == idmerma && detalle.IdEstado == 1)
                .Select(
                    n => new
                    {
                        idempleado = n.IdEmpleadoMer,
                        txtempleado = n.IdEmpleadoMerNavigation.TxtPriNom == null ? n.IdEmpleadoMerNavigation.TxtRznSocial : $"{n.IdEmpleadoMerNavigation.TxtPriNom} {n.IdEmpleadoMerNavigation.TxtApePat}",
                        nrodoc = String.IsNullOrEmpty(n.IdEmpleadoMerNavigation.NroRuc) ? n.IdEmpleadoMerNavigation.NroDoc : n.IdEmpleadoMerNavigation.NroRuc,
                        condicion = n.IdEmpleadoMerNavigation.IdCondicionLaboralNavigation.TxtDesc,
                        telefono = !string.IsNullOrEmpty(n.IdEmpleadoMerNavigation.Celular1) ? n.IdEmpleadoMerNavigation.Celular1 : (!string.IsNullOrEmpty(n.IdEmpleadoMerNavigation.Celular2) ? n.IdEmpleadoMerNavigation.Celular2 : n.IdEmpleadoMerNavigation.Celular3),
                        categoria = n.IdEmpleadoMerNavigation.IdCategoriaEmpNavigation.TxtNombre,

                    })
                .ToList();
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(merma, settings);
                var detallesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(detallesmerma, settings);
                var empleadosSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Empmerma, settings);

                IdTemporal = mermavista?.IdMerma;
                IdTemporalProduccion = mermavista?.IdProduccion;
                IdTemporalCampana = mermavista?.IdCampana;
                IdTemporalPredio = mermavista?.IdPredio;
                ViewBag.Merma = campanaserialized;
                ViewBag.Detalles = detallesSerialized;
                ViewBag.Empleados = empleadosSerialized;
                return View("merma", mermavista);
            }
            else
            {
                return NotFound(); // O redirigir a otra página
            }
        }
        [HttpGet]

        public IActionResult Recargarpromer()
        {
            try
            {

                var updatedExtenv = _context.Pret14Produccions.Select(c => new
                {
                    id = c.IdProduccion,
                    codigo = c.NroPro,
                    nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                    fechaini = c.FechaProduccion
                })
                   .ToList();

                return Json(updatedExtenv);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar la Campan a merma: " + ex.Message);
            }
        }
        [HttpGet]

        public IActionResult VerificarPro(long idPro)
        {
            try
            {

                var updatedExtenv = _context.Pret14Produccions.Where(c => c.IdProduccion == idPro).Include(c => c.IdCampanaNavigation).ThenInclude(c => c.IdPredioNavigation).Select(c => new
                {
                    id = c.IdProduccion,
                    codigo = c.NroPro,
                    nrositio = c.IdCampanaNavigation.IdPredioNavigation.UnidadCatastral,
                    fechaini = c.FechaProduccion,
                    idC = c.IdCampana,
                    idP = c.IdPredio
                })
                   .FirstOrDefault();

                IdTemporalProduccion = idPro;
                IdTemporalCampana = updatedExtenv.idC;
                IdTemporalPredio = updatedExtenv.idP;



                return Json(updatedExtenv);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar la Campan a merma: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult removeIdTemporalPro()
        {
            IdTemporalProduccion = null;
            IdTemporalPredio = null;
            IdTemporalCampana = null;
            return Json(new { success = true });
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
        [HttpGet]
        public IActionResult SelecCampana(long idCampana)
        {
            IdTemporalCampana = idCampana;
            return Json(new { success = true });


        }
        [HttpGet]
        public IActionResult Recargarempmer([FromQuery] List<long> listaIds)
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
        public IActionResult Empleadomer(long empleadoID)
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
        public IActionResult RecargarProductom(List<long> productosSeleccionadosIds)
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
        public IActionResult CerrarMerma()
        {
            var redirectUrl = Url.Action("ListadoMer", "Pret16Merma");
            var response = new { redirectUrl };
            IdTemporal = null;
            IdTemporalPredio = null;
            IdTemporalProduccion = null;
            IdTemporalCampana = null;
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> CrearMerma(DateTime FechaMer, string? comentarioMer, string productosSeleccionado,
       string arregloempleadosmer, string tipomer, bool check)
        {
            try
            {
                var productosseleccionados = JsonConvert.DeserializeObject<List<ProductoSm>>(productosSeleccionado);
                var empleadosseleccionadospro = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadosmer);

                var existingMerma = _context.Pret16Mermas.AsNoTracking()
           .FirstOrDefault(c => c.IdMerma == IdTemporal);

                if (existingMerma != null)
                {

                    existingMerma.IdProduccion = IdTemporalProduccion;
                    existingMerma.IdPredio = (long)IdTemporalPredio;
                    existingMerma.IdCampana = (long)IdTemporalCampana;
                    existingMerma.Comentario = comentarioMer;
                    existingMerma.FechaMerma = FechaMer;
                    existingMerma.TipoMerma = tipomer;
                    existingMerma.IdUsuarioModificador = idusuario;
                    if (existingMerma.IdEstado == 3)
                    {
                        existingMerma.IdEstado = check == true ? 3 : 1;
                        existingMerma.TxtEstado = check == true ? "BORRADOR" : "ACTIVO";
                    }
                    existingMerma.TxtUsuarioModificador = txtusuario;
                    existingMerma.FechaModificacion = fechaHoy;

                    _context.Pret16Mermas.Update(existingMerma);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingMerma.IdMerma;

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idProductosEnArreglo = productosseleccionados.Select(p => p.idProducto).ToList();

                    // Actualizar registros existentes
                    foreach (ProductoSm producto in productosseleccionados)
                    {
                        var existingmERDet = _context.Pret17MermaDtls
                           .FirstOrDefault(d => d.IdMerma == IdTemporal && d.IdProductoMer == producto.idProducto);

                        if (existingmERDet != null)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            // Actualiza los valores
                            existingmERDet.IdProductoMer = producto.idProducto;
                            existingmERDet.TxtProMer = producto.nombreProducto;
                            existingmERDet.IdUmMer = idum;
                            existingmERDet.CantidadMer = producto.cantidad;
                            existingmERDet.TxtComentario = producto.tipormerma;
                            existingmERDet.TotalMer = (double)producto.total;
                            existingmERDet.IdEstado = 1;
                            existingmERDet.TxtEstado = "ACTIVO";

                            _context.Pret17MermaDtls.Update(existingmERDet);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (ProductoSm producto in productosseleccionados)
                    {
                        var existingmerdtll = _context.Pret17MermaDtls
                           .FirstOrDefault(d => d.IdMerma == IdTemporal && d.IdProductoMer == producto.idProducto);

                        if (existingmerdtll == null)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            // Crea un nuevo registro
                            var pret17merdtl = new Pret17MermaDtl
                            {
                                IdMerma = (long)(IdTemporal),
                                IdProductoMer = producto.idProducto,
                                TxtProMer = producto.nombreProducto,
                                IdUmMer = idum,
                                CantidadMer = producto.cantidad,
                                TotalMer = (double)producto.total,
                                TxtComentario = producto.tipormerma,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret17MermaDtls.Add(pret17merdtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminarMerDtl = _context.Pret17MermaDtls
                        .Where(d => d.IdMerma == IdTemporal && !idProductosEnArreglo.Contains((long)d.IdProductoMer))
                        .ToList();
                    foreach (var registro in registrosParaEliminarMerDtl)
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
                        var existinMerEmp = _context.Pret24MermaEmpleados
                           .FirstOrDefault(d => d.IdMerma == IdTemporal && d.IdEmpleadoMer == empleadoRec.idempleado);

                        if (existinMerEmp != null)
                        {
                            existinMerEmp.IdEmpleadoMer = empleadoRec.idempleado;
                            existinMerEmp.IdEstado = 1;
                            existinMerEmp.TxtEstado = "ACTIVO";

                            _context.Pret24MermaEmpleados.Update(existinMerEmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadospro)
                    {
                        var existingMerEmp = _context.Pret24MermaEmpleados
                           .FirstOrDefault(d => d.IdMerma == IdTemporal && d.IdEmpleadoMer == EmpleadoExt.idempleado);

                        if (existingMerEmp == null)
                        {
                            var Pret24MerEmp = new Pret24MermaEmpleado
                            {
                                IdMerma = (long)(IdTemporal),
                                IdEmpleadoMer = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret24MermaEmpleados.Add(Pret24MerEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret24MermaEmpleados
                        .Where(d => d.IdMerma == IdTemporal && !idempleadoRecEnArreglo.Contains((long)d.IdEmpleadoMer))
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
                    var cantidad = _context.Pret16Mermas./*Where(e => e.IdEstado != 5 && e.IdEstado != 2).*/Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nromer = txt_serie + "-" + txt_numero;
                    var Pret16Merma = new Pret16Merma
                    {
                        IdProduccion = IdTemporalProduccion,
                        IdPredio = (long)IdTemporalPredio,
                        IdCampana = (long)IdTemporalCampana,
                        NroMer = nromer,
                        Comentario = comentarioMer,
                        FechaMerma = FechaMer,
                        TipoMerma = tipomer,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check == true ? 3 : 1,
                        TxtEstado = check == true ? "BORRADOR" : "ACTIVO",
                    };

                    _context.Pret16Mermas.Add(Pret16Merma);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret16Merma.IdMerma;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {

                        foreach (ProductoSm producto in productosseleccionados)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            var Pret17MerDet = new Pret17MermaDtl
                            {
                                IdMerma = (long)(IdTemporal),
                                IdProductoMer = producto.idProducto,
                                TxtProMer = producto.nombreProducto,
                                IdUmMer = idum,
                                CantidadMer = producto.cantidad,
                                TotalMer = (double)producto.total,
                                TxtComentario = producto.tipormerma,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret17MermaDtls.Add(Pret17MerDet);
                            await _context.SaveChangesAsync();


                        }
                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadospro)
                        {

                            var EmpleadoMer = new Pret24MermaEmpleado
                            {
                                IdMerma = (long)(IdTemporal),
                                IdEmpleadoMer = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret24MermaEmpleados.Add(EmpleadoMer);
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
        public async Task<IActionResult> CrearMermayCerrar(DateTime FechaMer, string? comentarioMer, string productosSeleccionado,
       string arregloempleadosmer, string tipomer, bool check)
        {
            try
            {
                var productosseleccionados = JsonConvert.DeserializeObject<List<ProductoSm>>(productosSeleccionado);
                var empleadosseleccionadospro = JsonConvert.DeserializeObject<List<EmpleadoExt>>(arregloempleadosmer);

                var existingMerma = _context.Pret16Mermas.AsNoTracking()
           .FirstOrDefault(c => c.IdMerma == IdTemporal);

                if (existingMerma != null)
                {

                    existingMerma.IdProduccion = IdTemporalProduccion;
                    existingMerma.IdPredio = (long)IdTemporalPredio;
                    existingMerma.IdCampana = (long)IdTemporalCampana;
                    existingMerma.Comentario = comentarioMer;
                    existingMerma.FechaMerma = FechaMer;
                    existingMerma.TipoMerma = tipomer;
                    existingMerma.IdUsuarioModificador = idusuario;
                    if (existingMerma.IdEstado == 3)
                    {
                        existingMerma.IdEstado = check == true ? 3 : 1;
                        existingMerma.TxtEstado = check == true ? "BORRADOR" : "ACTIVO";
                    }
                    existingMerma.TxtUsuarioModificador = txtusuario;
                    existingMerma.FechaModificacion = fechaHoy;

                    _context.Pret16Mermas.Update(existingMerma);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingMerma.IdMerma;

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idProductosEnArreglo = productosseleccionados.Select(p => p.idProducto).ToList();

                    // Actualizar registros existentes
                    foreach (ProductoSm producto in productosseleccionados)
                    {
                        var existingmERDet = _context.Pret17MermaDtls
                           .FirstOrDefault(d => d.IdMerma == IdTemporal && d.IdProductoMer == producto.idProducto);

                        if (existingmERDet != null)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            // Actualiza los valores
                            existingmERDet.IdProductoMer = producto.idProducto;
                            existingmERDet.TxtProMer = producto.nombreProducto;
                            existingmERDet.IdUmMer = idum;
                            existingmERDet.CantidadMer = producto.cantidad;
                            existingmERDet.TxtComentario = producto.tipormerma;
                            existingmERDet.TotalMer = (double)producto.total;
                            existingmERDet.IdEstado = 1;
                            existingmERDet.TxtEstado = "ACTIVO";

                            _context.Pret17MermaDtls.Update(existingmERDet);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (ProductoSm producto in productosseleccionados)
                    {
                        var existingmerdtll = _context.Pret17MermaDtls
                           .FirstOrDefault(d => d.IdMerma == IdTemporal && d.IdProductoMer == producto.idProducto);

                        if (existingmerdtll == null)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            // Crea un nuevo registro
                            var pret17merdtl = new Pret17MermaDtl
                            {
                                IdMerma = (long)(IdTemporal),
                                IdProductoMer = producto.idProducto,
                                TxtProMer = producto.nombreProducto,
                                IdUmMer = idum,
                                CantidadMer = producto.cantidad,
                                TotalMer = (double)producto.total,
                                TxtComentario = producto.tipormerma,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret17MermaDtls.Add(pret17merdtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminarMerDtl = _context.Pret17MermaDtls
                        .Where(d => d.IdMerma == IdTemporal && !idProductosEnArreglo.Contains((long)d.IdProductoMer))
                        .ToList();
                    foreach (var registro in registrosParaEliminarMerDtl)
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
                        var existinMerEmp = _context.Pret24MermaEmpleados
                           .FirstOrDefault(d => d.IdMerma == IdTemporal && d.IdEmpleadoMer == empleadoRec.idempleado);

                        if (existinMerEmp != null)
                        {
                            existinMerEmp.IdEmpleadoMer = empleadoRec.idempleado;
                            existinMerEmp.IdEstado = 1;
                            existinMerEmp.TxtEstado = "ACTIVO";

                            _context.Pret24MermaEmpleados.Update(existinMerEmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //CREAR
                    foreach (EmpleadoExt EmpleadoExt in empleadosseleccionadospro)
                    {
                        var existingMerEmp = _context.Pret24MermaEmpleados
                           .FirstOrDefault(d => d.IdMerma == IdTemporal && d.IdEmpleadoMer == EmpleadoExt.idempleado);

                        if (existingMerEmp == null)
                        {
                            var Pret24MerEmp = new Pret24MermaEmpleado
                            {
                                IdMerma = (long)(IdTemporal),
                                IdEmpleadoMer = EmpleadoExt.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret24MermaEmpleados.Add(Pret24MerEmp);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var registrosParaActualizarExtemp = _context.Pret24MermaEmpleados
                        .Where(d => d.IdMerma == IdTemporal && !idempleadoRecEnArreglo.Contains((long)d.IdEmpleadoMer))
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
                    var cantidad = _context.Pret16Mermas./*Where(e => e.IdEstado != 5 && e.IdEstado != 2).*/Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string nromer = txt_serie + "-" + txt_numero;
                    var Pret16Merma = new Pret16Merma
                    {
                        IdProduccion = IdTemporalProduccion,
                        IdPredio = (long)IdTemporalPredio,
                        IdCampana = (long)IdTemporalCampana,
                        NroMer = nromer,
                        Comentario = comentarioMer,
                        FechaMerma = FechaMer,
                        TipoMerma = tipomer,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check == true ? 3 : 1,
                        TxtEstado = check == true ? "BORRADOR" : "ACTIVO",
                    };

                    _context.Pret16Mermas.Add(Pret16Merma);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret16Merma.IdMerma;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {

                        foreach (ProductoSm producto in productosseleccionados)
                        {
                            var idum = _context.Prot09Productos.Where(p => p.IdProducto == producto.idProducto).Select(p => p.IdUm).FirstOrDefault();
                            var Pret17MerDet = new Pret17MermaDtl
                            {
                                IdMerma = (long)(IdTemporal),
                                IdProductoMer = producto.idProducto,
                                TxtProMer = producto.nombreProducto,
                                IdUmMer = idum,
                                CantidadMer = producto.cantidad,
                                TotalMer = (double)producto.total,
                                TxtComentario = producto.tipormerma,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret17MermaDtls.Add(Pret17MerDet);
                            await _context.SaveChangesAsync();


                        }
                        foreach (EmpleadoExt EmpleadosExt in empleadosseleccionadospro)
                        {

                            var EmpleadoMer = new Pret24MermaEmpleado
                            {
                                IdMerma = (long)(IdTemporal),
                                IdEmpleadoMer = EmpleadosExt.idempleado,
                                //SalarioEmp = EmpleadosExt.salario,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret24MermaEmpleados.Add(EmpleadoMer);
                            await _context.SaveChangesAsync();


                        }

                    }



                }
                await _context.SaveChangesAsync();
                IdTemporal = null;
                IdTemporalPredio = null;
                IdTemporalCampana = null;
                IdTemporalProduccion = null;
                return Json(new { redirectUrl = Url.Action("ListadoMer", "Pret16Merma") });

            }

            catch (Exception ex)
            {
                return Json(new { errores = new List<string> { ex.Message, ex.StackTrace } });
            }

        }



        public IActionResult CargarMood()
        {
            if (IdTemporal != null)
            {
                var estado = _context.Pret16Mermas.Where(t => t.IdMerma == IdTemporal).Select(t => t.IdEstado).FirstOrDefault();
                var estadoName = _context.Pret16Mermas.Where(t => t.IdMerma == IdTemporal).Select(t => t.TxtEstado).FirstOrDefault();
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
        public async Task<IActionResult> EliminarMerma(long id)
        {
            try
            {
                var existingMerma = _context.Pret16Mermas.Where(p => p.IdMerma == id).FirstOrDefault();

                if (existingMerma != null)
                {

                    var existingEmpleado = _context.Pret24MermaEmpleados.Where(p => p.IdMerma == id).ToList();


                    foreach (Pret24MermaEmpleado empleado in existingEmpleado)
                    {
                        //empleado.IdEstado = 0;
                        //empleado.TxtEstado = "INACTIVO";
                        _context.Pret24MermaEmpleados.Remove(empleado);
                        await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                    var existingDtll = _context.Pret17MermaDtls.Where(p => p.IdMerma == id).ToList();

                    if (existingDtll != null)
                    {
                        foreach (Pret17MermaDtl Detalle in existingDtll)
                        {
                            //Detalle.IdEstado = 0;
                            //Detalle.TxtEstado = "INACTIVO";
                            _context.Pret17MermaDtls.Remove(Detalle);
                            await _context.SaveChangesAsync();
                        }
                    }
                    _context.Pret16Mermas.Remove(existingMerma);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("ListadoMer", "Pret16Merma") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AnularMerma(long id)
        {
            try
            {
                var existingmER = _context.Pret16Mermas.Where(c => c.IdMerma == id).ToList();
                foreach (Pret16Merma Produccion in existingmER)
                {
                    Produccion.IdEstado = 5;
                    Produccion.TxtEstado = "ANULADO";
                }
                await _context.SaveChangesAsync();

                if (existingmER != null)
                {

                    var existingEmpleado = _context.Pret24MermaEmpleados.Where(p => p.IdEstado == 1 && p.IdMerma == id).ToList();


                    foreach (Pret24MermaEmpleado empleado in existingEmpleado)
                    {
                        empleado.IdEstado = 0;
                        empleado.TxtEstado = "INACTIVO";
                        //_context.Pret21EnvioEmpleados.Remove(empleado);
                        //await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                    var existingDtll = _context.Pret17MermaDtls.Where(p => p.IdEstado == 1 && p.IdMerma == id).ToList();

                    if (existingDtll != null)
                    {
                        foreach (Pret17MermaDtl Detalle in existingDtll)
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

                return Json(new { redirectUrl = Url.Action("ListadoMer", "Pret16Merma") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }




    }
}

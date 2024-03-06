using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Models;
using Microsoft.CodeAnalysis;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;
using Maderera_Aplicacion_Web.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using Maderera_Aplicacion_Web.Controllers;
using System.Net;

namespace Aplicacion_Maderera.Controllers
{
    [Authorize]
    public class Tnst04CompEmitidoController : Controller
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


        public Tnst04CompEmitidoController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: Tnst04CompEmitido/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Tnst04CompEmitidos == null)
            {
                return NotFound();
            }
            var DetallesForComprobante = _context.Tnst05CompEmitidoDtls.Where(dc => dc.IdCompEmitido == id).ToList();
            ViewBag.DetalledorComp = DetallesForComprobante;
            var tnst04CompEmitido = await _context.Tnst04CompEmitidos
                .Include(t => t.IdCanVtaNavigation)
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdLocationNavigation)
                .Include(t => t.IdLocationToNavigation)
                .Include(t => t.IdMesaNavigation)
                .Include(t => t.IdTipoCompNavigation)
                .Include(t => t.IdTipoMonedaNavigation)
                .Include(t => t.IdTipoOrdenNavigation)
                .Include(t => t.IdTurnoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCompEmitido == id);
            if (tnst04CompEmitido == null)
            {
                return NotFound();
            }
            return View(tnst04CompEmitido);
        }

        public IActionResult boleta_v_listado()
        {
            var listadoComprobantesActivos = _context.Tnst04CompEmitidos
                .Include(c => c.IdTipoCompNavigation)
                .Include(c => c.IdClienteNavigation)
                .Where(c => c.IdEstado == 1 || c.IdEstado == 3 || c.IdEstado == 2)
                .ToList();

            var usuarioEncontrado = _context.Pert01Usuarios
                .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario().ToString());

            if (usuarioEncontrado != null)
            {
                var id_usuario = usuarioEncontrado.IdUsuario;
                // Realizar operaciones con id_usuario
                ViewBag.Kj = id_usuario;
            }
            else
            {
                // No se encontró un usuario con el nombre especificado
                // Puedes manejar esta situación según tus necesidades
                ViewBag.Kj = "No"; // o algún valor predeterminado
            }

            var comprobantesViewModel = listadoComprobantesActivos.Select(comprobante => new
            {
                Comprobante = comprobante,
                NombreCompletoCliente = String.IsNullOrEmpty(comprobante.IdClienteNavigation.TxtRznSocial) ? $"{comprobante.IdClienteNavigation.TxtApePat} {comprobante.IdClienteNavigation.TxtApeMat}, {comprobante.IdClienteNavigation.TxtPriNom} {comprobante.IdClienteNavigation.TxtSegNom}" : comprobante.IdClienteNavigation.TxtRznSocial,
                FecEmi = comprobante.FecEmi,
                MontoN = comprobante.MtoNeto,
                MontoD = comprobante.MtoDsctoTot,
                MontoS = comprobante.MtoSubTot,
                MontoI = comprobante.MtoImptoTot,
                MontoT = comprobante.MtoTotComp,
                Id = comprobante.IdCompEmitido,
                ide = comprobante.IdEstado,
                txte = comprobante.TxtEstado

            }).ToList();

            ViewBag.Comprobantes = comprobantesViewModel;
            return View();
        }


        [HttpGet]
        public IActionResult boleta_v()
        {
            IdTemporal = null;
            IdTemporalCampana = null;
            IdTemporalPredio = null;
            //Usuario
            var id_usuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario()).IdUsuario;
            ViewBag.idUsuario = id_usuario;
            ViewBag.txtUsuario = _pert01UsuarioController.GetUsuario();

            var cantidad = _context.Tnst04CompEmitidos.Count() + 1;
            var txt_serie = "001";
            var txt_numero = cantidad.ToString().PadLeft(7, '0');
            string NroCompEmitido = txt_serie + "-" + txt_numero;
            ViewBag.numpedido = NroCompEmitido;
            //Cliente
            var clientsLocation = _context.Pert02Clientes
                    .Include(e => e.IdNacionalidadNavigation)
                    .Include(e => e.IdDistNavigation)
                        .ThenInclude(d => d.IdProvNavigation)
                            .ThenInclude(p => p.IdDptoNavigation)
                            .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        IdCliente = e.IdCliente,
                        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}," +
                        $" {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                .ToList();
            ViewBag.Cliente = clientsLocation;

            //var choferWithNationalityAndLocation = _context.Pert04Empleados
            //    .Include(e => e.IdNacionalidadNavigation)
            //    .Include(e => e.IdDistNavigation)
            //        .ThenInclude(d => d.IdProvNavigation)
            //            .ThenInclude(p => p.IdDptoNavigation)
            //    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 2)
            //    .Select(e => new
            //    {
            //        Idempleado = e.IdEmpleado,
            //        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
            //        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
            //        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
            //        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
            //    })
            //    .ToList();

            //ViewBag.Chofer = choferWithNationalityAndLocation;

            //var operarioWithNationalityAndLocation = _context.Pert04Empleados
            //    .Include(e => e.IdNacionalidadNavigation)
            //    .Include(e => e.IdDistNavigation)
            //        .ThenInclude(d => d.IdProvNavigation)
            //            .ThenInclude(p => p.IdDptoNavigation)
            //    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 3)
            //    .Select(e => new
            //    {
            //        Idempleado = e.IdEmpleado,
            //        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
            //        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
            //        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
            //        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
            //    })
            //    .ToList();

            //ViewBag.Operario = operarioWithNationalityAndLocation;


            //var AutorizadorWithNationalityAndLocation = _context.Pert04Empleados
            //    .Include(e => e.IdNacionalidadNavigation)
            //    .Include(e => e.IdDistNavigation)
            //        .ThenInclude(d => d.IdProvNavigation)
            //            .ThenInclude(p => p.IdDptoNavigation)
            //    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 4)
            //    .Select(e => new
            //    {
            //        Idempleado = e.IdEmpleado,
            //        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
            //        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
            //        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
            //        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
            //    })
            //    .ToList();

            //ViewBag.Autorizador = AutorizadorWithNationalityAndLocation;
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

            ViewBag.EmpVen = empleadosventas;
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

            var productos = _context.Prot09Productos.Include(p => p.IdUmNavigation).
               Where(p => p.MtoPvpuSinIgv != null && p.AlturaProd != null && p.AnchoProd != null && p.LargoProd != null).Select(
               e => new
               {
                   IdProducto = e.IdProducto,
                   NombreProducto = e.TxtDesc,
                   Monto_si = e.MtoPvpuSinIgv,
                   Monto_ci = e.MtoPvpuConIgv,
                   igv = e.PorImpto,
                   unm = e.IdUmNavigation.TxtDesc,
                   dimension = e.LargoProd + "x" + e.AnchoProd + "x" + e.AlturaProd,
                   diametro = e.DiametroProd,
                   peso = e.PesoProd
               }).ToList();
            ViewBag.Productos = productos;
            ViewData["IdTipoLocation"] = new SelectList(_context.Mstt09TipoLocations, "IdTipoLocation", "TxtDesc");
            //ViewData["IdDist"] = new SelectList(_context.Sntt33Distritos, "IdDist", "TxtDesc");
            //ViewData["IdProv"] = new SelectList(_context.Sntt32Provincia, "IdProv", "TxtDesc");
            ViewData["IdDep"] = new SelectList(_context.Sntt31Departamentos, "IdDpto", "TxtDesc");
            //ViewData["IdLocation"] = new SelectList(_context.Mstt08Locations, "IdLocation", "TxtDesc");

            // Obtén la lista de tipos de órdenes desde tu contexto
            var tipoOrdenes = _context.Mstt03TipoOrdens.ToList();

            // Filtra la lista para excluir elementos con TxtDesc nulo o vacío
            tipoOrdenes = tipoOrdenes.Where(t => !string.IsNullOrEmpty(t.TxtDesc)).ToList();

            // Crea un SelectList con los elementos filtrados
            ViewBag.TipoOrden = new SelectList(tipoOrdenes, "IdTipoOrden", "TxtDesc");

            // Obtén la lista de canales de venta desde tu contexto
            var canalesVenta = _context.Mstt04CanalVta.ToList();

            // Filtra la lista para excluir elementos con TxtDesc nulo o vacío
            canalesVenta = canalesVenta.Where(c => !string.IsNullOrEmpty(c.TxtDesc)).ToList();

            // Crea un SelectList con los elementos filtrados
            ViewBag.CanalVta = new SelectList(canalesVenta, "IdCanVta", "TxtDesc");


            //ViewData["IdMesa"] = new SelectList(_context.Mstt14Mesas, "IdMesa", "IdMesa");
            //ViewData["IdTipoComp"] = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "IdTipoComp");
            //ViewData["IdTipoMoneda"] = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "IdTipoMoneda");
            //ViewData["IdTurno"] = new SelectList(_context.Mstt13Turnos, "IdTurno", "IdTurno");
            //ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }
        [HttpGet]
        public IActionResult ObtenerCampanas(long idPredio)
        {
            // Obtiene las provincias según el departamento seleccionado
            var campanas = _context.Pret02Campanas
                .Where(p => p.IdPredio == idPredio)
                .Select(p => new { idcampana = p.IdCampana, txtDesc = p.CodigoCampana })
                .ToList();

            return Json(campanas);
        }

        [HttpGet]
        public IActionResult Recargarempven([FromQuery] List<long> listaIds)
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
        public IActionResult EmpleadoVen(long empleadoID)
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
        public IActionResult ObtenerProvincias(int departamentoId)
        {
            // Obtiene las provincias según el departamento seleccionado
            var provincias = _context.Sntt32Provincia
                .Where(p => p.IdDpto == departamentoId)
                .Select(p => new { idProv = p.IdProv, txtDesc = p.TxtDesc })
                .ToList();

            return Json(provincias);
        }

        [HttpGet]
        public IActionResult ObtenerDistritos(int provinciaId)
        {
            // Obtiene los distritos según la provincia seleccionada
            var distritos = _context.Sntt33Distritos
                .Where(d => d.IdProv == provinciaId)
                .Select(d => new { idDist = d.IdDist, txtDesc = d.TxtDesc })
                .ToList();

            return Json(distritos);
        }

        [HttpPost]
        public IActionResult CrearCliente(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
          int? tipo_doc, string? nro_doc, string? numtelefono, int? id_distrito, string? direccioncli)
        {
            try
            {
                var codtiper = "N";
                string? dni = null;
                string? ruc = null;

                if (tipo_doc == 1)
                {
                    codtiper = "N";
                    dni = nro_doc;
                }
                else
                {
                    codtiper = "J";
                    ruc = nro_doc;

                }

                // Crea una instancia de la entidad Cliente
                var cliente = new Pert02Cliente
                {
                    TxtPriNom = txt_prim_nomb,
                    TxtSegNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznSocial = razonsocial,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    NroDoc = dni,
                    NroRuc = ruc,
                    CodTipoPer = codtiper,
                    Celular1 = numtelefono,
                    IdDist = id_distrito,
                    TxtDireccion1 = direccioncli
                };

                // Agrega el cliente a tu contexto y guarda los cambios
                _context.Pert02Clientes.Add(cliente);
                _context.SaveChanges();

                var updatedClientsLocation = _context.Pert02Clientes
                    .Include(e => e.IdNacionalidadNavigation)
                    .Include(e => e.IdDistNavigation)
                        .ThenInclude(d => d.IdProvNavigation)
                            .ThenInclude(p => p.IdDptoNavigation)
                            .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        IdCliente = e.IdCliente,
                        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}," +
                        $" {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                .ToList();
                ViewBag.Cliente = updatedClientsLocation;

                return Json(updatedClientsLocation);
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el cliente: " + ex.Message);
            }
        }
        [HttpGet]

        public IActionResult RecargarCliente()
        {
            try
            {
                var updatedClientsLocation = _context.Pert02Clientes
                    .Include(e => e.IdNacionalidadNavigation)
                    .Include(e => e.IdDistNavigation)
                        .ThenInclude(d => d.IdProvNavigation)
                            .ThenInclude(p => p.IdDptoNavigation)
                            .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        IdCliente = e.IdCliente,
                        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}," +
                        $" {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                .ToList();
                ViewBag.Cliente = updatedClientsLocation;

                return Json(updatedClientsLocation);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el cliente: " + ex.Message);
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
        public IActionResult CerrarCampanaVen()
        {
            var redirectUrl = Url.Action("boleta_v_listado", "Tnst04CompEmitido");
            var response = new { redirectUrl };
            IdTemporal = null;
            IdTemporalCampana = null;
            IdTemporalPredio = null;

            return Json(response);
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
        [HttpPost]
        public IActionResult CrearChofer(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
            int? tipo_doc, string? nro_doc, string? numtelefono, int? id_distrito, string? direccion, int? id_cat)
        {
            try
            {
                var codtiper = "N";
                string? dni = null;
                string? ruc = null;

                if (tipo_doc == 1)
                {
                    codtiper = "N";
                    dni = nro_doc;
                }
                else
                {
                    codtiper = "J";
                    ruc = nro_doc;

                }

                // Crea una instancia de la entidad Cliente
                var chofer = new Pert04Empleado
                {
                    TxtPriNom = txt_prim_nomb,
                    TxtSegNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznSocial = razonsocial,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    NroDoc = dni,
                    NroRuc = ruc,
                    CodTipoPer = codtiper,
                    Celular1 = numtelefono,
                    IdDist = id_distrito,
                    TxtDireccion1 = direccion,
                    IdCategoriaEmp = id_cat
                };

                //Agrega el cliente a tu contexto y guarda los cambios
                _context.Pert04Empleados.Add(chofer);
                _context.SaveChanges();
                return Json(new { message = "Chofer creado correctamente" });

            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el chofer: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult RecargarChofer()
        {
            try
            {
                var updateChofer = _context.Pert04Empleados
               .Include(e => e.IdNacionalidadNavigation)
               .Include(e => e.IdDistNavigation)
                   .ThenInclude(d => d.IdProvNavigation)
                       .ThenInclude(p => p.IdDptoNavigation)
               .Where(e => e.Celular1 != null && e.IdDist != null && e.Celular1 != "" && e.IdCategoriaEmp == 2)
               .Select(e => new
               {
                   Idempleado = e.IdEmpleado,
                   NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                   Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                   Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                   Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
               })
               .ToList();

                // Retorna una respuesta exitosa
                return Json(updateChofer);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el chofer: " + ex.Message);
            }
        }

        public IActionResult CrearOperario(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
            int? tipo_doc, string? nro_doc, string? numtelefono, int? id_distrito, string? direccion, int? id_cat)
        {
            try
            {
                var codtiper = "N";
                string? dni = null;
                string? ruc = null;

                if (tipo_doc == 1)
                {
                    codtiper = "N";
                    dni = nro_doc;
                }
                else
                {
                    codtiper = "J";
                    ruc = nro_doc;

                }

                // Crea una instancia de la entidad Empleado
                var operario = new Pert04Empleado
                {
                    TxtPriNom = txt_prim_nomb,
                    TxtSegNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznSocial = razonsocial,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    NroDoc = dni,
                    NroRuc = ruc,
                    CodTipoPer = codtiper,
                    Celular1 = numtelefono,
                    IdDist = id_distrito,
                    TxtDireccion1 = direccion,
                    IdCategoriaEmp = id_cat

                };
                _context.Pert04Empleados.Add(operario);
                _context.SaveChanges();
                return Json(new { message = "Operario creado correctamente" });
                // Agrega el cliente a tu contexto y guarda los cambios


            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Operario: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult RecargarOperario()
        {
            try
            {
                var updateOperario = _context.Pert04Empleados
                .Include(e => e.IdNacionalidadNavigation)
                .Include(e => e.IdDistNavigation)
                    .ThenInclude(d => d.IdProvNavigation)
                        .ThenInclude(p => p.IdDptoNavigation)
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 3)
                .Select(e => new
                {
                    Idempleado = e.IdEmpleado,
                    NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                    Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                    Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                    Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                })
                .ToList();
                // Retorna una respuesta exitosa
                return Json(updateOperario);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el operario: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult ProductoDatos(int idProducto, float cantidad, float descuento)
        {
            try
            {
                var producto = _context.Prot09Productos.FirstOrDefault(p => p.IdProducto == idProducto && p.IdEstado == 1);
                if (producto != null)
                {
                    var productoDetalles = new
                    {
                        IdProducto = producto.IdProducto,
                        NombreProducto = producto.TxtDesc,
                        Monto_si = producto.MtoPvpuSinIgv,
                        Monto_ci = producto.MtoPvpuConIgv,
                        igv = producto.PorImpto,
                        neto = (cantidad) * ((float)(producto.MtoPvpuSinIgv)),
                        descuentoPorcentaje = descuento / 100,
                        mtodescuento = ((descuento / 100)) * (float)(producto.MtoPvpuConIgv) * cantidad,
                        mtoigv = (((float)producto.PorImpto / 100) * ((float)(producto.MtoPvpuSinIgv)) * cantidad),
                        subtotal = (((float)producto.PorImpto / 100) + 1) * ((cantidad) * ((float)(producto.MtoPvpuSinIgv))),
                        total = (((float)producto.PorImpto / 100) + 1) * ((cantidad) * ((float)(producto.MtoPvpuSinIgv))) - ((descuento / 100)) * (float)(producto.MtoPvpuConIgv) * cantidad,

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
        public IActionResult RecargarProducto(List<long> productosSeleccionadosIds)
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
                    unm = e.IdUmNavigation.TxtDesc,
                    dimension = e.LargoProd + "x" + e.AnchoProd + "x" + e.AlturaProd,
                    diametro = e.DiametroProd,
                    peso = e.PesoProd
                })
                .ToList();

            // Retorna una respuesta exitosa
            return Json(productosFiltrados);
        }
        [HttpPost]
        public IActionResult CrearAutorizador(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
           int? tipo_doc, string? nro_doc, string? numtelefono, int? id_distrito, string? direccion, int? id_cat)
        {
            try
            {
                var codtiper = "N";
                string? dni = null;
                string? ruc = null;

                if (tipo_doc == 1)
                {
                    codtiper = "N";
                    dni = nro_doc;
                }
                else
                {
                    codtiper = "J";
                    ruc = nro_doc;

                }


                // Crea una instancia de la entidad Cliente
                var chofer = new Pert04Empleado
                {
                    TxtPriNom = txt_prim_nomb,
                    TxtSegNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznSocial = razonsocial,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    NroDoc = dni,
                    NroRuc = ruc,
                    CodTipoPer = codtiper,
                    Celular1 = numtelefono,
                    IdDist = id_distrito,
                    TxtDireccion1 = direccion,
                    IdCategoriaEmp = id_cat
                };

                // Agrega el cliente a tu contexto y guarda los cambios
                _context.Pert04Empleados.Add(chofer);
                _context.SaveChanges();
                // Retorna una respuesta exitosa
                return Json(new { message = "Autorizador creado correctamente" });
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Autorizador: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult RecargarAutorizador()
        {
            try
            {
                var updateAutorizador = _context.Pert04Empleados
                .Include(e => e.IdNacionalidadNavigation)
                .Include(e => e.IdDistNavigation)
                    .ThenInclude(d => d.IdProvNavigation)
                        .ThenInclude(p => p.IdDptoNavigation)
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 4)
                .Select(e => new
                {
                    Idempleado = e.IdEmpleado,
                    NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                    Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                    Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                    Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                })
                .ToList();
                // Retorna una respuesta exitosa
                return Json(updateAutorizador);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el autorizador: " + ex.Message);
            }
        }

        public IActionResult CrearCortador(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
    int? tipo_doc, string? nro_doc, string? numtelefono, int? id_distrito, string? direccion, int? id_cat)
        {
            try
            {
                var codtiper = "N";
                string? dni = null;
                string? ruc = null;

                if (tipo_doc == 1)
                {
                    codtiper = "N";
                    dni = nro_doc;
                }
                else
                {
                    codtiper = "J";
                    ruc = nro_doc;
                }

                // Crea una instancia de la entidad Cortador
                var cortador = new Pert04Empleado
                {
                    TxtPriNom = txt_prim_nomb,
                    TxtSegNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznSocial = razonsocial,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    NroDoc = dni,
                    NroRuc = ruc,
                    CodTipoPer = codtiper,
                    Celular1 = numtelefono,
                    IdDist = id_distrito,
                    TxtDireccion1 = direccion,
                    IdCategoriaEmp = id_cat
                };

                // Agrega el cortador a tu contexto y guarda los cambios
                _context.Pert04Empleados.Add(cortador);
                _context.SaveChanges();
                // Retorna una respuesta exitosa
                return Json(new { message = "Cortador creado correctamente" });
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Cortador: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult RecargarCortador()
        {
            try
            {
                var updateCortador = _context.Pert04Empleados
                .Include(e => e.IdNacionalidadNavigation)
                .Include(e => e.IdDistNavigation)
                    .ThenInclude(d => d.IdProvNavigation)
                        .ThenInclude(p => p.IdDptoNavigation)
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 5)
                .Select(e => new
                {
                    Idempleado = e.IdEmpleado,
                    NombreCompleto = $"{e.TxtPriNom} {e.TxtApePat}",
                    Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                    Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                    Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                })
                .ToList();
                // Retorna una respuesta exitosa
                return Json(updateCortador);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el cortador: " + ex.Message);
            }
        }

        public IActionResult CrearAserradero(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
    int? tipo_doc, string? nro_doc, string? numtelefono, int? id_distrito, string? direccion, int? id_cat)
        {
            try
            {
                var codtiper = "N";
                string? dni = null;
                string? ruc = null;

                if (tipo_doc == 1)
                {
                    codtiper = "N";
                    dni = nro_doc;
                }
                else
                {
                    codtiper = "J";
                    ruc = nro_doc;
                }

                // Crea una instancia de la entidad Aserradero
                var aserradero = new Pert04Empleado
                {
                    TxtPriNom = txt_prim_nomb,
                    TxtSegNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznSocial = razonsocial,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    NroDoc = dni,
                    NroRuc = ruc,
                    CodTipoPer = codtiper,
                    Celular1 = numtelefono,
                    IdDist = id_distrito,
                    TxtDireccion1 = direccion,
                    IdCategoriaEmp = id_cat
                };

                // Agrega el aserradero a tu contexto y guarda los cambios
                _context.Pert04Empleados.Add(aserradero);
                _context.SaveChanges();
                // Retorna una respuesta exitosa
                return Json(new { message = "Aserradero creado correctamente" });
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Aserradero: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult RecargarAserradero()
        {
            try
            {
                var updateAserradero = _context.Pert04Empleados
                .Include(e => e.IdNacionalidadNavigation)
                .Include(e => e.IdDistNavigation)
                    .ThenInclude(d => d.IdProvNavigation)
                        .ThenInclude(p => p.IdDptoNavigation)
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 6)
                .Select(e => new
                {
                    Idempleado = e.IdEmpleado,
                    NombreCompleto = $"{e.TxtPriNom} {e.TxtApePat}",
                    Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                    Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                    Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                })
                .ToList();
                // Retorna una respuesta exitosa
                return Json(updateAserradero);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el aserradero: " + ex.Message);
            }
        }

        public IActionResult CrearUnimoq(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
            int? tipo_doc, string? nro_doc, string? numtelefono, int? id_distrito, string? direccion, int? id_cat)
        {
            try
            {
                var codtiper = "N";
                string? dni = null;
                string? ruc = null;

                if (tipo_doc == 1)
                {
                    codtiper = "N";
                    dni = nro_doc;
                }
                else
                {
                    codtiper = "J";
                    ruc = nro_doc;
                }

                // Crea una instancia de la entidad Unimoq
                var unimoq = new Pert04Empleado
                {
                    TxtPriNom = txt_prim_nomb,
                    TxtSegNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznSocial = razonsocial,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    NroDoc = dni,
                    NroRuc = ruc,
                    CodTipoPer = codtiper,
                    Celular1 = numtelefono,
                    IdDist = id_distrito,
                    TxtDireccion1 = direccion,
                    IdCategoriaEmp = id_cat
                };

                // Agrega el unimoq a tu contexto y guarda los cambios
                _context.Pert04Empleados.Add(unimoq);
                _context.SaveChanges();
                // Retorna una respuesta exitosa
                return Json(new { message = "Unimoq creado correctamente" });
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Unimoq: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult RecargarUNIM()
        {
            try
            {
                var updateUnimoq = _context.Pert04Empleados
                .Include(e => e.IdNacionalidadNavigation)
                .Include(e => e.IdDistNavigation)
                    .ThenInclude(d => d.IdProvNavigation)
                        .ThenInclude(p => p.IdDptoNavigation)
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 7)
                .Select(e => new
                {
                    Idempleado = e.IdEmpleado,
                    NombreCompleto = $"{e.TxtPriNom} {e.TxtApePat}",
                    Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                    Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                    Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                })
                .ToList();
                // Retorna una respuesta exitosa
                return Json(updateUnimoq);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el unimoq: " + ex.Message);
            }
        }

        public IActionResult CrearCargador(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
            int? tipo_doc, string? nro_doc, string? numtelefono, int? id_distrito, string? direccion, int? id_cat)
        {
            try
            {
                var codtiper = "N";
                string? dni = null;
                string? ruc = null;

                if (tipo_doc == 1)
                {
                    codtiper = "N";
                    dni = nro_doc;
                }
                else
                {
                    codtiper = "J";
                    ruc = nro_doc;
                }

                // Crea una instancia de la entidad Cargador
                var cargador = new Pert04Empleado
                {
                    TxtPriNom = txt_prim_nomb,
                    TxtSegNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznSocial = razonsocial,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    NroDoc = dni,
                    NroRuc = ruc,
                    CodTipoPer = codtiper,
                    Celular1 = numtelefono,
                    IdDist = id_distrito,
                    TxtDireccion1 = direccion,
                    IdCategoriaEmp = id_cat
                };

                // Agrega el cargador a tu contexto y guarda los cambios
                _context.Pert04Empleados.Add(cargador);
                _context.SaveChanges();
                // Retorna una respuesta exitosa
                return Json(new { message = "Cargador creado correctamente" });
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Cargador: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult RecargarCargador()
        {
            try
            {
                var updateCargador = _context.Pert04Empleados
                .Include(e => e.IdNacionalidadNavigation)
                .Include(e => e.IdDistNavigation)
                    .ThenInclude(d => d.IdProvNavigation)
                        .ThenInclude(p => p.IdDptoNavigation)
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 8)
                .Select(e => new
                {
                    Idempleado = e.IdEmpleado,
                    NombreCompleto = $"{e.TxtPriNom} {e.TxtApePat}",
                    Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                    Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                    Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                })
                .ToList();
                // Retorna una respuesta exitosa
                return Json(updateCargador);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el cargador: " + ex.Message);
            }
        }


        [HttpPost]
        public IActionResult CrearLocalizacion(string? nombreloc, DateTime? fechanegloc, int IdTipoLocation, int? id_distrito, string? direccionloc)
        {
            try
            {
                // Crea una instancia de la entidad Cliente
                var Location = new Mstt08Location
                {
                    TxtDesc = nombreloc,
                    FechaNegocio = fechanegloc,
                    IdTipoLocation = IdTipoLocation,
                    IdEstado = 1,
                    TxtEstado = "ACTIVO",
                    IdDist = id_distrito,
                    TxtDireccion1 = direccionloc
                };

                // Agrega el cliente a tu contexto y guarda los cambios
                _context.Mstt08Locations.Add(Location);
                _context.SaveChanges();
                return Json(new { message = "Autorizador creado correctamente" });
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear la Localización: " + ex.Message);
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

        [HttpPost]
        public async Task<ActionResult> Guardar(/*long? IdTemporal,*/
    //long? NroCheque,
    //int IdTipoComp,
    int IdCliente,
    //int IdEmpChofer,
    //int IdEmpleado,
    //int IdEmpAutorizador,
    //int IdEmpcortador,
    //int IdEmpaserradero,
    //int IdEmpunimoq,
    //int IdEmpcargador,
    //string? CodCaja,
    //string TxtSerie,
    //string TxtNumero,
    //string? TxtSerieFe,
    //string? TxtNumeroFe,
    //DateTime FecNegocio,
    //DateTime FecRegEmitido,
    //DateTime FecRegistro,
    DateTime FecEmi,
    //DateTime FecVcto,
    //DateTime FecCanc,
    //int? IdTipoMoneda,
    //int IdCanVta,
    int IdTipoOrden,
    int IdLocation,
    int IdLocationto,
    string TxtObserv,
    //decimal MtoTcVta,
    decimal MtoNeto,
    //decimal MtoExonerado,
    //decimal MtoNoAfecto,
    decimal MtoDsctoTot,
    //decimal MtoServicio,
    decimal MtoSubTot,
    decimal MtoImptoTot,
    decimal MtoTotComp,
    //int? RefIdCompEmitido,
    //string? RefTipoComprobante,
    //DateTime? RefFecha,
    //string? RefSerie,
    //string? RefNumero,
    //bool SnChkAbierto,
    //bool SnChkEnviado,
    //decimal? TaxPor01,
    //decimal? TaxPor02,
    //decimal? TaxPor03,
    //decimal? TaxPor04,
    //decimal? TaxPor05,
    //decimal? TaxPor06,
    //decimal? TaxPor07,
    //decimal? TaxPor08,
    decimal? TaxMto01,
    //decimal? TaxMto02,
    //decimal? TaxMto03,
    //decimal? TaxMto04,
    //decimal? TaxMto05,
    //decimal? TaxMto06,
    //decimal? TaxMto07,
    //decimal? TaxMto08,
    //string? Info01,
    //string? Info02,
    //string? Info03,
    //string? Info04,
    //string? Info05,
    //string? Info06,
    //string? Info07,
    //string? Info08,
    //string? Info09,
    //string? Info10,
    //DateTime? InfoDate01,
    //DateTime? InfoDate02,
    //DateTime? InfoDate03,
    //DateTime? InfoDate04,
    //DateTime? InfoDate05,
    //decimal? InfoMto01,
    //decimal? InfoMto02,
    //decimal? InfoMto03,
    //decimal? InfoMto04,
    //decimal? InfoMto05,
    //int? Post,
    //DateTime? PostDate,
    //int? NumComensales,
    //int IdEstado,
    //string TxtEstado,
    //int? IdMesa,
    //int? IdTurno,
    string productosSeleccionado,
    string empleadosSeleccionados,
 bool check
)
        {
            try
            {

                var productosseleccionados = JsonConvert.DeserializeObject<List<ProductoS>>(productosSeleccionado);
                var empleadosseleccionados = JsonConvert.DeserializeObject<List<EmpleadoExt>>(empleadosSeleccionados);

                var existingCompEmitido = _context.Tnst04CompEmitidos
            .FirstOrDefault(c => c.IdCompEmitido == IdTemporal);

                if (existingCompEmitido != null)
                {
                    //COMPROBANTE
                    existingCompEmitido.NroCheque = null;
                    existingCompEmitido.IdCliente = IdCliente;
                    //existingCompEmitido.IdEmpChofer = IdEmpChofer;
                    //existingCompEmitido.IdEmpleado = IdEmpleado;
                    //existingCompEmitido.IdEmpAutorizador = IdEmpAutorizador;
                    existingCompEmitido.CodCaja = null;
                    existingCompEmitido.TxtSerieFe = null;
                    existingCompEmitido.TxtNumeroFe = null;
                    //existingCompEmitido.FecNegocio = FecNegocio;
                    //existingCompEmitido.FecRegEmitido = FecRegEmitido;
                    //existingCompEmitido.FecRegistro = FecRegistro;
                    existingCompEmitido.FecEmi = FecEmi;
                    //existingCompEmitido.FecVcto = null;
                    //existingCompEmitido.FecCanc = null;
                    //existingCompEmitido.IdTipoMoneda = IdTipoMoneda;
                    //existingCompEmitido.IdCanVta = IdCanVta;
                    existingCompEmitido.IdTipoOrden = IdTipoOrden;
                    existingCompEmitido.IdLocation = IdLocation;
                    existingCompEmitido.IdLocationTo = IdLocationto;
                    existingCompEmitido.TxtObserv = TxtObserv;
                    //existingCompEmitido.MtoTcVta = MtoTcVta;
                    existingCompEmitido.MtoNeto = MtoNeto;
                    //existingCompEmitido.MtoExonerado = MtoExonerado;
                    //existingCompEmitido.MtoNoAfecto = MtoNoAfecto;
                    existingCompEmitido.MtoDsctoTot = MtoDsctoTot;
                    //existingCompEmitido.MtoServicio = MtoServicio;
                    existingCompEmitido.MtoSubTot = MtoSubTot;
                    existingCompEmitido.MtoImptoTot = MtoImptoTot;
                    existingCompEmitido.MtoTotComp = MtoTotComp;
                    //existingCompEmitido.RefIdCompEmitido = RefIdCompEmitido;
                    //existingCompEmitido.RefTipoComprobante = RefTipoComprobante;
                    //existingCompEmitido.RefFecha = RefFecha;
                    //existingCompEmitido.RefSerie = RefSerie;
                    //existingCompEmitido.RefNumero = RefNumero;
                    //existingCompEmitido.SnChkAbierto = SnChkAbierto;
                    //existingCompEmitido.SnChkEnviado = SnChkEnviado;
                    //existingCompEmitido.TaxPor01 = TaxPor01;
                    //existingCompEmitido.TaxPor02 = TaxPor02;
                    //existingCompEmitido.TaxPor03 = TaxPor03;
                    //existingCompEmitido.TaxPor04 = TaxPor04;
                    //existingCompEmitido.TaxPor05 = TaxPor05;
                    //existingCompEmitido.TaxPor06 = TaxPor06;
                    //existingCompEmitido.TaxPor07 = TaxPor07;
                    //existingCompEmitido.TaxPor08 = TaxPor08;
                    existingCompEmitido.TaxMto01 = TaxMto01;
                    //existingCompEmitido.TaxMto02 = TaxMto02;
                    //existingCompEmitido.TaxMto03 = TaxMto03;
                    //existingCompEmitido.TaxMto04 = TaxMto04;
                    //existingCompEmitido.TaxMto05 = TaxMto05;
                    //existingCompEmitido.TaxMto06 = TaxMto06;
                    //existingCompEmitido.TaxMto07 = TaxMto07;
                    //existingCompEmitido.TaxMto08 = TaxMto08;
                    //existingCompEmitido.Info01 = Info01;
                    //existingCompEmitido.Info02 = Info02;
                    //existingCompEmitido.Info03 = Info03;
                    //existingCompEmitido.Info04 = Info04;
                    //existingCompEmitido.Info05 = Info05;
                    //existingCompEmitido.Info06 = Info06;
                    //existingCompEmitido.Info07 = Info07;
                    //existingCompEmitido.Info08 = Info08;
                    //existingCompEmitido.Info09 = Info09;
                    //existingCompEmitido.Info10 = Info10;
                    //existingCompEmitido.InfoDate01 = InfoDate01;
                    //existingCompEmitido.InfoDate02 = InfoDate02;
                    //existingCompEmitido.InfoDate03 = InfoDate03;
                    //existingCompEmitido.InfoDate04 = InfoDate04;
                    //existingCompEmitido.InfoDate05 = InfoDate05;
                    //existingCompEmitido.InfoMto01 = InfoMto01;
                    //existingCompEmitido.InfoMto02 = InfoMto02;
                    //existingCompEmitido.InfoMto03 = InfoMto03;
                    //existingCompEmitido.InfoMto04 = InfoMto04;
                    //existingCompEmitido.InfoMto05 = InfoMto05;
                    //existingCompEmitido.Post = Post;
                    //existingCompEmitido.PostDate = PostDate;
                    //existingCompEmitido.NumComensales = NumComensales;
                    existingCompEmitido.IdUsuarioModificador = idusuario;
                    existingCompEmitido.TxtUsuarioModificador = txtusuario;
                    existingCompEmitido.FechaModificacion = fechaHoy;
                    if (existingCompEmitido.IdEstado == 1)
                    {
                        existingCompEmitido.IdEstado = check ? 1 : 3;
                        existingCompEmitido.TxtEstado = check ? "BORRADOR" : "FINALIZADO";
                    }

                    //existingCompEmitido.IdMesa = IdMesa;
                    //existingCompEmitido.IdTurno = IdTurno;
                    existingCompEmitido.IdPredio = IdTemporalPredio;
                    existingCompEmitido.IdCampana = IdTemporalCampana;
                    _context.Tnst04CompEmitidos.Update(existingCompEmitido);
                    await _context.SaveChangesAsync();

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idProductosEnArreglo = productosseleccionados.Select(p => p.idProducto).ToList();

                    // Actualizar registros existentes
                    foreach (ProductoS producto in productosseleccionados)
                    {
                        var existingCompEmitidodtl = _context.Tnst05CompEmitidoDtls
                           .FirstOrDefault(d => d.IdCompEmitido == IdTemporal && d.IdProducto == producto.idProducto);

                        if (existingCompEmitidodtl != null)
                        {
                            // Actualiza los valores
                            existingCompEmitidodtl.IdProducto = producto.idProducto;
                            existingCompEmitidodtl.TxtProducto = producto.nombreProducto;
                            existingCompEmitidodtl.IdUm = null;
                            existingCompEmitidodtl.Cantidad = producto.cantidad;
                            existingCompEmitidodtl.Peso = null;
                            existingCompEmitidodtl.PorDscto = producto.descuento;
                            existingCompEmitidodtl.MtoDsctoSinTax = 0;
                            existingCompEmitidodtl.MtoDsctoConTax = 0;
                            existingCompEmitidodtl.PunitSinTax = producto.monto_si;
                            existingCompEmitidodtl.PunitConTax = producto.monto_ci;
                            existingCompEmitidodtl.TaxPorTot = producto.igv;
                            existingCompEmitidodtl.TaxMtoTot = producto.mtoigv;
                            existingCompEmitidodtl.TaxPor01 = producto.igv;
                            existingCompEmitidodtl.TaxPor02 = 0;
                            existingCompEmitidodtl.TaxPor03 = 0;
                            existingCompEmitidodtl.TaxPor04 = 0;
                            existingCompEmitidodtl.TaxPor05 = 0;
                            existingCompEmitidodtl.TaxPor06 = 0;
                            existingCompEmitidodtl.TaxPor07 = 0;
                            existingCompEmitidodtl.TaxPor08 = 0;
                            existingCompEmitidodtl.TaxMto01 = producto.mtoigv;
                            existingCompEmitidodtl.TaxMto02 = 0;
                            existingCompEmitidodtl.TaxMto03 = 0;
                            existingCompEmitidodtl.TaxMto04 = 0;
                            existingCompEmitidodtl.TaxMto05 = 0;
                            existingCompEmitidodtl.TaxMto06 = 0;
                            existingCompEmitidodtl.TaxMto07 = 0;
                            existingCompEmitidodtl.TaxMto08 = 0;
                            existingCompEmitidodtl.MtoVtaSinTax = producto.neto;
                            existingCompEmitidodtl.MtoVtaConTax = producto.total;
                            existingCompEmitidodtl.IdRazon = null;
                            existingCompEmitidodtl.TxtObserv = producto.observacion;
                            existingCompEmitidodtl.IdEstado = 1;
                            existingCompEmitidodtl.TxtEstado = "ACTIVO";

                            _context.Tnst05CompEmitidoDtls.Update(existingCompEmitidodtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (ProductoS producto in productosseleccionados)
                    {
                        var existingCompEmitidodtl = _context.Tnst05CompEmitidoDtls
                           .FirstOrDefault(d => d.IdCompEmitido == IdTemporal && d.IdProducto == producto.idProducto);

                        if (existingCompEmitidodtl == null)
                        {
                            // Crea un nuevo registro
                            var tnst05CompEmitidoDtl = new Tnst05CompEmitidoDtl
                            {
                                IdCompEmitido = (long)(IdTemporal),
                                IdProducto = producto.idProducto,
                                TxtProducto = producto.nombreProducto,
                                IdUm = null,
                                Cantidad = producto.cantidad,
                                Peso = null,
                                PorDscto = producto.descuento,
                                MtoDsctoSinTax = 0,
                                MtoDsctoConTax = 0,
                                PunitSinTax = producto.monto_si,
                                PunitConTax = producto.monto_ci,
                                TaxPorTot = producto.igv,
                                TaxMtoTot = producto.mtoigv,
                                TaxPor01 = producto.igv,
                                TaxPor02 = 0,
                                TaxPor03 = 0,
                                TaxPor04 = 0,
                                TaxPor05 = 0,
                                TaxPor06 = 0,
                                TaxPor07 = 0,
                                TaxPor08 = 0,
                                TaxMto01 = producto.mtoigv,
                                TaxMto02 = 0,
                                TaxMto03 = 0,
                                TaxMto04 = 0,
                                TaxMto05 = 0,
                                TaxMto06 = 0,
                                TaxMto07 = 0,
                                TaxMto08 = 0,
                                MtoVtaSinTax = producto.neto,
                                MtoVtaConTax = producto.total,
                                IdRazon = null,
                                TxtObserv = producto.observacion,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Tnst05CompEmitidoDtls.Add(tnst05CompEmitidoDtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminar = _context.Tnst05CompEmitidoDtls
                        .Where(d => d.IdCompEmitido == IdTemporal && !idProductosEnArreglo.Contains(d.IdProducto))
                        .ToList();
                    foreach (var registro in registrosParaEliminar)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    //_context.Tnst05CompEmitidoDtls.RemoveRange(registrosParaEliminar);
                    await _context.SaveChangesAsync();
                    var idEmpleadosEnArreglo = empleadosseleccionados.Select(p => p.idempleado).ToList();

                    // Actualizar registros existentes
                    foreach (EmpleadoExt empleado in empleadosseleccionados)
                    {
                        var existingEmpVenta = _context.Pret23VentaEmpleados
                           .FirstOrDefault(d => d.IdCompEmitido == IdTemporal && d.IdEmpleadoVen == empleado.idempleado);

                        if (existingEmpVenta != null)
                        {
                            // Actualiza los valores
                            existingEmpVenta.IdEmpleadoVen = empleado.idempleado;
                            existingEmpVenta.IdEstado = 1;
                            existingEmpVenta.TxtEstado = "ACTIVO";

                            _context.Pret23VentaEmpleados.Update(existingEmpVenta);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (EmpleadoExt empleado in empleadosseleccionados)
                    {
                        var existingCompEmitidodtl = _context.Pret23VentaEmpleados
                           .FirstOrDefault(d => d.IdCompEmitido == IdTemporal && d.IdEmpleadoVen == empleado.idempleado);

                        if (existingCompEmitidodtl == null)
                        {
                            // Crea un nuevo registro
                            var Pret23EmpVen = new Pret23VentaEmpleado
                            {
                                IdCompEmitido = (long)(IdTemporal),
                                IdEmpleadoVen = empleado.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret23VentaEmpleados.Add(Pret23EmpVen);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminarEmpleado = _context.Pret23VentaEmpleados
                        .Where(d => d.IdCompEmitido == IdTemporal && !idEmpleadosEnArreglo.Contains((long)d.IdEmpleadoVen))
                        .ToList();
                    foreach (var registro in registrosParaEliminarEmpleado)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    //_context.Tnst05CompEmitidoDtls.RemoveRange(registrosParaEliminar);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cantidad = _context.Tnst04CompEmitidos.Where(e => e.IdEstado != 1).Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string NroCompEmitido = txt_serie + "-" + txt_numero;
                    //COMPROBANTE
                    var tnst04CompEmitido = new Tnst04CompEmitido
                    {
                        NroCompEmitido = NroCompEmitido,
                        NroCheque = null,
                        IdTipoComp = 4,
                        IdCliente = IdCliente,
                        //IdEmpChofer = IdEmpChofer,
                        //IdEmpleado = IdEmpleado,
                        //IdEmpAutorizador = IdEmpAutorizador,
                        CodCaja = null,
                        TxtSerie = txt_serie,
                        TxtNumero = txt_numero,
                        TxtSerieFe = null,
                        TxtNumeroFe = null,
                        FecNegocio = fechaHoy,
                        FecRegEmitido = null,
                        FecRegistro = fechaHoy,
                        FecEmi = fechaHoy,
                        FecVcto = null,
                        FecCanc = null,
                        IdTipoMoneda = null,
                        IdCanVta = null,
                        IdTipoOrden = IdTipoOrden,
                        IdLocation = IdLocation,
                        IdLocationTo = IdLocationto,
                        TxtObserv = TxtObserv,
                        MtoTcVta = 0,
                        MtoNeto = MtoNeto,
                        MtoExonerado = 0,
                        MtoNoAfecto = 0,
                        MtoDsctoTot = MtoDsctoTot,
                        MtoServicio = 0,
                        MtoSubTot = MtoSubTot,
                        MtoImptoTot = MtoImptoTot,
                        MtoTotComp = MtoTotComp,
                        RefIdCompEmitido = null,
                        RefTipoComprobante = null,
                        RefFecha = null,
                        RefSerie = null,
                        RefNumero = null,
                        SnChkAbierto = false,
                        SnChkEnviado = false,
                        TaxPor01 = (decimal)0.18,
                        TaxPor02 = null,
                        TaxPor03 = null,
                        TaxPor04 = null,
                        TaxPor05 = null,
                        TaxPor06 = null,
                        TaxPor07 = null,
                        TaxPor08 = null,
                        TaxMto01 = TaxMto01,
                        TaxMto02 = null,
                        TaxMto03 = null,
                        TaxMto04 = null,
                        TaxMto05 = null,
                        TaxMto06 = null,
                        TaxMto07 = null,
                        TaxMto08 = null,
                        Info01 = null,
                        Info02 = null,
                        Info03 = null,
                        Info04 = null,
                        Info05 = null,
                        Info06 = null,
                        Info07 = null,
                        Info08 = null,
                        Info09 = null,
                        Info10 = null,
                        InfoDate01 = null,
                        InfoDate02 = null,
                        InfoDate03 = null,
                        InfoDate04 = null,
                        InfoDate05 = null,
                        InfoMto01 = null,
                        InfoMto02 = null,
                        InfoMto03 = null,
                        InfoMto04 = null,
                        InfoMto05 = null,
                        Post = null,
                        PostDate = null,
                        NumComensales = null,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check ? 1 : 3,
                        TxtEstado = check ? "BORRADOR" : "FINALIZADO",
                        IdMesa = null,
                        IdTurno = null,
                        IdPredio = IdTemporalPredio,
                        IdCampana = IdTemporalCampana
                    };

                    _context.Tnst04CompEmitidos.Add(tnst04CompEmitido);
                    await _context.SaveChangesAsync();
                    IdTemporal = tnst04CompEmitido.IdCompEmitido;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE
                        foreach (ProductoS producto in productosseleccionados)
                        {

                            var tnst05CompEmitidoDtl = new Tnst05CompEmitidoDtl
                            {
                                IdCompEmitido = (long)(IdTemporal),
                                IdProducto = producto.idProducto,
                                TxtProducto = producto.nombreProducto,
                                IdUm = null,
                                Cantidad = producto.cantidad,
                                Peso = null,
                                PorDscto = producto.descuento,
                                MtoDsctoSinTax = 0,
                                MtoDsctoConTax = 0,
                                PunitSinTax = producto.monto_si,
                                PunitConTax = producto.monto_ci,
                                TaxPorTot = producto.igv,
                                TaxMtoTot = producto.mtoigv,
                                TaxPor01 = producto.igv,
                                TaxPor02 = 0,
                                TaxPor03 = 0,
                                TaxPor04 = 0,
                                TaxPor05 = 0,
                                TaxPor06 = 0,
                                TaxPor07 = 0,
                                TaxPor08 = 0,
                                TaxMto01 = producto.mtoigv,
                                TaxMto02 = 0,
                                TaxMto03 = 0,
                                TaxMto04 = 0,
                                TaxMto05 = 0,
                                TaxMto06 = 0,
                                TaxMto07 = 0,
                                TaxMto08 = 0,
                                MtoVtaSinTax = producto.neto,
                                MtoVtaConTax = producto.total,
                                IdRazon = null,
                                TxtObserv = producto.observacion,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Tnst05CompEmitidoDtls.Add(tnst05CompEmitidoDtl);
                            await _context.SaveChangesAsync();


                        }
                        //COMPROBANTE DETALLE
                        foreach (EmpleadoExt empleado in empleadosseleccionados)
                        {

                            var Pret23VentEmp = new Pret23VentaEmpleado
                            {
                                IdCompEmitido = (long)(IdTemporal),
                                IdEmpleadoVen = empleado.idempleado,

                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret23VentaEmpleados.Add(Pret23VentEmp);
                            await _context.SaveChangesAsync();


                        }
                    }



                }

                return Json(new { mensaje = "Guardado correctamente" });
            }

            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante el proceso de guardado
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GuardaryCerrar(/*long? IdTemporal,*/
    //long? NroCheque,
    //int IdTipoComp,
    int IdCliente,
    //int IdEmpChofer,
    //int IdEmpleado,
    //int IdEmpAutorizador,
    //int IdEmpcortador,
    //int IdEmpaserradero,
    //int IdEmpunimoq,
    //int IdEmpcargador,
    //string? CodCaja,
    //string TxtSerie,
    //string TxtNumero,
    //string? TxtSerieFe,
    //string? TxtNumeroFe,
    //DateTime FecNegocio,
    //DateTime FecRegEmitido,
    //DateTime FecRegistro,
    DateTime FecEmi,
    //DateTime FecVcto,
    //DateTime FecCanc,
    //int? IdTipoMoneda,
    //int IdCanVta,
    int IdTipoOrden,
    int IdLocation,
    int IdLocationto,
    string TxtObserv,
    //decimal MtoTcVta,
    decimal MtoNeto,
    //decimal MtoExonerado,
    //decimal MtoNoAfecto,
    decimal MtoDsctoTot,
    //decimal MtoServicio,
    decimal MtoSubTot,
    decimal MtoImptoTot,
    decimal MtoTotComp,
    //int? RefIdCompEmitido,
    //string? RefTipoComprobante,
    //DateTime? RefFecha,
    //string? RefSerie,
    //string? RefNumero,
    //bool SnChkAbierto,
    //bool SnChkEnviado,
    //decimal? TaxPor01,
    //decimal? TaxPor02,
    //decimal? TaxPor03,
    //decimal? TaxPor04,
    //decimal? TaxPor05,
    //decimal? TaxPor06,
    //decimal? TaxPor07,
    //decimal? TaxPor08,
    decimal? TaxMto01,
    //decimal? TaxMto02,
    //decimal? TaxMto03,
    //decimal? TaxMto04,
    //decimal? TaxMto05,
    //decimal? TaxMto06,
    //decimal? TaxMto07,
    //decimal? TaxMto08,
    //string? Info01,
    //string? Info02,
    //string? Info03,
    //string? Info04,
    //string? Info05,
    //string? Info06,
    //string? Info07,
    //string? Info08,
    //string? Info09,
    //string? Info10,
    //DateTime? InfoDate01,
    //DateTime? InfoDate02,
    //DateTime? InfoDate03,
    //DateTime? InfoDate04,
    //DateTime? InfoDate05,
    //decimal? InfoMto01,
    //decimal? InfoMto02,
    //decimal? InfoMto03,
    //decimal? InfoMto04,
    //decimal? InfoMto05,
    //int? Post,
    //DateTime? PostDate,
    //int? NumComensales,
    //int IdEstado,
    //string TxtEstado,
    //int? IdMesa,
    //int? IdTurno,
    string productosSeleccionado,
    string empleadosSeleccionados,
    bool check
)
        {
            try
            {

                var productosseleccionados = JsonConvert.DeserializeObject<List<ProductoS>>(productosSeleccionado);
                var empleadosseleccionados = JsonConvert.DeserializeObject<List<EmpleadoExt>>(empleadosSeleccionados);

                var existingCompEmitido = _context.Tnst04CompEmitidos
            .FirstOrDefault(c => c.IdCompEmitido == IdTemporal);

                if (existingCompEmitido != null)
                {
                    //COMPROBANTE
                    existingCompEmitido.NroCheque = null;
                    existingCompEmitido.IdCliente = IdCliente;
                    //existingCompEmitido.IdEmpChofer = IdEmpChofer;
                    //existingCompEmitido.IdEmpleado = IdEmpleado;
                    //existingCompEmitido.IdEmpAutorizador = IdEmpAutorizador;
                    existingCompEmitido.CodCaja = null;
                    existingCompEmitido.TxtSerieFe = null;
                    existingCompEmitido.TxtNumeroFe = null;
                    //existingCompEmitido.FecNegocio = FecNegocio;
                    //existingCompEmitido.FecRegEmitido = FecRegEmitido;
                    //existingCompEmitido.FecRegistro = FecRegistro;
                    existingCompEmitido.FecEmi = FecEmi;
                    //existingCompEmitido.FecVcto = null;
                    //existingCompEmitido.FecCanc = null;
                    //existingCompEmitido.IdTipoMoneda = IdTipoMoneda;
                    //existingCompEmitido.IdCanVta = IdCanVta;
                    existingCompEmitido.IdTipoOrden = IdTipoOrden;
                    existingCompEmitido.IdLocation = IdLocation;
                    existingCompEmitido.IdLocationTo = IdLocationto;
                    existingCompEmitido.TxtObserv = TxtObserv;
                    //existingCompEmitido.MtoTcVta = MtoTcVta;
                    existingCompEmitido.MtoNeto = MtoNeto;
                    //existingCompEmitido.MtoExonerado = MtoExonerado;
                    //existingCompEmitido.MtoNoAfecto = MtoNoAfecto;
                    existingCompEmitido.MtoDsctoTot = MtoDsctoTot;
                    //existingCompEmitido.MtoServicio = MtoServicio;
                    existingCompEmitido.MtoSubTot = MtoSubTot;
                    existingCompEmitido.MtoImptoTot = MtoImptoTot;
                    existingCompEmitido.MtoTotComp = MtoTotComp;
                    //existingCompEmitido.RefIdCompEmitido = RefIdCompEmitido;
                    //existingCompEmitido.RefTipoComprobante = RefTipoComprobante;
                    //existingCompEmitido.RefFecha = RefFecha;
                    //existingCompEmitido.RefSerie = RefSerie;
                    //existingCompEmitido.RefNumero = RefNumero;
                    //existingCompEmitido.SnChkAbierto = SnChkAbierto;
                    //existingCompEmitido.SnChkEnviado = SnChkEnviado;
                    //existingCompEmitido.TaxPor01 = TaxPor01;
                    //existingCompEmitido.TaxPor02 = TaxPor02;
                    //existingCompEmitido.TaxPor03 = TaxPor03;
                    //existingCompEmitido.TaxPor04 = TaxPor04;
                    //existingCompEmitido.TaxPor05 = TaxPor05;
                    //existingCompEmitido.TaxPor06 = TaxPor06;
                    //existingCompEmitido.TaxPor07 = TaxPor07;
                    //existingCompEmitido.TaxPor08 = TaxPor08;
                    existingCompEmitido.TaxMto01 = TaxMto01;
                    //existingCompEmitido.TaxMto02 = TaxMto02;
                    //existingCompEmitido.TaxMto03 = TaxMto03;
                    //existingCompEmitido.TaxMto04 = TaxMto04;
                    //existingCompEmitido.TaxMto05 = TaxMto05;
                    //existingCompEmitido.TaxMto06 = TaxMto06;
                    //existingCompEmitido.TaxMto07 = TaxMto07;
                    //existingCompEmitido.TaxMto08 = TaxMto08;
                    //existingCompEmitido.Info01 = Info01;
                    //existingCompEmitido.Info02 = Info02;
                    //existingCompEmitido.Info03 = Info03;
                    //existingCompEmitido.Info04 = Info04;
                    //existingCompEmitido.Info05 = Info05;
                    //existingCompEmitido.Info06 = Info06;
                    //existingCompEmitido.Info07 = Info07;
                    //existingCompEmitido.Info08 = Info08;
                    //existingCompEmitido.Info09 = Info09;
                    //existingCompEmitido.Info10 = Info10;
                    //existingCompEmitido.InfoDate01 = InfoDate01;
                    //existingCompEmitido.InfoDate02 = InfoDate02;
                    //existingCompEmitido.InfoDate03 = InfoDate03;
                    //existingCompEmitido.InfoDate04 = InfoDate04;
                    //existingCompEmitido.InfoDate05 = InfoDate05;
                    //existingCompEmitido.InfoMto01 = InfoMto01;
                    //existingCompEmitido.InfoMto02 = InfoMto02;
                    //existingCompEmitido.InfoMto03 = InfoMto03;
                    //existingCompEmitido.InfoMto04 = InfoMto04;
                    //existingCompEmitido.InfoMto05 = InfoMto05;
                    //existingCompEmitido.Post = Post;
                    //existingCompEmitido.PostDate = PostDate;
                    //existingCompEmitido.NumComensales = NumComensales;
                    existingCompEmitido.IdUsuarioModificador = idusuario;
                    existingCompEmitido.TxtUsuarioModificador = txtusuario;
                    existingCompEmitido.FechaModificacion = fechaHoy;
                    if (existingCompEmitido.IdEstado == 1)
                    {
                        existingCompEmitido.IdEstado = check ? 1 : 3;
                        existingCompEmitido.TxtEstado = check ? "BORRADOR" : "FINALIZADO";
                    }

                    //existingCompEmitido.IdMesa = IdMesa;
                    //existingCompEmitido.IdTurno = IdTurno;
                    existingCompEmitido.IdPredio = IdTemporalPredio;
                    existingCompEmitido.IdCampana = IdTemporalCampana;
                    _context.Tnst04CompEmitidos.Update(existingCompEmitido);
                    await _context.SaveChangesAsync();

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idProductosEnArreglo = productosseleccionados.Select(p => p.idProducto).ToList();

                    // Actualizar registros existentes
                    foreach (ProductoS producto in productosseleccionados)
                    {
                        var existingCompEmitidodtl = _context.Tnst05CompEmitidoDtls
                           .FirstOrDefault(d => d.IdCompEmitido == IdTemporal && d.IdProducto == producto.idProducto);

                        if (existingCompEmitidodtl != null)
                        {
                            // Actualiza los valores
                            existingCompEmitidodtl.IdProducto = producto.idProducto;
                            existingCompEmitidodtl.TxtProducto = producto.nombreProducto;
                            existingCompEmitidodtl.IdUm = null;
                            existingCompEmitidodtl.Cantidad = producto.cantidad;
                            existingCompEmitidodtl.Peso = null;
                            existingCompEmitidodtl.PorDscto = producto.descuento;
                            existingCompEmitidodtl.MtoDsctoSinTax = 0;
                            existingCompEmitidodtl.MtoDsctoConTax = 0;
                            existingCompEmitidodtl.PunitSinTax = producto.monto_si;
                            existingCompEmitidodtl.PunitConTax = producto.monto_ci;
                            existingCompEmitidodtl.TaxPorTot = producto.igv;
                            existingCompEmitidodtl.TaxMtoTot = producto.mtoigv;
                            existingCompEmitidodtl.TaxPor01 = producto.igv;
                            existingCompEmitidodtl.TaxPor02 = 0;
                            existingCompEmitidodtl.TaxPor03 = 0;
                            existingCompEmitidodtl.TaxPor04 = 0;
                            existingCompEmitidodtl.TaxPor05 = 0;
                            existingCompEmitidodtl.TaxPor06 = 0;
                            existingCompEmitidodtl.TaxPor07 = 0;
                            existingCompEmitidodtl.TaxPor08 = 0;
                            existingCompEmitidodtl.TaxMto01 = producto.mtoigv;
                            existingCompEmitidodtl.TaxMto02 = 0;
                            existingCompEmitidodtl.TaxMto03 = 0;
                            existingCompEmitidodtl.TaxMto04 = 0;
                            existingCompEmitidodtl.TaxMto05 = 0;
                            existingCompEmitidodtl.TaxMto06 = 0;
                            existingCompEmitidodtl.TaxMto07 = 0;
                            existingCompEmitidodtl.TaxMto08 = 0;
                            existingCompEmitidodtl.MtoVtaSinTax = producto.neto;
                            existingCompEmitidodtl.MtoVtaConTax = producto.total;
                            existingCompEmitidodtl.IdRazon = null;
                            existingCompEmitidodtl.TxtObserv = producto.observacion;
                            existingCompEmitidodtl.IdEstado = 1;
                            existingCompEmitidodtl.TxtEstado = "ACTIVO";

                            _context.Tnst05CompEmitidoDtls.Update(existingCompEmitidodtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (ProductoS producto in productosseleccionados)
                    {
                        var existingCompEmitidodtl = _context.Tnst05CompEmitidoDtls
                           .FirstOrDefault(d => d.IdCompEmitido == IdTemporal && d.IdProducto == producto.idProducto);

                        if (existingCompEmitidodtl == null)
                        {
                            // Crea un nuevo registro
                            var tnst05CompEmitidoDtl = new Tnst05CompEmitidoDtl
                            {
                                IdCompEmitido = (long)(IdTemporal),
                                IdProducto = producto.idProducto,
                                TxtProducto = producto.nombreProducto,
                                IdUm = null,
                                Cantidad = producto.cantidad,
                                Peso = null,
                                PorDscto = producto.descuento,
                                MtoDsctoSinTax = 0,
                                MtoDsctoConTax = 0,
                                PunitSinTax = producto.monto_si,
                                PunitConTax = producto.monto_ci,
                                TaxPorTot = producto.igv,
                                TaxMtoTot = producto.mtoigv,
                                TaxPor01 = producto.igv,
                                TaxPor02 = 0,
                                TaxPor03 = 0,
                                TaxPor04 = 0,
                                TaxPor05 = 0,
                                TaxPor06 = 0,
                                TaxPor07 = 0,
                                TaxPor08 = 0,
                                TaxMto01 = producto.mtoigv,
                                TaxMto02 = 0,
                                TaxMto03 = 0,
                                TaxMto04 = 0,
                                TaxMto05 = 0,
                                TaxMto06 = 0,
                                TaxMto07 = 0,
                                TaxMto08 = 0,
                                MtoVtaSinTax = producto.neto,
                                MtoVtaConTax = producto.total,
                                IdRazon = null,
                                TxtObserv = producto.observacion,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Tnst05CompEmitidoDtls.Add(tnst05CompEmitidoDtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminar = _context.Tnst05CompEmitidoDtls
                        .Where(d => d.IdCompEmitido == IdTemporal && !idProductosEnArreglo.Contains(d.IdProducto))
                        .ToList();
                    foreach (var registro in registrosParaEliminar)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    //_context.Tnst05CompEmitidoDtls.RemoveRange(registrosParaEliminar);
                    await _context.SaveChangesAsync();
                    var idEmpleadosEnArreglo = empleadosseleccionados.Select(p => p.idempleado).ToList();

                    // Actualizar registros existentes
                    foreach (EmpleadoExt empleado in empleadosseleccionados)
                    {
                        var existingEmpVenta = _context.Pret23VentaEmpleados
                           .FirstOrDefault(d => d.IdCompEmitido == IdTemporal && d.IdEmpleadoVen == empleado.idempleado);

                        if (existingEmpVenta != null)
                        {
                            // Actualiza los valores
                            existingEmpVenta.IdEmpleadoVen = empleado.idempleado;
                            existingEmpVenta.IdEstado = 1;
                            existingEmpVenta.TxtEstado = "ACTIVO";

                            _context.Pret23VentaEmpleados.Update(existingEmpVenta);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (EmpleadoExt empleado in empleadosseleccionados)
                    {
                        var existingCompEmitidodtl = _context.Pret23VentaEmpleados
                           .FirstOrDefault(d => d.IdCompEmitido == IdTemporal && d.IdEmpleadoVen == empleado.idempleado);

                        if (existingCompEmitidodtl == null)
                        {
                            // Crea un nuevo registro
                            var Pret23EmpVen = new Pret23VentaEmpleado
                            {
                                IdCompEmitido = (long)(IdTemporal),
                                IdEmpleadoVen = empleado.idempleado,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret23VentaEmpleados.Add(Pret23EmpVen);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminarEmpleado = _context.Pret23VentaEmpleados
                        .Where(d => d.IdCompEmitido == IdTemporal && !idEmpleadosEnArreglo.Contains((long)d.IdEmpleadoVen))
                        .ToList();
                    foreach (var registro in registrosParaEliminarEmpleado)
                    {
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }
                    //_context.Tnst05CompEmitidoDtls.RemoveRange(registrosParaEliminar);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cantidad = _context.Tnst04CompEmitidos.Where(e => e.IdEstado != 1).Count() + 1;
                    var txt_serie = "001";
                    var txt_numero = cantidad.ToString().PadLeft(7, '0');
                    string NroCompEmitido = txt_serie + "-" + txt_numero;
                    //COMPROBANTE
                    var tnst04CompEmitido = new Tnst04CompEmitido
                    {
                        NroCompEmitido = NroCompEmitido,
                        NroCheque = null,
                        IdTipoComp = 4,
                        IdCliente = IdCliente,
                        //IdEmpChofer = IdEmpChofer,
                        //IdEmpleado = IdEmpleado,
                        //IdEmpAutorizador = IdEmpAutorizador,
                        CodCaja = null,
                        TxtSerie = txt_serie,
                        TxtNumero = txt_numero,
                        TxtSerieFe = null,
                        TxtNumeroFe = null,
                        FecNegocio = fechaHoy,
                        FecRegEmitido = null,
                        FecRegistro = fechaHoy,
                        FecEmi = fechaHoy,
                        FecVcto = null,
                        FecCanc = null,
                        IdTipoMoneda = null,
                        IdCanVta = null,
                        IdTipoOrden = IdTipoOrden,
                        IdLocation = IdLocation,
                        IdLocationTo = IdLocationto,
                        TxtObserv = TxtObserv,
                        MtoTcVta = 0,
                        MtoNeto = MtoNeto,
                        MtoExonerado = 0,
                        MtoNoAfecto = 0,
                        MtoDsctoTot = MtoDsctoTot,
                        MtoServicio = 0,
                        MtoSubTot = MtoSubTot,
                        MtoImptoTot = MtoImptoTot,
                        MtoTotComp = MtoTotComp,
                        RefIdCompEmitido = null,
                        RefTipoComprobante = null,
                        RefFecha = null,
                        RefSerie = null,
                        RefNumero = null,
                        SnChkAbierto = false,
                        SnChkEnviado = false,
                        TaxPor01 = (decimal)0.18,
                        TaxPor02 = null,
                        TaxPor03 = null,
                        TaxPor04 = null,
                        TaxPor05 = null,
                        TaxPor06 = null,
                        TaxPor07 = null,
                        TaxPor08 = null,
                        TaxMto01 = TaxMto01,
                        TaxMto02 = null,
                        TaxMto03 = null,
                        TaxMto04 = null,
                        TaxMto05 = null,
                        TaxMto06 = null,
                        TaxMto07 = null,
                        TaxMto08 = null,
                        Info01 = null,
                        Info02 = null,
                        Info03 = null,
                        Info04 = null,
                        Info05 = null,
                        Info06 = null,
                        Info07 = null,
                        Info08 = null,
                        Info09 = null,
                        Info10 = null,
                        InfoDate01 = null,
                        InfoDate02 = null,
                        InfoDate03 = null,
                        InfoDate04 = null,
                        InfoDate05 = null,
                        InfoMto01 = null,
                        InfoMto02 = null,
                        InfoMto03 = null,
                        InfoMto04 = null,
                        InfoMto05 = null,
                        Post = null,
                        PostDate = null,
                        NumComensales = null,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = check ? 1 : 3,
                        TxtEstado = check ? "BORRADOR" : "FINALIZADO",
                        IdMesa = null,
                        IdTurno = null,
                        IdPredio = IdTemporalPredio,
                        IdCampana = IdTemporalCampana
                    };

                    _context.Tnst04CompEmitidos.Add(tnst04CompEmitido);
                    await _context.SaveChangesAsync();
                    IdTemporal = tnst04CompEmitido.IdCompEmitido;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE
                        foreach (ProductoS producto in productosseleccionados)
                        {

                            var tnst05CompEmitidoDtl = new Tnst05CompEmitidoDtl
                            {
                                IdCompEmitido = (long)(IdTemporal),
                                IdProducto = producto.idProducto,
                                TxtProducto = producto.nombreProducto,
                                IdUm = null,
                                Cantidad = producto.cantidad,
                                Peso = null,
                                PorDscto = producto.descuento,
                                MtoDsctoSinTax = 0,
                                MtoDsctoConTax = 0,
                                PunitSinTax = producto.monto_si,
                                PunitConTax = producto.monto_ci,
                                TaxPorTot = producto.igv,
                                TaxMtoTot = producto.mtoigv,
                                TaxPor01 = producto.igv,
                                TaxPor02 = 0,
                                TaxPor03 = 0,
                                TaxPor04 = 0,
                                TaxPor05 = 0,
                                TaxPor06 = 0,
                                TaxPor07 = 0,
                                TaxPor08 = 0,
                                TaxMto01 = producto.mtoigv,
                                TaxMto02 = 0,
                                TaxMto03 = 0,
                                TaxMto04 = 0,
                                TaxMto05 = 0,
                                TaxMto06 = 0,
                                TaxMto07 = 0,
                                TaxMto08 = 0,
                                MtoVtaSinTax = producto.neto,
                                MtoVtaConTax = producto.total,
                                IdRazon = null,
                                TxtObserv = producto.observacion,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Tnst05CompEmitidoDtls.Add(tnst05CompEmitidoDtl);
                            await _context.SaveChangesAsync();


                        }
                        //COMPROBANTE DETALLE
                        foreach (EmpleadoExt empleado in empleadosseleccionados)
                        {

                            var Pret23VentEmp = new Pret23VentaEmpleado
                            {
                                IdCompEmitido = (long)(IdTemporal),
                                IdEmpleadoVen = empleado.idempleado,

                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret23VentaEmpleados.Add(Pret23VentEmp);
                            await _context.SaveChangesAsync();


                        }
                    }



                }
                IdTemporal = null;
                IdTemporalCampana = null;
                IdTemporalPredio = null;
                return Json(new { redirectUrl = Url.Action("boleta_v_listado", "Tnst04CompEmitido") });
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante el proceso de guardado
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }


        public IActionResult editar(int id)
        {
            //Usuario
            var id_usuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario()).IdUsuario;
            ViewBag.idUsuario = id_usuario;
            ViewBag.txtUsuario = _pert01UsuarioController.GetUsuario();

            var cantidad = _context.Tnst04CompEmitidos.Include(t => t.IdCanVtaNavigation).Include(t => t.IdClienteNavigation).
                Include(t => t.IdLocationNavigation).
                Include(t => t.IdMesaNavigation).Include(t => t.IdTipoCompNavigation).Include(t => t.IdTipoMonedaNavigation).
                Include(t => t.IdTipoOrdenNavigation).Include(t => t.IdTurnoNavigation).Include(t => t.IdUsuarioNavigation)
                .Count();
            ViewBag.cantidad = cantidad;
            //Cliente
            var clientsLocation = _context.Pert02Clientes
                    .Include(e => e.IdNacionalidadNavigation)
                    .Include(e => e.IdDistNavigation)
                        .ThenInclude(d => d.IdProvNavigation)
                            .ThenInclude(p => p.IdDptoNavigation)
                            .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        IdCliente = e.IdCliente,
                        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
                        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}," +
                        $" {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
                        ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
                    })
                .ToList();
            ViewBag.Cliente = clientsLocation;

            //var choferWithNationalityAndLocation = _context.Pert04Empleados
            //    .Include(e => e.IdNacionalidadNavigation)
            //    .Include(e => e.IdDistNavigation)
            //        .ThenInclude(d => d.IdProvNavigation)
            //            .ThenInclude(p => p.IdDptoNavigation)
            //    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 2)
            //    .Select(e => new
            //    {
            //        Idempleado = e.IdEmpleado,
            //        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
            //        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
            //        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
            //        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
            //    })
            //    .ToList();

            //ViewBag.Chofer = choferWithNationalityAndLocation;

            //var operarioWithNationalityAndLocation = _context.Pert04Empleados
            //    .Include(e => e.IdNacionalidadNavigation)
            //    .Include(e => e.IdDistNavigation)
            //        .ThenInclude(d => d.IdProvNavigation)
            //            .ThenInclude(p => p.IdDptoNavigation)
            //    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 3)
            //    .Select(e => new
            //    {
            //        Idempleado = e.IdEmpleado,
            //        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
            //        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
            //        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
            //        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
            //    })
            //    .ToList();

            //ViewBag.Operario = operarioWithNationalityAndLocation;


            //var AutorizadorWithNationalityAndLocation = _context.Pert04Empleados
            //    .Include(e => e.IdNacionalidadNavigation)
            //    .Include(e => e.IdDistNavigation)
            //        .ThenInclude(d => d.IdProvNavigation)
            //            .ThenInclude(p => p.IdDptoNavigation)
            //    .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdCategoriaEmp == 4)
            //    .Select(e => new
            //    {
            //        Idempleado = e.IdEmpleado,
            //        NombreCompleto = e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}",
            //        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : (!string.IsNullOrEmpty(e.Celular2) ? e.Celular2 : e.Celular3),
            //        Direccion = $"{e.TxtDireccion1} - {e.IdDistNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.TxtDesc}, {e.IdDistNavigation.IdProvNavigation.IdDptoNavigation.TxtDesc}",
            //        Ruc = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc
            //    })
            //    .ToList();

            //ViewBag.Autorizador = AutorizadorWithNationalityAndLocation;
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

            ViewBag.EmpVen = empleadosventas;
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

            var productos = _context.Prot09Productos.Include(p => p.IdUmNavigation).
                Where(p => p.MtoPvpuSinIgv != null && p.AlturaProd != null && p.AnchoProd != null && p.LargoProd != null).Select(
                e => new
                {
                    IdProducto = e.IdProducto,
                    NombreProducto = e.TxtDesc,
                    Monto_si = e.MtoPvpuSinIgv,
                    Monto_ci = e.MtoPvpuConIgv,
                    igv = e.PorImpto,
                    unm = e.IdUmNavigation.TxtDesc,
                    dimension = e.LargoProd + "x" + e.AnchoProd + "x" + e.AlturaProd,
                    diametro = e.DiametroProd,
                    peso = e.PesoProd
                }).ToList();
            ViewBag.Productos = productos;
            ViewData["IdTipoLocation"] = new SelectList(_context.Mstt09TipoLocations, "IdTipoLocation", "TxtDesc");

            ViewData["IdDep"] = new SelectList(_context.Sntt31Departamentos, "IdDpto", "TxtDesc");

            // Obtén la lista de tipos de órdenes desde tu contexto
            var tipoOrdenes = _context.Mstt03TipoOrdens.ToList();

            // Filtra la lista para excluir elementos con TxtDesc nulo o vacío
            tipoOrdenes = tipoOrdenes.Where(t => !string.IsNullOrEmpty(t.TxtDesc)).ToList();

            // Crea un SelectList con los elementos filtrados
            ViewBag.TipoOrden = new SelectList(tipoOrdenes, "IdTipoOrden", "TxtDesc");

            // Obtén la lista de canales de venta desde tu contexto
            var canalesVenta = _context.Mstt04CanalVta.ToList();

            // Filtra la lista para excluir elementos con TxtDesc nulo o vacío
            canalesVenta = canalesVenta.Where(c => !string.IsNullOrEmpty(c.TxtDesc)).ToList();

            // Crea un SelectList con los elementos filtrados
            ViewBag.CanalVta = new SelectList(canalesVenta, "IdCanVta", "TxtDesc");


            //ViewData["IdMesa"] = new SelectList(_context.Mstt14Mesas, "IdMesa", "IdMesa");
            //ViewData["IdTipoComp"] = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "IdTipoComp");
            ViewData["IdTipoMoneda"] = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "IdTipoMoneda");
            ViewData["IdTurno"] = new SelectList(_context.Mstt13Turnos, "IdTurno", "IdTurno");
            //ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            var comprobantevista = _context.Tnst04CompEmitidos
            .Where(c => c.IdCompEmitido == id)
            .FirstOrDefault();

            var comprobante = _context.Tnst04CompEmitidos
            .Include(t => t.IdCanVtaNavigation)
            .Include(t => t.IdPredioNavigation)
            .Include(t => t.IdCampanaNavigation)
            .Include(c => c.IdClienteNavigation)
            .Include(c => c.IdLocationNavigation)
            .Include(c => c.IdLocationToNavigation)
            .Include(c => c.IdMesaNavigation)
            .Include(c => c.IdTipoCompNavigation)
            .Include(c => c.IdTipoMonedaNavigation)
            .Include(c => c.IdTipoOrdenNavigation)
            .Include(c => c.IdTurnoNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Where(c => c.IdCompEmitido == id)
            .Select(comprobante => new
            {
                Comprobante = comprobante,
                NombreCompletoCliente = String.IsNullOrEmpty(comprobante.IdClienteNavigation.TxtRznSocial) ? $"{comprobante.IdClienteNavigation.TxtApePat} {comprobante.IdClienteNavigation.TxtApeMat}, {comprobante.IdClienteNavigation.TxtPriNom} {comprobante.IdClienteNavigation.TxtSegNom}" : comprobante.IdClienteNavigation.TxtRznSocial,
                telcliente = comprobante.IdClienteNavigation.Celular1,
                loc = comprobante.IdLocationNavigation.TxtDesc,
                locto = comprobante.IdLocationToNavigation.TxtDesc,
                idlocto = comprobante.IdLocationTo,
                uniC = comprobante.IdPredioNavigation.UnidadCatastral,
                codC = comprobante.IdCampanaNavigation.CodigoCampana,
                dircliente = comprobante.IdClienteNavigation.TxtDireccion1,
            })
            .FirstOrDefault();
            //  Creamos nuevas columnas para manejarlas y a parte el mismo registro de comprobante



            if (comprobante != null)
            {
                var detallesComprobante = _context.Tnst05CompEmitidoDtls
                .Where(detalle => detalle.IdCompEmitido == id)
                .ToList();
                var Empextraccion = _context.Pret23VentaEmpleados
                .Where(detalle => detalle.IdCompEmitido == id && detalle.IdEstado == 1)
                .Select(
                    n => new
                    {
                        idempleado = n.IdEmpleadoVen,
                        txtempleado = n.IdEmpleadoVenNavigation.TxtPriNom == null ? n.IdEmpleadoVenNavigation.TxtRznSocial : $"{n.IdEmpleadoVenNavigation.TxtPriNom} {n.IdEmpleadoVenNavigation.TxtApePat}",
                        nrodoc = String.IsNullOrEmpty(n.IdEmpleadoVenNavigation.NroRuc) ? n.IdEmpleadoVenNavigation.NroDoc : n.IdEmpleadoVenNavigation.NroRuc,
                        condicion = n.IdEmpleadoVenNavigation.IdCondicionLaboralNavigation.TxtDesc,
                        categoria = n.IdEmpleadoVenNavigation.IdCategoriaEmpNavigation.TxtNombre,
                        telefono = !string.IsNullOrEmpty(n.IdEmpleadoVenNavigation.Celular1) ? n.IdEmpleadoVenNavigation.Celular1 : (!string.IsNullOrEmpty(n.IdEmpleadoVenNavigation.Celular2) ? n.IdEmpleadoVenNavigation.Celular2 : n.IdEmpleadoVenNavigation.Celular3),
                    })
                .ToList();
                // Establecer configuraciones para ignorar las referencias circulares
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                // Serializar los datos con las configuraciones establecidas
                var comprobanteSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(comprobante, settings);
                var detallesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(detallesComprobante, settings);
                var empleadosSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Empextraccion, settings);

                // Pasar los datos serializados a la vista utilizando ViewBag
                IdTemporal = comprobantevista?.IdCompEmitido;
                IdTemporalCampana = comprobantevista?.IdCampana;
                IdTemporalPredio = comprobantevista?.IdPredio;
                ViewBag.numpedido = comprobante.Comprobante.NroCompEmitido;

                ViewBag.Comprobante = comprobanteSerialized;
                ViewBag.Detalles = detallesSerialized;
                ViewBag.Empleados = empleadosSerialized;

                // Redirige a la vista 'boleta_v'
                return View("boleta_v", comprobantevista);
            }
            else
            {
                // Manejar el caso donde el comprobante no existe
                return NotFound(); // O redirigir a otra página
            }
        }

        public IActionResult EliminarComprobante(int id)
        {
            var registro = _context.Tnst04CompEmitidos.FirstOrDefault(c => c.IdCompEmitido
            == id);

            if (registro == null)
            {
                return NotFound(); // Otra lógica si el registro no se encuentra
            }

            // Actualizar el estado del registro
            registro.IdEstado = 0;
            registro.TxtEstado = "INACTIVO";

            // Actualizar la base de datos
            _context.Entry(registro).State = EntityState.Modified;
            _context.SaveChanges();

            // Puedes redirigir o devolver un mensaje de éxito según tu lógica
            return RedirectToAction("boleta_v_listado"); // Reemplaza con tu acción deseada
        }
        public IActionResult SelecCampana(long idCampana)
        {
            IdTemporalCampana = idCampana;
            return Json(new { success = true });


        }
        public IActionResult CargarMood()
        {
            if (IdTemporal != null)
            {
                var estado = _context.Tnst04CompEmitidos.Where(t => t.IdCompEmitido == IdTemporal).Select(t => t.IdEstado).FirstOrDefault();
                var estadoName = _context.Tnst04CompEmitidos.Where(t => t.IdCompEmitido == IdTemporal).Select(t => t.TxtEstado).FirstOrDefault();
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
        public async Task<IActionResult> AnularVenta(long id)
        {
            try
            {
                var existingVenta = _context.Tnst04CompEmitidos.Where(p => p.IdEstado == 1 || p.IdEstado == 3 && p.IdCompEmitido == id).FirstOrDefault();

                if (existingVenta != null)
                {
                    existingVenta.IdEstado = 2;
                    existingVenta.TxtEstado = "ANULADO";
                    var existingEmpleado = _context.Pret23VentaEmpleados.Where(p => p.IdEstado == 1 || p.IdEstado == 2 && p.IdCompEmitido == id).ToList();

                    if (existingEmpleado != null)
                    {
                        foreach (Pret23VentaEmpleado empleado in existingEmpleado)
                        {
                            empleado.IdEstado = 2;
                            empleado.TxtEstado = "INACTIVO";

                        }
                        await _context.SaveChangesAsync();
                    }
                    var existingDtll = _context.Tnst05CompEmitidoDtls.Where(p => p.IdEstado == 1 && p.IdCompEmitido == id).ToList();

                    if (existingDtll != null)
                    {
                        foreach (Tnst05CompEmitidoDtl Detalle in existingDtll)
                        {
                            Detalle.IdEstado = 2;
                            Detalle.TxtEstado = "INACTIVO";

                        }
                    }
                    //_context.Tnst04CompEmitidos.Remove(existingVenta);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("boleta_v_listado", "Tnst04CompEmitido") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarVenta(long id)
        {
            try
            {
                var existingVenta = _context.Tnst04CompEmitidos.Where(p => p.IdEstado == 1 || p.IdEstado == 3 && p.IdCompEmitido == id).FirstOrDefault();

                if (existingVenta != null)
                {
                    //existingVenta.IdEstado = 2;
                    //existingVenta.TxtEstado = "CANCELADO";
                    var existingEmpleado = _context.Pret23VentaEmpleados.Where(p => p.IdEstado == 1 || p.IdEstado == 2 && p.IdCompEmitido == id).ToList();

                    if (existingEmpleado != null)
                    {
                        foreach (Pret23VentaEmpleado empleado in existingEmpleado)
                        {
                            //empleado.IdEstado = 2;
                            //empleado.TxtEstado = "INACTIVO";
                            _context.Pret23VentaEmpleados.Remove(empleado);
                        }
                        await _context.SaveChangesAsync();
                    }
                    var existingDtll = _context.Tnst05CompEmitidoDtls.Where(p => p.IdEstado == 1 && p.IdCompEmitido == id).ToList();

                    if (existingDtll != null)
                    {
                        foreach (Tnst05CompEmitidoDtl Detalle in existingDtll)
                        {
                            //Detalle.IdEstado = 2;
                            //Detalle.TxtEstado = "INACTIVO";
                            _context.Tnst05CompEmitidoDtls.Remove(Detalle);

                        }
                    }
                    _context.Tnst04CompEmitidos.Remove(existingVenta);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return Json(new { redirectUrl = Url.Action("boleta_v_listado", "Tnst04CompEmitido") }); // Redirige a donde quieras después de la eliminación
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }


    }
}

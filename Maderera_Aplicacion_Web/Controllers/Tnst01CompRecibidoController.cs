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
using Maderera_Aplicacion_Web.Controllers;
using Newtonsoft.Json;

namespace Aplicacion_Maderera.Controllers
{
    public class Tnst01CompRecibidoController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;

        public Tnst01CompRecibidoController(EagleContext context, IPert01UsuarioController pert01UsuarioController)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
        }

        // GET: Tnst01CompRecibido
        public async Task<IActionResult> Index()
        {
            var eagleContext = _context.Tnst01CompRecibidos.Include(t => t.IdLocationNavigation).Include(t => t.IdProveedorNavigation).Include(t => t.IdTipoCompNavigation).Include(t => t.IdTipoMonedaNavigation).Include(t => t.IdTipoOrdenNavigation).Include(t => t.IdUsuarioNavigation);
            var cantidad = _context.Tnst01CompRecibidos.Include(t => t.IdLocationNavigation).Include(t => t.IdProveedorNavigation).Include(t => t.IdTipoCompNavigation).Include(t => t.IdTipoMonedaNavigation).Include(t => t.IdTipoOrdenNavigation).Include(t => t.IdUsuarioNavigation).Count();

            return View(await eagleContext.ToListAsync());
        }

        // GET: Tnst01CompRecibido/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Tnst01CompRecibidos == null)
            {
                return NotFound();
            }

            var tnst01CompRecibido = await _context.Tnst01CompRecibidos
                .Include(t => t.IdLocationNavigation)
                .Include(t => t.IdProveedorNavigation)
                .Include(t => t.IdTipoCompNavigation)
                .Include(t => t.IdTipoMonedaNavigation)
                .Include(t => t.IdTipoOrdenNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCompRecibido == id);
            if (tnst01CompRecibido == null)
            {
                return NotFound();
            }

            return View(tnst01CompRecibido);
        }
        public IActionResult boleta_c()
        {
            var id_usuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario;
            ViewBag.idUsuario = id_usuario;
            ViewBag.txtUsuario = _pert01UsuarioController.GetUsuario();

            var cantidad = _context.Tnst01CompRecibidos.Include(t => t.IdLocationNavigation).
                Include(t => t.IdProveedorNavigation).Include(t => t.IdTipoCompNavigation).
                Include(t => t.IdTipoMonedaNavigation).Include(t => t.IdTipoOrdenNavigation).
                Include(t => t.IdUsuarioNavigation).Count();
            ViewBag.cantidad = cantidad;

            var proveedor = _context.Pert03Proveedors

                   .Select(e => new
                   {
                       IdProveedor = e.IdProveedor,
                       RUC = e.NroRuc,
                       razon = e.TxtRznSocial,
                       direccion = e.TxtDireccion1
                   })
               .ToList();
            ViewBag.Proveedor = proveedor;

            var productos = _context.Prot18Productocoms.ToList().
                Where(p => p.MtoPvpuSinIgv != null).Select(
                e => new
                {
                    IdProducto = e.IdProducto,
                    NombreProducto = e.TxtDesc,
                    UmPro = e.IdUm,
                    Monto_si = e.MtoPvpuSinIgv,
                    Monto_ci = e.MtoPvpuConIgv,
                    UM = e.IdUm,
                    igv = e.PorImpto
                });

            ViewBag.Productos = productos;

            var autorizarFiltrados = _context.Pert04Empleados
                                    .Where(e => e.IdCategoriaEmp == 4)
                                    .Select(e => new
                                    {
                                        IdEmpleado = e.IdEmpleado,
                                        NombreCompleto = $"{e.TxtPriNom} {e.TxtApePat}"
                                    });
            ViewBag.Autorizar = new SelectList(autorizarFiltrados, "IdEmpleado", "NombreCompleto");
            ViewBag.IdProveedor = new SelectList(_context.Pert03Proveedors, "IdProveedor", "TxtRznSocial");
            ViewBag.IdTipoComp = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "TxtDesc");
            ViewBag.IdTipoMoneda = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "TxtDesc");
            ViewBag.IdTipoOrden = new SelectList(_context.Mstt03TipoOrdens, "IdTipoOrden", "TxtDesc");
            ViewBag.IdUsuario = new SelectList(_context.Pert01Usuarios, "IdUsuario", "TxtUsuario");
            ViewData["IdTip"] = new SelectList(_context.Prot19TipoProdComs, "IdTipoProd", "TxtDesc");
            ViewData["IdMar"] = new SelectList(_context.Prot20Marcacoms, "IdMarca", "TxtDesc");
            return View();
        }

        [HttpGet]

        public IActionResult ObtenerMarca(int tipoId)
        {
            // Obtiene la marca según el tipo seleccionado
            var marca = _context.Prot20Marcacoms
                .Where(p => p.IdTipoProd == tipoId)
                .Select(p => new { idmarca = p.IdMarca, txtDesc = p.TxtDesc })
                .ToList();

            return Json(marca);
        }

        [HttpGet]

        public IActionResult ObtenerModelo(int marcaId)
        {
            // Obtiene la marca según el tipo seleccionado
            var modelo = _context.Prot21Modelocoms
                .Where(p => p.IdMarca == marcaId)
                .Select(p => new { idmodelo = p.IdModelo, txtDesc = p.TxtDesc })
                .ToList();

            return Json(modelo);
        }

        [HttpGet]

        public IActionResult RecargarProveedor()
        {
            try
            {
                var updatedProveedor = _context.Pert03Proveedors

                   .Select(e => new
                   {
                       RUC = e.NroRuc,
                       razon = e.TxtRznSocial,
                       direccion = e.TxtDireccion1
                   })
               .ToList();
                ViewBag.Proveedor = updatedProveedor;



                return Json(updatedProveedor);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el proveedor: " + ex.Message);
            }
        }
        [HttpPost]

        public JsonResult ObtenerProductos(string searchTerm)
        {
            // Aquí deberías realizar la consulta a tu base de datos usando Entity Framework u otro ORM
            // Supongamos que tienes una lista de productos en tu base de datos
            // Esto es solo un ejemplo simplificado, ajusta según tu estructura real
            var productosFiltrados = _context.Prot18Productocoms
                                        .Where(p => p.TxtDesc.ToLower().Contains(searchTerm.ToLower()))
                                        .Select(p => new
                                        {
                                            Id = p.IdProducto,
                                            TxtDesc = p.TxtDesc
                                        })
                                        .ToList();

            return Json(productosFiltrados);
        }

        public JsonResult ObtenerDetallesProducto(int productoId)
        {
            // Aquí deberías realizar la consulta a tu base de datos para obtener detalles del producto por su ID
            // Supongamos que tienes una clase Producto y un contexto para acceder a la base de datos
            // Esto es solo un ejemplo simplificado, ajusta según tu estructura real
            var producto = _context.Prot18Productocoms
                                .FirstOrDefault(p => p.IdProducto == productoId);

            if (producto != null)
            {
                return Json(new
                {
                    IdUm = producto.IdUm,
                    CostoProd = producto.CostoProd
                });
            }

            return Json(null); // Si no se encuentra el producto, se envía una respuesta vacía
        }



































        public IActionResult boleta_c_listado()
        {
            var listadoComprobantesActivos = _context.Tnst01CompRecibidos
                .Include(t => t.IdProveedorNavigation)
                .Where(c => c.IdEstado == 1 || c.IdEstado == 2)
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
                TipoCompra = comprobante.TipoCompra,
                Comprobante = comprobante,
                RUCproveedor = $"{comprobante.IdProveedorNavigation.NroRuc}",
                MontoT = comprobante.MtoTotComp,
                igv = comprobante.TaxMto01,
                MontoN = comprobante.MtoNeto,
                FecEmi = comprobante.FecEmi,
                Razon = $"{comprobante.IdProveedorNavigation.TxtRznSocial}",
                Id = comprobante.IdCompRecibido

            }).ToList();

            ViewBag.Comprobantes = comprobantesViewModel;
            return View();
        }
        public IActionResult factura_c()
        {
            ViewBag.IdLocation = new SelectList(_context.Mstt08Locations, "IdLocation", "TxtDesc");
            ViewBag.IdProveedor = new SelectList(_context.Pert03Proveedors, "IdProveedor", "TxtRznSocial");
            ViewBag.IdTipoComp = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "TxtDesc");
            ViewBag.IdTipoMoneda = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "TxtDesc");
            ViewBag.IdTipoOrden = new SelectList(_context.Mstt03TipoOrdens, "IdTipoOrden", "TxtDesc");
            ViewBag.IdUsuario = new SelectList(_context.Pert01Usuarios, "IdUsuario", "TxtUsuario");
            return View();
        }
        public IActionResult nota_c()
        {
            ViewBag.IdLocation = new SelectList(_context.Mstt08Locations, "IdLocation", "TxtDesc");
            ViewBag.IdProveedor = new SelectList(_context.Pert03Proveedors, "IdProveedor", "TxtRznSocial");
            ViewBag.IdTipoComp = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "TxtDesc");
            ViewBag.IdTipoMoneda = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "TxtDesc");
            ViewBag.IdTipoOrden = new SelectList(_context.Mstt03TipoOrdens, "IdTipoOrden", "TxtDesc");
            ViewBag.IdUsuario = new SelectList(_context.Pert01Usuarios, "IdUsuario", "TxtUsuario");
            return View();
        }
        // GET: Tnst01CompRecibido/Create
        public IActionResult Create()
        {
            ViewData["IdLocation"] = new SelectList(_context.Mstt08Locations, "IdLocation", "TxtDesc");
            ViewData["IdProveedor"] = new SelectList(_context.Pert03Proveedors, "IdProveedor", "TxtRznSocial");
            ViewData["IdTipoComp"] = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "TxtDesc");
            ViewData["IdTipoMoneda"] = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "TxtDesc");
            ViewData["IdTipoOrden"] = new SelectList(_context.Mstt03TipoOrdens, "IdTipoOrden", "TxtDesc");
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "TxtUsuario");
            return View();
        }

        // POST: Tnst01CompRecibido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public IActionResult CrearProveedor(string? txt_razon_social, string? txt_ruc, string? txt_direc)
        {
            try
            {

                // Crea una instancia de la entidad Cliente
                var proveedor = new Pert03Proveedor
                {
                    TxtRznSocial = txt_razon_social,
                    NroRuc = txt_ruc,
                    TxtDireccion1 = txt_direc,
                    CodTipoPer = "J"
                };

                // Agrega el cliente a tu contexto y guarda los cambios
                _context.Pert03Proveedors.Add(proveedor);
                _context.SaveChanges();
                ViewBag.Proveedor = proveedor;
                return Json(new { message = "Proveedor creado correctamente" });
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Proveedor: " + ex.Message);
            }
        }

        [HttpPost]

        public IActionResult CrearProducto(string? txt_desc, int? id_tipo, int? id_modelo)
        {
            try
            {

                // Crea una instancia de la entidad Producto
                var productoc = new Prot18Productocom
                {
                    TxtDesc = txt_desc,
                    IdTipoProd = id_tipo,
                    IdModelo = id_modelo


                };

                // Agrega el producto a tu contexto y guarda los cambios
                _context.Prot18Productocoms.Add(productoc);
                _context.SaveChanges();

                // Obtén el ID del producto recién creado
                var nuevoIDProducto = productoc.IdProducto; // Suponiendo que 'Id' es la propiedad que almacena el ID del producto

                // Haz lo que necesites con el ID del producto (p. ej., almacenarlo en ViewBag o devolverlo en la respuesta JSON)
                // Almacena en ViewBag para usarlo en la vista si es necesario

                return Json(new { message = "Producto creado correctamente", nuevoID = nuevoIDProducto });


            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Proveedor: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Guardar(
            long? comprobanteId,
            string NroCompRecibido,
            int IdTipoComp,
            string TxtSerie,
            string TxtNumero,
            DateTime FecRegistro,
            DateTime? FecRegRecibido,
            DateTime FecEmi,
            DateTime? FecVcto,
            DateTime? FecCanc,
            int IdTipoMoneda,
            int? IdTipoOrden,
            string? TxtObserv,
            decimal MtoTcVta,
            decimal MtoNeto,
            decimal MtoExonerado,
            decimal MtoNoAfecto,
            decimal MtoDsctoTot,
            decimal MtoCmsTot,
            decimal MtoFleteTot,
            decimal MtoSubTot,
            decimal MtoImptoTot,
            decimal MtoServicio,
            decimal MtoTotComp,
            long? RefIdCompRecibido,
            string? RefTipoComprobante,
            DateTime? RefFecha,
            string? RefSerie,
            string? RefNumero,
            decimal TaxPor01,
            decimal TaxPor02,
            decimal TaxPor03,
            decimal TaxPor04,
            decimal TaxPor05,
            decimal TaxPor06,
            decimal TaxPor07,
            decimal TaxPor08,
            decimal TaxMto01,
            decimal TaxMto02,
            decimal TaxMto03,
            decimal TaxMto04,
            decimal TaxMto05,
            decimal TaxMto06,
            decimal TaxMto07,
            decimal TaxMto08,
            string? Info01,
            string? Info02,
            string? Info03,
            string? Info04,
            string? Info05,
            string? Info06,
            string? Info07,
            string? Info08,
            string? Info09,
            string? Info10,
            DateTime? InfoDate01,
            DateTime? InfoDate02,
            DateTime? InfoDate03,
            DateTime? InfoDate04,
            DateTime? InfoDate05,
            decimal? InfoMto01,
            decimal? InfoMto02,
            decimal? InfoMto03,
            decimal? InfoMto04,
            decimal? InfoMto05,
            int? Post,
            DateTime? PostDate,
            bool SnCredito,
            bool SnCancelada,
            long? IdUsuarioModificador,
            string? TxtUsuarioModificador,
            DateTime? FechaModificacion,
            string TxtUsuario,
            int IdEstado,
            string TxtEstado,
            long IdProveedor,
            long IdUsuario,
            int IdLocation,
            long? IdCliente,
            string TipoCompra,
            string productosCseleccionados
)
        {
            try
            {
                var idusuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario()).IdUsuario;
                var txtusuario = _pert01UsuarioController.GetUsuario();
                //CONVERTIR EL STRING A UNA LISTA YA QUE LLEGA DESDE JSon
                var productosCSeleccionados = JsonConvert.DeserializeObject<List<ProductoS>>(productosCseleccionados);

                var existingCompRecibido = _context.Tnst01CompRecibidos
            .FirstOrDefault(c => c.IdCompRecibido == comprobanteId);

                if (existingCompRecibido != null)
                {
                    //COMPROBANTE
                    existingCompRecibido.NroCompRecibido = NroCompRecibido;
                    existingCompRecibido.IdTipoComp = IdTipoComp;
                    existingCompRecibido.TxtSerie = TxtSerie;
                    existingCompRecibido.TxtNumero = TxtNumero;
                    existingCompRecibido.FecRegistro = FecRegistro;
                    existingCompRecibido.FecRegRecibido = FecRegRecibido;
                    existingCompRecibido.FecEmi = FecEmi;
                    existingCompRecibido.FecVcto = FecVcto;
                    existingCompRecibido.FecCanc = FecCanc;
                    existingCompRecibido.IdTipoMoneda = IdTipoMoneda;
                    existingCompRecibido.IdTipoOrden = IdTipoOrden;
                    existingCompRecibido.TxtObserv = TxtObserv;
                    existingCompRecibido.MtoTcVta = MtoTcVta;
                    existingCompRecibido.MtoNeto = MtoNeto;
                    existingCompRecibido.MtoExonerado = MtoExonerado;
                    existingCompRecibido.MtoNoAfecto = MtoNoAfecto;
                    existingCompRecibido.MtoDsctoTot = MtoDsctoTot;
                    existingCompRecibido.MtoCmsTot = MtoCmsTot;
                    existingCompRecibido.MtoFleteTot = MtoFleteTot;
                    existingCompRecibido.MtoSubTot = MtoSubTot;
                    existingCompRecibido.MtoImptoTot = MtoImptoTot;
                    existingCompRecibido.MtoServicio = MtoServicio;
                    existingCompRecibido.MtoTotComp = MtoTotComp;
                    existingCompRecibido.RefIdCompRecibido = RefIdCompRecibido;
                    existingCompRecibido.RefTipoComprobante = RefTipoComprobante;
                    existingCompRecibido.RefFecha = RefFecha;
                    existingCompRecibido.RefSerie = RefSerie;
                    existingCompRecibido.RefNumero = RefNumero;
                    existingCompRecibido.TaxPor01 = TaxPor01;
                    existingCompRecibido.TaxPor02 = TaxPor02;
                    existingCompRecibido.TaxPor03 = TaxPor03;
                    existingCompRecibido.TaxPor04 = TaxPor04;
                    existingCompRecibido.TaxPor05 = TaxPor05;
                    existingCompRecibido.TaxPor06 = TaxPor06;
                    existingCompRecibido.TaxPor07 = TaxPor07;
                    existingCompRecibido.TaxPor08 = TaxPor08;
                    existingCompRecibido.TaxMto01 = TaxMto01;
                    existingCompRecibido.TaxMto02 = TaxMto02;
                    existingCompRecibido.TaxMto03 = TaxMto03;
                    existingCompRecibido.TaxMto04 = TaxMto04;
                    existingCompRecibido.TaxMto05 = TaxMto05;
                    existingCompRecibido.TaxMto06 = TaxMto06;
                    existingCompRecibido.TaxMto07 = TaxMto07;
                    existingCompRecibido.TaxMto08 = TaxMto08;
                    existingCompRecibido.Info01 = Info01;
                    existingCompRecibido.Info02 = Info02;
                    existingCompRecibido.Info03 = Info03;
                    existingCompRecibido.Info04 = Info04;
                    existingCompRecibido.Info05 = Info05;
                    existingCompRecibido.Info06 = Info06;
                    existingCompRecibido.Info07 = Info07;
                    existingCompRecibido.Info08 = Info08;
                    existingCompRecibido.Info09 = Info09;
                    existingCompRecibido.Info10 = Info10;
                    existingCompRecibido.InfoDate01 = InfoDate01;
                    existingCompRecibido.InfoDate02 = InfoDate02;
                    existingCompRecibido.InfoDate03 = InfoDate03;
                    existingCompRecibido.InfoDate04 = InfoDate04;
                    existingCompRecibido.InfoDate05 = InfoDate05;
                    existingCompRecibido.InfoMto01 = InfoMto01;
                    existingCompRecibido.InfoMto02 = InfoMto02;
                    existingCompRecibido.InfoMto03 = InfoMto03;
                    existingCompRecibido.InfoMto04 = InfoMto04;
                    existingCompRecibido.InfoMto05 = InfoMto05;
                    existingCompRecibido.Post = Post;
                    existingCompRecibido.PostDate = PostDate;
                    existingCompRecibido.TxtUsuario = txtusuario;
                    existingCompRecibido.IdEstado = 1;
                    existingCompRecibido.TxtEstado = "ACTIVO";
                    existingCompRecibido.IdProveedor = IdProveedor;
                    existingCompRecibido.IdUsuario = idusuario;
                    existingCompRecibido.IdLocation = IdLocation;
                    existingCompRecibido.IdCliente = IdCliente;
                    existingCompRecibido.TipoCompra = TipoCompra;

                    _context.Tnst01CompRecibidos.Update(existingCompRecibido);
                    await _context.SaveChangesAsync();

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idProductosEnArreglo = productosCSeleccionados.Select(p => p.idProducto).ToList();

                    // Actualizar registros existentes
                    foreach (ProductoS producto in productosCSeleccionados)
                    {
                        var existingCompRecibidodtl = _context.Tnst02CompRecibidoDtls
                           .FirstOrDefault(d => d.IdCompRecibido == comprobanteId && d.IdProducto == producto.idProducto);

                        if (existingCompRecibidodtl != null)
                        {
                            // Actualiza los valores
                            existingCompRecibidodtl.IdProducto = producto.idProducto;
                            existingCompRecibidodtl.TxtProducto = producto.nombreProducto;
                            existingCompRecibidodtl.IdUm = null;
                            existingCompRecibidodtl.Cantidad = producto.cantidad;
                            existingCompRecibidodtl.Peso = null;
                            existingCompRecibidodtl.PorDscto = producto.descuento;
                            existingCompRecibidodtl.MtoDsctoSinTax = 0;
                            existingCompRecibidodtl.MtoDsctoConTax = 0;
                            existingCompRecibidodtl.PunitSinTax = producto.monto_si;
                            existingCompRecibidodtl.PunitConTax = producto.monto_ci;
                            existingCompRecibidodtl.TaxPorTot = producto.igv;
                            existingCompRecibidodtl.TaxMtoTot = producto.mtoigv;
                            existingCompRecibidodtl.TaxPor01 = producto.igv;
                            existingCompRecibidodtl.TaxPor02 = 0;
                            existingCompRecibidodtl.TaxPor03 = 0;
                            existingCompRecibidodtl.TaxPor04 = 0;
                            existingCompRecibidodtl.TaxPor05 = 0;
                            existingCompRecibidodtl.TaxPor06 = 0;
                            existingCompRecibidodtl.TaxPor07 = 0;
                            existingCompRecibidodtl.TaxPor08 = 0;
                            existingCompRecibidodtl.TaxMto01 = producto.mtoigv;
                            existingCompRecibidodtl.TaxMto02 = 0;
                            existingCompRecibidodtl.TaxMto03 = 0;
                            existingCompRecibidodtl.TaxMto04 = 0;
                            existingCompRecibidodtl.TaxMto05 = 0;
                            existingCompRecibidodtl.TaxMto06 = 0;
                            existingCompRecibidodtl.TaxMto07 = 0;
                            existingCompRecibidodtl.TaxMto08 = 0;
                            existingCompRecibidodtl.MtoVtaSinTax = producto.neto;
                            existingCompRecibidodtl.MtoVtaConTax = producto.total;
                            existingCompRecibidodtl.IdRazon = null;
                            existingCompRecibidodtl.TxtObserv = producto.observacion;
                            existingCompRecibidodtl.IdEstado = 1;
                            existingCompRecibidodtl.TxtEstado = "ACTIVO";

                            _context.Tnst02CompRecibidoDtls.Update(existingCompRecibidodtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    //Crear nuevos registros
                    foreach (ProductoS producto in productosCSeleccionados)
                    {
                        var existingCompEmitidodtl = _context.Tnst02CompRecibidoDtls
                           .FirstOrDefault(d => d.IdCompRecibido == comprobanteId && d.IdProducto == producto.idProducto);

                        if (existingCompEmitidodtl == null)
                        {
                            // Crea un nuevo registro
                            var tnst02CompRecibidoDtl = new Tnst02CompRecibidoDtl
                            {
                                IdCompRecibido = (long)(comprobanteId),
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
                            _context.Tnst02CompRecibidoDtls.Add(tnst02CompRecibidoDtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminar = _context.Tnst02CompRecibidoDtls
                        .Where(d => d.IdCompRecibido == comprobanteId && !idProductosEnArreglo.Contains(d.IdProducto))
                        .ToList();

                    _context.Tnst02CompRecibidoDtls.RemoveRange(registrosParaEliminar);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    //COMPROBANTE
                    var tnst01CompRecibido = new Tnst01CompRecibido
                    {
                        NroCompRecibido = NroCompRecibido,
                        IdTipoComp = IdTipoComp,
                        TxtSerie = TxtSerie,
                        TxtNumero = TxtNumero,
                        FecRegistro = FecRegistro,
                        FecRegRecibido = FecRegRecibido,
                        FecEmi = FecEmi,
                        FecVcto = FecVcto,
                        FecCanc = FecCanc,
                        IdTipoMoneda = IdTipoMoneda,
                        IdTipoOrden = IdTipoOrden,
                        TxtObserv = TxtObserv,
                        MtoTcVta = MtoTcVta,
                        MtoNeto = MtoNeto,
                        MtoExonerado = MtoExonerado,
                        MtoNoAfecto = MtoNoAfecto,
                        MtoDsctoTot = MtoDsctoTot,
                        MtoCmsTot = MtoCmsTot,
                        MtoFleteTot = MtoFleteTot,
                        MtoSubTot = MtoSubTot,
                        MtoImptoTot = MtoImptoTot,
                        MtoServicio = MtoServicio,
                        MtoTotComp = MtoTotComp,
                        RefIdCompRecibido = RefIdCompRecibido,
                        RefTipoComprobante = RefTipoComprobante,
                        RefFecha = RefFecha,
                        RefSerie = RefSerie,
                        RefNumero = RefNumero,
                        TaxPor01 = TaxPor01,
                        TaxPor02 = TaxPor02,
                        TaxPor03 = TaxPor03,
                        TaxPor04 = TaxPor04,
                        TaxPor05 = TaxPor05,
                        TaxPor06 = TaxPor06,
                        TaxPor07 = TaxPor07,
                        TaxPor08 = TaxPor08,
                        TaxMto01 = TaxMto01,
                        TaxMto02 = TaxMto02,
                        TaxMto03 = TaxMto03,
                        TaxMto04 = TaxMto04,
                        TaxMto05 = TaxMto05,
                        TaxMto06 = TaxMto06,
                        TaxMto07 = TaxMto07,
                        TaxMto08 = TaxMto08,
                        Info01 = Info01,
                        Info02 = Info02,
                        Info03 = Info03,
                        Info04 = Info04,
                        Info05 = Info05,
                        Info06 = Info06,
                        Info07 = Info07,
                        Info08 = Info08,
                        Info09 = Info09,
                        Info10 = Info10,
                        InfoDate01 = InfoDate01,
                        InfoDate02 = InfoDate02,
                        InfoDate03 = InfoDate03,
                        InfoDate04 = InfoDate04,
                        InfoDate05 = InfoDate05,
                        InfoMto01 = InfoMto01,
                        InfoMto02 = InfoMto02,
                        InfoMto03 = InfoMto03,
                        InfoMto04 = InfoMto04,
                        InfoMto05 = InfoMto05,
                        Post = Post,
                        PostDate = PostDate,
                        SnCredito = SnCredito,
                        SnCancelada = SnCancelada,
                        TxtUsuario = txtusuario,
                        IdEstado = 1,
                        TxtEstado = "ACTIVO",
                        IdProveedor = IdProveedor,
                        IdUsuario = idusuario,
                        IdLocation = IdLocation,
                        IdCliente = IdCliente,
                        TipoCompra = TipoCompra

                    };

                    _context.Tnst01CompRecibidos.Add(tnst01CompRecibido);
                    await _context.SaveChangesAsync();
                    comprobanteId = tnst01CompRecibido.IdCompRecibido;

                    if (comprobanteId != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE
                        foreach (ProductoS producto in productosCSeleccionados)
                        {

                            var tnst02CompRecibidoDtl = new Tnst02CompRecibidoDtl
                            {
                                IdCompRecibido = (long)(comprobanteId),
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
                            _context.Tnst02CompRecibidoDtls.Add(tnst02CompRecibidoDtl);
                            await _context.SaveChangesAsync();


                        }
                    }



                }

                return Json(new { mensaje = "Guardado correctamente", comprobanteId });
            }

            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante el proceso de guardado
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GuardaryCerrarC(
            long? comprobanteId,
            string NroCompRecibido,
            int IdTipoComp,
            string TxtSerie,
            string TxtNumero,
            DateTime FecRegistro,
            DateTime? FecRegRecibido,
            DateTime FecEmi,
            DateTime? FecVcto,
            DateTime? FecCanc,
            int IdTipoMoneda,
            int? IdTipoOrden,
            string? TxtObserv,
            decimal MtoTcVta,
            decimal MtoNeto,
            decimal MtoExonerado,
            decimal MtoNoAfecto,
            decimal MtoDsctoTot,
            decimal MtoCmsTot,
            decimal MtoFleteTot,
            decimal MtoSubTot,
            decimal MtoImptoTot,
            decimal MtoServicio,
            decimal MtoTotComp,
            long? RefIdCompRecibido,
            string? RefTipoComprobante,
            DateTime? RefFecha,
            string? RefSerie,
            string? RefNumero,
            decimal TaxPor01,
            decimal TaxPor02,
            decimal TaxPor03,
            decimal TaxPor04,
            decimal TaxPor05,
            decimal TaxPor06,
            decimal TaxPor07,
            decimal TaxPor08,
            decimal TaxMto01,
            decimal TaxMto02,
            decimal TaxMto03,
            decimal TaxMto04,
            decimal TaxMto05,
            decimal TaxMto06,
            decimal TaxMto07,
            decimal TaxMto08,
            string? Info01,
            string? Info02,
            string? Info03,
            string? Info04,
            string? Info05,
            string? Info06,
            string? Info07,
            string? Info08,
            string? Info09,
            string? Info10,
            DateTime? InfoDate01,
            DateTime? InfoDate02,
            DateTime? InfoDate03,
            DateTime? InfoDate04,
            DateTime? InfoDate05,
            decimal? InfoMto01,
            decimal? InfoMto02,
            decimal? InfoMto03,
            decimal? InfoMto04,
            decimal? InfoMto05,
            int? Post,
            DateTime? PostDate,
            bool SnCredito,
            bool SnCancelada,
            long? IdUsuarioModificador,
            string? TxtUsuarioModificador,
            DateTime? FechaModificacion,
            string TxtUsuario,
            int IdEstado,
            string TxtEstado,
            long IdProveedor,
            long IdUsuario,
            int IdLocation,
            long? IdCliente,
            string TipoCompra,
            string productosCseleccionados
)
        {
            try
            {
                var idusuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario()).IdUsuario;
                var txtusuario = _pert01UsuarioController.GetUsuario();
                //CONVERTIR EL STRING A UNA LISTA YA QUE LLEGA DESDE JSon
                var productosCSeleccionados = JsonConvert.DeserializeObject<List<ProductoS>>(productosCseleccionados);

                var existingCompRecibido = _context.Tnst01CompRecibidos
            .FirstOrDefault(c => c.IdCompRecibido == comprobanteId);

                if (existingCompRecibido != null)
                {
                    //COMPROBANTE
                    existingCompRecibido.NroCompRecibido = NroCompRecibido;
                    existingCompRecibido.IdTipoComp = IdTipoComp;
                    existingCompRecibido.TxtSerie = TxtSerie;
                    existingCompRecibido.TxtNumero = TxtNumero;
                    existingCompRecibido.FecRegistro = FecRegistro;
                    existingCompRecibido.FecRegRecibido = FecRegRecibido;
                    existingCompRecibido.FecEmi = FecEmi;
                    existingCompRecibido.FecVcto = FecVcto;
                    existingCompRecibido.FecCanc = FecCanc;
                    existingCompRecibido.IdTipoMoneda = IdTipoMoneda;
                    existingCompRecibido.IdTipoOrden = IdTipoOrden;
                    existingCompRecibido.TxtObserv = TxtObserv;
                    existingCompRecibido.MtoTcVta = MtoTcVta;
                    existingCompRecibido.MtoNeto = MtoNeto;
                    existingCompRecibido.MtoExonerado = MtoExonerado;
                    existingCompRecibido.MtoNoAfecto = MtoNoAfecto;
                    existingCompRecibido.MtoDsctoTot = MtoDsctoTot;
                    existingCompRecibido.MtoCmsTot = MtoCmsTot;
                    existingCompRecibido.MtoFleteTot = MtoFleteTot;
                    existingCompRecibido.MtoSubTot = MtoSubTot;
                    existingCompRecibido.MtoImptoTot = MtoImptoTot;
                    existingCompRecibido.MtoServicio = MtoServicio;
                    existingCompRecibido.MtoTotComp = MtoTotComp;
                    existingCompRecibido.RefIdCompRecibido = RefIdCompRecibido;
                    existingCompRecibido.RefTipoComprobante = RefTipoComprobante;
                    existingCompRecibido.RefFecha = RefFecha;
                    existingCompRecibido.RefSerie = RefSerie;
                    existingCompRecibido.RefNumero = RefNumero;
                    existingCompRecibido.TaxPor01 = TaxPor01;
                    existingCompRecibido.TaxPor02 = TaxPor02;
                    existingCompRecibido.TaxPor03 = TaxPor03;
                    existingCompRecibido.TaxPor04 = TaxPor04;
                    existingCompRecibido.TaxPor05 = TaxPor05;
                    existingCompRecibido.TaxPor06 = TaxPor06;
                    existingCompRecibido.TaxPor07 = TaxPor07;
                    existingCompRecibido.TaxPor08 = TaxPor08;
                    existingCompRecibido.TaxMto01 = TaxMto01;
                    existingCompRecibido.TaxMto02 = TaxMto02;
                    existingCompRecibido.TaxMto03 = TaxMto03;
                    existingCompRecibido.TaxMto04 = TaxMto04;
                    existingCompRecibido.TaxMto05 = TaxMto05;
                    existingCompRecibido.TaxMto06 = TaxMto06;
                    existingCompRecibido.TaxMto07 = TaxMto07;
                    existingCompRecibido.TaxMto08 = TaxMto08;
                    existingCompRecibido.Info01 = Info01;
                    existingCompRecibido.Info02 = Info02;
                    existingCompRecibido.Info03 = Info03;
                    existingCompRecibido.Info04 = Info04;
                    existingCompRecibido.Info05 = Info05;
                    existingCompRecibido.Info06 = Info06;
                    existingCompRecibido.Info07 = Info07;
                    existingCompRecibido.Info08 = Info08;
                    existingCompRecibido.Info09 = Info09;
                    existingCompRecibido.Info10 = Info10;
                    existingCompRecibido.InfoDate01 = InfoDate01;
                    existingCompRecibido.InfoDate02 = InfoDate02;
                    existingCompRecibido.InfoDate03 = InfoDate03;
                    existingCompRecibido.InfoDate04 = InfoDate04;
                    existingCompRecibido.InfoDate05 = InfoDate05;
                    existingCompRecibido.InfoMto01 = InfoMto01;
                    existingCompRecibido.InfoMto02 = InfoMto02;
                    existingCompRecibido.InfoMto03 = InfoMto03;
                    existingCompRecibido.InfoMto04 = InfoMto04;
                    existingCompRecibido.InfoMto05 = InfoMto05;
                    existingCompRecibido.Post = Post;
                    existingCompRecibido.PostDate = PostDate;
                    existingCompRecibido.TxtUsuario = txtusuario;
                    existingCompRecibido.IdEstado = 1;
                    existingCompRecibido.TxtEstado = "ACTIVO";
                    existingCompRecibido.IdProveedor = IdProveedor;
                    existingCompRecibido.IdUsuario = idusuario;
                    existingCompRecibido.IdLocation = IdLocation;
                    existingCompRecibido.IdCliente = IdCliente;
                    existingCompRecibido.TipoCompra = TipoCompra;

                    _context.Tnst01CompRecibidos.Update(existingCompRecibido);
                    await _context.SaveChangesAsync();

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idProductosEnArreglo = productosCSeleccionados.Select(p => p.idProducto).ToList();

                    // Actualizar registros existentes
                    foreach (ProductoS producto in productosCSeleccionados)
                    {
                        var existingCompRecibidodtl = _context.Tnst02CompRecibidoDtls
                           .FirstOrDefault(d => d.IdCompRecibido == comprobanteId && d.IdProducto == producto.idProducto);

                        if (existingCompRecibidodtl != null)
                        {
                            // Actualiza los valores
                            existingCompRecibidodtl.IdProducto = producto.idProducto;
                            existingCompRecibidodtl.TxtProducto = producto.nombreProducto;
                            existingCompRecibidodtl.IdUm = null;
                            existingCompRecibidodtl.Cantidad = producto.cantidad;
                            existingCompRecibidodtl.Peso = null;
                            existingCompRecibidodtl.PorDscto = producto.descuento;
                            existingCompRecibidodtl.MtoDsctoSinTax = 0;
                            existingCompRecibidodtl.MtoDsctoConTax = 0;
                            existingCompRecibidodtl.PunitSinTax = producto.monto_si;
                            existingCompRecibidodtl.PunitConTax = producto.monto_ci;
                            existingCompRecibidodtl.TaxPorTot = producto.igv;
                            existingCompRecibidodtl.TaxMtoTot = producto.mtoigv;
                            existingCompRecibidodtl.TaxPor01 = producto.igv;
                            existingCompRecibidodtl.TaxPor02 = 0;
                            existingCompRecibidodtl.TaxPor03 = 0;
                            existingCompRecibidodtl.TaxPor04 = 0;
                            existingCompRecibidodtl.TaxPor05 = 0;
                            existingCompRecibidodtl.TaxPor06 = 0;
                            existingCompRecibidodtl.TaxPor07 = 0;
                            existingCompRecibidodtl.TaxPor08 = 0;
                            existingCompRecibidodtl.TaxMto01 = producto.mtoigv;
                            existingCompRecibidodtl.TaxMto02 = 0;
                            existingCompRecibidodtl.TaxMto03 = 0;
                            existingCompRecibidodtl.TaxMto04 = 0;
                            existingCompRecibidodtl.TaxMto05 = 0;
                            existingCompRecibidodtl.TaxMto06 = 0;
                            existingCompRecibidodtl.TaxMto07 = 0;
                            existingCompRecibidodtl.TaxMto08 = 0;
                            existingCompRecibidodtl.MtoVtaSinTax = producto.neto;
                            existingCompRecibidodtl.MtoVtaConTax = producto.total;
                            existingCompRecibidodtl.IdRazon = null;
                            existingCompRecibidodtl.TxtObserv = producto.observacion;
                            existingCompRecibidodtl.IdEstado = 1;
                            existingCompRecibidodtl.TxtEstado = "ACTIVO";

                            _context.Tnst02CompRecibidoDtls.Update(existingCompRecibidodtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    //Crear nuevos registros
                    foreach (ProductoS producto in productosCSeleccionados)
                    {
                        var existingCompEmitidodtl = _context.Tnst02CompRecibidoDtls
                           .FirstOrDefault(d => d.IdCompRecibido == comprobanteId && d.IdProducto == producto.idProducto);

                        if (existingCompEmitidodtl == null)
                        {
                            // Crea un nuevo registro
                            var tnst02CompRecibidoDtl = new Tnst02CompRecibidoDtl
                            {
                                IdCompRecibido = (long)(comprobanteId),
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
                            _context.Tnst02CompRecibidoDtls.Add(tnst02CompRecibidoDtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Eliminar registros que no están en el arreglo
                    var registrosParaEliminar = _context.Tnst02CompRecibidoDtls
                        .Where(d => d.IdCompRecibido == comprobanteId && !idProductosEnArreglo.Contains(d.IdProducto))
                        .ToList();

                    _context.Tnst02CompRecibidoDtls.RemoveRange(registrosParaEliminar);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    //COMPROBANTE
                    var tnst01CompRecibido = new Tnst01CompRecibido
                    {
                        NroCompRecibido = NroCompRecibido,
                        IdTipoComp = IdTipoComp,
                        TxtSerie = TxtSerie,
                        TxtNumero = TxtNumero,
                        FecRegistro = FecRegistro,
                        FecRegRecibido = FecRegRecibido,
                        FecEmi = FecEmi,
                        FecVcto = FecVcto,
                        FecCanc = FecCanc,
                        IdTipoMoneda = IdTipoMoneda,
                        IdTipoOrden = IdTipoOrden,
                        TxtObserv = TxtObserv,
                        MtoTcVta = MtoTcVta,
                        MtoNeto = MtoNeto,
                        MtoExonerado = MtoExonerado,
                        MtoNoAfecto = MtoNoAfecto,
                        MtoDsctoTot = MtoDsctoTot,
                        MtoCmsTot = MtoCmsTot,
                        MtoFleteTot = MtoFleteTot,
                        MtoSubTot = MtoSubTot,
                        MtoImptoTot = MtoImptoTot,
                        MtoServicio = MtoServicio,
                        MtoTotComp = MtoTotComp,
                        RefIdCompRecibido = RefIdCompRecibido,
                        RefTipoComprobante = RefTipoComprobante,
                        RefFecha = RefFecha,
                        RefSerie = RefSerie,
                        RefNumero = RefNumero,
                        TaxPor01 = TaxPor01,
                        TaxPor02 = TaxPor02,
                        TaxPor03 = TaxPor03,
                        TaxPor04 = TaxPor04,
                        TaxPor05 = TaxPor05,
                        TaxPor06 = TaxPor06,
                        TaxPor07 = TaxPor07,
                        TaxPor08 = TaxPor08,
                        TaxMto01 = TaxMto01,
                        TaxMto02 = TaxMto02,
                        TaxMto03 = TaxMto03,
                        TaxMto04 = TaxMto04,
                        TaxMto05 = TaxMto05,
                        TaxMto06 = TaxMto06,
                        TaxMto07 = TaxMto07,
                        TaxMto08 = TaxMto08,
                        Info01 = Info01,
                        Info02 = Info02,
                        Info03 = Info03,
                        Info04 = Info04,
                        Info05 = Info05,
                        Info06 = Info06,
                        Info07 = Info07,
                        Info08 = Info08,
                        Info09 = Info09,
                        Info10 = Info10,
                        InfoDate01 = InfoDate01,
                        InfoDate02 = InfoDate02,
                        InfoDate03 = InfoDate03,
                        InfoDate04 = InfoDate04,
                        InfoDate05 = InfoDate05,
                        InfoMto01 = InfoMto01,
                        InfoMto02 = InfoMto02,
                        InfoMto03 = InfoMto03,
                        InfoMto04 = InfoMto04,
                        InfoMto05 = InfoMto05,
                        Post = Post,
                        PostDate = PostDate,
                        SnCredito = SnCredito,
                        SnCancelada = SnCancelada,
                        TxtUsuario = txtusuario,
                        IdEstado = 1,
                        TxtEstado = "ACTIVO",
                        IdProveedor = IdProveedor,
                        IdUsuario = idusuario,
                        IdLocation = IdLocation,
                        IdCliente = IdCliente,
                        TipoCompra = TipoCompra

                    };

                    _context.Tnst01CompRecibidos.Add(tnst01CompRecibido);
                    await _context.SaveChangesAsync();
                    comprobanteId = tnst01CompRecibido.IdCompRecibido;

                    if (comprobanteId != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE
                        foreach (ProductoS producto in productosCSeleccionados)
                        {

                            var tnst02CompRecibidoDtl = new Tnst02CompRecibidoDtl
                            {
                                IdCompRecibido = (long)(comprobanteId),
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
                            _context.Tnst02CompRecibidoDtls.Add(tnst02CompRecibidoDtl);
                            await _context.SaveChangesAsync();


                        }
                    }



                }

                return Json(new { mensaje = "Guardado correctamente", comprobanteId });
            }

            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante el proceso de guardado
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }

        // GET: Tnst01CompRecibido/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Tnst01CompRecibidos == null)
            {
                return NotFound();
            }

            var tnst01CompRecibido = await _context.Tnst01CompRecibidos.FindAsync(id);
            if (tnst01CompRecibido == null)
            {
                return NotFound();
            }
            ViewData["IdLocation"] = new SelectList(_context.Mstt08Locations, "IdLocation", "IdLocation", tnst01CompRecibido.IdLocation);
            ViewData["IdProveedor"] = new SelectList(_context.Pert03Proveedors, "IdProveedor", "IdProveedor", tnst01CompRecibido.IdProveedor);
            ViewData["IdTipoComp"] = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "IdTipoComp", tnst01CompRecibido.IdTipoComp);
            ViewData["IdTipoMoneda"] = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "IdTipoMoneda", tnst01CompRecibido.IdTipoMoneda);
            ViewData["IdTipoOrden"] = new SelectList(_context.Mstt03TipoOrdens, "IdTipoOrden", "IdTipoOrden", tnst01CompRecibido.IdTipoOrden);
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", tnst01CompRecibido.IdUsuario);
            return View(tnst01CompRecibido);
        }

        // POST: Tnst01CompRecibido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCompRecibido,NroCompRecibido,IdTipoComp,TxtSerie,TxtNumero,FecRegistro,FecRegRecibido,FecEmi,FecVcto,FecCanc,IdTipoMoneda,IdTipoOrden,TxtObserv,MtoTcVta,MtoNeto,MtoExonerado,MtoNoAfecto,MtoDsctoTot,MtoCmsTot,MtoFleteTot,MtoSubTot,MtoImptoTot,MtoServicio,MtoTotComp,RefIdCompRecibido,RefTipoComprobante,RefFecha,RefSerie,RefNumero,TaxPor01,TaxPor02,TaxPor03,TaxPor04,TaxPor05,TaxPor06,TaxPor07,TaxPor08,TaxMto01,TaxMto02,TaxMto03,TaxMto04,TaxMto05,TaxMto06,TaxMto07,TaxMto08,Info01,Info02,Info03,Info04,Info05,Info06,Info07,Info08,Info09,Info10,InfoDate01,InfoDate02,InfoDate03,InfoDate04,InfoDate05,InfoMto01,InfoMto02,InfoMto03,InfoMto04,InfoMto05,Post,PostDate,SnCredito,SnCancelada,IdUsuarioModificador,TxtUsuarioModificador,FechaModificacion,TxtUsuario,IdEstado,TxtEstado,IdProveedor,IdUsuario,IdLocation")] Tnst01CompRecibido tnst01CompRecibido)
        {
            if (id != tnst01CompRecibido.IdCompRecibido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tnst01CompRecibido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tnst01CompRecibidoExists(tnst01CompRecibido.IdCompRecibido))
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
            ViewData["IdLocation"] = new SelectList(_context.Mstt08Locations, "IdLocation", "IdLocation", tnst01CompRecibido.IdLocation);
            ViewData["IdProveedor"] = new SelectList(_context.Pert03Proveedors, "IdProveedor", "IdProveedor", tnst01CompRecibido.IdProveedor);
            ViewData["IdTipoComp"] = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "IdTipoComp", tnst01CompRecibido.IdTipoComp);
            ViewData["IdTipoMoneda"] = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "IdTipoMoneda", tnst01CompRecibido.IdTipoMoneda);
            ViewData["IdTipoOrden"] = new SelectList(_context.Mstt03TipoOrdens, "IdTipoOrden", "IdTipoOrden", tnst01CompRecibido.IdTipoOrden);
            ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario", tnst01CompRecibido.IdUsuario);
            return View(tnst01CompRecibido);
        }

        // GET: Tnst01CompRecibido/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Tnst01CompRecibidos == null)
            {
                return NotFound();
            }

            var tnst01CompRecibido = await _context.Tnst01CompRecibidos
                .Include(t => t.IdLocationNavigation)
                .Include(t => t.IdProveedorNavigation)
                .Include(t => t.IdTipoCompNavigation)
                .Include(t => t.IdTipoMonedaNavigation)
                .Include(t => t.IdTipoOrdenNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCompRecibido == id);
            if (tnst01CompRecibido == null)
            {
                return NotFound();
            }

            return View(tnst01CompRecibido);
        }

        // POST: Tnst01CompRecibido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Tnst01CompRecibidos == null)
            {
                return Problem("Entity set 'EagleContext.Tnst01CompRecibidos'  is null.");
            }
            var tnst01CompRecibido = await _context.Tnst01CompRecibidos.FindAsync(id);
            if (tnst01CompRecibido != null)
            {
                _context.Tnst01CompRecibidos.Remove(tnst01CompRecibido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Tnst01CompRecibidoExists(long id)
        {
            return (_context.Tnst01CompRecibidos?.Any(e => e.IdCompRecibido == id)).GetValueOrDefault();
        }



        public IActionResult Eliminar(int id)
        {
            var registro = _context.Tnst01CompRecibidos.FirstOrDefault(c => c.IdCompRecibido
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
            return RedirectToAction("boleta_c_listado"); // Reemplaza con tu acción deseada
        }


        public IActionResult editar(int id)
        {
            var cantidad = _context.Tnst01CompRecibidos.Include(t => t.IdLocationNavigation).
                Include(t => t.IdProveedorNavigation).Include(t => t.IdTipoCompNavigation).
                Include(t => t.IdTipoMonedaNavigation).Include(t => t.IdTipoOrdenNavigation).
                Include(t => t.IdUsuarioNavigation).Count();
            ViewBag.cantidad = cantidad;
            //Usuario
            var id_usuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario;
            ViewBag.idUsuario = id_usuario;
            ViewBag.txtUsuario = _pert01UsuarioController.GetUsuario();
            ViewData["IdTip"] = new SelectList(_context.Prot19TipoProdComs, "IdTipoProd", "TxtDesc");
            ViewData["IdMar"] = new SelectList(_context.Prot20Marcacoms, "IdMarca", "TxtDesc");
            //Cliente
            var proveedor = _context.Pert03Proveedors
                .Where(e => !string.IsNullOrEmpty(e.NroRuc) && !string.IsNullOrEmpty(e.TxtDireccion1))
                   .Select(e => new
                   {
                       IdProveedor = e.IdProveedor,
                       RUC = String.IsNullOrEmpty(e.NroRuc) ? e.NroDoc : e.NroRuc,
                       razon = e.TxtRznSocial,
                       direccion = e.TxtDireccion1
                   })
               .ToList();
            ViewBag.Proveedor = proveedor;

            var productos = _context.Prot18Productocoms.ToList().
                Where(p => p.MtoPvpuSinIgv != null).Select(
                e => new
                {
                    IdProducto = e.IdProducto,
                    NombreProducto = e.TxtDesc,
                    UmPro = e.IdUm,
                    Monto_si = e.MtoPvpuSinIgv,
                    Monto_ci = e.MtoPvpuConIgv,
                    UM = e.IdUm,
                    igv = e.PorImpto
                });


            ViewBag.Productos = productos;


            //ViewData["IdMesa"] = new SelectList(_context.Mstt14Mesas, "IdMesa", "IdMesa");
            //ViewData["IdTipoComp"] = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "IdTipoComp");
            ViewData["IdTipoMoneda"] = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "IdTipoMoneda");
            ViewData["IdTurno"] = new SelectList(_context.Mstt13Turnos, "IdTurno", "IdTurno");
            //ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            var comprobantevista = _context.Tnst01CompRecibidos
            .Where(c => c.IdCompRecibido == id)
            .FirstOrDefault();
            var comprobante = _context.Tnst01CompRecibidos.Include(t => t.IdLocationNavigation).
                Include(t => t.IdProveedorNavigation).Include(t => t.IdTipoCompNavigation).
                Include(t => t.IdTipoMonedaNavigation).Include(t => t.IdTipoOrdenNavigation).
                Include(t => t.IdUsuarioNavigation).Where(c => c.IdCompRecibido == id)

            .Select(comprobante => new
            {
                Comprobante = comprobante,
                RUCproveedor = $"{comprobante.IdProveedorNavigation.NroRuc}",
                MontoT = comprobante.MtoTotComp,
                igv = comprobante.TaxMto01,
                MontoN = comprobante.MtoNeto,
                FecEmi = comprobante.FecEmi,
                Razon = $"{comprobante.IdProveedorNavigation.TxtRznSocial}",
                ruc = comprobante.IdProveedorNavigation.NroRuc,
                direccion = $"{comprobante.IdProveedorNavigation.TxtDireccion1}",
                Id = comprobante.IdCompRecibido,
                IdProveedor = comprobante.IdProveedor

            })
            .FirstOrDefault();
            //  Creamos nuevas columnas para manejarlas y a parte el mismo registro de comprobante



            if (comprobante != null)
            {
                var detallesComprobante = _context.Tnst02CompRecibidoDtls
                .Where(detalle => detalle.IdCompRecibido == id && detalle.IdEstado == 1)
                .Select(detalle => new
                {
                    detalle = detalle,
                    tipo = detalle.IdProductoNavigation.IdModeloNavigation.IdMarcaNavigation.IdTipoProdNavigation.TxtDesc,
                    marca = detalle.IdProductoNavigation.IdModeloNavigation.IdMarcaNavigation.TxtDesc,
                    modelo = detalle.IdProductoNavigation.IdModeloNavigation.TxtDesc
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

                // Pasar los datos serializados a la vista utilizando ViewBag
                ViewBag.numpedido = comprobante.Comprobante.NroCompRecibido;

                ViewBag.Comprobante = comprobanteSerialized;
                ViewBag.Detalles = detallesSerialized;

                // Redirige a la vista 'boleta_v'
                return View("boleta_c", comprobantevista);
            }
            else
            {
                // Manejar el caso donde el comprobante no existe
                return NotFound(); // O redirigir a otra página
            }
        }


    }

}



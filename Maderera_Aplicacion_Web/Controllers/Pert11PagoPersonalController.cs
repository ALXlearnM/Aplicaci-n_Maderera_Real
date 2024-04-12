using Maderera_Aplicacion_Web.Data.Interfaces;
using Maderera_Aplicacion_Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Maderera_Aplicacion_Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pert11PagoPersonalController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;
        private readonly long idusuario;
        private readonly string txtusuario;
        private readonly DateTime fechaHoy;
        private static long? IdTemporal { get; set; }
        private static long? Temporalidcrono { get; set; }
        private static long? IdTemporalPredio { get; set; }
        private static long? IdTemporalCampana { get; set; }
        private static decimal? cuotatemp{ get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public Pert11PagoPersonalController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Tnst01CompRecibido
        public async Task<IActionResult> Index()
        {
            var eagleContext = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).Include(t => t.IdPredioNavigation).Include(t => t.IdCampanaNavigation).Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation);
            var cantidad = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).Include(t => t.IdPredioNavigation).Include(t => t.IdCampanaNavigation).Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation).Count();

            return View(await eagleContext.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {

            var pert11PagoPersonal = await _context.Pert11PagoPersonals
                .Include(t => t.IdAutorizadorNavigation)
                .Include(t => t.IdCampanaNavigation)
                .Include(t => t.IdConceptoNavigation)
                .Include(t => t.IdEmpleadoNavigation)
                .Include(t => t.IdPredioNavigation)
                .FirstOrDefaultAsync(m => m.IdPagoPersonal == id);
            if (pert11PagoPersonal == null)
            {
                return NotFound();
            }

            return View(pert11PagoPersonal);
        }





        [HttpGet]

        public IActionResult Obtenercampaña(int predioId)
        {
            // Obtiene la marca según el tipo seleccionado
            var campana = _context.Pret02Campanas
                .Where(p => p.IdPredio == predioId)
                .Select(p => new { idCampana = p.IdCampana, codigo = p.CodigoCampana })
                .ToList();

            return Json(campana);
        }




        public IActionResult pago_p_listado()
        {
            var listadoComprobantesActivos = _context.Pert11PagoPersonals
                .Include(t => t.IdEmpleadoNavigation).Include(t => t.IdAutorizadorNavigation)
                .Include(t => t.IdConceptoNavigation).Include(t => t.IdPredioNavigation).Include(t => t.IdTipopagoNavigation)
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
                fecha = comprobante.Fecha,
                campana = comprobante.IdCampana,
                usuario = $"{comprobante.IdAutorizadorNavigation.TxtUsuario}",
                concepto = $"{comprobante.IdConceptoNavigation.TxtDesc}",
                predio = $"{comprobante.IdPredioNavigation.UnidadCatastral}",
                estado = comprobante.TxtEstado,
                salida = comprobante.MontoTotal,
                tipo = comprobante.IdTipopagoNavigation.TxtDesc,
                Id = comprobante.IdPagoPersonal

            }).ToList();

            ViewBag.Comprobantes = comprobantesViewModel;
            return View();
        }

        [HttpPost]

        public IActionResult CrearConcepto(string? txt_desc)
        {
            try
            {

                // Crea una instancia de la entidad Cliente
                var concepto = new Pert10Concepto
                {
                    TxtDesc = txt_desc
                };

                // Agrega el cliente a tu contexto y guarda los cambios
                _context.Pert10Conceptos.Add(concepto);
                _context.SaveChanges();
                ViewBag.Concepto = concepto;

                return Json(new { message = "Concepto creado correctamente" });
            }
            catch (Exception ex)
            {
                // Retorna un error en caso de que ocurra una excepción
                return BadRequest("Error al crear el Concepto: " + ex.Message);
            }
        }



        [HttpPost]

        public IActionResult RecargarConcepto()
        {
            try
            {
                var updatedConcepto = _context.Pert10Conceptos

                   .Select(e => new
                   {
                       idConcepto = e.IdConcepto,
                       nombre = e.TxtDesc
                   })
               .ToList();
                ViewBag.Concepto = updatedConcepto;



                return Json(updatedConcepto);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el Concepto: " + ex.Message);
            }
        }
        [HttpPost]

        public async Task<ActionResult> Guardar(
            long IdEmpleado,
            int Tipo,
            DateTime Fecha,
            DateTime Mes_anho,
            bool Estado,
            long IdConcepto,
            decimal MontoT,
            decimal MontoP,
            decimal MontoD

            )
        {
            try
            {
                var idusuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario()).IdUsuario;
                var txtusuario = _pert01UsuarioController.GetUsuario();
                //CONVERTIR EL STRING A UNA LISTA YA QUE LLEGA DESDE JSon


                var existingPagoPersonal = _context.Pert11PagoPersonals
            .FirstOrDefault(c => c.IdPagoPersonal == IdTemporal);

                if (existingPagoPersonal != null)
                {
                    //COMPROBANTE

                    existingPagoPersonal.IdEmpleado = IdEmpleado;
                    existingPagoPersonal.IdTipopago = Tipo;
                    existingPagoPersonal.Fecha = Fecha;
                    existingPagoPersonal.Mes = Mes_anho.Month.ToString();
                    existingPagoPersonal.Anho = Mes_anho.Year.ToString();
                    existingPagoPersonal.IdAutorizador = idusuario;
                    existingPagoPersonal.IdPredio =(long) IdTemporalPredio;
                    existingPagoPersonal.IdCampana = (long)IdTemporalCampana;

                    existingPagoPersonal.Estado = String.Empty;

                    existingPagoPersonal.IdConcepto = IdConcepto;
                    existingPagoPersonal.MontoTotal = MontoT;
                    existingPagoPersonal.MontoPrestamo = MontoP;
                    existingPagoPersonal.MontoDeposito = MontoD;
                    existingPagoPersonal.IdUsuarioModificador = idusuario;
                    existingPagoPersonal.TxtUsuarioModificador= txtusuario;
                    existingPagoPersonal.FechaModificacion= fechaHoy;
                    if (existingPagoPersonal.IdEstado == 3)
                    {
                        existingPagoPersonal.IdEstado = Estado == true ? 3 : 1;
                        existingPagoPersonal.TxtEstado = Estado == true ? "BORRADOR" : "ACTIVO";
                    }
                    IdTemporal = existingPagoPersonal.IdPagoPersonal;
                    _context.Pert11PagoPersonals.Update(existingPagoPersonal);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    //COMPROBANTE
                    var pert11PagoPersonals = new Pert11PagoPersonal
                    {

                        IdEmpleado = IdEmpleado,
                        IdTipopago = Tipo,
                        Fecha = Fecha,
                        Mes = Mes_anho.Month.ToString(),
                        Anho= Mes_anho.Year.ToString(),
                        IdAutorizador = idusuario,
                        IdPredio = (long)IdTemporalPredio,
                        IdCampana = (long)IdTemporalCampana,
                        Estado = String.Empty,
                        IdConcepto = IdConcepto,
                        MontoTotal = MontoT,
                        MontoPrestamo = MontoP,
                        MontoDeposito= MontoD,
                        IdEstado = Estado == true ? 3 : 6,
                        TxtEstado = Estado == true ? "BORRADOR" : "PRESTADO",
                        IdUsuario=idusuario,
                        TxtUsuario=txtusuario,
                        FechaCreacion =fechaHoy
                    };

                    _context.Pert11PagoPersonals.Add(pert11PagoPersonals);
                    await _context.SaveChangesAsync();
                    IdTemporal = pert11PagoPersonals.IdPagoPersonal;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE

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

        public async Task<ActionResult> GuardaryCerrar(
            long IdEmpleado,
            int Tipo,
            DateTime Fecha,
            DateTime Mes_anho,
            bool Estado,
            long IdConcepto,
            decimal MontoT,
            decimal MontoP,
            decimal MontoD

            )
        {
            try
            {
                var idusuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario()).IdUsuario;
                var txtusuario = _pert01UsuarioController.GetUsuario();
                //CONVERTIR EL STRING A UNA LISTA YA QUE LLEGA DESDE JSon


                var existingPagoPersonal = _context.Pert11PagoPersonals
            .FirstOrDefault(c => c.IdPagoPersonal == IdTemporal);

                if (existingPagoPersonal != null)
                {
                    //COMPROBANTE

                    existingPagoPersonal.IdEmpleado = IdEmpleado;
                    existingPagoPersonal.IdTipopago = Tipo;
                    existingPagoPersonal.Fecha = Fecha;
                    existingPagoPersonal.Mes = Mes_anho.Month.ToString();
                    existingPagoPersonal.Anho = Mes_anho.Year.ToString();
                    existingPagoPersonal.IdAutorizador = idusuario;
                    existingPagoPersonal.IdPredio = (long)IdTemporalPredio;
                    existingPagoPersonal.IdCampana = (long)IdTemporalCampana;

                    existingPagoPersonal.Estado = String.Empty;

                    existingPagoPersonal.IdConcepto = IdConcepto;
                    existingPagoPersonal.MontoTotal = MontoT;
                    existingPagoPersonal.MontoPrestamo = MontoP;
                    existingPagoPersonal.MontoDeposito = MontoD;
                    existingPagoPersonal.IdUsuarioModificador = idusuario;
                    existingPagoPersonal.TxtUsuarioModificador = txtusuario;
                    existingPagoPersonal.FechaModificacion = fechaHoy;
                    if (existingPagoPersonal.IdEstado == 3)
                    {
                        existingPagoPersonal.IdEstado = Estado == true ? 3 : 1;
                        existingPagoPersonal.TxtEstado = Estado == true ? "BORRADOR" : "ACTIVO";
                    }
                    IdTemporal = existingPagoPersonal.IdPagoPersonal;
                    _context.Pert11PagoPersonals.Update(existingPagoPersonal);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    //COMPROBANTE
                    var pert11PagoPersonals = new Pert11PagoPersonal
                    {

                        IdEmpleado = IdEmpleado,
                        IdTipopago = Tipo,
                        Fecha = Fecha,
                        Mes = Mes_anho.Month.ToString(),
                        Anho = Mes_anho.Year.ToString(),
                        IdAutorizador = idusuario,
                        IdPredio = (long)IdTemporalPredio,
                        IdCampana = (long)IdTemporalCampana,
                        Estado = String.Empty,
                        IdConcepto = IdConcepto,
                        MontoTotal = MontoT,
                        MontoPrestamo = MontoP,
                        MontoDeposito = MontoD,
                        IdEstado = Estado == true ? 3 : 6,
                        TxtEstado = Estado == true ? "BORRADOR" : "PRESTADO",
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        FechaCreacion = fechaHoy
                    };

                    _context.Pert11PagoPersonals.Add(pert11PagoPersonals);
                    await _context.SaveChangesAsync();
                    IdTemporal = pert11PagoPersonals.IdPagoPersonal;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE

                    }



                }
                IdTemporal = null;
                IdTemporalCampana = null;
                IdTemporalPredio = null;
                Temporalidcrono = null;
                cuotatemp = null;
                return Json(new { redirectUrl = Url.Action("pago_p_listado", "Pert11PagoPersonal") });
            }

            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante el proceso de guardado
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }

        [HttpGet]
        public ActionResult Buscarprestamos(long? idempleado)
        {
            var existingprest = _context.Pret33Prestamos.Where(p => p.IdEmpleado == idempleado && p.IdEstado==6).FirstOrDefault();
            if (existingprest != null)
            {
                Temporalidcrono = existingprest.IdPrestamo;
                decimal? montoc= existingprest.MontoCuota;
                cuotatemp = montoc;
                return Json(new { estado= existingprest.IdPrestamo,montoc=montoc});
            }
            else
            {
                return Json(new {estado=-1});
            }
        }
        [HttpGet]
        public ActionResult returncuota()
        {
            return Json(new {cuota=cuotatemp});
        }
        [HttpGet]
        public void removeempleado()
        {
            cuotatemp = null;

        }
        [HttpGet]
        public IActionResult CargarMood()
        {
            if (IdTemporal != null)
            {
                var estado = _context.Pert11PagoPersonals.Where(t => t.IdPagoPersonal== IdTemporal).Select(t => t.IdEstado).FirstOrDefault();
                var estadoName = _context.Pert11PagoPersonals.Where(t => t.IdPagoPersonal== IdTemporal).Select(t => t.TxtEstado).FirstOrDefault();
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
        public IActionResult Cronograma()
        {
            ViewBag.ListadoPers = _context.Pret34CronogramaPagos.Where(P => (P.IdEstado == 3 || P.IdEstado == 7 || P.IdEstado == 8) && P.IdPrestamo ==Temporalidcrono).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult VolverPago()
        {
            try
            {

                return Json(new { redirectUrl = Url.Action("EditarPago", "Pret11PagoPersonal", new { idpago= IdTemporal }) });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var registro = _context.Pert11PagoPersonals.FirstOrDefault(c => c.IdPagoPersonal
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
            return RedirectToAction("pago_p_listado"); // Reemplaza con tu acción deseada
        }
        public IActionResult pago_p()
        {
            IdTemporal = null;
            IdTemporalCampana = null;
            IdTemporalPredio = null;
            Temporalidcrono = null;

            var empleado = _context.Pert04Empleados
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdEstado == 1)
                   .Select(e => new
                   {
                       IdEmpleado = e.IdEmpleado,
                       nombre = String.IsNullOrEmpty(e.TxtRznSocial) ? e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}" : e.TxtRznSocial,
                       apellido = e.TxtApePat,
                       codigo = e.CodEmpleado
                   })
               .ToList();
            ViewBag.Empleado = empleado;

            var concepto = _context.Pert10Conceptos

                   .Select(e => new
                   {
                       IdConcepto = e.IdConcepto,
                       nombre = e.TxtDesc

                   })
               .ToList();
            ViewBag.Concepto = concepto;
            var Predios = _context.Pret01Predios
                    .Where(e => e.IdEstado == 1)
                    .Select(e => new
                    {
                        id = e.IdPredio,
                        unidC = e.UnidadCatastral.Trim(),
                        nroS = e.NroSitio,
                        fechA = e.FechaAdquisicion,
                        fechC = e.FechaCompra,

                    })
                    .ToList();

            ViewBag.Predios = Predios;
            // Obtener los datos de la tabla Pert10Concepto



            var autorizarFiltrados = _context.Pert04Empleados
                                    .Where(e => e.IdCategoriaEmp == 4)
                                    .Select(e => new
                                    {
                                        IdEmpleado = e.IdEmpleado,
                                        NombreCompleto = $"{e.TxtPriNom} {e.TxtApePat}"
                                    });

            ViewBag.Autorizar = new SelectList(autorizarFiltrados, "IdEmpleado", "NombreCompleto");
            ViewBag.IdUsuario = new SelectList(_context.Pert01Usuarios, "IdUsuario", "TxtUsuario");
            ViewData["IdPre"] = new SelectList(_context.Pret01Predios, "IdPredio", "UnidadCatastral");
            ViewData["IdTipoPago"] = new SelectList(_context.Pret36TipoPagos, "IdTipopago", "TxtDesc");
            return View();
        }
        public IActionResult EditarPago(int idpago)
        {
            IdTemporal = null;
            IdTemporalCampana = null;
            IdTemporalPredio = null;
            Temporalidcrono = null;

            var cantidad = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).
                Include(t => t.IdPredioNavigation).Include(t => t.IdCampanaNavigation).
                Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation).
                Count();
            ViewBag.cantidad = cantidad;

            ViewData["IdPre"] = new SelectList(_context.Pret01Predios, "IdPredio", "UnidadCatastral");
            //Cliente
            var empleado = _context.Pert04Empleados
                .Where(e => e.IdDist != null && !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdEstado == 1)
                  .Select(e => new
                  {
                      IdEmpleado = e.IdEmpleado,
                      nombre = String.IsNullOrEmpty(e.TxtRznSocial) ? e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}" : e.TxtRznSocial,
                      apellido = e.TxtApePat,
                      codigo = e.CodEmpleado
                  })
              .ToList();
            ViewBag.Empleado = empleado;

            var concepto = _context.Pert10Conceptos

                   .Select(e => new
                   {
                       IdConcepto = e.IdConcepto,
                       nombre = e.TxtDesc

                   })
               .ToList();
            ViewBag.Concepto = concepto;
            var Predios = _context.Pret01Predios
                    .Where(e => e.IdEstado == 1)
                    .Select(e => new
                    {
                        id = e.IdPredio,
                        unidC = e.UnidadCatastral.Trim(),
                        nroS = e.NroSitio,
                        fechA = e.FechaAdquisicion,
                        fechC = e.FechaCompra,

                    })
                    .ToList();

            ViewBag.Predios = Predios;

            //ViewData["IdMesa"] = new SelectList(_context.Mstt14Mesas, "IdMesa", "IdMesa");
            //ViewData["IdTipoComp"] = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "IdTipoComp");
            ViewData["IdTipoMoneda"] = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "IdTipoMoneda");
            ViewData["IdTurno"] = new SelectList(_context.Mstt13Turnos, "IdTurno", "IdTurno");
            ViewData["IdTipoPago"] = new SelectList(_context.Pret36TipoPagos, "IdTipopago", "TxtDesc");
            //ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            var comprobantevista = _context.Pert11PagoPersonals
            .Where(c => c.IdPagoPersonal == idpago)
            .FirstOrDefault();
            var comprobante = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).
                Include(t => t.IdPredioNavigation).Include(t => t.IdCampanaNavigation).
                Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation)
                .Where(c => c.IdPagoPersonal == idpago)

            .Select(comprobante => new
            {
                Comprobante = comprobante,
                nombre = $"{comprobante.IdEmpleadoNavigation.TxtPriNom}",
                tipo = comprobante.IdTipopago,
                fecha = comprobante.Fecha,
                mes = comprobante.Mes,
                idauto = $"{comprobante.IdAutorizadorNavigation.TxtUsuario}",
                predio = $"{comprobante.IdPredioNavigation.UnidadCatastral}",
                campaña = $"{comprobante.IdCampanaNavigation.CodigoCampana}",
                estado = comprobante.Estado,
                concepto = $"{comprobante.IdConceptoNavigation.TxtDesc}",
                monto = comprobante.MontoTotal,
                IdEmpleado = comprobante.IdEmpleado,
                IdConcepto = comprobante.IdConcepto

            })
            .FirstOrDefault();
            //  Creamos nuevas columnas para manejarlas y a parte el mismo registro de comprobante



            if (comprobante != null)
            {

                var existingprest = _context.Pret33Prestamos.Where(p => p.IdEmpleado == comprobante.IdEmpleado && p.IdEstado == 7).FirstOrDefault();
                Temporalidcrono = existingprest.IdPrestamo;
                // Establecer configuraciones para ignorar las referencias circulares
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                // Serializar los datos con las configuraciones establecidas
                var comprobanteSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(comprobante, settings);


                // Pasar los datos serializados a la vista utilizando ViewBag
                ViewBag.IdPagoPersonal = comprobante.Comprobante.IdPagoPersonal;

                ViewBag.Comprobante = comprobanteSerialized;
                IdTemporal = comprobante.Comprobante.IdPagoPersonal;

                // Redirige a la vista 'boleta_v'
                return View("pago_p", comprobantevista);
            }
            else
            {
                // Manejar el caso donde el comprobante no existe
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
                        unidC = e.UnidadCatastral.Trim(),
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
                            codC = e.CodigoCampana.Trim(),
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
                            codC = e.CodigoCampana.Trim(),
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
        public IActionResult CerrarCampanaPag()
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
        public IActionResult SelecCampana(long idCampana)
        {
            IdTemporalCampana = idCampana;
            return Json(new { success = true });


        }
    }
}

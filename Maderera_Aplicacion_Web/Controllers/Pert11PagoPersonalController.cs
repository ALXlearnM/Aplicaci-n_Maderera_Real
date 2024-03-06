using Maderera_Aplicacion_Web.Data.Interfaces;
using Maderera_Aplicacion_Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Maderera_Aplicacion_Web.Models;
using Newtonsoft.Json;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pert11PagoPersonalController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;

        public Pert11PagoPersonalController(EagleContext context, IPert01UsuarioController pert01UsuarioController)
        {
            _context = context;
            _pert01UsuarioController = pert01UsuarioController;
        }

        // GET: Tnst01CompRecibido
        public async Task<IActionResult> Index()
        {
            var eagleContext = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).Include(t => t.IdPredioNavigation).Include(t => t.IdCampañaNavigation).Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation);
            var cantidad = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).Include(t => t.IdPredioNavigation).Include(t => t.IdCampañaNavigation).Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation).Count();

            return View(await eagleContext.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {

            var pert11PagoPersonal = await _context.Pert11PagoPersonals
                .Include(t => t.IdAutorizadorNavigation)
                .Include(t => t.IdCampañaNavigation)
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



        //public IActionResult RecargarEmpleado()
        //{
        //    try
        //    {
        //        var updatedEmpleado = _context.Pert04Empleados

        //           .Select(e => new
        //           {
        //               Idempleado = e.IdEmpleado,
        //               nombre = String.IsNullOrEmpty(e.TxtRznSocial)? e.TxtPriNom == null ? e.TxtRznSocial : $"{e.TxtPriNom} {e.TxtApePat}":e.TxtRznSocial,
        //               apellido = e.TxtApePat,
        //               codigo = e.CodEmpleado
        //           })
        //       .ToList();
        //        ViewBag.Empleado = updatedEmpleado;



        //        return Json(updatedEmpleado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error al actualizar el Empleado: " + ex.Message);
        //    }
        //}


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
                .Include(t => t.IdConceptoNavigation).Include(t => t.IdPredioNavigation)
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
                fecha = comprobante.Fecha,
                campana = comprobante.IdCampaña,
                usuario = $"{comprobante.IdAutorizadorNavigation.TxtUsuario}",
                concepto = $"{comprobante.IdConceptoNavigation.TxtDesc}",
                predio = $"{comprobante.IdPredioNavigation.UnidadCatastral}",
                estado = comprobante.Estado,
                salida = comprobante.Monto,
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
            long? pagoId,
            long IdEmpleado,
            string Tipo,
            DateTime Fecha,
            string Mes,
            long IdAutorizador,
            long IdPredio,
            long IdCampaña,
            string Estado,
            long IdConcepto,
            decimal Monto,
            int IdEstado,
            string TxtEstado)
        {
            try
            {
                var idusuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario()).IdUsuario;
                var txtusuario = _pert01UsuarioController.GetUsuario();
                //CONVERTIR EL STRING A UNA LISTA YA QUE LLEGA DESDE JSon


                var existingPagoPersonal = _context.Pert11PagoPersonals
            .FirstOrDefault(c => c.IdPagoPersonal == pagoId);

                if (existingPagoPersonal != null)
                {
                    //COMPROBANTE

                    existingPagoPersonal.IdEmpleado = IdEmpleado;
                    existingPagoPersonal.Tipo = Tipo;
                    existingPagoPersonal.Fecha = Fecha;
                    existingPagoPersonal.Mes = Mes;
                    existingPagoPersonal.IdAutorizador = IdAutorizador;
                    existingPagoPersonal.IdPredio = IdPredio;
                    existingPagoPersonal.IdCampaña = IdCampaña;
                    existingPagoPersonal.Estado = Estado;
                    existingPagoPersonal.IdConcepto = IdConcepto;
                    existingPagoPersonal.Monto = Monto;
                    existingPagoPersonal.IdEstado = IdEstado;
                    existingPagoPersonal.TxtEstado = TxtEstado;

                    _context.Pert11PagoPersonals.Update(existingPagoPersonal);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    //COMPROBANTE
                    var pert11PagoPersonals = new Pert11PagoPersonal
                    {

                        IdEmpleado = IdEmpleado,
                        Tipo = Tipo,
                        Fecha = Fecha,
                        Mes = Mes,
                        IdAutorizador = IdAutorizador,
                        IdPredio = IdPredio,
                        IdCampaña = IdCampaña,
                        Estado = Estado,
                        IdConcepto = IdConcepto,
                        Monto = Monto,
                        IdEstado = IdEstado,
                        TxtEstado = TxtEstado
                    };

                    _context.Pert11PagoPersonals.Add(pert11PagoPersonals);
                    await _context.SaveChangesAsync();
                    pagoId = pert11PagoPersonals.IdPagoPersonal;

                    if (pagoId != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE

                    }



                }

                return Json(new { mensaje = "Guardado correctamente", pagoId });
            }

            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante el proceso de guardado
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }
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
            var id_usuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario;
            ViewBag.idUsuario = id_usuario;
            ViewBag.txtUsuario = _pert01UsuarioController.GetUsuario();

            var cantidad = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).
                Include(t => t.IdPredioNavigation).Include(t => t.IdCampañaNavigation).
                Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation).
                Count();
            ViewBag.cantidad = cantidad;

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
            return View();
        }
        public IActionResult editar(int id)
        {
            var cantidad = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).
                Include(t => t.IdPredioNavigation).Include(t => t.IdCampañaNavigation).
                Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation).
                Count();
            ViewBag.cantidad = cantidad;
            //Usuario
            var id_usuario = _context.Pert01Usuarios
                    .FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario;
            ViewBag.idUsuario = id_usuario;
            ViewBag.txtUsuario = _pert01UsuarioController.GetUsuario();
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


            //ViewData["IdMesa"] = new SelectList(_context.Mstt14Mesas, "IdMesa", "IdMesa");
            //ViewData["IdTipoComp"] = new SelectList(_context.Sntt10TipoComprobantes, "IdTipoComp", "IdTipoComp");
            ViewData["IdTipoMoneda"] = new SelectList(_context.Sntt04TipoMoneda, "IdTipoMoneda", "IdTipoMoneda");
            ViewData["IdTurno"] = new SelectList(_context.Mstt13Turnos, "IdTurno", "IdTurno");
            //ViewData["IdUsuario"] = new SelectList(_context.Pert01Usuarios, "IdUsuario", "IdUsuario");
            var comprobantevista = _context.Pert11PagoPersonals
            .Where(c => c.IdPagoPersonal == id)
            .FirstOrDefault();
            var comprobante = _context.Pert11PagoPersonals.Include(t => t.IdEmpleadoNavigation).
                Include(t => t.IdPredioNavigation).Include(t => t.IdCampañaNavigation).
                Include(t => t.IdConceptoNavigation).Include(t => t.IdAutorizadorNavigation)
                .Where(c => c.IdPagoPersonal == id)

            .Select(comprobante => new
            {
                Comprobante = comprobante,
                nombre = $"{comprobante.IdEmpleadoNavigation.TxtPriNom}",
                tipo = comprobante.Tipo,
                fecha = comprobante.Fecha,
                mes = comprobante.Mes,
                idauto = $"{comprobante.IdAutorizadorNavigation.TxtUsuario}",
                predio = $"{comprobante.IdPredioNavigation.UnidadCatastral}",
                campaña = $"{comprobante.IdCampañaNavigation.CodigoCampana}",
                estado = comprobante.Estado,
                concepto = $"{comprobante.IdConceptoNavigation.TxtDesc}",
                monto = comprobante.Monto,
                IdEmpleado = comprobante.IdEmpleado,
                IdConcepto = comprobante.IdConcepto

            })
            .FirstOrDefault();
            //  Creamos nuevas columnas para manejarlas y a parte el mismo registro de comprobante



            if (comprobante != null)
            {


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


                // Redirige a la vista 'boleta_v'
                return View("pago_p", comprobantevista);
            }
            else
            {
                // Manejar el caso donde el comprobante no existe
                return NotFound(); // O redirigir a otra página
            }
        }

    }
}

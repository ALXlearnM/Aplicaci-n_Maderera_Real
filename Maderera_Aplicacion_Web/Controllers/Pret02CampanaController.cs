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
namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pret02CampanaController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;
        private readonly long idusuario;
        private readonly string txtusuario;
        private readonly DateTime fechaHoy;
        private static long? IdTemporal { get; set; }
        private static long? IdPredioTemporal { get; set; }
        public Pret02CampanaController(EagleContext context, IPert01UsuarioController pert01UsuarioController)
        {
            _pert01UsuarioController = pert01UsuarioController;
            _context = context;
            // Asigna valores a las propiedades de solo lectura
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
        }


        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Pret02Campanas == null)
            {
                return NotFound();
            }

            var pret02Campana = await _context.Pret02Campanas
                .Include(p => p.IdDistritoNavigation)
                .Include(p => p.IdPredioNavigation)
                .Include(p => p.IdTipoCampanaNavigation)
                .FirstOrDefaultAsync(m => m.IdCampana == id);
            if (pret02Campana == null)
            {
                return NotFound();
            }

            return View(pret02Campana);
        }
        [HttpGet]
        public IActionResult ListadoCam(long id)
        {
            id = IdPredioTemporal == null ? id : (long)IdPredioTemporal;
            var ListadoPredio = _context.Pret02Campanas.Include(p => p.IdDistritoNavigation).
                Include(p => p.IdPredioNavigation).Include(p => p.IdTipoCampanaNavigation).Where(p => p.IdPredio == id);


            IdPredioTemporal = id == null ? IdPredioTemporal : id;
            ViewBag.ListadoPredio = ListadoPredio.Where(lp => lp.IdEstado == 1).ToList();

            return View();

        }

        public IActionResult Campana(long? id)
        {
            int CantCamp = _context.Pret02Campanas.Where(c => c.IdPredio == IdPredioTemporal).Count()+1;
            var txt_numero = CantCamp.ToString().PadLeft(2, '0');
            var UnidCat = _context.Pret01Predios.Where(p => p.IdPredio == IdPredioTemporal).Select(p=>p.UnidadCatastral).FirstOrDefault();
            string CodigoCampana = UnidCat + "-" + txt_numero;
            ViewBag.CodCamp = CodigoCampana;

            var nroSitio = _context.Pret01Predios
            .Where(p => p.IdPredio == IdPredioTemporal)
            .Select(p => p.NroSitio)
            .FirstOrDefault();
            ViewBag.NroSitio = nroSitio;
            var UC = _context.Pret01Predios
            .Where(p => p.IdPredio == IdPredioTemporal)
            .Select(p => p.UnidadCatastral)
            .FirstOrDefault();
            ViewBag.UC = UC;
            ViewData["IdDep"] = new SelectList(_context.Sntt31Departamentos, "IdDpto", "TxtDesc");
            // Obtén la lista original de Pret03TipoCampanas
            var tipoCampanas = _context.Pret03TipoCampanas.ToList();

            // Encuentra el índice del elemento con TxtDesc igual a "Reforestado"
            int indiceReforestado = tipoCampanas.FindIndex(x => x.TxtDesc == "Reforestado");

            // Mueve el elemento "Reforestado" al principio de la lista
            if (indiceReforestado > 0)
            {
                var elementoReforestado = tipoCampanas[indiceReforestado];
                tipoCampanas.RemoveAt(indiceReforestado);
                tipoCampanas.Insert(0, elementoReforestado);
            }

            // Crea el SelectList con la lista que tiene "Reforestado" al principio
            ViewData["IdTipoCampana"] = new SelectList(tipoCampanas, "IdTipoCampana", "TxtDesc");

            var ListadoTA = _context.Pret06TipoArbols.
                Select(e => new
                {
                    TxtDesc = e.Txtdesc,
                    IdTipoarbol = e.IdTipoarbol,

                })
                .ToList();

            ViewBag.TipoArbol = ListadoTA;
            var campanavista = _context.Pret02Campanas
            .Where(c => c.IdCampana == id && c.IdEstado == 1)
            .FirstOrDefault();

            var CampanaTAdpto = _context.Pret02Campanas
            .Include(t => t.IdDistritoNavigation)
            .ThenInclude(t => t.IdProvNavigation)
            .ThenInclude(t => t.IdDptoNavigation)
            .Include(t => t.IdTipoCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdCampana == id)
            .Select(campanaTA => new
            {
                dptocamp = campanaTA.IdDistritoNavigation.IdProvNavigation.IdDpto,
            })
            .FirstOrDefault();

            var CampanaTApro = _context.Pret02Campanas
            .Include(t => t.IdDistritoNavigation)
            .ThenInclude(t => t.IdProvNavigation)
            .ThenInclude(t => t.IdDptoNavigation)
            .Include(t => t.IdTipoCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdCampana == id)
            .Select(campanaTA => new
            {
                provcamp = campanaTA.IdDistritoNavigation.IdProv,
            })
            .FirstOrDefault();

            var CampanaTA = _context.Pret02Campanas
            .Include(t => t.IdDistritoNavigation)
            .ThenInclude(t => t.IdProvNavigation)
            .ThenInclude(t => t.IdDptoNavigation)
            .Include(t => t.IdTipoCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdCampana == id)
            .Select(campanaTA => new
            {
                CampanaTA = campanaTA,
                IdDistrito = campanaTA.IdDistritoNavigation.IdDist,
                tipocampana = campanaTA.IdTipoCampanaNavigation.TxtDesc,
                provcamp = campanaTA.IdDistritoNavigation.IdProvNavigation.IdProv,
                dptocamp = campanaTA.IdDistritoNavigation.IdProvNavigation.IdDptoNavigation.IdDpto,
            })
            .FirstOrDefault();

            if (CampanaTA != null)
            {
                var detallescampana = _context.Pret04CampanaTipoArbols
                .Where(detalle => detalle.IdCampana == id)
                .ToList();

                // Establecer configuraciones para ignorar las referencias circulares
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                // Serializar los datos con las configuraciones establecidas
                var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(CampanaTA, settings);
                var detallesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(detallescampana, settings);

                IdTemporal = campanavista?.IdCampana;

                ViewBag.Campana = campanaserialized;
                ViewBag.Detalles = detallesSerialized;
                ViewBag.dptocamp = Newtonsoft.Json.JsonConvert.SerializeObject(CampanaTAdpto, settings);
                ViewBag.provcamp = Newtonsoft.Json.JsonConvert.SerializeObject(CampanaTApro, settings);
                // Redirige a la vista 'boleta_v'
                return View("Campana", campanavista);
            }
            else
            {
                IdTemporal = null;
                return View();

            }
        }

        [HttpGet]
        public IActionResult EditarTA(long? idcampana)
        {

            var nroSitio = _context.Pret01Predios
            .Where(p => p.IdPredio == IdPredioTemporal && p.IdEstado == 1)
            .Select(p => p.NroSitio)
            .FirstOrDefault();
            ViewBag.NroSitio = nroSitio;
            var UC = _context.Pret01Predios
            .Where(p => p.IdPredio == IdPredioTemporal)
            .Select(p => p.UnidadCatastral)
            .FirstOrDefault();
            ViewBag.UC = UC;
            ViewData["IdTipoCampana"] = new SelectList(_context.Pret03TipoCampanas, "IdTipoCampana", "TxtDesc");

            ViewData["IdDep"] = new SelectList(_context.Sntt31Departamentos, "IdDpto", "TxtDesc");

            var ListadoTA = _context.Pret06TipoArbols.
                Select(e => new
                {
                    TxtDesc = e.Txtdesc,
                    IdTipoarbol = e.IdTipoarbol,

                })
                .ToList();
            ViewBag.TipoArbol = ListadoTA;
            ViewData["IdDep"] = new SelectList(_context.Sntt31Departamentos, "IdDpto", "TxtDesc");

            var campanavista = _context.Pret02Campanas
            .Where(c => c.IdCampana == idcampana && c.IdEstado == 1)
            .FirstOrDefault();

            var CampanaTAdpto = _context.Pret02Campanas
            .Include(t => t.IdDistritoNavigation)
            .ThenInclude(t => t.IdProvNavigation)
            .ThenInclude(t => t.IdDptoNavigation)
            .Include(t => t.IdTipoCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdCampana == idcampana && c.IdEstado == 1)
            .Select(campanaTA => new
            {
                dptocamp = campanaTA.IdDistritoNavigation.IdProvNavigation.IdDpto,
            })
            .FirstOrDefault();

            var CampanaTApro = _context.Pret02Campanas
            .Include(t => t.IdDistritoNavigation)
            .ThenInclude(t => t.IdProvNavigation)
            .ThenInclude(t => t.IdDptoNavigation)
            .Include(t => t.IdTipoCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdCampana == idcampana && c.IdEstado == 1)
            .Select(campanaTA => new
            {
                provcamp = campanaTA.IdDistritoNavigation.IdProv,
            })
            .FirstOrDefault();

            var CampanaTA = _context.Pret02Campanas
            .Include(t => t.IdDistritoNavigation)
            .ThenInclude(t => t.IdProvNavigation)
            .ThenInclude(t => t.IdDptoNavigation)
            .Include(t => t.IdTipoCampanaNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdCampana == idcampana && c.IdEstado == 1)
            .Select(campanaTA => new
            {
                CampanaTA = campanaTA,
                IdDistrito = campanaTA.IdDistritoNavigation.IdDist,
                tipocampana = campanaTA.IdTipoCampanaNavigation.TxtDesc,
                provcamp = campanaTA.IdDistritoNavigation.IdProvNavigation.IdProv,
                dptocamp = campanaTA.IdDistritoNavigation.IdProvNavigation.IdDptoNavigation.IdDpto,
            })
            .FirstOrDefault();

            if (CampanaTA != null)
            {
                var detallescampana = _context.Pret04CampanaTipoArbols
                .Where(detalle => detalle.IdCampana == idcampana)
                .ToList();

                // Establecer configuraciones para ignorar las referencias circulares
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                // Serializar los datos con las configuraciones establecidas
                var campanaserialized = Newtonsoft.Json.JsonConvert.SerializeObject(CampanaTA, settings);
                var detallesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(detallescampana, settings);

                IdTemporal = campanavista?.IdCampana;

                ViewBag.Campana = campanaserialized;
                ViewBag.Detalles = detallesSerialized;
                ViewBag.dptocamp = Newtonsoft.Json.JsonConvert.SerializeObject(CampanaTAdpto, settings);
                ViewBag.provcamp = Newtonsoft.Json.JsonConvert.SerializeObject(CampanaTApro, settings);
                // Redirige a la vista 'boleta_v'
                return View("Campana", campanavista);
            }
            else
            {
                // Manejar el caso donde el comprobante no existe
                return NotFound(); // O redirigir a otra página
            }
        }



        [HttpPost]
        public async Task<IActionResult> CrearCampana(int IdTipoCampana,  int NroHectarea, int NroArboles,
    float Area, string? Latitud, string? Longitud, DateTime FechaInicio, string campanatipoarbolSeleccionado)
        {
            try
            {
                //CONVERTIR EL STRING A UNA LISTA YA QUE LLEGA DESDE JSon
                var campanatipoarbolseleccionados = JsonConvert.DeserializeObject<List<campanatipoarbol>>(campanatipoarbolSeleccionado);
                var IdDistrito = _context.Pret01Predios.Where(p => p.IdPredio == IdPredioTemporal && p.IdEstado == 1).Select(p => p.IdDistrito).FirstOrDefault();
                var existingCampana = _context.Pret02Campanas
            .FirstOrDefault(c => c.IdCampana == IdTemporal);

                if (existingCampana != null)
                {
                    //COMPROBANTE
                    existingCampana.IdPredio = (long)IdPredioTemporal;
                    existingCampana.IdTipoCampana = IdTipoCampana;
                    existingCampana.Area = Area;
                    existingCampana.NroHectarea = NroHectarea;
                    existingCampana.NroArboles = NroArboles;
                    existingCampana.Coordenadas = (String.IsNullOrEmpty(Latitud) || String.IsNullOrEmpty(Longitud)) ? String.Empty : (Latitud + " , " + Longitud);
                    existingCampana.Latitud = Latitud;
                    existingCampana.Longitud = Longitud;
                    existingCampana.FechaInicio = FechaInicio;
                    existingCampana.IdUsuarioModificador = idusuario;
                    existingCampana.TxtUsuarioModificador = txtusuario;
                    existingCampana.FechaModificacion = fechaHoy;
                    _context.Pret02Campanas.Update(existingCampana);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingCampana.IdCampana;

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idcampanaTAEnArreglo = campanatipoarbolseleccionados.Select(p => p.idTipoArbol).ToList();

                    // Actualizar registros existentes
                    foreach (campanatipoarbol campanaTA in campanatipoarbolseleccionados)
                    {
                        var existinCampanaDtl = _context.Pret04CampanaTipoArbols
                           .FirstOrDefault(d => d.IdCampana == IdTemporal && d.IdTipoarbol == campanaTA.idTipoArbol);

                        if (existinCampanaDtl != null)
                        {
                            // Actualiza los valores
                            existinCampanaDtl.IdTipoarbol = campanaTA.idTipoArbol;
                            existinCampanaDtl.TxtTipoarbol = campanaTA.nombretipoarbol;
                            existinCampanaDtl.NroHectareas = campanaTA.numhectareas;
                            existinCampanaDtl.NroArboles = campanaTA.numarbol;
                            existinCampanaDtl.Area = (float)campanaTA.area;
                            existinCampanaDtl.Latitud = campanaTA.latitud;
                            existinCampanaDtl.Longitud = campanaTA.longitud;
                            existinCampanaDtl.Coordenadas = campanaTA.latitud.ToString() + " , " + campanaTA.longitud.ToString();
                            existinCampanaDtl.IdEstado = 1;
                            existinCampanaDtl.TxtEstado = "ACTIVO";
                            _context.Pret04CampanaTipoArbols.Update(existinCampanaDtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (campanatipoarbol campanaTA in campanatipoarbolseleccionados)
                    {
                        var existingCampanaDtl = _context.Pret04CampanaTipoArbols
                           .FirstOrDefault(d => d.IdCampana == IdTemporal && d.IdTipoarbol == campanaTA.idTipoArbol);

                        if (existingCampanaDtl == null)
                        {
                            // Crea un nuevo registro
                            var Pret04Campana_Tipo_Arbol = new Pret04CampanaTipoArbol
                            {
                                IdCampana = (long)(IdTemporal),
                                IdTipoarbol = campanaTA.idTipoArbol,
                                TxtTipoarbol = campanaTA.nombretipoarbol,
                                NroHectareas = campanaTA.numhectareas,
                                NroArboles = campanaTA.numarbol,
                                Area = (float)campanaTA.area,
                                Latitud = campanaTA.latitud,
                                Longitud = campanaTA.longitud,
                                Coordenadas = campanaTA.latitud.ToString() + " , " + campanaTA.longitud.ToString(),
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret04CampanaTipoArbols.Add(Pret04Campana_Tipo_Arbol);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Obtener los registros que no están en el arreglo
                    var registrosParaActualizar = _context.Pret04CampanaTipoArbols
                        .Where(d => d.IdCampana == IdTemporal && !idcampanaTAEnArreglo.Contains(d.IdTipoarbol))
                        .ToList();

                    foreach (var registro in registrosParaActualizar)
                    {
                        // Actualizar el campo IdEstado y TxtEstado para marcar como inactivo
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();


                }
                else
                {
                    int CantCamp = _context.Pret02Campanas.Where(c => c.IdPredio == IdPredioTemporal).Count() + 1;
                    var txt_numero = CantCamp.ToString().PadLeft(2, '0');
                    var UnidCat = _context.Pret01Predios.Where(p => p.IdPredio == IdPredioTemporal).Select(p => p.UnidadCatastral).FirstOrDefault();
                    string CodigoCampana = UnidCat + "-" + txt_numero;
                    //COMPROBANTE
                    var Pret02Campana = new Pret02Campana
                    {
                        IdPredio = (long)IdPredioTemporal,
                        IdTipoCampana = IdTipoCampana,
                        CodigoCampana = CodigoCampana,
                        NroArboles = NroArboles,
                        NroHectarea = NroHectarea,
                        Area = Area,
                        Longitud = Longitud,
                        Latitud = Latitud,
                        Coordenadas = (String.IsNullOrEmpty(Latitud) || String.IsNullOrEmpty(Longitud)) ? String.Empty : (Latitud + " , " + Longitud),
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = 1,
                        TxtEstado = "ACTIVO",
                        IdDistrito = (int)IdDistrito,
                        FechaInicio = FechaInicio,
                    };

                    _context.Pret02Campanas.Add(Pret02Campana);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret02Campana.IdCampana;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE
                        foreach (campanatipoarbol campanaTA in campanatipoarbolseleccionados)
                        {

                            var CampanaDtl = new Pret04CampanaTipoArbol
                            {
                                IdCampana = (long)(IdTemporal),
                                IdTipoarbol = campanaTA.idTipoArbol,
                                TxtTipoarbol = campanaTA.nombretipoarbol,
                                NroHectareas = campanaTA.numhectareas,
                                NroArboles = campanaTA.numarbol,
                                Area = (float)campanaTA.area,
                                Latitud = campanaTA.latitud,
                                Longitud = campanaTA.longitud,
                                Coordenadas = campanaTA.latitud.ToString() + " , " + campanaTA.longitud.ToString(),
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret04CampanaTipoArbols.Add(CampanaDtl);
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
        public async Task<IActionResult> CrearCampanayCerrar(int IdTipoCampana, int NroHectarea, int NroArboles,
    float Area, string? Latitud, string? Longitud, DateTime FechaInicio, string campanatipoarbolSeleccionado)
        {
            try
            {
                //CONVERTIR EL STRING A UNA LISTA YA QUE LLEGA DESDE JSon
                var campanatipoarbolseleccionados = JsonConvert.DeserializeObject<List<campanatipoarbol>>(campanatipoarbolSeleccionado);
                var IdDistrito = _context.Pret01Predios.Where(p => p.IdPredio == IdPredioTemporal && p.IdEstado == 1).Select(p => p.IdDistrito).FirstOrDefault();
                var existingCampana = _context.Pret02Campanas
            .FirstOrDefault(c => c.IdCampana == IdTemporal);

                if (existingCampana != null)
                {
                    //COMPROBANTE
                    existingCampana.IdPredio = (long)IdPredioTemporal;
                    existingCampana.IdTipoCampana = IdTipoCampana;
                    existingCampana.Area = Area;
                    existingCampana.NroHectarea = NroHectarea;
                    existingCampana.NroArboles = NroArboles;
                    existingCampana.Coordenadas = (String.IsNullOrEmpty(Latitud) || String.IsNullOrEmpty(Longitud)) ? String.Empty : (Latitud + " , " + Longitud);
                    existingCampana.Latitud = Latitud;
                    existingCampana.Longitud = Longitud;
                    existingCampana.FechaInicio = FechaInicio;
                    existingCampana.IdUsuarioModificador = idusuario;
                    existingCampana.TxtUsuarioModificador = txtusuario;
                    existingCampana.FechaModificacion = fechaHoy;
                    _context.Pret02Campanas.Update(existingCampana);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingCampana.IdCampana;

                    //COMPROBANTE DETALLE
                    //Los productos del arreglo/ comprobante 
                    var idcampanaTAEnArreglo = campanatipoarbolseleccionados.Select(p => p.idTipoArbol).ToList();

                    // Actualizar registros existentes
                    foreach (campanatipoarbol campanaTA in campanatipoarbolseleccionados)
                    {
                        var existinCampanaDtl = _context.Pret04CampanaTipoArbols
                           .FirstOrDefault(d => d.IdCampana == IdTemporal && d.IdTipoarbol == campanaTA.idTipoArbol);

                        if (existinCampanaDtl != null)
                        {
                            // Actualiza los valores
                            existinCampanaDtl.IdTipoarbol = campanaTA.idTipoArbol;
                            existinCampanaDtl.TxtTipoarbol = campanaTA.nombretipoarbol;
                            existinCampanaDtl.NroHectareas = campanaTA.numhectareas;
                            existinCampanaDtl.NroArboles = campanaTA.numarbol;
                            existinCampanaDtl.Area = (float)campanaTA.area;
                            existinCampanaDtl.Latitud = campanaTA.latitud;
                            existinCampanaDtl.Longitud = campanaTA.longitud;
                            existinCampanaDtl.Coordenadas = campanaTA.latitud.ToString() + " , " + campanaTA.longitud.ToString();
                            existinCampanaDtl.IdEstado = 1;
                            existinCampanaDtl.TxtEstado = "ACTIVO";
                            _context.Pret04CampanaTipoArbols.Update(existinCampanaDtl);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Crear nuevos registros
                    foreach (campanatipoarbol campanaTA in campanatipoarbolseleccionados)
                    {
                        var existingCampanaDtl = _context.Pret04CampanaTipoArbols
                           .FirstOrDefault(d => d.IdCampana == IdTemporal && d.IdTipoarbol == campanaTA.idTipoArbol);

                        if (existingCampanaDtl == null)
                        {
                            // Crea un nuevo registro
                            var Pret04Campana_Tipo_Arbol = new Pret04CampanaTipoArbol
                            {
                                IdCampana = (long)(IdTemporal),
                                IdTipoarbol = campanaTA.idTipoArbol,
                                TxtTipoarbol = campanaTA.nombretipoarbol,
                                NroHectareas = campanaTA.numhectareas,
                                NroArboles = campanaTA.numarbol,
                                Area = (float)campanaTA.area,
                                Latitud = campanaTA.latitud,
                                Longitud = campanaTA.longitud,
                                Coordenadas = campanaTA.latitud.ToString() + " , " + campanaTA.longitud.ToString(),
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret04CampanaTipoArbols.Add(Pret04Campana_Tipo_Arbol);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Obtener los registros que no están en el arreglo
                    var registrosParaActualizar = _context.Pret04CampanaTipoArbols
                        .Where(d => d.IdCampana == IdTemporal && !idcampanaTAEnArreglo.Contains(d.IdTipoarbol))
                        .ToList();

                    foreach (var registro in registrosParaActualizar)
                    {
                        // Actualizar el campo IdEstado y TxtEstado para marcar como inactivo
                        registro.IdEstado = 0; // Ejemplo: Marcar como inactivo (ajusta según tu lógica)
                        registro.TxtEstado = "INACTIVO"; // Puedes ajustar el texto según tus necesidades
                    }

                    await _context.SaveChangesAsync();


                }
                else
                {
                    int CantCamp = _context.Pret02Campanas.Where(c => c.IdPredio == IdPredioTemporal).Count() + 1;
                    var txt_numero = CantCamp.ToString().PadLeft(2, '0');
                    var UnidCat = _context.Pret01Predios.Where(p => p.IdPredio == IdPredioTemporal).Select(p => p.UnidadCatastral).FirstOrDefault();
                    string CodigoCampana = UnidCat + "-" + txt_numero;
                    //COMPROBANTE
                    var Pret02Campana = new Pret02Campana
                    {
                        IdPredio = (long)IdPredioTemporal,
                        IdTipoCampana = IdTipoCampana,
                        CodigoCampana = CodigoCampana,
                        NroArboles = NroArboles,
                        NroHectarea = NroHectarea,
                        Area = Area,
                        Longitud = Longitud,
                        Latitud = Latitud,
                        Coordenadas = (String.IsNullOrEmpty(Latitud) || String.IsNullOrEmpty(Longitud)) ? String.Empty : (Latitud + " , " + Longitud),
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,
                        IdEstado = 1,
                        TxtEstado = "ACTIVO",
                        IdDistrito = (int)IdDistrito,
                        FechaInicio = FechaInicio,
                    };

                    _context.Pret02Campanas.Add(Pret02Campana);
                    await _context.SaveChangesAsync();
                    IdTemporal = Pret02Campana.IdCampana;

                    if (IdTemporal != null) // Verificar si el comprobante principal se creó correctamente
                    {
                        //COMPROBANTE DETALLE
                        foreach (campanatipoarbol campanaTA in campanatipoarbolseleccionados)
                        {

                            var CampanaDtl = new Pret04CampanaTipoArbol
                            {
                                IdCampana = (long)(IdTemporal),
                                IdTipoarbol = campanaTA.idTipoArbol,
                                TxtTipoarbol = campanaTA.nombretipoarbol,
                                NroHectareas = campanaTA.numhectareas,
                                NroArboles = campanaTA.numarbol,
                                Area = (float)campanaTA.area,
                                Latitud = campanaTA.latitud,
                                Longitud = campanaTA.longitud,
                                Coordenadas = campanaTA.latitud.ToString() + " , " + campanaTA.longitud.ToString(),
                                IdEstado = 1,
                                TxtEstado = "ACTIVO"
                            };
                            _context.Pret04CampanaTipoArbols.Add(CampanaDtl);
                            await _context.SaveChangesAsync();


                        }
                    }



                }

                IdTemporal = null;
                return Json(new { redirectUrl = Url.Action("ListadoCam", "Pret02Campana", new { IdPredioTemporal }) });

            }

            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante el proceso de guardado
                return Json(new { errores = new List<string> { ex.Message } });
            }
        }
        public IActionResult Tipoarbol(int idtipoarbol)
        {
            try
            {
                var TipoArbol = _context.Pret06TipoArbols.FirstOrDefault(p => p.IdTipoarbol == idtipoarbol);
                if (TipoArbol != null)
                {
                    var campanata = new
                    {
                        IdTipoArbol = TipoArbol.IdTipoarbol,
                        Nombretipoarbol = TipoArbol.Txtdesc,
                    };

                    // Retorna una respuesta exitosa
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
        [HttpPost]
        public async Task<IActionResult> CrearTipoArbol(string txtdesc)
        {
            try
            {
                var pret06TipoArbol = new Pret06TipoArbol
                {
                    Txtdesc = txtdesc,

                    IdEstado = 1,
                    TxtEstado = "ACTIVO"
                };

                _context.Add(pret06TipoArbol);
                await _context.SaveChangesAsync();
                var ListadoTA = _context.Pret06TipoArbols.Select(e => new
                {
                    TxtDesc = e.Txtdesc,
                    IdTipoarbol = e.IdTipoarbol,

                }).ToList();
                return Json(ListadoTA);


            }
            catch (Exception ex)
            {
                return BadRequest("Error al crear Campaña: " + ex.Message);
            }
        }



        public IActionResult RecargarTipoDeArbol(List<long> tipoarbolSeleccionadosIds)
        {

            // Crear una consulta de subconjunto de productos excluyendo aquellos en la lista productosSeleccionadosIds
            var productosExcluidos = _context.Pret06TipoArbols
                .Where(p => tipoarbolSeleccionadosIds.Contains(p.IdTipoarbol)
               );

            // Filtrar productos que no están en la lista de seleccionados
            var ListadoTA = _context.Pret06TipoArbols
                .Except(productosExcluidos).Select(e => new
                {
                    TxtDesc = e.Txtdesc,
                    IdTipoarbol = e.IdTipoarbol,

                })
                .ToList();

            // Retorna una respuesta exitosa
            return Json(ListadoTA);
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
        [HttpGet]
        public IActionResult CerrarCampana()
        {
            var redirectUrl = Url.Action("ListadoCam", "Pret02Campana");

            // Asumiendo que IdPredioTemporal es una propiedad o variable definida en tu controlador.
            // Puedes agregar más datos según sea necesario.
            var response = new { redirectUrl, IdPredioTemporal };

            return Json(response);
        }


        [HttpGet]
        public IActionResult CerrarListCampana()
        {
            var redirectUrl = Url.Action("ListadoPre", "Pret01Predio");

            // Asumiendo que IdPredioTemporal es una propiedad o variable definida en tu controlador.
            // Puedes agregar más datos según sea necesario.
            IdPredioTemporal = null;
            var response = new { redirectUrl };

            return Json(response);
        }


    }
}

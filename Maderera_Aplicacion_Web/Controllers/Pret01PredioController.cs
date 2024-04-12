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

namespace Aplicacion_Maderera.Controllers
{
    [Authorize]
    public class Pret01PredioController : Controller
    {
        private readonly EagleContext _context;
        private readonly IPert01UsuarioController _pert01UsuarioController;
        private readonly long idusuario;
        private readonly string txtusuario;
        private readonly DateTime fechaHoy;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private static long? IdTemporal { get; set; }
        public Pret01PredioController(EagleContext context, IPert01UsuarioController pert01UsuarioController, IWebHostEnvironment webHostEnvironment)
        {
            _pert01UsuarioController = pert01UsuarioController;
            _context = context;

            // Asigna valores a las propiedades de solo lectura
            idusuario = _context.Pert01Usuarios.FirstOrDefault(u => u.TxtUsuario == _pert01UsuarioController.GetUsuario())?.IdUsuario ?? 0;
            txtusuario = _pert01UsuarioController.GetUsuario();
            fechaHoy = DateTime.Today;
            _webHostEnvironment = webHostEnvironment;
        }



        // GET: Pret01Predio
        public IActionResult ListadoPre()
        {
            var ListadoPredios = _context.Pret01Predios.Include(p => p.IdDistritoNavigation).Include(p => p.IdInversionistaNavigation)
                .Where(p => p.IdEstado == 1).ToList();
            return View(ListadoPredios);
        }

        // GET: Pret01Predio/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Pret01Predios == null)
            {
                return NotFound();
            }

            var pret01Predio = await _context.Pret01Predios
                .Include(p => p.IdDistritoNavigation)
                .Include(p => p.IdInversionistaNavigation)
                .FirstOrDefaultAsync(m => m.IdPredio == id);
            if (pret01Predio == null)
            {
                return NotFound();
            }

            return View(pret01Predio);
        }

        // GET: Pret01Predio/Create
        public IActionResult Predio(long? id)
        {

            ViewData["IdDep"] = new SelectList(_context.Sntt31Departamentos, "IdDpto", "TxtDesc");
            ViewData["IdTipoPredio"] = new SelectList(_context.Pret09TipoPredios, "IdTipoPredio", "TxtDesc");
            var updatedInversionista = _context.Pert09Inversionista
                    .Include(e => e.IdTipoDocumentoNavigation)
                            .Where(e => e.IdEstado == 1)
                    .Select(e => new
                    {
                        IdInversionista = e.IdInversionista,
                        NombreCompleto = e.CodInversionista != null ? e.CodInversionista : (e.TxtRznScl != null ? e.TxtRznScl : $"{e.TxtPrimNom} {e.TxtApePat}"),
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : e.Celular2,
                        ruc = e.NroDoc
                    })
                .ToList();
            ViewBag.Inversionista = updatedInversionista;


            var prediovista = _context.Pret01Predios
            .Where(c => c.IdPredio == id && c.IdEstado == 1)
            .FirstOrDefault();

            var Predio = _context.Pret01Predios
            .Include(t => t.IdDistritoNavigation)
            .ThenInclude(t => t.IdProvNavigation)
            .ThenInclude(t => t.IdDptoNavigation)
            .Include(t => t.IdTipoPredioNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdPredio == id && c.IdEstado == 1)
            .Select(predio => new
            {
                Predio = predio,
                IdDistrito = predio.IdDistritoNavigation.IdDist,
                tipopredio = predio.IdTipoPredioNavigation.TxtDesc,
                provpre = predio.IdDistritoNavigation.IdProvNavigation.IdProv,
                dptopre = predio.IdDistritoNavigation.IdProvNavigation.IdDptoNavigation.IdDpto,
                nombreInv = String.IsNullOrEmpty(predio.IdInversionistaNavigation.TxtRznScl) ? $"{predio.IdInversionistaNavigation.TxtPrimNom} {predio.IdInversionistaNavigation.TxtApePat}" : predio.IdInversionistaNavigation.TxtRznScl,
            })
            .FirstOrDefault();

            if (Predio != null)
            {
                var archivos = _context.Pret17Archivos
                .Where(a => a.IdEstado == 1 && a.IdPredio == id)
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



                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                // Serializar los datos con las configuraciones establecidas
                var predioserialized = Newtonsoft.Json.JsonConvert.SerializeObject(Predio, settings);
                var archivosserialized = Newtonsoft.Json.JsonConvert.SerializeObject(archivosParaVista, settings);

                IdTemporal = prediovista?.IdPredio;
                ViewBag.Predio = predioserialized;
                ViewBag.archivos64 = archivosserialized;
                return View("Predio", prediovista);
            }
            else
            {
                IdTemporal = null;
                return View();
            }
        }


        public IActionResult EditP(long id)
        {
            ViewData["IdDep"] = new SelectList(_context.Sntt31Departamentos, "IdDpto", "TxtDesc");
            ViewData["IdTipoPredio"] = new SelectList(_context.Pret09TipoPredios, "IdTipoPredio", "TxtDesc");
            var updatedInversionista = _context.Pert09Inversionista
                    .Include(e => e.IdTipoDocumentoNavigation)
                            .Where(e => !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1) && e.IdEstado == 1)
                    .Select(e => new
                    {
                        IdInversionista = e.IdInversionista,
                        NombreCompleto = e.TxtRznScl != null ? e.TxtRznScl : $"{e.TxtPrimNom} {e.TxtApePat}",
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : e.Celular2,
                        ruc = e.NroDoc
                    })
                .ToList();
            ViewBag.Inversionista = updatedInversionista;
            var prediovista = _context.Pret01Predios
            .Where(c => c.IdPredio == id && c.IdEstado == 1)
            .FirstOrDefault();

            var Predio = _context.Pret01Predios
            .Include(t => t.IdDistritoNavigation)
            .ThenInclude(t => t.IdProvNavigation)
            .ThenInclude(t => t.IdDptoNavigation)
            .Include(t => t.IdTipoPredioNavigation)
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdUsuarioModificadorNavigation)
            .Where(c => c.IdPredio == id && c.IdEstado == 1)
            .Select(predio => new
            {
                Predio = predio,
                IdDistrito = predio.IdDistritoNavigation.IdDist,
                tipopredio = predio.IdTipoPredioNavigation.TxtDesc,
                provpre = predio.IdDistritoNavigation.IdProvNavigation.IdProv,
                dptopre = predio.IdDistritoNavigation.IdProvNavigation.IdDptoNavigation.IdDpto,
                nombreInv = String.IsNullOrEmpty(predio.IdInversionistaNavigation.TxtRznScl) ? $"{predio.IdInversionistaNavigation.TxtPrimNom} {predio.IdInversionistaNavigation.TxtApePat}" : predio.IdInversionistaNavigation.TxtRznScl,
            })
            .FirstOrDefault();

            if (Predio != null)
            {
                var archivos = _context.Pret17Archivos
                .Where(a => a.IdEstado == 1 && a.IdPredio == id)
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



                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                // Serializar los datos con las configuraciones establecidas
                var predioserialized = Newtonsoft.Json.JsonConvert.SerializeObject(Predio, settings);
                var archivosserialized = Newtonsoft.Json.JsonConvert.SerializeObject(archivosParaVista, settings);

                IdTemporal = prediovista?.IdPredio;
                ViewBag.Predio = predioserialized;
                ViewBag.ArchivosParaVista = archivosParaVista;
                return View("Predio", prediovista);
            }
            else
            {
                // Manejar el caso donde el comprobante no existe
                return NotFound(); // O redirigir a otra página
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPredio(string nroSitio, decimal area, string unidadCatastral, decimal nroHectareas, string? latitud,
            string? longitud, int idDistrito, long idInversionista, string? nrocomp, string? partidaregistral, long? idtipoPredio,
            DateTime fechaadquisicion, DateTime fechacompra, List<IFormFile> archivos)
        {
            try
            {
                var existingPredio = _context.Pret01Predios.AsNoTracking()
           .FirstOrDefault(c => c.IdPredio == IdTemporal);

                if (existingPredio != null)
                {
                    existingPredio.IdTipoPredio = (long)idtipoPredio;
                    existingPredio.UnidadCatastral = unidadCatastral;
                    existingPredio.NroComprobante = nrocomp;
                    existingPredio.NroHectareas = nroHectareas;
                    existingPredio.Area = area;
                    existingPredio.Latitud = latitud;
                    existingPredio.Longitud = longitud;
                    existingPredio.Coordenadas = (String.IsNullOrEmpty(latitud) || String.IsNullOrEmpty(longitud)) ? String.Empty : (latitud + " , " + longitud);
                    existingPredio.IdDistrito = idDistrito;
                    existingPredio.IdInversionista = idInversionista;
                    existingPredio.FechaAdquisicion = fechaadquisicion;
                    existingPredio.FechaCompra = fechacompra;
                    existingPredio.IdUsuarioModificador = idusuario;
                    existingPredio.TxtUsuarioModificador = txtusuario;
                    existingPredio.FechaModificacion = fechaHoy;
                    _context.Pret01Predios.Update(existingPredio);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingPredio.IdPredio;
                    if (IdTemporal != null)
                    {
                        var existingCamp = _context.Pret02Campanas.Where(c => c.IdPredio == IdTemporal && c.IdEstado == 1).ToList();
                        foreach (Pret02Campana campana in existingCamp)
                        {
                            campana.IdDistrito = idDistrito;
                            _context.Pret02Campanas.Update(campana);
                            await _context.SaveChangesAsync();


                        }
                    }
                    var IdEnviosEnArreglo = archivos.Select(p => p.FileName).ToList();
                    //Agregar
                    foreach (var archivo in archivos)
                    {
                        var existingArchivo = _context.Pret17Archivos
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdPredio == IdTemporal);
                        if (existingArchivo == null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "predio", IdTemporal.ToString());


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
                                    IdPredio = existingPredio.IdPredio,
                                    IdTipoDir = 1,
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
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdPredio == IdTemporal);
                        if (existingArchivo != null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "predio", IdTemporal.ToString());


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
                                existingArchivo.IdPredio = existingPredio.IdPredio;
                                existingArchivo.IdTipoDir = 1;

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Update(existingArchivo);
                                await _context.SaveChangesAsync();

                            }
                        }
                    }

                    var registrosParaActualizar = _context.Pret17Archivos
                       .Where(d => d.IdPredio == IdTemporal && !IdEnviosEnArreglo.Contains(d.NombreArchivo))
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
                    bool nroSitioExistente = ValidarYLimpiarString(nroSitio);

                    if (nroSitioExistente)
                    {

                        // Devolver el mensaje JSON
                        return Json(new { mensaje = "El número de Sitio ya se encuentra registrado" });
                    }

                    var pret01Predio = new Pret01Predio
                    {
                        NroSitio = nroSitio,
                        Area = area,
                        UnidadCatastral = unidadCatastral,
                        NroHectareas = nroHectareas,
                        Latitud = latitud,
                        Longitud = longitud,
                        Coordenadas = (String.IsNullOrEmpty(latitud) || String.IsNullOrEmpty(longitud)) ? String.Empty : (latitud + " , " + longitud),
                        IdDistrito = idDistrito,
                        IdInversionista = idInversionista,
                        FechaAdquisicion = fechaadquisicion,
                        IdEstado = 1,
                        TxtEstado = "ACTIVO",
                        FechaCreacion=fechaHoy,
                        IdTipoPredio = (long)idtipoPredio,
                        NroComprobante = nrocomp,
                        FechaCompra = fechacompra,
                        PartidaRegistral = partidaregistral,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,


                    };


                    // Resto del código para guardar en la base de datos
                    _context.Add(pret01Predio);
                    await _context.SaveChangesAsync();
                    IdTemporal = pret01Predio.IdPredio;
                    foreach (var archivo in archivos)
                    {
                        if (archivo != null && archivo.Length > 0)
                        {
                            // Obtener la ruta de almacenamiento dentro del proyecto
                            var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "predio", IdTemporal.ToString());


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
                                IdPredio = pret01Predio.IdPredio,
                                IdTipoDir = 1,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO",
                            };

                            // Agregar el nuevo archivo a la base de datos
                            _context.Pret17Archivos.Add(pret17Archivo);
                            await _context.SaveChangesAsync();

                        }
                    }


                }

                return Json(new { mensaje = "Guardado Correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el predio: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CrearPredioyCerrar(string nroSitio, decimal area, string unidadCatastral, decimal nroHectareas, string? latitud,
            string? longitud, int idDistrito, long idInversionista, string? nrocomp, string? partidaregistral, long? idtipoPredio,
            DateTime fechaadquisicion, DateTime fechacompra, List<IFormFile> archivos)
        {
            try
            {
                var existingPredio = _context.Pret01Predios.AsNoTracking()
           .FirstOrDefault(c => c.IdPredio == IdTemporal);

                if (existingPredio != null)
                {
                    existingPredio.IdTipoPredio = (long)idtipoPredio;
                    existingPredio.UnidadCatastral = unidadCatastral;
                    existingPredio.NroComprobante = nrocomp;
                    existingPredio.NroHectareas = nroHectareas;
                    existingPredio.Area = area;
                    existingPredio.Latitud = latitud;
                    existingPredio.Longitud = longitud;
                    existingPredio.Coordenadas = (String.IsNullOrEmpty(latitud) || String.IsNullOrEmpty(longitud)) ? String.Empty : (latitud + " , " + longitud);
                    existingPredio.IdDistrito = idDistrito;
                    existingPredio.IdInversionista = idInversionista;
                    existingPredio.FechaAdquisicion = fechaadquisicion;
                    existingPredio.FechaCompra = fechacompra;
                    existingPredio.IdUsuarioModificador = idusuario;
                    existingPredio.TxtUsuarioModificador = txtusuario;
                    existingPredio.FechaModificacion = fechaHoy;
                    _context.Pret01Predios.Update(existingPredio);
                    await _context.SaveChangesAsync();
                    IdTemporal = existingPredio.IdPredio;
                    if (IdTemporal != null)
                    {
                        var existingCamp = _context.Pret02Campanas.Where(c => c.IdPredio == IdTemporal && c.IdEstado == 1).ToList();
                        foreach (Pret02Campana campana in existingCamp)
                        {
                            campana.IdDistrito = idDistrito;
                            _context.Pret02Campanas.Update(campana);
                            await _context.SaveChangesAsync();


                        }
                    }
                    var idcampanaTAEnArreglo = archivos.Select(p => p.FileName).ToList();
                    //Agregar
                    foreach (var archivo in archivos)
                    {
                        var existingArchivo = _context.Pret17Archivos
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdPredio == IdTemporal);
                        if (existingArchivo == null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "predio", IdTemporal.ToString());


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
                                    IdPredio = existingPredio.IdPredio,
                                    IdTipoDir = 1,
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
                            .FirstOrDefault(a => a.NombreArchivo == archivo.FileName && a.IdPredio == IdTemporal);
                        if (existingArchivo != null)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {
                                // Obtener la ruta de almacenamiento dentro del proyecto
                                var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "predio", IdTemporal.ToString());


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
                                existingArchivo.IdPredio = existingPredio.IdPredio;
                                existingArchivo.IdTipoDir = 1;

                                // Agregar el nuevo archivo a la base de datos
                                _context.Pret17Archivos.Update(existingArchivo);
                                await _context.SaveChangesAsync();

                            }
                        }
                    }

                    var registrosParaActualizar = _context.Pret17Archivos
                       .Where(d => d.IdPredio == IdTemporal && !idcampanaTAEnArreglo.Contains(d.NombreArchivo))
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
                    bool nroSitioExistente = ValidarYLimpiarString(nroSitio);

                    if (nroSitioExistente)
                    {

                        // Devolver el mensaje JSON
                        return Json(new { mensaje = "El número de Sitio ya se encuentra registrado" });
                    }

                    var pret01Predio = new Pret01Predio
                    {
                        NroSitio = nroSitio,
                        Area = area,
                        UnidadCatastral = unidadCatastral,
                        NroHectareas = nroHectareas,
                        Latitud = latitud,
                        Longitud = longitud,
                        Coordenadas = (String.IsNullOrEmpty(latitud) || String.IsNullOrEmpty(longitud)) ? String.Empty : (latitud + " , " + longitud),
                        IdDistrito = idDistrito,
                        IdInversionista = idInversionista,
                        FechaAdquisicion = fechaadquisicion,
                        IdEstado = 1,
                        TxtEstado = "ACTIVO",
                        FechaCreacion=fechaHoy,
                        IdTipoPredio = (long)idtipoPredio,
                        NroComprobante = nrocomp,
                        FechaCompra = fechacompra,
                        PartidaRegistral = partidaregistral,
                        IdUsuario = idusuario,
                        TxtUsuario = txtusuario,


                    };


                    // Resto del código para guardar en la base de datos
                    _context.Add(pret01Predio);
                    await _context.SaveChangesAsync();
                    IdTemporal = pret01Predio.IdPredio;
                    foreach (var archivo in archivos)
                    {
                        if (archivo != null && archivo.Length > 0)
                        {
                            // Obtener la ruta de almacenamiento dentro del proyecto
                            var rutaAlmacenamiento = Path.Combine(_webHostEnvironment.WebRootPath, "Archivos", "predio", IdTemporal.ToString());


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
                                IdPredio = pret01Predio.IdPredio,
                                IdTipoDir = 1,
                                IdEstado = 1,
                                TxtEstado = "ACTIVO",
                            };

                            // Agregar el nuevo archivo a la base de datos
                            _context.Pret17Archivos.Add(pret17Archivo);
                            await _context.SaveChangesAsync();

                        }
                    }


                }
                IdTemporal = null;
                return Json(new { redirectUrl = Url.Action("ListadoPre", "Pret01Predio") });
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el predio: " + ex.Message);
            }
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
        public IActionResult CrearInversionista(string? txt_prim_nomb, string? txt_seg_nomb, string? txt_ape_pat, string? txt_ape_mat, string? razonsocial,
    int tipo_doc, string nro_doc, string numtelefono, string direccioninv)
        {
            try
            {
                // Crea una instancia de la entidad Cliente
                var Inversionista = new Pert09Inversionistum
                {
                    TxtPrimNom = txt_prim_nomb,
                    TxtSegunNom = txt_seg_nomb,
                    TxtApePat = txt_ape_pat,
                    TxtApeMat = txt_ape_mat,
                    TxtRznScl = razonsocial,
                    IdEstado = 1,
                    IdTipoDocumento = tipo_doc,
                    TxtEstado = "ACTIVO",
                    NroDoc = nro_doc,
                    Celular1 = numtelefono,
                    TxtDireccion1 = direccioninv,
                    FechaCreacion=fechaHoy

                };

                // Agrega el cliente a tu contexto y guarda los cambios
                _context.Pert09Inversionista.Add(Inversionista);
                _context.SaveChanges();

                var updatedInversionista = _context.Pert09Inversionista
                    .Include(e => e.IdTipoDocumentoNavigation)
                            .Where(e => !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        IdInversionista = e.IdInversionista,
                        NombreCompleto = e.TxtRznScl != null ? e.TxtRznScl : $"{e.TxtPrimNom} {e.TxtApePat}",
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : e.Celular2,
                        ruc = e.NroDoc
                    })
                .ToList();
                ViewBag.Inversionista = updatedInversionista;

                return Json(updatedInversionista);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el inversionista: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }

                return BadRequest("Error al crear el inversionista");
            }

        }
        [HttpGet]
        public IActionResult RecargarInversionista()
        {
            try
            {
                var updatedInversionista = _context.Pert09Inversionista
                    .Include(e => e.IdTipoDocumentoNavigation)
                            .Where(e => !string.IsNullOrEmpty(e.Celular1) && !string.IsNullOrEmpty(e.TxtDireccion1))
                    .Select(e => new
                    {
                        IdInversionista = e.IdInversionista,
                        NombreCompleto = e.TxtRznScl != null ? e.TxtRznScl : $"{e.TxtPrimNom} {e.TxtApePat}",
                        Telefono = !string.IsNullOrEmpty(e.Celular1) ? e.Celular1 : e.Celular2,
                        ruc = e.NroDoc
                    })
                .ToList();
                ViewBag.Cliente = updatedInversionista;

                return Json(updatedInversionista);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el inversionista: " + ex.Message);
            }
        }
        private bool ValidarYLimpiarString(string VarString)
        {
            // Verifica si el nroSitio es nulo y lo asigna como cadena vacía si es así
            VarString = VarString ?? string.Empty;

            // Elimina los espacios en blanco al principio y al final del string
            VarString = VarString.Trim();

            // Verifica si el nroSitio ya existe en la base de datos
            bool nroSitioExistente = _context.Pret01Predios.Any(p => p.NroSitio == VarString);

            // Puedes hacer algo con nroSitioExistente, como logear o manejar el caso según tus necesidades

            // Devuelve true si nroSitio ya existe, de lo contrario, devuelve false
            return nroSitioExistente;
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPredio(long id)
        {
            try
            {
                var existingPredio = _context.Pret01Predios.Find(id);

                if (existingPredio != null)
                {
                    //existingPredio.IdEstado = 2;
                    //existingPredio.TxtEstado = "INACTIVO";
                    //ELIMINACIÓN DE ARCHIVOS

                    var existingArchivo = _context.Pret17Archivos.Where(c => c.IdPredio == id).ToList();
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
                    var existingVenta = _context.Tnst04CompEmitidos.Where(p => /*p.IdEstado == 1 || p.IdEstado == 3 &&*/ p.IdPredio == id).ToList();
                    //ELIMINACIÓN DE VENTA
                    if (existingVenta != null)
                    {
                        foreach (Tnst04CompEmitido existingVentaUn in existingVenta)
                        {
                            //existingVentaUn.IdEstado = 2;
                            //existingVentaUn.TxtEstado = "CANCELADO";
                            long idEliminarVenta = existingVentaUn.IdCompEmitido;
                            var existingEmpleado = _context.Pret23VentaEmpleados.Where(p =>/* p.IdEstado == 1 || p.IdEstado == 2 &&*/ p.IdCompEmitido == idEliminarVenta).ToList();
                            if (existingEmpleado != null)
                            {
                                foreach (Pret23VentaEmpleado empleado in existingEmpleado)
                                {
                                    //empleado.IdEstado = 2;
                                    //empleado.TxtEstado = "INACTIVO";
                                    _context.Pret23VentaEmpleados.Remove(empleado);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            var existingDtll = _context.Tnst05CompEmitidoDtls.Where(p => /*p.IdEstado == 1 &&*/ p.IdCompEmitido == idEliminarVenta).ToList();

                            if (existingDtll != null)
                            {
                                foreach (Tnst05CompEmitidoDtl Detalle in existingDtll)
                                {
                                    //Detalle.IdEstado = 2;
                                    //Detalle.TxtEstado = "INACTIVO";
                                    _context.Tnst05CompEmitidoDtls.Remove(Detalle);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            _context.Tnst04CompEmitidos.Remove(existingVentaUn);
                            await _context.SaveChangesAsync();

                        }
                    }
                    //ELIMINACIÓN DE MERMA
                    var existingMerma = _context.Pret16Mermas.Where(p => p.IdPredio == id).ToList();

                    if (existingMerma != null)
                    {
                        foreach (Pret16Merma merma in existingMerma)
                        {
                            long idmerma = merma.IdMerma;
                            var existingEmpleado = _context.Pret24MermaEmpleados.Where(p => p.IdMerma == idmerma).ToList();


                            foreach (Pret24MermaEmpleado empleado in existingEmpleado)
                            {
                                //empleado.IdEstado = 0;
                                //empleado.TxtEstado = "INACTIVO";
                                _context.Pret24MermaEmpleados.Remove(empleado);
                                await _context.SaveChangesAsync();
                            }
                            await _context.SaveChangesAsync();
                            var existingDtll = _context.Pret17MermaDtls.Where(p => p.IdMerma == idmerma).ToList();

                            if (existingDtll != null)
                            {
                                foreach (Pret17MermaDtl Detalle in existingDtll)
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
                        await _context.SaveChangesAsync();
                    }

                    //ELIMINACIÓN DE PRODUCCIÓN
                    var existingProduccion = _context.Pret14Produccions.Where(p => p.IdPredio == id).ToList();

                    if (existingProduccion != null)
                    {
                        foreach (Pret14Produccion produccion in existingProduccion)
                        {
                            long idprod = produccion.IdProduccion;
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
                    await _context.SaveChangesAsync();

                    //ELIMINACIÓN DE CAMPAÑA
                    var existingCampana = _context.Pret02Campanas.Where(c => c.IdEstado == 1 && c.IdPredio == id).ToList();
                    if (existingCampana != null)
                    {
                        foreach (Pret02Campana campana in existingCampana)
                        {
                            //campana.IdEstado = 2;
                            //campana.TxtEstado = "INACTIVO";

                            long idcampanaeliminar = campana.IdCampana;
                            var existingCampanaTA = _context.Pret04CampanaTipoArbols.Where(tc => tc.IdEstado == 1 && tc.IdCampana == idcampanaeliminar).ToList();
                            if (existingCampanaTA != null)
                            {
                                foreach (Pret04CampanaTipoArbol campTA in existingCampanaTA)
                                {
                                    //campTA.IdEstado = 2;
                                    //campTA.TxtEstado = "INACTIVO";
                                    _context.Pret04CampanaTipoArbols.Remove(campTA);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            var existingPagoP = _context.Pert11PagoPersonals.Where(tc => tc.IdEstado == 1 && tc.IdCampana == idcampanaeliminar).ToList();
                            if (existingPagoP != null)
                            {
                                foreach (Pert11PagoPersonal PagoPersonal in existingPagoP)
                                {
                                    //campTA.IdEstado = 2;
                                    //campTA.TxtEstado = "INACTIVO";
                                    _context.Pert11PagoPersonals.Remove(PagoPersonal);
                                    await _context.SaveChangesAsync();
                                }
                            }

                            var existingExtraccion = _context.Pret07Extraccions.Where(c => c.IdEstado == 1 && c.IdCampana == idcampanaeliminar).ToList();
                            if (existingExtraccion != null)
                            {
                                foreach (Pret07Extraccion ExtReg in existingExtraccion)
                                {
                                    //ExtReg.IdEstado = 2;
                                    //ExtReg.TxtEstado = "INACTIVO";

                                    var idextraccioneliminar = ExtReg.IdExtraccion;
                                    var ExistingExtDtll = _context.Pret08ExtraccionDtls.Where(c => /*c.IdEstado == 1 &&*/ c.IdExtraccion == idextraccioneliminar).ToList();
                                    if (ExistingExtDtll != null)
                                    {
                                        foreach (Pret08ExtraccionDtl detalleext in ExistingExtDtll)
                                        {
                                            //detalleext.IdEstado = 2;
                                            //detalleext.TxtEstado = "INACTIVO";
                                            _context.Pret08ExtraccionDtls.Remove(detalleext);
                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                    var ExistingExtEmp = _context.Pret19ExtraccionEmpleados.Where(c => /*c.IdEstado == 1 &&*/ c.IdExtraccion == idextraccioneliminar).ToList();
                                    if (ExistingExtEmp != null)
                                    {
                                        foreach (Pret19ExtraccionEmpleado empleadoext in ExistingExtEmp)
                                        {
                                            //empleadoext.IdEstado = 2;
                                            //empleadoext.TxtEstado = "INACTIVO";
                                            _context.Pret19ExtraccionEmpleados.Remove(empleadoext);
                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                    var ExistingEnvio = _context.Pret10Envios.Where(c => /*c.IdEstado == 1 &&*/ c.IdExtraccion == idextraccioneliminar).ToList();
                                    if (ExistingEnvio != null)
                                    {
                                        foreach (Pret10Envio envioext in ExistingEnvio)
                                        {
                                            //empleadoext.IdEstado = 2;
                                            //empleadoext.TxtEstado = "INACTIVO";
                                            var idenvio = envioext.IdEnvio;
                                            var ExistingRecepcion = _context.Pret11Recepcions.Where(c => /*c.IdEstado == 1 &&*/ c.IdEnvio == idenvio).ToList();
                                            if (ExistingEnvio != null)
                                            {
                                                foreach (Pret11Recepcion recepcion in ExistingRecepcion)
                                                {
                                                    //empleadoext.IdEstado = 2;
                                                    //empleadoext.TxtEstado = "INACTIVO";
                                                    var idrecepcion = recepcion.IdRecepcion;
                                                    var existingArchivoRec = _context.Pret17Archivos.Where(c => c.IdRecepcion == idrecepcion).ToList();
                                                    if (existingArchivoRec != null)
                                                    {
                                                        foreach (Pret17Archivo archivoReg in existingArchivoRec)
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
                                                    var ExistingRecDtll = _context.Pret12RecepcionDtls.Where(c => /*c.IdEstado == 1 &&*/ c.IdRecepcion == idrecepcion).ToList();
                                                    if (ExistingRecDtll != null)
                                                    {
                                                        foreach (Pret12RecepcionDtl detallerec in ExistingRecDtll)
                                                        {
                                                            //detalleext.IdEstado = 2;
                                                            //detalleext.TxtEstado = "INACTIVO";
                                                            _context.Pret12RecepcionDtls.Remove(detallerec);
                                                            await _context.SaveChangesAsync();
                                                        }
                                                    }
                                                    var ExistingRecEmp = _context.Pret22RecepcionEmpleados.Where(c => /*c.IdEstado == 1 &&*/ c.IdRecepcion == idrecepcion).ToList();
                                                    if (ExistingRecEmp != null)
                                                    {
                                                        foreach (Pret22RecepcionEmpleado empleadorec in ExistingRecEmp)
                                                        {
                                                            //empleadoext.IdEstado = 2;
                                                            //empleadoext.TxtEstado = "INACTIVO";
                                                            _context.Pret22RecepcionEmpleados.Remove(empleadorec);
                                                            await _context.SaveChangesAsync();
                                                        }
                                                    }
                                                    _context.Pret11Recepcions.Remove(recepcion);
                                                    await _context.SaveChangesAsync();
                                                }
                                            }

                                            var existingArchivoEnv = _context.Pret17Archivos.Where(c => c.IdEnvio == idenvio).ToList();
                                            if (existingArchivoEnv != null)
                                            {
                                                foreach (Pret17Archivo archivoReg in existingArchivoEnv)
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
                                            var ExistingEnvDtll = _context.Pret13EnvioDtls.Where(c => /*c.IdEstado == 1 &&*/ c.IdEnvio == idenvio).ToList();
                                            if (ExistingEnvDtll != null)
                                            {
                                                foreach (Pret13EnvioDtl detallenv in ExistingEnvDtll)
                                                {
                                                    //detalleext.IdEstado = 2;
                                                    //detalleext.TxtEstado = "INACTIVO";
                                                    _context.Pret13EnvioDtls.Remove(detallenv);
                                                    await _context.SaveChangesAsync();
                                                }
                                            }
                                            var ExistingEnvEmp = _context.Pret21EnvioEmpleados.Where(c => /*c.IdEstado == 1 &&*/ c.IdEnvio == idenvio).ToList();
                                            if (ExistingEnvEmp != null)
                                            {
                                                foreach (Pret21EnvioEmpleado empleadoenv in ExistingEnvEmp)
                                                {
                                                    //empleadoext.IdEstado = 2;
                                                    //empleadoext.TxtEstado = "INACTIVO";
                                                    _context.Pret21EnvioEmpleados.Remove(empleadoenv);
                                                    await _context.SaveChangesAsync();
                                                }
                                            }
                                            _context.Pret10Envios.Remove(envioext);
                                            await _context.SaveChangesAsync();

                                        }

                                    }
                                    _context.Pret07Extraccions.Remove(ExtReg);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            _context.Pret02Campanas.Remove(campana);
                            await _context.SaveChangesAsync();
                        }
                    }


                    _context.Pret01Predios.Remove(existingPredio);
                    await _context.SaveChangesAsync();
                }


                await _context.SaveChangesAsync();
                return Json(new { redirectUrl = Url.Action("ListadoPre", "Pret01Predio") });
            }
            catch (Exception ex)
            {
                // Manejar el error, redirigir a una vista de error, o realizar otras acciones según tu lógica
                return RedirectToAction("Error");
            }
        }





    }
}

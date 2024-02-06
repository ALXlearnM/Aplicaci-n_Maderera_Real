
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Models;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ConfigUtilitarios;

namespace Maderera_Aplicacion_Web.Controllers
{
    public class Pert01UsuarioController : Controller
    {
        private readonly EagleContext _context;

        public Pert01UsuarioController(EagleContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            //        List<string> contraseñas = new List<string>
            //{
            //    "bjZ+0P/vLrXi6rIsmqe/hg==",
            //    "mZmEIIoHCx577CXj4NpJlw==",
            //    "S3MdT5041ox77CXj4NpJlw==",
            //    "OP9/UzE2zwUTJSVAqTGtHA==",
            //    "ZslRWCzw1t+YDf1GYYnsRw==",
            //    "o75GkRYq0lvLWoshNIqGwQ=="
            //};

            //        List<string> contraseñasCodificadas = new List<string>();
            //        Encription encription = new Encription();

            //        foreach (string contraseña in contraseñas)
            //        {
            //            string contraseñaEncriptada = (encription.Decryption(contraseña));
            //            contraseñasCodificadas.Add(contraseñaEncriptada);
            //        }
            //        ViewBag.UsuarioContra = contraseñasCodificadas;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Pert01Usuario u)
        {
            try
            {
                ViewBag.Paso1 = "Paso1";
                using (var con = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    ViewBag.Paso2 = "Paso2";


                    using (var cmd = new SqlCommand("sp_validar_usuario", con))
                    {
                        ViewBag.Paso3 = "Paso3";

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        ViewBag.Paso4 = "Paso4";
                        cmd.Parameters.Add(new SqlParameter("@TxtUsuario", System.Data.SqlDbType.VarChar) { Value = u.TxtUsuario });
                        ViewBag.Paso5 = "Paso5";
                        cmd.Parameters.Add(new SqlParameter("@TxtClave", System.Data.SqlDbType.VarChar) { Value = new Encription().Encryption(u.TxtClave) });
                        ViewBag.Paso6 = "Paso6";
                        con.Open();
                        ViewBag.Paso7 = "Paso7";
                        var reader = cmd.ExecuteReader();
                        ViewBag.Paso8 = "Paso8";

                        while (reader.Read())
                        {
                            if (reader["txt_usuario"] != null && u.TxtUsuario != null)
                            {

                                List<Claim> c = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, u.TxtUsuario),
                                };
                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();
                                p.AllowRefresh = true;
                                p.IsPersistent = u.MantenerActivo;
                                if (!u.MantenerActivo)
                                {
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);
                                }
                                else
                                {
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);
                                }


                                // Convertir los datos decodificados a una cadena legible
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Credenciales incorrectas -- Cuenta No Registrada";
                            }
                        }
                        con.Close();
                    }
                    return View();
                }
            }
            catch (System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }



        public string GetUsuario()
        {
            var nombreUsuario = "";
            {
                nombreUsuario = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            return nombreUsuario;
        }

        public long GetIdUsuario()
        {
            var nombreUsuario = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var idusuario = _context.Pert01Usuarios.FirstOrDefault(c => c.TxtUsuario == nombreUsuario)?.IdUsuario ?? 0;
            return idusuario;
        }

        public void setSid(long a)
        {
            var claimsIdentity = new ClaimsIdentity(HttpContext.User.Identity);

            // Elimina la claim existente con el tipo "Sid" si ya existe
            var existingSidClaim = claimsIdentity.FindFirst(ClaimTypes.Sid);
            if (existingSidClaim != null)
            {
                claimsIdentity.RemoveClaim(existingSidClaim);
            }

            // Agrega la nueva claim "Sid" con el valor proporcionado
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, a.ToString()));

            // Crea un nuevo principal con la identidad actualizada
            var newClaimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Actualiza el principal en el contexto actual
            HttpContext.User = newClaimsPrincipal;

            // También podrías querer actualizar la cookie de autenticación si estás usando autenticación basada en cookies
            // Ejemplo para ASP.NET Core:
            HttpContext.SignInAsync(newClaimsPrincipal);


        }
        public long GetSid()
        {
            var sidClaim = HttpContext.User.FindFirst(ClaimTypes.Sid);

            // Verifica si la claim existe y si tiene un valor válido
            if (sidClaim != null && long.TryParse(sidClaim.Value, out var sidValue))
            {
                return sidValue;
            }

            // Valor predeterminado o manejo de error si la claim no existe o no tiene un valor válido
            return 0; // O puedes lanzar una excepción u otro valor predeterminado según tu lógica de manejo de errores.
        }

        public void RemoveSid()
        {
            var claimsIdentity = new ClaimsIdentity(HttpContext.User.Identity);

            // Elimina la claim existente con el tipo "Sid" si ya existe
            var existingSidClaim = claimsIdentity.FindFirst(ClaimTypes.Sid);
            if (existingSidClaim != null)
            {
                claimsIdentity.RemoveClaim(existingSidClaim);
            }

            // Crea un nuevo principal con la identidad actualizada (sin la claim "Sid")
            var newClaimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Actualiza el principal en el contexto actual
            HttpContext.User = newClaimsPrincipal;

            // También podrías querer actualizar la cookie de autenticación si estás usando autenticación basada en cookies
            // Ejemplo para ASP.NET Core:
            HttpContext.SignInAsync(newClaimsPrincipal);
        }



    }
}

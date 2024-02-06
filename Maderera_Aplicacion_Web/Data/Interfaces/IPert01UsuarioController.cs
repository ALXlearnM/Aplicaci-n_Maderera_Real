using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maderera_Aplicacion_Web.Data;
using Maderera_Aplicacion_Web.Models;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ConfigUtilitarios;
namespace Maderera_Aplicacion_Web.Data.Interfaces
{
    public interface IPert01UsuarioController
    {
        string GetUsuario();
        public long GetSid();
        public void setSid(long a);
        public void RemoveSid();
    }

    public class Pert01UsuarioController : IPert01UsuarioController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Pert01UsuarioController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsuario()
        {
            var nombreUsuario = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return nombreUsuario;
        }
        public long GetSid()
        {
            var sidClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);

            // Verifica si la claim existe y si tiene un valor válido
            if (sidClaim != null && long.TryParse(sidClaim.Value, out var sidValue))
            {
                return sidValue;
            }

            // Valor predeterminado o manejo de error si la claim no existe o no tiene un valor válido
            return 0; // O puedes lanzar una excepción u otro valor predeterminado según tu lógica de manejo de errores.
        }
        public void setSid(long a)
        {
            var claimsIdentity = new ClaimsIdentity(_httpContextAccessor.HttpContext.User.Identity);

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
            _httpContextAccessor.HttpContext.User = newClaimsPrincipal;

            // También podrías querer actualizar la cookie de autenticación si estás usando autenticación basada en cookies
            // Ejemplo para ASP.NET Core:
            _httpContextAccessor.HttpContext.SignInAsync(newClaimsPrincipal);


        }
        public void RemoveSid()
        {
            var claimsIdentity = new ClaimsIdentity(_httpContextAccessor.HttpContext.User.Identity);

            // Elimina la claim existente con el tipo "Sid" si ya existe
            var existingSidClaim = claimsIdentity.FindFirst(ClaimTypes.Sid);
            if (existingSidClaim != null)
            {
                claimsIdentity.RemoveClaim(existingSidClaim);
            }

            // Crea un nuevo principal con la identidad actualizada (sin la claim "Sid")
            var newClaimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Actualiza el principal en el contexto actual
            _httpContextAccessor.HttpContext.User = newClaimsPrincipal;

            // También podrías querer actualizar la cookie de autenticación si estás usando autenticación basada en cookies
            // Ejemplo para ASP.NET Core:
            _httpContextAccessor.HttpContext.SignInAsync(newClaimsPrincipal);
        }

    }

}

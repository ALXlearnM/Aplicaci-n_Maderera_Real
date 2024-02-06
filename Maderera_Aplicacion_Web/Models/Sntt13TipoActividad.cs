using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt13TipoActividad
    {
        public int IdTipoOperacion { get; set; }
        public string? CodTipoActividad { get; set; }
        public string? CodTipoActividadPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
    }
}

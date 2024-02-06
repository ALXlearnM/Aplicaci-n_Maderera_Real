using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Grlt02Estado
    {
        public int IdEstado { get; set; }
        public string? CodEstado { get; set; }
        public string? TxtAbrv { get; set; }
        public string TxtDesc { get; set; } = null!;
    }
}

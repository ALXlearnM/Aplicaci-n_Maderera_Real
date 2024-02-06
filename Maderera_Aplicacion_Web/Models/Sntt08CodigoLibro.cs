using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt08CodigoLibro
    {
        public int IdLibro { get; set; }
        public string? CodLibro { get; set; }
        public string? CodLibroPle { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
    }
}

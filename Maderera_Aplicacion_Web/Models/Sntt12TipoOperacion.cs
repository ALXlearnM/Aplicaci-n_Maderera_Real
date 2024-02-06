using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt12TipoOperacion
    {
        public int IdTipoOperacion { get; set; }
        public string? CodTipoOperacion { get; set; }
        public string? CodTipoOperacionPle { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
    }
}

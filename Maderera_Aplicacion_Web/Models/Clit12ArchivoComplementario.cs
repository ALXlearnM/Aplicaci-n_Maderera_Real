using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit12ArchivoComplementario
    {
        public long IdArchivoComplelemtario { get; set; }
        public string? CodArchivoComplelemtario { get; set; }
        public long? IdEstudioComplementario { get; set; }
        public string? TxtPath { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Clit11EstudioComplementario? IdEstudioComplementarioNavigation { get; set; }
    }
}

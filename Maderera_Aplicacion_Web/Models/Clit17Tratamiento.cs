using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit17Tratamiento
    {
        public long IdTratamiento { get; set; }
        public string? CodTratamiento { get; set; }
        public string? TxtDesc { get; set; }
        public DateTime? FecNegocio { get; set; }
        public DateTime? FecRegistro { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public long IdAtencion { get; set; }

        public virtual Clit03Atencion IdAtencionNavigation { get; set; } = null!;
    }
}

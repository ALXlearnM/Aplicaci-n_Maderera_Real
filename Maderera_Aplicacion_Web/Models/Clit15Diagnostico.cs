using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit15Diagnostico
    {
        public long IdDiagnostico { get; set; }
        public string? CodDiagnostico { get; set; }
        public string? TxtDesc { get; set; }
        public DateTime? FecNegocio { get; set; }
        public DateTime? FecRegistro { get; set; }
        public string? Cod4Cie10 { get; set; }
        public string? TxtCie10 { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int IdCie10 { get; set; }
        public long? IdAtencion { get; set; }

        public virtual Clit03Atencion? IdAtencionNavigation { get; set; }
        public virtual Clit16Cie10 IdCie10Navigation { get; set; } = null!;
    }
}

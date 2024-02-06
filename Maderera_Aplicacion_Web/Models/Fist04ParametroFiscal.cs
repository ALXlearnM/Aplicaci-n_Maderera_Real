using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Fist04ParametroFiscal
    {
        public Fist04ParametroFiscal()
        {
            Fist05ConfiguracionFiscalCajas = new HashSet<Fist05ConfiguracionFiscalCaja>();
        }

        public int IdParametroFiscal { get; set; }
        public string CodParametroFiscal { get; set; } = null!;
        public string TxtDesc { get; set; } = null!;
        public string? ValorDefault { get; set; }

        public virtual ICollection<Fist05ConfiguracionFiscalCaja> Fist05ConfiguracionFiscalCajas { get; set; }
    }
}

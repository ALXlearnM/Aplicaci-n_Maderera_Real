using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Fist05ConfiguracionFiscalCaja
    {
        public int IdConfiguracionFiscalCaja { get; set; }
        public string? Valor { get; set; }
        public int IdParametroFiscal { get; set; }
        public int IdCaja { get; set; }

        public virtual Mstt12Caja IdCajaNavigation { get; set; } = null!;
        public virtual Fist04ParametroFiscal IdParametroFiscalNavigation { get; set; } = null!;
    }
}

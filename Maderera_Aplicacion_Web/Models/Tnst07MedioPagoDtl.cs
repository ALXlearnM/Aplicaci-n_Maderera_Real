using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tnst07MedioPagoDtl
    {
        public long IdMedioPagoDtl { get; set; }
        public long IdCompEmitido { get; set; }
        public int IdMedioPago { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Monto { get; set; }
        public decimal MtoTipoCambio { get; set; }
        public string? TxtObserv { get; set; }
        public string? TxtRef { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Tnst04CompEmitido IdCompEmitidoNavigation { get; set; } = null!;
        public virtual Mstt01MedioPago IdMedioPagoNavigation { get; set; } = null!;
    }
}

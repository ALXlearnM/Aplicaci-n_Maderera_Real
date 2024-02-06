using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tnst08DescuentoDtl
    {
        public long IdDescuentoDtl { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Monto { get; set; }
        public string? TxtObserv { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdCompEmitido { get; set; }
        public int IdDescuento { get; set; }
        public int? IdRazon { get; set; }
        public long? IdEmpAutorizador { get; set; }

        public virtual Tnst04CompEmitido IdCompEmitidoNavigation { get; set; } = null!;
        public virtual Mstt02Descuento IdDescuentoNavigation { get; set; } = null!;
        public virtual Pert04Empleado? IdEmpAutorizadorNavigation { get; set; }
        public virtual Mstt05Razon? IdRazonNavigation { get; set; }
    }
}

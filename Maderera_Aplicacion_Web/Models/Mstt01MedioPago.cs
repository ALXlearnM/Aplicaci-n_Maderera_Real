using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt01MedioPago
    {
        public Mstt01MedioPago()
        {
            Tnst07MedioPagoDtls = new HashSet<Tnst07MedioPagoDtl>();
        }

        public int IdMedioPago { get; set; }
        public string? CodMedioPago { get; set; }
        public string TxtDesc { get; set; } = null!;
        public bool ReqRef { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public int? IdTipoMedioPago { get; set; }

        public virtual Sntt01TipoMedioPago? IdTipoMedioPagoNavigation { get; set; }
        public virtual ICollection<Tnst07MedioPagoDtl> Tnst07MedioPagoDtls { get; set; }
    }
}

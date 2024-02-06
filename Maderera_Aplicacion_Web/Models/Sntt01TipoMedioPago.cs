using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt01TipoMedioPago
    {
        public Sntt01TipoMedioPago()
        {
            Mstt01MedioPagos = new HashSet<Mstt01MedioPago>();
        }

        public int IdTipoMedioPago { get; set; }
        public string? CodTipoMedioPago { get; set; }
        public string? CodTipoMedioPagoPle { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Mstt01MedioPago> Mstt01MedioPagos { get; set; }
    }
}

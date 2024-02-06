using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret15ProduccionDtl
    {
        public long IdProducciondtl { get; set; }
        public long? IdProduccion { get; set; }
        public long? IdProductoPro { get; set; }
        public string? TxtProPro { get; set; }
        public int? IdUmPro { get; set; }
        public int? CantidadPro { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public string? TxtComentario { get; set; }
        public double TotalProp { get; set; }

        public virtual Pret14Produccion? IdProduccionNavigation { get; set; }
        public virtual Prot09Producto? IdProductoProNavigation { get; set; }
        public virtual Sntt06UnidadMedidum? IdUmProNavigation { get; set; }
    }
}

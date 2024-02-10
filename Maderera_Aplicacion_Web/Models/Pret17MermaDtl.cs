using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret17MermaDtl
    {
        public long IdMermadtl { get; set; }
        public long IdMerma { get; set; }
        public long? IdProductoMer { get; set; }
        public string? TxtProMer { get; set; }
        public int? IdUmMer { get; set; }
        public int? CantidadMer { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public string? TxtComentario { get; set; }
        public double TotalMer { get; set; }

        public virtual Pret16Merma IdMermaNavigation { get; set; } = null!;
        public virtual Prot09Producto? IdProductoMerNavigation { get; set; }
        public virtual Sntt06UnidadMedidum? IdUmMerNavigation { get; set; }
    }
}

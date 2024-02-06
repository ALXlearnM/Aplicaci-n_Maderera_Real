using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot11RecetaDtl
    {
        public long IdRecetaDtl { get; set; }
        public long? IdProducto { get; set; }
        public string? TxtProducto { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Cantidad { get; set; }
        public int IdUm { get; set; }
        public long IdReceta { get; set; }

        public virtual Prot09Producto? IdProductoNavigation { get; set; }
        public virtual Prot10Recetum IdRecetaNavigation { get; set; } = null!;
        public virtual Sntt06UnidadMedidum IdUmNavigation { get; set; } = null!;
    }
}

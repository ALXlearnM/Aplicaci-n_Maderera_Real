using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret17MermaDtl
    {
        public int IdMermadtl { get; set; }
        public int IdMerma { get; set; }
        public long IdProducto { get; set; }
        public DateTime? FechaMermadtl { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Pret16Merma IdMermaNavigation { get; set; } = null!;
        public virtual Prot09Producto IdProductoNavigation { get; set; } = null!;
    }
}

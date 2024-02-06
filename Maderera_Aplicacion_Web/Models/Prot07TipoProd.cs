using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot07TipoProd
    {
        public Prot07TipoProd()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
        }

        public int IdTipoProd { get; set; }
        public string? CodTipoProd { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
    }
}

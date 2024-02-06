using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot06ClaseProd
    {
        public Prot06ClaseProd()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
        }

        public int IdClaseProd { get; set; }
        public string? CodClaseProd { get; set; }
        public int IdFamilia { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int IdGrupoProd { get; set; }

        public virtual Prot05GrupoProd IdGrupoProdNavigation { get; set; } = null!;
        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
    }
}

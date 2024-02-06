using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot04Subfamilium
    {
        public Prot04Subfamilium()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
        }

        public int IdSubfamilia { get; set; }
        public string? CodSubfamilia { get; set; }
        public int IdFamilia { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Prot03Familium IdFamiliaNavigation { get; set; } = null!;
        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
    }
}

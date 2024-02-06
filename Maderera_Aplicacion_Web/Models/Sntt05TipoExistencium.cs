using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt05TipoExistencium
    {
        public Sntt05TipoExistencium()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
        }

        public int IdTipoExistencia { get; set; }
        public string? CodTipoExistencia { get; set; }
        public string? CodTipoExistenciaPle { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
    }
}

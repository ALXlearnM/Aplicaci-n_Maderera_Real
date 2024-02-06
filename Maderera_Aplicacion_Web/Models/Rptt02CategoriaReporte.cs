using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Rptt02CategoriaReporte
    {
        public Rptt02CategoriaReporte()
        {
            Rptt01Reportes = new HashSet<Rptt01Reporte>();
        }

        public int IdCategoriaReporte { get; set; }
        public string? CodCategoriaReporte { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Rptt01Reporte> Rptt01Reportes { get; set; }
    }
}

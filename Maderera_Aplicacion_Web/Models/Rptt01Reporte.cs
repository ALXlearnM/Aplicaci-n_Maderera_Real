using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Rptt01Reporte
    {
        public int IdReporte { get; set; }
        public string? CodReporte { get; set; }
        public string TxtDesc { get; set; } = null!;
        public bool SnDateRange { get; set; }
        public bool SnRvcRange { get; set; }
        public bool SnEmpleado { get; set; }
        public bool SnClaseEmpleado { get; set; }
        public bool SnProductoPorNombre { get; set; }
        public bool SnProductoPorFamilia { get; set; }
        public bool SnProductoPorSubfamilia { get; set; }
        public bool SnTurno { get; set; }
        public string? TxtPath { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public int IdCategoriaReporte { get; set; }

        public virtual Rptt02CategoriaReporte IdCategoriaReporteNavigation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret24MermaEmpleado
    {
        public long IdEmpven { get; set; }
        public long IdMerma { get; set; }
        public decimal? Salario { get; set; }
        public long? IdEmpleadoMer { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Pert04Empleado? IdEmpleadoMerNavigation { get; set; }
        public virtual Pret16Merma IdMermaNavigation { get; set; } = null!;
    }
}

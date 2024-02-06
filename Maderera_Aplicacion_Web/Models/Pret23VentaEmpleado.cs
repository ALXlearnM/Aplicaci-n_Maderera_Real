using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret23VentaEmpleado
    {
        public long IdEmpven { get; set; }
        public long IdCompEmitido { get; set; }
        public decimal? Salario { get; set; }
        public long? IdEmpleadoVen { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Tnst04CompEmitido IdCompEmitidoNavigation { get; set; } = null!;
        public virtual Pert04Empleado? IdEmpleadoVenNavigation { get; set; }
    }
}

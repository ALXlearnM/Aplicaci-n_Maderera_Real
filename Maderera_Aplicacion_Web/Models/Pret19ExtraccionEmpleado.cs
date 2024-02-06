using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret19ExtraccionEmpleado
    {
        public long IdEmpext { get; set; }
        public long IdExtraccion { get; set; }
        public long? IdEmpleadoEx { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public decimal? SalarioEmp { get; set; }

        public virtual Pert04Empleado? IdEmpleadoExNavigation { get; set; }
        public virtual Pret07Extraccion IdExtraccionNavigation { get; set; } = null!;
    }
}

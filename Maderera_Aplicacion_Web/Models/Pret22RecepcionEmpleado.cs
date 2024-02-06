using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret22RecepcionEmpleado
    {
        public int IdRecemp { get; set; }
        public long IdRecepcion { get; set; }
        public long? IdEmpleadoRec { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public decimal? SalarioEmp { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Pert04Empleado? IdEmpleadoRecNavigation { get; set; }
        public virtual Pret11Recepcion IdRecepcionNavigation { get; set; } = null!;
    }
}

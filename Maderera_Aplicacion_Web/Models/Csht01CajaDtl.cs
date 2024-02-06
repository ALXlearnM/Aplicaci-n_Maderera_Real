using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Csht01CajaDtl
    {
        public long IdCajaDtl { get; set; }
        public int IdCaja { get; set; }
        public long IdEmpleado { get; set; }
        public long IdEmpAutorizador { get; set; }
        public bool SnOpen { get; set; }
        public bool SnClose { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int IdTurno { get; set; }

        public virtual Mstt12Caja IdCajaNavigation { get; set; } = null!;
        public virtual Pert04Empleado IdEmpAutorizadorNavigation { get; set; } = null!;
        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual Mstt13Turno IdTurnoNavigation { get; set; } = null!;
    }
}

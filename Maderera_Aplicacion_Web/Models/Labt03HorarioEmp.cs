using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Labt03HorarioEmp
    {
        public Labt03HorarioEmp()
        {
            Labt04HorarioEmpDtls = new HashSet<Labt04HorarioEmpDtl>();
        }

        public long IdHorarioEmp { get; set; }
        public DateTime FechaInicioHorario { get; set; }
        public DateTime FechaFinHorario { get; set; }
        public long IdEmpleado { get; set; }

        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual ICollection<Labt04HorarioEmpDtl> Labt04HorarioEmpDtls { get; set; }
    }
}

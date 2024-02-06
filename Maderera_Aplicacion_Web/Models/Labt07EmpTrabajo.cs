using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Labt07EmpTrabajo
    {
        public Labt07EmpTrabajo()
        {
            Labt01Asistencia = new HashSet<Labt01Asistencium>();
        }

        public long IdEmpTrabajo { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public int IdTrabajo { get; set; }
        public long IdEmpleado { get; set; }

        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual Labt06Trabajo IdTrabajoNavigation { get; set; } = null!;
        public virtual ICollection<Labt01Asistencium> Labt01Asistencia { get; set; }
    }
}

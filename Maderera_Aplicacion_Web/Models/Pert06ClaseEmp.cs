using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert06ClaseEmp
    {
        public Pert06ClaseEmp()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
            Pert08SecurityAccesses = new HashSet<Pert08SecurityAccess>();
        }

        public int IdClaseEmp { get; set; }
        public string? CodClaseEmp { get; set; }
        public string TxtNombre { get; set; } = null!;
        public string? TxtDesc { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
        public virtual ICollection<Pert08SecurityAccess> Pert08SecurityAccesses { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert05CategoriaEmp
    {
        public Pert05CategoriaEmp()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdCategoriaEmp { get; set; }
        public string? CodCategoriaEmp { get; set; }
        public string TxtNombre { get; set; } = null!;
        public string? TxtDesc { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

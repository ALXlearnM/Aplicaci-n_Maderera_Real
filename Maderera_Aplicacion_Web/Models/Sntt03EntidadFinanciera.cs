using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt03EntidadFinanciera
    {
        public Sntt03EntidadFinanciera()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdEntidadFinanciera { get; set; }
        public string? CodEntidadFinanciera { get; set; }
        public string? CodEntidadFinancieraPle { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

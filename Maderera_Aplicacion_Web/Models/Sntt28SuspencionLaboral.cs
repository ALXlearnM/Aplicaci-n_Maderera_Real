using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt28SuspencionLaboral
    {
        public Sntt28SuspencionLaboral()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdSuspencionLaboral { get; set; }
        public string? CodSuspencionLaboral { get; set; }
        public string? CodSuspencionLaboralPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

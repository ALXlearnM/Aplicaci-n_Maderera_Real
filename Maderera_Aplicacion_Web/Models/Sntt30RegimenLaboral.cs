using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt30RegimenLaboral
    {
        public Sntt30RegimenLaboral()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdRegimenLaboral { get; set; }
        public string? CodRegimenLaboral { get; set; }
        public string? CodRegimenLaboralPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

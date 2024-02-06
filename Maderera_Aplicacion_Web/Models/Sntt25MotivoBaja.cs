using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt25MotivoBaja
    {
        public Sntt25MotivoBaja()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdMotivoBaja { get; set; }
        public string? CodMotivoBaja { get; set; }
        public string? CodMotivoBajaPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt24Situacion
    {
        public Sntt24Situacion()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdSituacion { get; set; }
        public string? CodSituacion { get; set; }
        public string? CodSituacionPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

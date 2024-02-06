using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt23SaludEp
    {
        public Sntt23SaludEp()
        {
            Clit01Pacientes = new HashSet<Clit01Paciente>();
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdSaludEps { get; set; }
        public string? CodSaludEps { get; set; }
        public string? CodSaludEpsPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Clit01Paciente> Clit01Pacientes { get; set; }
        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

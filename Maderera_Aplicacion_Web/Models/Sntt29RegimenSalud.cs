using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt29RegimenSalud
    {
        public Sntt29RegimenSalud()
        {
            Clit01Pacientes = new HashSet<Clit01Paciente>();
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdRegimenSalud { get; set; }
        public string? CodRegimenSalud { get; set; }
        public string? CodRegimenSaludPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Clit01Paciente> Clit01Pacientes { get; set; }
        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt22PeriodoRemuneracion
    {
        public Sntt22PeriodoRemuneracion()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdPeriodoRemuneracion { get; set; }
        public string? CodPeriodoRemuneracion { get; set; }
        public string? CodPeriodoRemuneracionPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

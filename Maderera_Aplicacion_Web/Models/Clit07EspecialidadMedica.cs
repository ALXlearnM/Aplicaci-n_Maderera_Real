using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit07EspecialidadMedica
    {
        public Clit07EspecialidadMedica()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdEspecialidadMedica { get; set; }
        public string? CodEspecialidadMedica { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int? IdTipoEspecialidad { get; set; }

        public virtual Clit06TipoEspecialidad? IdTipoEspecialidadNavigation { get; set; }
        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

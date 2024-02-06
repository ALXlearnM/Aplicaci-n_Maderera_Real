using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt17TipoTrabajador
    {
        public Sntt17TipoTrabajador()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdTipoTrabajador { get; set; }
        public string? CodTipoTrabajador { get; set; }
        public string? CodTipoTrabajadorPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

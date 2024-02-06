using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt07EstadoCivil
    {
        public Mstt07EstadoCivil()
        {
            Pert02Clientes = new HashSet<Pert02Cliente>();
            Pert03Proveedors = new HashSet<Pert03Proveedor>();
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdEstadoCivil { get; set; }
        public string? CodEstadoCivil { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert02Cliente> Pert02Clientes { get; set; }
        public virtual ICollection<Pert03Proveedor> Pert03Proveedors { get; set; }
        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

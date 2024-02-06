using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt14Nacionalidad
    {
        public Sntt14Nacionalidad()
        {
            Pert02Clientes = new HashSet<Pert02Cliente>();
            Pert03Proveedors = new HashSet<Pert03Proveedor>();
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdNacionalidad { get; set; }
        public string? CodNacionalidad { get; set; }
        public string? CodNacionalidadPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert02Cliente> Pert02Clientes { get; set; }
        public virtual ICollection<Pert03Proveedor> Pert03Proveedors { get; set; }
        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

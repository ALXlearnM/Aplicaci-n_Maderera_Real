using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt02TipoDocIdentidad
    {
        public Sntt02TipoDocIdentidad()
        {
            Pert02Clientes = new HashSet<Pert02Cliente>();
            Pert03Proveedors = new HashSet<Pert03Proveedor>();
            Pert04Empleados = new HashSet<Pert04Empleado>();
            Pert09Inversionista = new HashSet<Pert09Inversionistum>();
        }

        public int IdTipoDocIdentidad { get; set; }
        public string? CodTipoDocIdentidad { get; set; }
        public string? CodTipoDocIdentidadPle { get; set; }
        public string TxtAbrv { get; set; } = null!;
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert02Cliente> Pert02Clientes { get; set; }
        public virtual ICollection<Pert03Proveedor> Pert03Proveedors { get; set; }
        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
        public virtual ICollection<Pert09Inversionistum> Pert09Inversionista { get; set; }
    }
}

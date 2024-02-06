using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt33Distrito
    {
        public Sntt33Distrito()
        {
            Mstt08Locations = new HashSet<Mstt08Location>();
            Pert02Clientes = new HashSet<Pert02Cliente>();
            Pert03Proveedors = new HashSet<Pert03Proveedor>();
            Pert04Empleados = new HashSet<Pert04Empleado>();
            Pret01Predios = new HashSet<Pret01Predio>();
            Pret02Campanas = new HashSet<Pret02Campana>();
        }

        public int IdDist { get; set; }
        public string CodDist { get; set; } = null!;
        public string? CodDistPle { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdProv { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Sntt32Provincium IdProvNavigation { get; set; } = null!;
        public virtual ICollection<Mstt08Location> Mstt08Locations { get; set; }
        public virtual ICollection<Pert02Cliente> Pert02Clientes { get; set; }
        public virtual ICollection<Pert03Proveedor> Pert03Proveedors { get; set; }
        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
        public virtual ICollection<Pret01Predio> Pret01Predios { get; set; }
        public virtual ICollection<Pret02Campana> Pret02Campanas { get; set; }
    }
}

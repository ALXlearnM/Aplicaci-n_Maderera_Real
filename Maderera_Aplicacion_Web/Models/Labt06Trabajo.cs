using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Labt06Trabajo
    {
        public Labt06Trabajo()
        {
            Labt07EmpTrabajos = new HashSet<Labt07EmpTrabajo>();
        }

        public int IdTrabajo { get; set; }
        public string? CodTrabajo { get; set; }
        public string TxtNombre { get; set; } = null!;
        public string? TxtDesc { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Labt07EmpTrabajo> Labt07EmpTrabajos { get; set; }
    }
}

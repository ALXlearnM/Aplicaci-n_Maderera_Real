using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot03Familium
    {
        public Prot03Familium()
        {
            Prot04Subfamilia = new HashSet<Prot04Subfamilium>();
        }

        public int IdFamilia { get; set; }
        public string? CodFamilia { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Prot04Subfamilium> Prot04Subfamilia { get; set; }
    }
}

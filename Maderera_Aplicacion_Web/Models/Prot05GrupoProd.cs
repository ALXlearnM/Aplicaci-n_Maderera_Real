using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot05GrupoProd
    {
        public Prot05GrupoProd()
        {
            Prot06ClaseProds = new HashSet<Prot06ClaseProd>();
        }

        public int IdGrupoProd { get; set; }
        public string? CodGrupoProd { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Prot06ClaseProd> Prot06ClaseProds { get; set; }
    }
}

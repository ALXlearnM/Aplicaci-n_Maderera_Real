using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot19TipoProdCom
    {
        public Prot19TipoProdCom()
        {
            Prot20Marcacoms = new HashSet<Prot20Marcacom>();
        }

        public int IdTipoProd { get; set; }
        public string? CodTipoProd { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Prot20Marcacom> Prot20Marcacoms { get; set; }
    }
}

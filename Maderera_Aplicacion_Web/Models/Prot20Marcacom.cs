using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot20Marcacom
    {
        public Prot20Marcacom()
        {
            Prot21Modelocoms = new HashSet<Prot21Modelocom>();
        }

        public int IdMarca { get; set; }
        public string? CodMarca { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int? IdTipoProd { get; set; }

        public virtual Prot19TipoProdCom? IdTipoProdNavigation { get; set; }
        public virtual ICollection<Prot21Modelocom> Prot21Modelocoms { get; set; }
    }
}

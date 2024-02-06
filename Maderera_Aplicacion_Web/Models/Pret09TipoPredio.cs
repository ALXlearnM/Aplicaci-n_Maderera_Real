using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret09TipoPredio
    {
        public Pret09TipoPredio()
        {
            Pret01Predios = new HashSet<Pret01Predio>();
        }

        public long IdTipoPredio { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Pret01Predio> Pret01Predios { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret03TipoCampana
    {
        public Pret03TipoCampana()
        {
            Pret02Campanas = new HashSet<Pret02Campana>();
        }

        public long IdTipoCampana { get; set; }
        public string? TxtDesc { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Pret02Campana> Pret02Campanas { get; set; }
    }
}

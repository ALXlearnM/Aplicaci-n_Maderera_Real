using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret18TipoMotivo
    {
        public Pret18TipoMotivo()
        {
            Pret05CtrlCalidads = new HashSet<Pret05CtrlCalidad>();
        }

        public long IdTipoMo { get; set; }
        public string? Txtdesc { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Pret05CtrlCalidad> Pret05CtrlCalidads { get; set; }
    }
}

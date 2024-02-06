using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit16Cie10
    {
        public Clit16Cie10()
        {
            Clit15Diagnosticos = new HashSet<Clit15Diagnostico>();
        }

        public int IdCie10 { get; set; }
        public string? Cod3 { get; set; }
        public string TxtCategoria { get; set; } = null!;
        public string? Cod4 { get; set; }
        public string TxtDesc { get; set; } = null!;

        public virtual ICollection<Clit15Diagnostico> Clit15Diagnosticos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot17ComboGrupo
    {
        public Prot17ComboGrupo()
        {
            Prot13Combos = new HashSet<Prot13Combo>();
        }

        public int IdComboGrupo { get; set; }
        public string? CodComboGrupo { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Prot13Combo> Prot13Combos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt16TipoRazon
    {
        public Mstt16TipoRazon()
        {
            Mstt05Razons = new HashSet<Mstt05Razon>();
        }

        public int IdTipoRazon { get; set; }
        public string? CodTipoRazon { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Mstt05Razon> Mstt05Razons { get; set; }
    }
}

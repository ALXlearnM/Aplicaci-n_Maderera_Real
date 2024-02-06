using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt15EstadoMesa
    {
        public Mstt15EstadoMesa()
        {
            Mstt14Mesas = new HashSet<Mstt14Mesa>();
        }

        public int IdEstadoMesa { get; set; }
        public string? CodEstadoMesa { get; set; }
        public string TxtDesc { get; set; } = null!;
        public string? TxtColorHex { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Mstt14Mesa> Mstt14Mesas { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt09TipoLocation
    {
        public Mstt09TipoLocation()
        {
            Mstt08Locations = new HashSet<Mstt08Location>();
        }

        public int IdTipoLocation { get; set; }
        public string? CodTipoLocation { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Mstt08Location> Mstt08Locations { get; set; }
    }
}

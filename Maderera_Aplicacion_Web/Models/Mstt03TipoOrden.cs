using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt03TipoOrden
    {
        public Mstt03TipoOrden()
        {
            Tnst01CompRecibidos = new HashSet<Tnst01CompRecibido>();
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public int IdTipoOrden { get; set; }
        public string? CodTipoOrden { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Tnst01CompRecibido> Tnst01CompRecibidos { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}

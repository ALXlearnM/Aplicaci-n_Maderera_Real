using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Fist03TipoNumeracion
    {
        public Fist03TipoNumeracion()
        {
            Fist01ControlNumeracions = new HashSet<Fist01ControlNumeracion>();
        }

        public int IdTipoNumeracion { get; set; }
        public string? CodTipoNumeracion { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Fist01ControlNumeracion> Fist01ControlNumeracions { get; set; }
    }
}

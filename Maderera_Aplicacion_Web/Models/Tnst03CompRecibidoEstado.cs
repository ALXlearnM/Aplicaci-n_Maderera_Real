using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tnst03CompRecibidoEstado
    {
        public long IdCompRecibidoEstado { get; set; }
        public long IdCompRecibido { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }

        public virtual Tnst01CompRecibido IdCompRecibidoNavigation { get; set; } = null!;
    }
}

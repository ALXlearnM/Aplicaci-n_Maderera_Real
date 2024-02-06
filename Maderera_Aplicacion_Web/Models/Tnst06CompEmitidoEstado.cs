using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tnst06CompEmitidoEstado
    {
        public long IdCompEmitidoEstado { get; set; }
        public long IdCompEmitido { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public string TxtUsuario { get; set; } = null!;
        public long IdUsuario { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Tnst04CompEmitido IdCompEmitidoNavigation { get; set; } = null!;
    }
}

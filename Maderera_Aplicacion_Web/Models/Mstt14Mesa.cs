using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt14Mesa
    {
        public Mstt14Mesa()
        {
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public int IdMesa { get; set; }
        public string? CodMesa { get; set; }
        public string TxtNum { get; set; } = null!;
        public int Capacidad { get; set; }
        public int? IdCanVta { get; set; }
        public int IdEstadoMesa { get; set; }

        public virtual Mstt04CanalVtum? IdCanVtaNavigation { get; set; }
        public virtual Mstt15EstadoMesa IdEstadoMesaNavigation { get; set; } = null!;
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt05Razon
    {
        public Mstt05Razon()
        {
            Labt01Asistencia = new HashSet<Labt01Asistencium>();
            Labt02AsistenciaAjustada = new HashSet<Labt02AsistenciaAjustadum>();
            Tnst02CompRecibidoDtls = new HashSet<Tnst02CompRecibidoDtl>();
            Tnst05CompEmitidoDtls = new HashSet<Tnst05CompEmitidoDtl>();
            Tnst08DescuentoDtls = new HashSet<Tnst08DescuentoDtl>();
        }

        public int IdRazon { get; set; }
        public string? CodRazon { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int IdTipoRazon { get; set; }

        public virtual Mstt16TipoRazon IdTipoRazonNavigation { get; set; } = null!;
        public virtual ICollection<Labt01Asistencium> Labt01Asistencia { get; set; }
        public virtual ICollection<Labt02AsistenciaAjustadum> Labt02AsistenciaAjustada { get; set; }
        public virtual ICollection<Tnst02CompRecibidoDtl> Tnst02CompRecibidoDtls { get; set; }
        public virtual ICollection<Tnst05CompEmitidoDtl> Tnst05CompEmitidoDtls { get; set; }
        public virtual ICollection<Tnst08DescuentoDtl> Tnst08DescuentoDtls { get; set; }
    }
}

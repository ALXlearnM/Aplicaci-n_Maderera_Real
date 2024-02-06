using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt11TipoImpresora
    {
        public Mstt11TipoImpresora()
        {
            Mstt10Impresoras = new HashSet<Mstt10Impresora>();
        }

        public int IdTipoImpresora { get; set; }
        public string? CodTipoImpresora { get; set; }
        public string? TxtDesc { get; set; }
        public string? TxtInfo01 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Mstt10Impresora> Mstt10Impresoras { get; set; }
    }
}

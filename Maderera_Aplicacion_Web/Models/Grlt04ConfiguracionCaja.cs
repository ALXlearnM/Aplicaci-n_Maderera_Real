using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Grlt04ConfiguracionCaja
    {
        public int IdConfig { get; set; }
        public int IdCaja { get; set; }
        public string? TxtPathlog { get; set; }

        public virtual Mstt12Caja IdCajaNavigation { get; set; } = null!;
    }
}

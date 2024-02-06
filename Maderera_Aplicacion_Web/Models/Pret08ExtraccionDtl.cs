using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret08ExtraccionDtl
    {
        public long IdExtracciondtl { get; set; }
        public long IdExtraccion { get; set; }
        public string CodigoExtraccion { get; set; } = null!;
        public int NroArboles { get; set; }
        public int NroTrozos { get; set; }
        public double DiamPro { get; set; }
        public double AltArbolPro { get; set; }
        public string? Comentario { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdTipoArbol { get; set; }
        public string TxtTipoArbol { get; set; } = null!;

        public virtual Pret07Extraccion IdExtraccionNavigation { get; set; } = null!;
        public virtual Pret06TipoArbol IdTipoArbolNavigation { get; set; } = null!;
    }
}

﻿using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret13EnvioDtl
    {
        public int IdEnviodtl { get; set; }
        public long? IdEnvio { get; set; }
        public long IdTipoArbol { get; set; }
        public int? NroArboles { get; set; }
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
        public string CodigoExtraccion { get; set; } = null!;
        public string? TxtTipoArbol { get; set; }

        public virtual Pret10Envio? IdEnvioNavigation { get; set; }
        public virtual Pret06TipoArbol IdTipoArbolNavigation { get; set; } = null!;
    }
}

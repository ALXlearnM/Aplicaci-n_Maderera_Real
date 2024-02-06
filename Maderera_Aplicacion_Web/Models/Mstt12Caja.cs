using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt12Caja
    {
        public Mstt12Caja()
        {
            Csht01CajaDtls = new HashSet<Csht01CajaDtl>();
            Fist01ControlNumeracions = new HashSet<Fist01ControlNumeracion>();
            Fist05ConfiguracionFiscalCajas = new HashSet<Fist05ConfiguracionFiscalCaja>();
            Grlt04ConfiguracionCajas = new HashSet<Grlt04ConfiguracionCaja>();
        }

        public int IdCaja { get; set; }
        public string? CodCaja { get; set; }
        public string TxtDesc { get; set; } = null!;
        public string? TxtIp { get; set; }
        public string? TxtInfo01 { get; set; }
        public string? TxtInfo02 { get; set; }
        public int? IdImpresora { get; set; }
        public int? IdImpresora02 { get; set; }
        public int? IdImpresora03 { get; set; }
        public int? IdImpresora04 { get; set; }
        public int? IdImpresora05 { get; set; }
        public int? IdImpresora06 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Mstt10Impresora? IdImpresora02Navigation { get; set; }
        public virtual Mstt10Impresora? IdImpresora03Navigation { get; set; }
        public virtual Mstt10Impresora? IdImpresora04Navigation { get; set; }
        public virtual Mstt10Impresora? IdImpresora05Navigation { get; set; }
        public virtual Mstt10Impresora? IdImpresora06Navigation { get; set; }
        public virtual Mstt10Impresora? IdImpresoraNavigation { get; set; }
        public virtual ICollection<Csht01CajaDtl> Csht01CajaDtls { get; set; }
        public virtual ICollection<Fist01ControlNumeracion> Fist01ControlNumeracions { get; set; }
        public virtual ICollection<Fist05ConfiguracionFiscalCaja> Fist05ConfiguracionFiscalCajas { get; set; }
        public virtual ICollection<Grlt04ConfiguracionCaja> Grlt04ConfiguracionCajas { get; set; }
    }
}

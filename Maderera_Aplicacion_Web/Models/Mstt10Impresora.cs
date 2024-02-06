using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt10Impresora
    {
        public Mstt10Impresora()
        {
            Mstt12CajaIdImpresora02Navigations = new HashSet<Mstt12Caja>();
            Mstt12CajaIdImpresora03Navigations = new HashSet<Mstt12Caja>();
            Mstt12CajaIdImpresora04Navigations = new HashSet<Mstt12Caja>();
            Mstt12CajaIdImpresora05Navigations = new HashSet<Mstt12Caja>();
            Mstt12CajaIdImpresora06Navigations = new HashSet<Mstt12Caja>();
            Mstt12CajaIdImpresoraNavigations = new HashSet<Mstt12Caja>();
        }

        public int IdImpresora { get; set; }
        public string? CodImpresora { get; set; }
        public string? TxtDesc { get; set; }
        public string? TxtIp { get; set; }
        public int IdTipoImpresora { get; set; }
        public string? TxtMarca { get; set; }
        public string? TxtModelo { get; set; }
        public string? TxtInfo01 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Mstt11TipoImpresora IdTipoImpresoraNavigation { get; set; } = null!;
        public virtual ICollection<Mstt12Caja> Mstt12CajaIdImpresora02Navigations { get; set; }
        public virtual ICollection<Mstt12Caja> Mstt12CajaIdImpresora03Navigations { get; set; }
        public virtual ICollection<Mstt12Caja> Mstt12CajaIdImpresora04Navigations { get; set; }
        public virtual ICollection<Mstt12Caja> Mstt12CajaIdImpresora05Navigations { get; set; }
        public virtual ICollection<Mstt12Caja> Mstt12CajaIdImpresora06Navigations { get; set; }
        public virtual ICollection<Mstt12Caja> Mstt12CajaIdImpresoraNavigations { get; set; }
    }
}

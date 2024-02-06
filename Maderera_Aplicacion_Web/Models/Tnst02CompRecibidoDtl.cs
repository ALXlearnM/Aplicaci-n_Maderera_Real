using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tnst02CompRecibidoDtl
    {
        public long IdCompRecibidoDtl { get; set; }
        public long IdProducto { get; set; }
        public long IdCompRecibido { get; set; }
        public string TxtProducto { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public decimal? Peso { get; set; }
        public decimal PorDscto { get; set; }
        public decimal MtoDsctoSinTax { get; set; }
        public decimal MtoDsctoConTax { get; set; }
        public decimal PunitSinTax { get; set; }
        public decimal PunitConTax { get; set; }
        public decimal TaxPorTot { get; set; }
        public decimal TaxMtoTot { get; set; }
        public decimal TaxPor01 { get; set; }
        public decimal TaxPor02 { get; set; }
        public decimal TaxPor03 { get; set; }
        public decimal TaxPor04 { get; set; }
        public decimal TaxPor05 { get; set; }
        public decimal TaxPor06 { get; set; }
        public decimal TaxPor07 { get; set; }
        public decimal TaxPor08 { get; set; }
        public decimal TaxMto01 { get; set; }
        public decimal TaxMto02 { get; set; }
        public decimal TaxMto03 { get; set; }
        public decimal TaxMto04 { get; set; }
        public decimal TaxMto05 { get; set; }
        public decimal TaxMto06 { get; set; }
        public decimal TaxMto07 { get; set; }
        public decimal TaxMto08 { get; set; }
        public decimal MtoVtaSinTax { get; set; }
        public decimal MtoVtaConTax { get; set; }
        public string? TxtObserv { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public int? IdUm { get; set; }
        public int? IdRazon { get; set; }

        public virtual Tnst01CompRecibido IdCompRecibidoNavigation { get; set; } = null!;
        public virtual Prot18Productocom IdProductoNavigation { get; set; } = null!;
        public virtual Mstt05Razon? IdRazonNavigation { get; set; }
        public virtual Sntt06UnidadMedidum? IdUmNavigation { get; set; }
    }
}

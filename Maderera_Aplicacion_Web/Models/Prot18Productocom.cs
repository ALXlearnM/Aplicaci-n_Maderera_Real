using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot18Productocom
    {
        public Prot18Productocom()
        {
            Tnst02CompRecibidoDtls = new HashSet<Tnst02CompRecibidoDtl>();
        }

        public long IdProducto { get; set; }
        public string? CodProducto { get; set; }
        public string? CodProducto2 { get; set; }
        public string? CodBarra { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdUm { get; set; }
        public int? IdModelo { get; set; }
        public decimal? PorImpto { get; set; }
        public int? IdTipoMoneda { get; set; }
        public decimal? MtoPvpuConIgv { get; set; }
        public decimal? MtoPvmiConIgv { get; set; }
        public decimal? MtoPvmaConIgv { get; set; }
        public decimal? MtoPvpuSinIgv { get; set; }
        public decimal? MtoPvmiSinIgv { get; set; }
        public decimal? MtoPvmaSinIgv { get; set; }
        public decimal? CostoProd { get; set; }
        public string? PesoProd { get; set; }
        public string? LargoProd { get; set; }
        public string? AnchoProd { get; set; }
        public string? AlturaProd { get; set; }
        public string? DiametroProd { get; set; }
        public string? UrlImgProd { get; set; }
        public string? TxtReferencia { get; set; }
        public int? SnCombo { get; set; }
        public int? IdTipoExistencia { get; set; }
        public int? IdSubfamilia { get; set; }
        public int? IdTipoProd { get; set; }
        public int? IdClaseProd { get; set; }
        public long? IdReceta { get; set; }
        public int? SnExento { get; set; }
        public int? SnInafecto { get; set; }
        public int? SnVenta { get; set; }
        public int? SnCompra { get; set; }
        public int? SnReceta { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int? IdImpuesto { get; set; }
        public long? IdCombo { get; set; }
        public int? SnIncluyeImpto { get; set; }
        public int? IdFamilia { get; set; }

        public virtual Prot21Modelocom? IdModeloNavigation { get; set; }
        public virtual Sntt06UnidadMedidum? IdUmNavigation { get; set; }
        public virtual ICollection<Tnst02CompRecibidoDtl> Tnst02CompRecibidoDtls { get; set; }
    }
}

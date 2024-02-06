using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot09Producto
    {
        public Prot09Producto()
        {
            Pret14Produccions = new HashSet<Pret14Produccion>();
            Pret15ProduccionDtls = new HashSet<Pret15ProduccionDtl>();
            Pret17MermaDtls = new HashSet<Pret17MermaDtl>();
            Prot08PrecioProds = new HashSet<Prot08PrecioProd>();
            Prot11RecetaDtls = new HashSet<Prot11RecetaDtl>();
            Prot14ComboFixedDtls = new HashSet<Prot14ComboFixedDtl>();
            Prot16ComboVariableDtls = new HashSet<Prot16ComboVariableDtl>();
            Tnst05CompEmitidoDtls = new HashSet<Tnst05CompEmitidoDtl>();
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
        public int SnIncluyeImpto { get; set; }

        public virtual Prot06ClaseProd? IdClaseProdNavigation { get; set; }
        public virtual Prot13Combo? IdComboNavigation { get; set; }
        public virtual Mstt06Impuesto? IdImpuestoNavigation { get; set; }
        public virtual Prot02Modelo? IdModeloNavigation { get; set; }
        public virtual Prot10Recetum? IdRecetaNavigation { get; set; }
        public virtual Prot04Subfamilium? IdSubfamiliaNavigation { get; set; }
        public virtual Sntt05TipoExistencium? IdTipoExistenciaNavigation { get; set; }
        public virtual Sntt04TipoMonedum? IdTipoMonedaNavigation { get; set; }
        public virtual Prot07TipoProd? IdTipoProdNavigation { get; set; }
        public virtual Sntt06UnidadMedidum? IdUmNavigation { get; set; }
        public virtual ICollection<Pret14Produccion> Pret14Produccions { get; set; }
        public virtual ICollection<Pret15ProduccionDtl> Pret15ProduccionDtls { get; set; }
        public virtual ICollection<Pret17MermaDtl> Pret17MermaDtls { get; set; }
        public virtual ICollection<Prot08PrecioProd> Prot08PrecioProds { get; set; }
        public virtual ICollection<Prot11RecetaDtl> Prot11RecetaDtls { get; set; }
        public virtual ICollection<Prot14ComboFixedDtl> Prot14ComboFixedDtls { get; set; }
        public virtual ICollection<Prot16ComboVariableDtl> Prot16ComboVariableDtls { get; set; }
        public virtual ICollection<Tnst05CompEmitidoDtl> Tnst05CompEmitidoDtls { get; set; }
    }
}

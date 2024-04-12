using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tnst01CompRecibido
    {
        public Tnst01CompRecibido()
        {
            Tnst02CompRecibidoDtls = new HashSet<Tnst02CompRecibidoDtl>();
            Tnst03CompRecibidoEstados = new HashSet<Tnst03CompRecibidoEstado>();
        }

        public long IdCompRecibido { get; set; }
        public string NroCompRecibido { get; set; } = null!;
        public int IdTipoComp { get; set; }
        public string TxtSerie { get; set; } = null!;
        public string TxtNumero { get; set; } = null!;
        public DateTime FecRegistro { get; set; }
        public DateTime? FecRegRecibido { get; set; }
        public DateTime FecEmi { get; set; }
        public DateTime? FecVcto { get; set; }
        public DateTime? FecCanc { get; set; }
        public int IdTipoMoneda { get; set; }
        public int? IdTipoOrden { get; set; }
        public string? TxtObserv { get; set; }
        public decimal MtoTcVta { get; set; }
        public decimal MtoNeto { get; set; }
        public decimal MtoExonerado { get; set; }
        public decimal MtoNoAfecto { get; set; }
        public decimal MtoDsctoTot { get; set; }
        public decimal MtoCmsTot { get; set; }
        public decimal MtoFleteTot { get; set; }
        public decimal MtoSubTot { get; set; }
        public decimal MtoImptoTot { get; set; }
        public decimal MtoServicio { get; set; }
        public decimal MtoTotComp { get; set; }
        public long? RefIdCompRecibido { get; set; }
        public string? RefTipoComprobante { get; set; }
        public DateTime? RefFecha { get; set; }
        public string? RefSerie { get; set; }
        public string? RefNumero { get; set; }
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
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public string? Info05 { get; set; }
        public string? Info06 { get; set; }
        public string? Info07 { get; set; }
        public string? Info08 { get; set; }
        public string? Info09 { get; set; }
        public string? Info10 { get; set; }
        public DateTime? InfoDate01 { get; set; }
        public DateTime? InfoDate02 { get; set; }
        public DateTime? InfoDate03 { get; set; }
        public DateTime? InfoDate04 { get; set; }
        public DateTime? InfoDate05 { get; set; }
        public decimal? InfoMto01 { get; set; }
        public decimal? InfoMto02 { get; set; }
        public decimal? InfoMto03 { get; set; }
        public decimal? InfoMto04 { get; set; }
        public decimal? InfoMto05 { get; set; }
        public int? Post { get; set; }
        public DateTime? PostDate { get; set; }
        public bool SnCredito { get; set; }
        public bool SnCancelada { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdProveedor { get; set; }
        public long IdUsuario { get; set; }
        public int IdLocation { get; set; }
        public long? IdCliente { get; set; }
        public string? TipoCompra { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual Pert02Cliente? IdClienteNavigation { get; set; }
        public virtual Mstt08Location IdLocationNavigation { get; set; } = null!;
        public virtual Pert03Proveedor IdProveedorNavigation { get; set; } = null!;
        public virtual Sntt10TipoComprobante IdTipoCompNavigation { get; set; } = null!;
        public virtual Sntt04TipoMonedum IdTipoMonedaNavigation { get; set; } = null!;
        public virtual Mstt03TipoOrden? IdTipoOrdenNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Tnst02CompRecibidoDtl> Tnst02CompRecibidoDtls { get; set; }
        public virtual ICollection<Tnst03CompRecibidoEstado> Tnst03CompRecibidoEstados { get; set; }
    }
}

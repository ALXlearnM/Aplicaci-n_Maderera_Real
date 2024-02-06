using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tnst04CompEmitido
    {
        public Tnst04CompEmitido()
        {
            Pret23VentaEmpleados = new HashSet<Pret23VentaEmpleado>();
            Tnst05CompEmitidoDtls = new HashSet<Tnst05CompEmitidoDtl>();
            Tnst06CompEmitidoEstados = new HashSet<Tnst06CompEmitidoEstado>();
            Tnst07MedioPagoDtls = new HashSet<Tnst07MedioPagoDtl>();
            Tnst08DescuentoDtls = new HashSet<Tnst08DescuentoDtl>();
        }

        public long IdCompEmitido { get; set; }
        public string NroCompEmitido { get; set; } = null!;
        public long? NroCheque { get; set; }
        public int IdTipoComp { get; set; }
        public long IdCliente { get; set; }
        public string? CodCaja { get; set; }
        public string TxtSerie { get; set; } = null!;
        public string TxtNumero { get; set; } = null!;
        public string? TxtSerieFe { get; set; }
        public string? TxtNumeroFe { get; set; }
        public DateTime FecNegocio { get; set; }
        public DateTime? FecRegEmitido { get; set; }
        public DateTime FecRegistro { get; set; }
        public DateTime FecEmi { get; set; }
        public DateTime? FecVcto { get; set; }
        public DateTime? FecCanc { get; set; }
        public int? IdTipoMoneda { get; set; }
        public int? IdCanVta { get; set; }
        public int? IdTipoOrden { get; set; }
        public int IdLocation { get; set; }
        public string? TxtObserv { get; set; }
        public decimal MtoTcVta { get; set; }
        public decimal MtoNeto { get; set; }
        public decimal MtoExonerado { get; set; }
        public decimal MtoNoAfecto { get; set; }
        public decimal MtoDsctoTot { get; set; }
        public decimal MtoServicio { get; set; }
        public decimal MtoSubTot { get; set; }
        public decimal MtoImptoTot { get; set; }
        public decimal MtoTotComp { get; set; }
        public long? RefIdCompEmitido { get; set; }
        public string? RefTipoComprobante { get; set; }
        public DateTime? RefFecha { get; set; }
        public string? RefSerie { get; set; }
        public string? RefNumero { get; set; }
        public bool SnChkAbierto { get; set; }
        public bool SnChkEnviado { get; set; }
        public decimal? TaxPor01 { get; set; }
        public decimal? TaxPor02 { get; set; }
        public decimal? TaxPor03 { get; set; }
        public decimal? TaxPor04 { get; set; }
        public decimal? TaxPor05 { get; set; }
        public decimal? TaxPor06 { get; set; }
        public decimal? TaxPor07 { get; set; }
        public decimal? TaxPor08 { get; set; }
        public decimal? TaxMto01 { get; set; }
        public decimal? TaxMto02 { get; set; }
        public decimal? TaxMto03 { get; set; }
        public decimal? TaxMto04 { get; set; }
        public decimal? TaxMto05 { get; set; }
        public decimal? TaxMto06 { get; set; }
        public decimal? TaxMto07 { get; set; }
        public decimal? TaxMto08 { get; set; }
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
        public int? NumComensales { get; set; }
        public long IdUsuario { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public long? IdUsuarioModificador { get; set; }
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public int? IdMesa { get; set; }
        public int? IdTurno { get; set; }
        public int IdLocationTo { get; set; }
        public long? IdPredio { get; set; }
        public long? IdCampana { get; set; }

        public virtual Pret02Campana? IdCampanaNavigation { get; set; }
        public virtual Mstt04CanalVtum? IdCanVtaNavigation { get; set; }
        public virtual Pert02Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Mstt08Location IdLocationNavigation { get; set; } = null!;
        public virtual Mstt08Location IdLocationToNavigation { get; set; } = null!;
        public virtual Mstt14Mesa? IdMesaNavigation { get; set; }
        public virtual Pret01Predio? IdPredioNavigation { get; set; }
        public virtual Sntt10TipoComprobante IdTipoCompNavigation { get; set; } = null!;
        public virtual Sntt04TipoMonedum? IdTipoMonedaNavigation { get; set; }
        public virtual Mstt03TipoOrden? IdTipoOrdenNavigation { get; set; }
        public virtual Mstt13Turno? IdTurnoNavigation { get; set; }
        public virtual Pert01Usuario? IdUsuarioModificadorNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Pret23VentaEmpleado> Pret23VentaEmpleados { get; set; }
        public virtual ICollection<Tnst05CompEmitidoDtl> Tnst05CompEmitidoDtls { get; set; }
        public virtual ICollection<Tnst06CompEmitidoEstado> Tnst06CompEmitidoEstados { get; set; }
        public virtual ICollection<Tnst07MedioPagoDtl> Tnst07MedioPagoDtls { get; set; }
        public virtual ICollection<Tnst08DescuentoDtl> Tnst08DescuentoDtls { get; set; }
    }
}

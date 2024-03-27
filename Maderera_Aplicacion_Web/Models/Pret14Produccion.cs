using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret14Produccion
    {
        public Pret14Produccion()
        {
            Pret15ProduccionDtls = new HashSet<Pret15ProduccionDtl>();
            Pret16Mermas = new HashSet<Pret16Merma>();
            Pret20ProduccionEmpleados = new HashSet<Pret20ProduccionEmpleado>();
        }

        public long IdProduccion { get; set; }
        public long? IdExtraccion { get; set; }
        public long IdPredio { get; set; }
        public long IdCampana { get; set; }
        public long IdProductoIns { get; set; }
        public int CantidadIns { get; set; }
        public int IdUmIns { get; set; }
        public string? TxtProIns { get; set; }
        public DateTime FechaProduccion { get; set; }
        public string TipoPro { get; set; } = null!;
        public string? ComentarioPro { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NroPro { get; set; } = null!;
        public int? Post { get; set; }
        public DateTime? PostDate { get; set; }

        public virtual Pret02Campana IdCampanaNavigation { get; set; } = null!;
        public virtual Pret07Extraccion? IdExtraccionNavigation { get; set; }
        public virtual Pret01Predio IdPredioNavigation { get; set; } = null!;
        public virtual Prot09Producto IdProductoInsNavigation { get; set; } = null!;
        public virtual Sntt06UnidadMedidum IdUmInsNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuarioModificadorNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Pret15ProduccionDtl> Pret15ProduccionDtls { get; set; }
        public virtual ICollection<Pret16Merma> Pret16Mermas { get; set; }
        public virtual ICollection<Pret20ProduccionEmpleado> Pret20ProduccionEmpleados { get; set; }
    }
}

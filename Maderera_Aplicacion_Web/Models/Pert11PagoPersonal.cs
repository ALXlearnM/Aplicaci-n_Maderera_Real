using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert11PagoPersonal
    {
        public long IdPagoPersonal { get; set; }
        public long IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public string Mes { get; set; } = null!;
        public long IdAutorizador { get; set; }
        public long IdPredio { get; set; }
        public string Estado { get; set; } = null!;
        public long IdConcepto { get; set; }
        public long IdTipopago { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long IdCampana { get; set; }
        public string Anho { get; set; } = null!;
        public decimal MontoTotal { get; set; }
        public decimal? MontoPrestamo { get; set; }
        public decimal? MontoDeposito { get; set; }

        public virtual Pert01Usuario IdAutorizadorNavigation { get; set; } = null!;
        public virtual Pret02Campana IdCampanaNavigation { get; set; } = null!;
        public virtual Pert10Concepto IdConceptoNavigation { get; set; } = null!;
        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual Pret01Predio IdPredioNavigation { get; set; } = null!;
        public virtual Pret36TipoPago IdTipopagoNavigation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret33Prestamo
    {
        public Pret33Prestamo()
        {
            Pret34CronogramaPagos = new HashSet<Pret34CronogramaPago>();
        }

        public long IdPrestamo { get; set; }
        public long? IdEmpleado { get; set; }
        public long? IdAutorizador { get; set; }
        public decimal MontoTotal { get; set; }
        public long IdTipoPrestamo { get; set; }
        public string? TxtObservacion { get; set; }
        public DateTime FechaAprobPrestamo { get; set; }
        public int Plazo { get; set; }
        public DateTime FechaVtoProg { get; set; }
        public int NroCuotasGracia { get; set; }
        public long IdTipoPlazo { get; set; }
        public bool? Posteo { get; set; }
        public long? IdMotivo { get; set; }
        public bool? CuotaDoble { get; set; }
        public decimal MontoTea { get; set; }
        public decimal MontoTcea { get; set; }
        public DateTime? FechaDesembolso { get; set; }
        public DateTime? FechaPrimPago { get; set; }
        public decimal? Comisiones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public long? IdUsuariomod { get; set; }
        public string? TxtUsuariomod { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Pert04Empleado? IdAutorizadorNavigation { get; set; }
        public virtual Pert04Empleado? IdEmpleadoNavigation { get; set; }
        public virtual Pret32Motivo? IdMotivoNavigation { get; set; }
        public virtual Pret31TipoPlazo IdTipoPlazoNavigation { get; set; } = null!;
        public virtual Pret30TipoPrestamo IdTipoPrestamoNavigation { get; set; } = null!;
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuariomodNavigation { get; set; }
        public virtual ICollection<Pret34CronogramaPago> Pret34CronogramaPagos { get; set; }
    }
}

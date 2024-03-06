using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret34CronogramaPago
    {
        public Pret34CronogramaPago()
        {
            Pret35PagoPrestamos = new HashSet<Pret35PagoPrestamo>();
        }

        public long IdCronograma { get; set; }
        public long? IdPrestamo { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Amortizacion { get; set; }
        public decimal Interes { get; set; }
        public decimal Cuota { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public long? IdUsuariomod { get; set; }
        public string? TxtUsuariomod { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Pret33Prestamo? IdPrestamoNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuariomodNavigation { get; set; }
        public virtual ICollection<Pret35PagoPrestamo> Pret35PagoPrestamos { get; set; }
    }
}

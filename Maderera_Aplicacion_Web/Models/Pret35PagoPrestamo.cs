using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret35PagoPrestamo
    {
        public long IdPagoPrestamo { get; set; }
        public long IdCronograma { get; set; }
        public DateTime FechaPagoReal { get; set; }
        public decimal Cuota { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public long? IdUsuariomod { get; set; }
        public string? TxtUsuariomod { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Pret34CronogramaPago IdCronogramaNavigation { get; set; } = null!;
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuariomodNavigation { get; set; }
    }
}

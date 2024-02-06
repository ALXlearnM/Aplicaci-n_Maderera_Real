using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert11PagoPersonal
    {
        public long IdPagoPersonal { get; set; }
        public long IdEmpleado { get; set; }
        public string Tipo { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Mes { get; set; } = null!;
        public long IdAutorizador { get; set; }
        public long IdPredio { get; set; }
        public long IdCampaña { get; set; }
        public string Estado { get; set; } = null!;
        public long IdConcepto { get; set; }
        public decimal Monto { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Pert01Usuario IdAutorizadorNavigation { get; set; } = null!;
        public virtual Pret02Campana IdCampañaNavigation { get; set; } = null!;
        public virtual Pert10Concepto IdConceptoNavigation { get; set; } = null!;
        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual Pret01Predio IdPredioNavigation { get; set; } = null!;
    }
}

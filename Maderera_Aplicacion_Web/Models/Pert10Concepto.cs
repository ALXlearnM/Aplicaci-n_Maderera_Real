using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert10Concepto
    {
        public Pert10Concepto()
        {
            Pert11PagoPersonals = new HashSet<Pert11PagoPersonal>();
        }

        public long IdConcepto { get; set; }
        public string TxtDesc { get; set; } = null!;

        public virtual ICollection<Pert11PagoPersonal> Pert11PagoPersonals { get; set; }
    }
}

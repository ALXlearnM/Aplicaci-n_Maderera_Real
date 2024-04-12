using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret36TipoPago
    {
        public Pret36TipoPago()
        {
            Pert11PagoPersonals = new HashSet<Pert11PagoPersonal>();
        }

        public long IdTipopago { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Pert11PagoPersonal> Pert11PagoPersonals { get; set; }
    }
}

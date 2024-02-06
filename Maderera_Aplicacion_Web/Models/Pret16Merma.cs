using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret16Merma
    {
        public Pret16Merma()
        {
            Pret17MermaDtls = new HashSet<Pret17MermaDtl>();
        }

        public int IdMerma { get; set; }
        public long? IdProduccion { get; set; }
        public long IdExtraccion { get; set; }
        public DateTime? FechaMerma { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Pret07Extraccion IdExtraccionNavigation { get; set; } = null!;
        public virtual Pret14Produccion? IdProduccionNavigation { get; set; }
        public virtual ICollection<Pret17MermaDtl> Pret17MermaDtls { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret16Merma
    {
        public Pret16Merma()
        {
            Pret17MermaDtls = new HashSet<Pret17MermaDtl>();
            Pret24MermaEmpleados = new HashSet<Pret24MermaEmpleado>();
        }

        public long IdMerma { get; set; }
        public long? IdProduccion { get; set; }
        public long IdPredio { get; set; }
        public long IdCampana { get; set; }
        public string? TipoMerma { get; set; }
        public DateTime? FechaMerma { get; set; }
        public string? Comentario { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Pret02Campana IdCampanaNavigation { get; set; } = null!;
        public virtual Pret01Predio IdPredioNavigation { get; set; } = null!;
        public virtual Pret14Produccion? IdProduccionNavigation { get; set; }
        public virtual ICollection<Pret17MermaDtl> Pret17MermaDtls { get; set; }
        public virtual ICollection<Pret24MermaEmpleado> Pret24MermaEmpleados { get; set; }
    }
}

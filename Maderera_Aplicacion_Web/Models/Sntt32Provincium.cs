using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt32Provincium
    {
        public Sntt32Provincium()
        {
            Sntt33Distritos = new HashSet<Sntt33Distrito>();
        }

        public int IdProv { get; set; }
        public string CodProv { get; set; } = null!;
        public string? CodProvPle { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdDpto { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Sntt31Departamento IdDptoNavigation { get; set; } = null!;
        public virtual ICollection<Sntt33Distrito> Sntt33Distritos { get; set; }
    }
}

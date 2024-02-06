using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt31Departamento
    {
        public Sntt31Departamento()
        {
            Sntt32Provincia = new HashSet<Sntt32Provincium>();
        }

        public int IdDpto { get; set; }
        public string CodDpto { get; set; } = null!;
        public string? CodDptoPle { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Sntt32Provincium> Sntt32Provincia { get; set; }
    }
}

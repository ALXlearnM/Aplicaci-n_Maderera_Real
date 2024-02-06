using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert07AccessItem
    {
        public Pert07AccessItem()
        {
            Pert08SecurityAccesses = new HashSet<Pert08SecurityAccess>();
        }

        public int IdAccessItem { get; set; }
        public string TxtDesc { get; set; } = null!;
        public string AppCode { get; set; } = null!;

        public virtual ICollection<Pert08SecurityAccess> Pert08SecurityAccesses { get; set; }
    }
}

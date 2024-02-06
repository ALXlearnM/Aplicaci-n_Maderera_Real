using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert08SecurityAccess
    {
        public int IdSecurityAccess { get; set; }
        public int SnNone { get; set; }
        public int SnFull { get; set; }
        public int SnAdd { get; set; }
        public int SnUpd { get; set; }
        public int SnDel { get; set; }
        public int SnRead { get; set; }
        public int IdAccessItem { get; set; }
        public int IdClaseEmp { get; set; }

        public virtual Pert07AccessItem IdAccessItemNavigation { get; set; } = null!;
        public virtual Pert06ClaseEmp IdClaseEmpNavigation { get; set; } = null!;
    }
}

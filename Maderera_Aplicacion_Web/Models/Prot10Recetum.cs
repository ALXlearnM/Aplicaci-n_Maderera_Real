using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot10Recetum
    {
        public Prot10Recetum()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
            Prot11RecetaDtls = new HashSet<Prot11RecetaDtl>();
        }

        public long IdReceta { get; set; }
        public string? CodReceta { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public long? IdRecetaGrupo { get; set; }

        public virtual Prot12RecetaGrupo? IdRecetaGrupoNavigation { get; set; }
        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
        public virtual ICollection<Prot11RecetaDtl> Prot11RecetaDtls { get; set; }
    }
}

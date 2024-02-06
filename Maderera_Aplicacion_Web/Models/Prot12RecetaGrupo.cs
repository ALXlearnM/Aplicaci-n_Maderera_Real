using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot12RecetaGrupo
    {
        public Prot12RecetaGrupo()
        {
            Prot10Receta = new HashSet<Prot10Recetum>();
        }

        public long IdRecetaGrupo { get; set; }
        public string? CodRecetaGrupo { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Prot10Recetum> Prot10Receta { get; set; }
    }
}

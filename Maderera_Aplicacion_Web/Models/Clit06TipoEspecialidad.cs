using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit06TipoEspecialidad
    {
        public Clit06TipoEspecialidad()
        {
            Clit07EspecialidadMedicas = new HashSet<Clit07EspecialidadMedica>();
        }

        public int IdTipoEspecialidad { get; set; }
        public string? CodTipoEspecialidad { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Clit07EspecialidadMedica> Clit07EspecialidadMedicas { get; set; }
    }
}

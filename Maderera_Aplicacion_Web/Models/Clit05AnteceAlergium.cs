using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit05AnteceAlergium
    {
        public long IdAnteceAlergia { get; set; }
        public string? CodAnteceAlergia { get; set; }
        public long? IdPaciente { get; set; }
        public string? TxtAntecedentes { get; set; }
        public string? TxtAlergias { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Clit01Paciente? IdPacienteNavigation { get; set; }
    }
}

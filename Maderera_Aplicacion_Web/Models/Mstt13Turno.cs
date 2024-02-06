using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt13Turno
    {
        public Mstt13Turno()
        {
            Csht01CajaDtls = new HashSet<Csht01CajaDtl>();
            Labt04HorarioEmpDtls = new HashSet<Labt04HorarioEmpDtl>();
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public int IdTurno { get; set; }
        public string? CodTurno { get; set; }
        public string? TxtAbrv { get; set; }
        public string TxtDesc { get; set; } = null!;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Csht01CajaDtl> Csht01CajaDtls { get; set; }
        public virtual ICollection<Labt04HorarioEmpDtl> Labt04HorarioEmpDtls { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}

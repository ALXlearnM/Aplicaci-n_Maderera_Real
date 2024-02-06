using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Labt04HorarioEmpDtl
    {
        public long IdHorarioEmpDtl { get; set; }
        public DateTime FechaLabor { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public TimeSpan? HoraInicioBreak { get; set; }
        public TimeSpan? HoraFinBreak { get; set; }
        public TimeSpan TiempoTolerancia { get; set; }
        public long IdHorarioEmp { get; set; }
        public int? IdTurno { get; set; }

        public virtual Labt03HorarioEmp IdHorarioEmpNavigation { get; set; } = null!;
        public virtual Mstt13Turno? IdTurnoNavigation { get; set; }
    }
}

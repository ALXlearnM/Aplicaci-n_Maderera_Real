using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Labt02AsistenciaAjustadum
    {
        public long IdAsistenciaAjustada { get; set; }
        public DateTime FechaCronologica { get; set; }
        public DateTime FechaCronologicaAjust { get; set; }
        public DateTime FechaNegocio { get; set; }
        public DateTime FechaNegocioAjust { get; set; }
        public string? TxtObsv { get; set; }
        public string? TxtPhotoPathClockInAjust { get; set; }
        public string? TxtPhotoPathClockOutAjust { get; set; }
        public int SnClockIn { get; set; }
        public int SnBreakIn { get; set; }
        public DateTime? ClockIn { get; set; }
        public DateTime? ClockInAjust { get; set; }
        public string? ClockInStatus { get; set; }
        public string? ClockInStatusAjust { get; set; }
        public DateTime? BreakIn { get; set; }
        public DateTime? BreakInAjust { get; set; }
        public string? BreakInStatus { get; set; }
        public string? BreakInStatusAjust { get; set; }
        public DateTime? BreakOut { get; set; }
        public DateTime? BreakOutAjust { get; set; }
        public string? BreakOutStatus { get; set; }
        public string? BreakOutStatusAjust { get; set; }
        public DateTime? ClockOut { get; set; }
        public DateTime? ClockOutAjust { get; set; }
        public string? ClockOutStatus { get; set; }
        public string? ClockOutStatusAjust { get; set; }
        public long IdAsistencia { get; set; }
        public int IdRazon { get; set; }
        public long IdEmpAutorizador { get; set; }

        public virtual Labt01Asistencium IdAsistenciaNavigation { get; set; } = null!;
        public virtual Pert04Empleado IdEmpAutorizadorNavigation { get; set; } = null!;
        public virtual Mstt05Razon IdRazonNavigation { get; set; } = null!;
    }
}

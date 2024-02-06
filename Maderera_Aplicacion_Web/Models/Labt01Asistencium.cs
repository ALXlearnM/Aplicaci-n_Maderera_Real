using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Labt01Asistencium
    {
        public Labt01Asistencium()
        {
            Labt02AsistenciaAjustada = new HashSet<Labt02AsistenciaAjustadum>();
        }

        public long IdAsistencia { get; set; }
        public DateTime FechaCronologica { get; set; }
        public DateTime FechaNegocio { get; set; }
        public DateTime FechaGmt { get; set; }
        public string? TxtObsv { get; set; }
        public string? TxtPhotoPathClockIn { get; set; }
        public string? TxtPhotoPathClockOut { get; set; }
        public int SnClockIn { get; set; }
        public int SnBreakIn { get; set; }
        public DateTime ClockIn { get; set; }
        public string? ClockInStatus { get; set; }
        public DateTime? BreakIn { get; set; }
        public string? BreakInStatus { get; set; }
        public DateTime? BreakOut { get; set; }
        public string? BreakOutStatus { get; set; }
        public DateTime? ClockOut { get; set; }
        public string? ClockOutStatus { get; set; }
        public int? IdLocation { get; set; }
        public int? IdCanVta { get; set; }
        public long? IdEmpAutorizador { get; set; }
        public int? IdRazon { get; set; }
        public long IdEmpTrabajo { get; set; }

        public virtual Mstt04CanalVtum? IdCanVtaNavigation { get; set; }
        public virtual Pert04Empleado? IdEmpAutorizadorNavigation { get; set; }
        public virtual Labt07EmpTrabajo IdEmpTrabajoNavigation { get; set; } = null!;
        public virtual Mstt08Location? IdLocationNavigation { get; set; }
        public virtual Mstt05Razon? IdRazonNavigation { get; set; }
        public virtual ICollection<Labt02AsistenciaAjustadum> Labt02AsistenciaAjustada { get; set; }
    }
}

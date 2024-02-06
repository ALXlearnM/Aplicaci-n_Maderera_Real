using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit04AtencionDtl
    {
        public long IdAtencionDtl { get; set; }
        public string? CodAtencionDtl { get; set; }
        public long? IdAtencion { get; set; }
        public DateTime? FecNegocio { get; set; }
        public DateTime? FecRegistro { get; set; }
        public long? IdActividad { get; set; }
        public string? TxtObservacion { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public long IdEmpleado { get; set; }

        public virtual Clit02Actividad? IdActividadNavigation { get; set; }
        public virtual Clit03Atencion? IdAtencionNavigation { get; set; }
        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
    }
}

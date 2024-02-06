using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit19Citum
    {
        public Clit19Citum()
        {
            Clit03Atencions = new HashSet<Clit03Atencion>();
        }

        public long IdCita { get; set; }
        public string? CodCita { get; set; }
        public DateTime? FecCita { get; set; }
        public DateTime? FecRegistro { get; set; }
        public string? Hora { get; set; }
        public string? Duracion { get; set; }
        public string? TxtObs { get; set; }
        public int? SnAsistencia { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public long IdEmpleado { get; set; }
        public long IdPaciente { get; set; }

        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual Clit01Paciente IdPacienteNavigation { get; set; } = null!;
        public virtual ICollection<Clit03Atencion> Clit03Atencions { get; set; }
    }
}

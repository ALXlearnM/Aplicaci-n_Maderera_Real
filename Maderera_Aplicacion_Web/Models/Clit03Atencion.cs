using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit03Atencion
    {
        public Clit03Atencion()
        {
            Clit04AtencionDtls = new HashSet<Clit04AtencionDtl>();
            Clit08FuncionesVitales = new HashSet<Clit08FuncionesVitale>();
            Clit09Anamnesis = new HashSet<Clit09Anamnesi>();
            Clit10ExamenFisicos = new HashSet<Clit10ExamenFisico>();
            Clit11EstudioComplementarios = new HashSet<Clit11EstudioComplementario>();
            Clit13Laboratorios = new HashSet<Clit13Laboratorio>();
            Clit15Diagnosticos = new HashSet<Clit15Diagnostico>();
            Clit17Tratamientos = new HashSet<Clit17Tratamiento>();
            Clit18Evolucions = new HashSet<Clit18Evolucion>();
        }

        public long IdAtencion { get; set; }
        public string? CodAtencion { get; set; }
        public DateTime? FecNegocio { get; set; }
        public DateTime? FecRegistro { get; set; }
        public long? IdActividad { get; set; }
        public long? IdCita { get; set; }
        public long IdPaciente { get; set; }
        public long IdEmpleado { get; set; }
        public long IdUsuario { get; set; }
        public string? TxtUsuario { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Clit02Actividad? IdActividadNavigation { get; set; }
        public virtual Clit19Citum? IdCitaNavigation { get; set; }
        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual Clit01Paciente IdPacienteNavigation { get; set; } = null!;
        public virtual ICollection<Clit04AtencionDtl> Clit04AtencionDtls { get; set; }
        public virtual ICollection<Clit08FuncionesVitale> Clit08FuncionesVitales { get; set; }
        public virtual ICollection<Clit09Anamnesi> Clit09Anamnesis { get; set; }
        public virtual ICollection<Clit10ExamenFisico> Clit10ExamenFisicos { get; set; }
        public virtual ICollection<Clit11EstudioComplementario> Clit11EstudioComplementarios { get; set; }
        public virtual ICollection<Clit13Laboratorio> Clit13Laboratorios { get; set; }
        public virtual ICollection<Clit15Diagnostico> Clit15Diagnosticos { get; set; }
        public virtual ICollection<Clit17Tratamiento> Clit17Tratamientos { get; set; }
        public virtual ICollection<Clit18Evolucion> Clit18Evolucions { get; set; }
    }
}

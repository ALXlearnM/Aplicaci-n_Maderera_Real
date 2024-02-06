using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret21EnvioEmpleado
    {
        public int IdEnvemp { get; set; }
        public long IdEnvio { get; set; }
        public long? IdEmpleadoEnv { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public decimal? SalarioEmp { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Pert04Empleado? IdEmpleadoEnvNavigation { get; set; }
        public virtual Pret10Envio IdEnvioNavigation { get; set; } = null!;
    }
}

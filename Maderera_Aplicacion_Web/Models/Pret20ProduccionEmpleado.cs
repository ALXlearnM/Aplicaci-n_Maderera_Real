using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret20ProduccionEmpleado
    {
        public long IdEmppro { get; set; }
        public long IdProduccion { get; set; }
        public decimal? Salario { get; set; }
        public long? IdEmpleadoPro { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Pert04Empleado? IdEmpleadoProNavigation { get; set; }
        public virtual Pret14Produccion IdProduccionNavigation { get; set; } = null!;
    }
}

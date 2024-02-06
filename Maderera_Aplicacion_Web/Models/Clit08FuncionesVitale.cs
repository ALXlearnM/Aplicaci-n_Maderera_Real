using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit08FuncionesVitale
    {
        public long IdFuncionesVitales { get; set; }
        public string? CodFuncionesVitales { get; set; }
        public DateTime? FecNegocio { get; set; }
        public DateTime? FecRegistro { get; set; }
        public string? Sistoles { get; set; }
        public string? Diastoles { get; set; }
        public string? Pulsaciones { get; set; }
        public string? RitmoRespiratorio { get; set; }
        public string? Temperatura { get; set; }
        public string? Altura { get; set; }
        public string? Peso { get; set; }
        public string? Imc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public long IdAtencion { get; set; }

        public virtual Clit03Atencion IdAtencionNavigation { get; set; } = null!;
    }
}

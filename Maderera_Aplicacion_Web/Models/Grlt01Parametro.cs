using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Grlt01Parametro
    {
        public int IdParametro { get; set; }
        public string? CodParametro { get; set; }
        public string? TxtDesc { get; set; }
        public decimal? DecValor { get; set; }
        public string? TxtValor { get; set; }
        public string? TxtObs { get; set; }
        public int SnEdit { get; set; }
    }
}

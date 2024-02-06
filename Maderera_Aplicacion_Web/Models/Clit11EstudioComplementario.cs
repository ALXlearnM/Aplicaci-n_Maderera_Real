﻿using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit11EstudioComplementario
    {
        public Clit11EstudioComplementario()
        {
            Clit12ArchivoComplementarios = new HashSet<Clit12ArchivoComplementario>();
        }

        public long IdEstudioComplementario { get; set; }
        public string? CodEstudioComplementario { get; set; }
        public string? TxtDesc { get; set; }
        public DateTime? FecNegocio { get; set; }
        public DateTime? FecRegistro { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public long? IdAtencion { get; set; }

        public virtual Clit03Atencion? IdAtencionNavigation { get; set; }
        public virtual ICollection<Clit12ArchivoComplementario> Clit12ArchivoComplementarios { get; set; }
    }
}

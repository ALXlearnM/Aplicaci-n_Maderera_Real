﻿using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt18SituacionEducativa
    {
        public Sntt18SituacionEducativa()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdSituacionEducativa { get; set; }
        public string? CodSituacionEducativa { get; set; }
        public string? CodSituacionEducativaPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

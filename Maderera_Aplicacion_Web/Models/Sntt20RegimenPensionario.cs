﻿using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt20RegimenPensionario
    {
        public Sntt20RegimenPensionario()
        {
            Pert04Empleados = new HashSet<Pert04Empleado>();
        }

        public int IdRegimenPensionario { get; set; }
        public string? CodRegimenPensionario { get; set; }
        public string? CodRegimenPensionarioPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pert04Empleado> Pert04Empleados { get; set; }
    }
}

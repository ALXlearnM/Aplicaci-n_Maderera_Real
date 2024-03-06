﻿using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret32Motivo
    {
        public Pret32Motivo()
        {
            Pret33Prestamos = new HashSet<Pret33Prestamo>();
        }

        public long IdMotivo { get; set; }
        public string TxtMotivo { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public long IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public long? IdUsuariomod { get; set; }
        public string? TxtUsuariomod { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuariomodNavigation { get; set; }
        public virtual ICollection<Pret33Prestamo> Pret33Prestamos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret35TipoRazonAnulacion
    {
        public Pret35TipoRazonAnulacion()
        {
            Pret33Prestamos = new HashSet<Pret33Prestamo>();
        }

        public int IdTiporazonAnulacion { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Pret33Prestamo> Pret33Prestamos { get; set; }
    }
}

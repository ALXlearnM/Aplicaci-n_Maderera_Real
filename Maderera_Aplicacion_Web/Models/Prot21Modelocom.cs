using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot21Modelocom
    {
        public Prot21Modelocom()
        {
            Prot18Productocoms = new HashSet<Prot18Productocom>();
        }

        public int IdModelo { get; set; }
        public string? CodModelo { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int IdMarca { get; set; }

        public virtual Prot20Marcacom IdMarcaNavigation { get; set; } = null!;
        public virtual ICollection<Prot18Productocom> Prot18Productocoms { get; set; }
    }
}

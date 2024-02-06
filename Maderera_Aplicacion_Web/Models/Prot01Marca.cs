using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot01Marca
    {
        public Prot01Marca()
        {
            Prot02Modelos = new HashSet<Prot02Modelo>();
        }

        public int IdMarca { get; set; }
        public string? CodMarca { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Prot02Modelo> Prot02Modelos { get; set; }
    }
}

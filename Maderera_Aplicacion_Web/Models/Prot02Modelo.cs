using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot02Modelo
    {
        public Prot02Modelo()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
        }

        public int IdModelo { get; set; }
        public string? CodModelo { get; set; }
        public string TxtDesc { get; set; } = null!;
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int IdMarca { get; set; }

        public virtual Prot01Marca IdMarcaNavigation { get; set; } = null!;
        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
    }
}

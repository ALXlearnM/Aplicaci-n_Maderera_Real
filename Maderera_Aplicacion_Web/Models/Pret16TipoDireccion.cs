using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret16TipoDireccion
    {
        public Pret16TipoDireccion()
        {
            Pret17Archivos = new HashSet<Pret17Archivo>();
        }

        public long IdTipo { get; set; }
        public string TxtTipo { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Pret17Archivo> Pret17Archivos { get; set; }
    }
}

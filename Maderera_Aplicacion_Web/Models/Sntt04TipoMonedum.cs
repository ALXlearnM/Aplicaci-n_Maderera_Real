using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt04TipoMonedum
    {
        public Sntt04TipoMonedum()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
            Tnst01CompRecibidos = new HashSet<Tnst01CompRecibido>();
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public int IdTipoMoneda { get; set; }
        public string? CodTipoMoneda { get; set; }
        public string? CodTipoMonedaPle { get; set; }
        public string? TxtAbrv { get; set; }
        public decimal? DecCambio { get; set; }
        public string? TxtDesc { get; set; }
        public string? TxtPais { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
        public virtual ICollection<Tnst01CompRecibido> Tnst01CompRecibidos { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}

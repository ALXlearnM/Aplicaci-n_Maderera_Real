using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt06Impuesto
    {
        public Mstt06Impuesto()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
            Prot13Combos = new HashSet<Prot13Combo>();
            Prot15ComboVariables = new HashSet<Prot15ComboVariable>();
        }

        public int IdImpuesto { get; set; }
        public string? CodImpuesto { get; set; }
        public string TxtDesc { get; set; } = null!;
        public string? TxtAbrv { get; set; }
        public decimal? PorImpto01 { get; set; }
        public decimal? PorImpto02 { get; set; }
        public decimal? PorImpto03 { get; set; }
        public decimal? PorImpto04 { get; set; }
        public decimal? PorImpto05 { get; set; }
        public decimal? PorImpto06 { get; set; }
        public decimal? PorImpto07 { get; set; }
        public decimal? PorImpto08 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
        public virtual ICollection<Prot13Combo> Prot13Combos { get; set; }
        public virtual ICollection<Prot15ComboVariable> Prot15ComboVariables { get; set; }
    }
}

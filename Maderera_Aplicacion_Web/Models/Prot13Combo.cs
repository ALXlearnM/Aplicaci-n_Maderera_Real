using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot13Combo
    {
        public Prot13Combo()
        {
            Prot09Productos = new HashSet<Prot09Producto>();
            Prot14ComboFixedDtls = new HashSet<Prot14ComboFixedDtl>();
        }

        public long IdCombo { get; set; }
        public string? CodCombo { get; set; }
        public string TxtDesc { get; set; } = null!;
        public decimal MtoPvpuSinTax { get; set; }
        public decimal MtoPvpuConTax { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public int IdComboGrupo { get; set; }
        public int? IdImpuesto { get; set; }
        public int SnIncluyeImpto { get; set; }
        public int SnPrecioAcumulado { get; set; }

        public virtual Prot17ComboGrupo IdComboGrupoNavigation { get; set; } = null!;
        public virtual Mstt06Impuesto? IdImpuestoNavigation { get; set; }
        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
        public virtual ICollection<Prot14ComboFixedDtl> Prot14ComboFixedDtls { get; set; }
    }
}

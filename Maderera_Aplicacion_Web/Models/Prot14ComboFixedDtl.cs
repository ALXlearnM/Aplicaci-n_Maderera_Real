using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot14ComboFixedDtl
    {
        public long IdComboFixedDtl { get; set; }
        public string? CodComboFixedDtl { get; set; }
        public decimal Cantidad { get; set; }
        public decimal MtoPvpuSinTax { get; set; }
        public decimal MtoPvpuConTax { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long? IdProducto { get; set; }
        public long IdCombo { get; set; }
        public long? IdComboVariable { get; set; }

        public virtual Prot13Combo IdComboNavigation { get; set; } = null!;
        public virtual Prot15ComboVariable? IdComboVariableNavigation { get; set; }
        public virtual Prot09Producto? IdProductoNavigation { get; set; }
    }
}

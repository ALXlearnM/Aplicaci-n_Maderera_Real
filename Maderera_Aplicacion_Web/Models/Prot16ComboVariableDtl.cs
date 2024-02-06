using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot16ComboVariableDtl
    {
        public long IdComboVariableDtl { get; set; }
        public string? CodComboVariableDtl { get; set; }
        public decimal Cantidad { get; set; }
        public decimal MtoPvpuSinTax { get; set; }
        public decimal MtoPvpuConTax { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdComboVariable { get; set; }
        public long IdProducto { get; set; }

        public virtual Prot15ComboVariable IdComboVariableNavigation { get; set; } = null!;
        public virtual Prot09Producto IdProductoNavigation { get; set; } = null!;
    }
}

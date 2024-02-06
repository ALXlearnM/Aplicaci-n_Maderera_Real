using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot15ComboVariable
    {
        public Prot15ComboVariable()
        {
            Prot14ComboFixedDtls = new HashSet<Prot14ComboFixedDtl>();
            Prot16ComboVariableDtls = new HashSet<Prot16ComboVariableDtl>();
        }

        public long IdComboVariable { get; set; }
        public string? CodComboVariable { get; set; }
        public string TxtDesc { get; set; } = null!;
        public decimal MtoPvpuSinTax { get; set; }
        public decimal MtoPvpuConTax { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public int SnIncluyeImpto { get; set; }
        public int? IdImpuesto { get; set; }

        public virtual Mstt06Impuesto? IdImpuestoNavigation { get; set; }
        public virtual ICollection<Prot14ComboFixedDtl> Prot14ComboFixedDtls { get; set; }
        public virtual ICollection<Prot16ComboVariableDtl> Prot16ComboVariableDtls { get; set; }
    }
}

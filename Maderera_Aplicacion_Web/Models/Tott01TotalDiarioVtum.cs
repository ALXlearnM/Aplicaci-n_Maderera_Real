using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tott01TotalDiarioVtum
    {
        public long IdTotalDiarioVta { get; set; }
        public int? IdLocation { get; set; }
        public DateTime FecRegistro { get; set; }
        public DateTime FecNegocio { get; set; }
        public int? CantOrdenes { get; set; }
        public decimal? MtoOrdenes { get; set; }
        public int? CantBoletas { get; set; }
        public decimal? MtoBoletas { get; set; }
        public int? CantFacturas { get; set; }
        public decimal? MtoFacturas { get; set; }
        public int? CantNotCred { get; set; }
        public decimal? MtoNotCred { get; set; }
        public int? CantComprobantes { get; set; }
        public decimal? MtoComprobantes { get; set; }
        public decimal? MtoFormaPago01 { get; set; }
        public decimal? MtoFormaPago02 { get; set; }
        public decimal? MtoFormaPago03 { get; set; }
        public decimal? MtoFormaPago04 { get; set; }
        public decimal? MtoFormaPago05 { get; set; }
        public decimal? MtoFormaPago06 { get; set; }
        public decimal? MtoFormaPago07 { get; set; }
        public decimal? MtoFormaPago08 { get; set; }
        public decimal? MtoFormaPago09 { get; set; }
        public decimal? MtoFormaPago10 { get; set; }
        public int? IdUsuario { get; set; }
        public string? TxtUsuario { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
    }
}

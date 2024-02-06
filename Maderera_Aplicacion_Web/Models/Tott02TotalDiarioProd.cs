using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Tott02TotalDiarioProd
    {
        public long IdTotalDiarioProd { get; set; }
        public int? IdLocation { get; set; }
        public DateTime FecRegistro { get; set; }
        public DateTime FecNegocio { get; set; }
        public int? IdCanVta { get; set; }
        public int? IdTipoOrden { get; set; }
        public long? IdProducto { get; set; }
        public int? IdUm { get; set; }
        public decimal? SohIni { get; set; }
        public decimal? SohFin { get; set; }
        public decimal? CantCompra { get; set; }
        public decimal? CantTransfIn { get; set; }
        public decimal? CantTransOut { get; set; }
        public decimal? CantVta { get; set; }
        public decimal? Precio { get; set; }
        public decimal? PrecioLast { get; set; }
        public decimal? PrecioAvg { get; set; }
        public decimal? Costo { get; set; }
        public long? IdUsuario { get; set; }
        public string? TxtUsuario { get; set; }
        public int? Post { get; set; }
        public DateTime? PostDate { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
    }
}

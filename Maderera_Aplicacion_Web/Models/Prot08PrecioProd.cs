using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Prot08PrecioProd
    {
        public long IdPrecioProd { get; set; }
        public long? IdProducto { get; set; }
        public decimal? MtoPuSinIgv1 { get; set; }
        public decimal? MtoPuSinIgv2 { get; set; }
        public decimal? MtoPuSinIgv3 { get; set; }
        public decimal? MtoPuSinIgv4 { get; set; }
        public decimal? MtoPuSinIgv5 { get; set; }
        public decimal? MtoPuSinIgv6 { get; set; }
        public decimal? MtoPuSinIgv7 { get; set; }
        public decimal? MtoPuSinIgv8 { get; set; }
        public decimal? MtoPuSinIgv9 { get; set; }
        public decimal? MtoPuSinIgv10 { get; set; }
        public decimal? MtoPuConIgv1 { get; set; }
        public decimal? MtoPuConIgv2 { get; set; }
        public decimal? MtoPuConIgv3 { get; set; }
        public decimal? MtoPuConIgv4 { get; set; }
        public decimal? MtoPuConIgv5 { get; set; }
        public decimal? MtoPuConIgv6 { get; set; }
        public decimal? MtoPuConIgv7 { get; set; }
        public decimal? MtoPuConIgv8 { get; set; }
        public decimal? MtoPuConIgv9 { get; set; }
        public decimal? MtoPuConIgv10 { get; set; }
        public decimal? MtoCosto1 { get; set; }
        public decimal? MtoCosto2 { get; set; }
        public decimal? MtoCosto3 { get; set; }
        public decimal? MtoCosto4 { get; set; }
        public decimal? MtoCosto5 { get; set; }
        public decimal? MtoCosto6 { get; set; }
        public decimal? MtoCosto7 { get; set; }
        public decimal? MtoCosto8 { get; set; }
        public decimal? MtoCosto9 { get; set; }
        public decimal? MtoCosto10 { get; set; }
        public DateTime? FecEfectivoDesde { get; set; }
        public DateTime? FecEfectivoHasta { get; set; }
        public string? TxtObsv { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Prot09Producto? IdProductoNavigation { get; set; }
    }
}

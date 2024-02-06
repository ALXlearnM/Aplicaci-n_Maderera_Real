namespace Maderera_Aplicacion_Web.Models
{
    public class ProductoS
    {
        public long idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public decimal neto { get; set; }
        public decimal descuento { get; set; }
        public decimal mtodescuento { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal mtoigv { get; set; }
        public decimal monto_si { get; set; }
        public decimal monto_ci { get; set; }
        public decimal total { get; set; }
        public string observacion { get; set; }
    }
}

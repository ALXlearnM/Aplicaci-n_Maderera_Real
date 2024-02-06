namespace Maderera_Aplicacion_Web.Models
{
    public class ProductoSp
    {
        public long idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public string?  um { get; set; }
        public decimal monto_ci { get; set; }
        public decimal total { get; set; }
        public string observacion { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IADatabase.Models
{
    public class DetalleVenta
    {
        [Key]
        public int IdDetalleVenta { get; set; }

        [ForeignKey("Venta")]
        public int IdVenta { get; set; }

        public Venta Venta { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        public decimal Total { get; set; }
    }
}

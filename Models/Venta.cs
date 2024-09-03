using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IADatabase.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        public Cliente Cliente { get; set; }

        public DateTime FechaVenta { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal TotalVenta { get; set; }
    }
}

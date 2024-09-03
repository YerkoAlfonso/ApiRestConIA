using System.ComponentModel.DataAnnotations;

namespace IADatabase.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCliente { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Correo { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(255)]
        public string Direccion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}

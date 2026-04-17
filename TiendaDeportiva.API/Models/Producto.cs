using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaDeportiva.API.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [Required]
        [RegularExpression("Fútbol|Básquetbol|Natación|Tenis",
            ErrorMessage = "Categoría inválida")]
        public string Categoria { get; set; }

        public bool Activo { get; set; } = true;

        // Relación
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
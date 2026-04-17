using System.ComponentModel.DataAnnotations;

namespace TiendaDeportiva.MVC.Models
{
    public class ProductoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public string Categoria { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaDeportiva.API.Models
{
    public class DetallePedido
    {
        public int Id { get; set; }

        [ForeignKey("Pedido")]
        public int IdPedido { get; set; }
        public virtual Pedido Pedido { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public virtual Producto Producto { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Precio unitario debe ser mayor a 0")]
        public decimal PrecioUnitario { get; set; }
    }
}
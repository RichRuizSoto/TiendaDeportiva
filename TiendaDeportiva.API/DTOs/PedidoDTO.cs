using System.Collections.Generic;

namespace TiendaDeportiva.API.DTOs
{
    public class PedidoDTO
    {
        public List<DetallePedidoDTO> Productos { get; set; }
    }

    public class DetallePedidoDTO
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
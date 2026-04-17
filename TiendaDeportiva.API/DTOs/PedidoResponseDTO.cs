using System;
using System.Collections.Generic;

namespace TiendaDeportiva.API.DTOs
{
    public class PedidoResponseDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoTotal { get; set; }
        public string Estado { get; set; }

        public List<DetallePedidoResponseDTO> Detalles { get; set; }
    }

    public class DetallePedidoResponseDTO
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TiendaDeportiva.API.Models;

namespace TiendaDeportiva.API.Controllers
{
    [RoutePrefix("api/detallepedidos")]
    public class DetallePedidosController : ApiController
    {
        private static readonly List<Pedido> pedidos = new List<Pedido>();

        // GET: api/detallepedidos/5 (detalles de un pedido)
        [HttpGet]
        [Route("{idPedido:int}")]
        public IHttpActionResult GetByPedido(int idPedido)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == idPedido);

            if (pedido == null)
                return NotFound();

            var detalles = pedido.DetallePedidos.Select(d => new DTOs.DetallePedidoResponseDTO
            {
                Producto = d.Producto?.Nombre,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Cantidad * d.PrecioUnitario
            }).ToList();

            var response = new DTOs.PedidoResponseDTO
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                Estado = pedido.Estado,
                MontoTotal = pedido.MontoTotal,
                Detalles = detalles
            };

            return Ok(response);
        }
    }
}
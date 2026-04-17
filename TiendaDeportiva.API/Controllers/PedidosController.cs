using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TiendaDeportiva.API.DTOs;
using TiendaDeportiva.API.Models;

namespace TiendaDeportiva.API.Controllers
{
    [RoutePrefix("api/pedidos")]
    public class PedidosController : ApiController
    {
        // Simulación de BD (luego lo cambias por EF)
        private static List<Pedido> pedidos = new List<Pedido>();
        private static List<Producto> productos = new List<Producto>(); // referencia simple

        // GET: api/pedidos
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(pedidos);
        }

        // GET: api/pedidos/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        // POST: api/pedidos
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] PedidoDTO dto)
        {
            if (dto == null || dto.Productos == null || !dto.Productos.Any())
                return BadRequest("El pedido debe contener productos");

            var pedido = new Pedido
            {
                Id = pedidos.Count + 1,
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                DetallePedidos = new List<DetallePedido>()
            };

            decimal total = 0;

            foreach (var item in dto.Productos)
            {
                var producto = productos.FirstOrDefault(p => p.Id == item.IdProducto);

                if (producto == null)
                    return BadRequest($"Producto con ID {item.IdProducto} no existe");

                if (item.Cantidad <= 0)
                    return BadRequest("La cantidad debe ser mayor a 0");

                var detalle = new DetallePedido
                {
                    IdProducto = producto.Id,
                    Producto = producto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio
                };

                total += producto.Precio * item.Cantidad;

                pedido.DetallePedidos.Add(detalle);
            }

            pedido.MontoTotal = total;

            pedidos.Add(pedido);

            return Ok(pedido);
        }

        // PUT: api/pedidos/5 (cambiar estado)
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] string estado)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            pedido.Estado = estado;

            return Ok(pedido);
        }

        // DELETE: api/pedidos/5 (eliminar lógico opcional)
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            pedidos.Remove(pedido);

            return Ok("Pedido eliminado correctamente");
        }
    }
}
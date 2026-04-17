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
        // Simulación de BD (en producción se reemplaza por EF DbContext)
        private static List<Pedido> pedidos = new List<Pedido>();

        // IMPORTANTE: esto debería venir de BD real (ProductosController / DbContext)
        private static List<Producto> productos = new List<Producto>();

        // =========================================================
        // GET: api/pedidos (con paginación básica)
        // =========================================================
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int pagina = 1, int tamPagina = 10)
        {
            var total = pedidos.Count;

            var data = pedidos
                .OrderByDescending(p => p.Fecha)
                .Skip((pagina - 1) * tamPagina)
                .Take(tamPagina)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Estado,
                    p.MontoTotal,
                    TotalProductos = p.DetallePedidos.Count
                })
                .ToList();

            return Ok(new
            {
                Total = total,
                Pagina = pagina,
                TamPagina = tamPagina,
                Data = data
            });
        }

        // =========================================================
        // GET: api/pedidos/5
        // =========================================================
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return Content(System.Net.HttpStatusCode.NotFound,
                    new { mensaje = "Pedido no encontrado" });

            return Ok(pedido);
        }

        // =========================================================
        // POST: api/pedidos (crear pedido con validaciones reales)
        // =========================================================
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] PedidoDTO dto)
        {
            if (dto == null || dto.Productos == null || !dto.Productos.Any())
                return BadRequest("El pedido debe contener al menos un producto");

            var pedido = new Pedido
            {
                Id = pedidos.Count > 0 ? pedidos.Max(p => p.Id) + 1 : 1,
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                DetallePedidos = new List<DetallePedido>()
            };

            decimal total = 0;

            foreach (var item in dto.Productos)
            {
                var producto = productos.FirstOrDefault(p => p.Id == item.IdProducto);

                if (producto == null)
                    return Content(System.Net.HttpStatusCode.BadRequest,
                        new { mensaje = $"Producto {item.IdProducto} no existe" });

                if (item.Cantidad <= 0)
                    return BadRequest("La cantidad debe ser mayor a 0");

                if (producto.Stock < item.Cantidad)
                    return BadRequest($"Stock insuficiente para {producto.Nombre}");

                var detalle = new DetallePedido
                {
                    IdProducto = producto.Id,
                    Producto = producto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio
                };

                total += producto.Precio * item.Cantidad;

                // descontar stock (lógica real de negocio)
                producto.Stock -= item.Cantidad;

                pedido.DetallePedidos.Add(detalle);
            }

            pedido.MontoTotal = total;

            pedidos.Add(pedido);

            return Ok(new
            {
                mensaje = "Pedido creado correctamente",
                data = pedido
            });
        }

        // =========================================================
        // PUT: api/pedidos/5 (cambiar estado con validación)
        // =========================================================
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] string estado)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return Content(System.Net.HttpStatusCode.NotFound,
                    new { mensaje = "Pedido no encontrado" });

            var estadosValidos = new[] { "Pendiente", "Procesando", "Enviado", "Entregado", "Cancelado" };

            if (!estadosValidos.Contains(estado))
                return BadRequest("Estado inválido");

            pedido.Estado = estado;

            return Ok(new
            {
                mensaje = "Estado actualizado correctamente",
                data = pedido
            });
        }

        // =========================================================
        // DELETE: api/pedidos/5 (eliminación lógica)
        // =========================================================
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return Content(System.Net.HttpStatusCode.NotFound,
                    new { mensaje = "Pedido no encontrado" });

            // soft delete real (mejor práctica que Remove)
            pedido.Estado = "Cancelado";

            return Ok(new
            {
                mensaje = "Pedido cancelado correctamente"
            });
        }
    }
}
using System;
using System.Linq;
using System.Web.Http;
using TiendaDeportiva.API.DTOs;
using TiendaDeportiva.API.Models;

namespace TiendaDeportiva.API.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        // GET: api/productos
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(
            string categoria = null,
            decimal? minPrecio = null,
            decimal? maxPrecio = null,
            int pagina = 1,
            int tamPagina = 10)
        {
            using (var db = new AppDbContext())
            {
                var query = db.Productos.Where(p => p.Activo);

                // Filtros
                if (!string.IsNullOrEmpty(categoria))
                    query = query.Where(p => p.Categoria == categoria);

                if (minPrecio.HasValue)
                    query = query.Where(p => p.Precio >= minPrecio.Value);

                if (maxPrecio.HasValue)
                    query = query.Where(p => p.Precio <= maxPrecio.Value);

                // Total para paginación
                var total = query.Count();

                var productos = query
                    .OrderBy(p => p.Id)
                    .Skip((pagina - 1) * tamPagina)
                    .Take(tamPagina)
                    .Select(p => new ProductoDTO
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        Stock = p.Stock,
                        Categoria = p.Categoria
                    })
                    .ToList();

                return Ok(new
                {
                    Total = total,
                    Pagina = pagina,
                    TamPagina = tamPagina,
                    Data = productos
                });
            }
        }

        // GET: api/productos/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            using (var db = new AppDbContext())
            {
                var p = db.Productos.FirstOrDefault(x => x.Id == id && x.Activo);

                if (p == null)
                    return Content(System.Net.HttpStatusCode.NotFound,
                        new { mensaje = "Producto no encontrado" });

                return Ok(new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Categoria = p.Categoria
                });
            }
        }

        // POST: api/productos
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] ProductoDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("El nombre es obligatorio");

            if (dto.Precio <= 0)
                return BadRequest("El precio debe ser mayor a 0");

            if (dto.Stock < 0)
                return BadRequest("El stock no puede ser negativo");

            if (dto.Categoria != "Fútbol" &&
                dto.Categoria != "Básquetbol" &&
                dto.Categoria != "Natación" &&
                dto.Categoria != "Tenis")
            {
                return BadRequest("Categoría inválida");
            }

            using (var db = new AppDbContext())
            {
                var entity = new Producto
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Precio = dto.Precio,
                    Stock = dto.Stock,
                    Categoria = dto.Categoria,
                    Activo = true
                };

                db.Productos.Add(entity);
                db.SaveChanges();

                dto.Id = entity.Id;

                return Ok(new
                {
                    mensaje = "Producto creado correctamente",
                    data = dto
                });
            }
        }

        // PUT: api/productos/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] ProductoDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            using (var db = new AppDbContext())
            {
                var entity = db.Productos.FirstOrDefault(x => x.Id == id && x.Activo);

                if (entity == null)
                    return Content(System.Net.HttpStatusCode.NotFound,
                        new { mensaje = "Producto no encontrado" });

                if (dto.Precio <= 0)
                    return BadRequest("El precio debe ser mayor a 0");

                if (dto.Stock < 0)
                    return BadRequest("El stock no puede ser negativo");

                entity.Nombre = dto.Nombre;
                entity.Descripcion = dto.Descripcion;
                entity.Precio = dto.Precio;
                entity.Stock = dto.Stock;
                entity.Categoria = dto.Categoria;

                db.SaveChanges();

                return Ok(new
                {
                    mensaje = "Producto actualizado correctamente",
                    data = dto
                });
            }
        }

        // DELETE: api/productos/5 (soft delete)
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            using (var db = new AppDbContext())
            {
                var entity = db.Productos.FirstOrDefault(x => x.Id == id && x.Activo);

                if (entity == null)
                    return Content(System.Net.HttpStatusCode.NotFound,
                        new { mensaje = "Producto no encontrado" });

                entity.Activo = false;
                db.SaveChanges();

                return Ok(new
                {
                    mensaje = "Producto desactivado correctamente"
                });
            }
        }
    }
}
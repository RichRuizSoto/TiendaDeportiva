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
            decimal? maxPrecio = null)
        {
            using (var db = new AppDbContext())
            {
                var query = db.Productos.Where(p => p.Activo);

                if (!string.IsNullOrEmpty(categoria))
                    query = query.Where(p => p.Categoria == categoria);

                if (minPrecio.HasValue)
                    query = query.Where(p => p.Precio >= minPrecio.Value);

                if (maxPrecio.HasValue)
                    query = query.Where(p => p.Precio <= maxPrecio.Value);

                var result = query.Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Categoria = p.Categoria
                }).ToList();

                return Ok(result);
            }
        }

        // GET by id
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            using (var db = new AppDbContext())
            {
                var p = db.Productos.FirstOrDefault(x => x.Id == id && x.Activo);

                if (p == null)
                    return NotFound();

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

        // POST
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] ProductoDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

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

                return Ok(dto);
            }
        }

        // PUT
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] ProductoDTO dto)
        {
            using (var db = new AppDbContext())
            {
                var entity = db.Productos.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                    return NotFound();

                entity.Nombre = dto.Nombre;
                entity.Descripcion = dto.Descripcion;
                entity.Precio = dto.Precio;
                entity.Stock = dto.Stock;
                entity.Categoria = dto.Categoria;

                db.SaveChanges();

                return Ok(dto);
            }
        }

        // DELETE (soft delete)
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            using (var db = new AppDbContext())
            {
                var entity = db.Productos.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                    return NotFound();

                entity.Activo = false;
                db.SaveChanges();

                return Ok("Producto desactivado");
            }
        }
    }
}
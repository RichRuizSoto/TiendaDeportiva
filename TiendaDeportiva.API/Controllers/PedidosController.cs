using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TiendaDeportiva.API.Models;

namespace TiendaDeportiva.API.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        private readonly AppDbContext db = new AppDbContext();

        // =========================
        // GET: api/productos
        // =========================
        [HttpGet]
        [Route("")]
        public IEnumerable<Producto> GetProductos()
        {
            return db.Productos.ToList();
        }

        // =========================
        // GET: api/productos/5
        // =========================
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetProducto(int id)
        {
            var producto = db.Productos.Find(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // =========================
        // POST: api/productos
        // =========================
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Productos.Add(producto);
            db.SaveChanges();

            return Created($"api/productos/{producto.Id}", producto);
        }

        // =========================
        // PUT: api/productos/5
        // =========================
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = db.Productos.Find(id);
            if (existing == null)
                return NotFound();

            existing.Nombre = producto.Nombre;
            existing.Descripcion = producto.Descripcion;
            existing.Precio = producto.Precio;
            existing.Stock = producto.Stock;
            existing.Categoria = producto.Categoria;

            db.SaveChanges();

            return Ok(existing);
        }

        // =========================
        // DELETE: api/productos/5
        // =========================
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteProducto(int id)
        {
            var producto = db.Productos.Find(id);

            if (producto == null)
                return NotFound();

            db.Productos.Remove(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        // =========================
        // Dispose
        // =========================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
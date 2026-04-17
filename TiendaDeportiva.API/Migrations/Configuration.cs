namespace TiendaDeportiva.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TiendaDeportiva.API.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TiendaDeportiva.API.Models.AppDbContext context)
        {
            context.Productos.AddOrUpdate(p => p.Nombre,
                new Models.Producto
                {
                    Nombre = "Balón Nike",
                    Descripcion = "Balón profesional de fútbol",
                    Precio = 25,
                    Stock = 10,
                    Categoria = "Fútbol",
                    Activo = true
                },
                new Models.Producto
                {
                    Nombre = "Zapatillas Adidas",
                    Descripcion = "Zapatillas running",
                    Precio = 80,
                    Stock = 15,
                    Categoria = "Tenis",
                    Activo = true
                },
                new Models.Producto
                {
                    Nombre = "Guantes Portero",
                    Descripcion = "Guantes de alta resistencia",
                    Precio = 15,
                    Stock = 20,
                    Categoria = "Fútbol",
                    Activo = true
                },
                new Models.Producto
                {
                    Nombre = "Raqueta Wilson",
                    Descripcion = "Raqueta profesional",
                    Precio = 120,
                    Stock = 5,
                    Categoria = "Tenis",
                    Activo = true
                },
                new Models.Producto
                {
                    Nombre = "Balón Molten",
                    Descripcion = "Balón oficial de básquet",
                    Precio = 30,
                    Stock = 12,
                    Categoria = "Básquetbol",
                    Activo = true
                }
            );

            context.SaveChanges();
        }
    }
}

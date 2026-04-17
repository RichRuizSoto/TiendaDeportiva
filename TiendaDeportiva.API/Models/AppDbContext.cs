using System.Data.Entity;

namespace TiendaDeportiva.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {
        }

        // TABLAS
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================
            // NOMBRES DE TABLAS (IMPORTANTE)
            // ==========================
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<DetallePedido>().ToTable("DetallePedidos");

            // ==========================
            // RELACIÓN: DetallePedido -> Producto
            // ==========================
            modelBuilder.Entity<DetallePedido>()
                .HasRequired(d => d.Producto)
                .WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .WillCascadeOnDelete(false);

            // ==========================
            // RELACIÓN: Pedido -> DetallePedido
            // ==========================
            modelBuilder.Entity<DetallePedido>()
                .HasRequired(d => d.Pedido)
                .WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .WillCascadeOnDelete(true);
        }
    }
}
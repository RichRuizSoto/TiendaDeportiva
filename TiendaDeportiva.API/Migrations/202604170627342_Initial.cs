namespace TiendaDeportiva.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DetallePedidoes", newName: "DetallePedidos");
            RenameTable(name: "dbo.Pedidoes", newName: "Pedidos");
            RenameTable(name: "dbo.Productoes", newName: "Productos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Productos", newName: "Productoes");
            RenameTable(name: "dbo.Pedidos", newName: "Pedidoes");
            RenameTable(name: "dbo.DetallePedidos", newName: "DetallePedidoes");
        }
    }
}

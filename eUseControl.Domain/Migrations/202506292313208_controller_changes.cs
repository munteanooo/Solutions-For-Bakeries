namespace eUseControl.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class controller_changes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderItems", newName: "OrderItem");
            RenameTable(name: "dbo.Orders", newName: "Order");
            RenameTable(name: "dbo.Products", newName: "Product");
            RenameTable(name: "dbo.Users", newName: "User");
            CreateIndex("dbo.Order", "UserId");
            AddForeignKey("dbo.Order", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "UserId", "dbo.User");
            DropIndex("dbo.Order", new[] { "UserId" });
            RenameTable(name: "dbo.User", newName: "Users");
            RenameTable(name: "dbo.Product", newName: "Products");
            RenameTable(name: "dbo.Order", newName: "Orders");
            RenameTable(name: "dbo.OrderItem", newName: "OrderItems");
        }
    }
}

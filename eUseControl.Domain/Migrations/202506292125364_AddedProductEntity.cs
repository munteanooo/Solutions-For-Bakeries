namespace eUseControl.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Expiration", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Expiration", c => c.String());
        }
    }
}

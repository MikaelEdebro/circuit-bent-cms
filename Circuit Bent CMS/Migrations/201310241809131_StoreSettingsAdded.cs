namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreSettingsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreSettings",
                c => new
                    {
                        StoreSettingsId = c.Int(nullable: false, identity: true),
                        ProductsInRow = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoreSettingsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StoreSettings");
        }
    }
}

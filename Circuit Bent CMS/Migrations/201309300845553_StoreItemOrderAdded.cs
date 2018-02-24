namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreItemOrderAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreItems", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreItems", "Order");
        }
    }
}

namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StringLengthFixed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StoreItems", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.StoreItems", "Price", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StoreItems", "Price", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.StoreItems", "Title", c => c.String(nullable: false, maxLength: 4000));
        }
    }
}

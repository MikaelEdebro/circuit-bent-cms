namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreThumbnailHeightAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreSettings", "ThumbnailHeight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreSettings", "ThumbnailHeight");
        }
    }
}

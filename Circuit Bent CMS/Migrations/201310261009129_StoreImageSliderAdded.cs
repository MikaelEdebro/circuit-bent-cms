namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreImageSliderAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreSettings", "ImageSliderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreSettings", "ImageSliderId");
        }
    }
}

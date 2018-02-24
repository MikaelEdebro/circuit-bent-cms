namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageSliderOnHomepage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageSliderSettings", "ImageSliderOnHomepage", c => c.Int(nullable: false));
            DropColumn("dbo.ImageSliderSettings", "ShowSlider");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageSliderSettings", "ShowSlider", c => c.Boolean(nullable: false));
            DropColumn("dbo.ImageSliderSettings", "ImageSliderOnHomepage");
        }
    }
}

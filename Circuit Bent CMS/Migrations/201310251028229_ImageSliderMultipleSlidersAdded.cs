namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageSliderMultipleSlidersAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageSliders",
                c => new
                    {
                        ImageSliderId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ImageSliderId);
            
            AddColumn("dbo.ImageSliderImages", "ImageSliderId", c => c.Int(nullable: false));
            AddForeignKey("dbo.ImageSliderImages", "ImageSliderId", "dbo.ImageSliders", "ImageSliderId", cascadeDelete: true);
            CreateIndex("dbo.ImageSliderImages", "ImageSliderId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ImageSliderImages", new[] { "ImageSliderId" });
            DropForeignKey("dbo.ImageSliderImages", "ImageSliderId", "dbo.ImageSliders");
            DropColumn("dbo.ImageSliderImages", "ImageSliderId");
            DropTable("dbo.ImageSliders");
        }
    }
}

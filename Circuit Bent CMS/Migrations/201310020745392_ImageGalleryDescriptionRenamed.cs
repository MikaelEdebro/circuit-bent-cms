namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageGalleryDescriptionRenamed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageDescription", c => c.String(maxLength: 4000));
            DropColumn("dbo.Images", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Description", c => c.String(maxLength: 4000));
            DropColumn("dbo.Images", "ImageDescription");
        }
    }
}

namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageGalleryRemoveDescription : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "ImageDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImageDescription", c => c.String(maxLength: 4000));
        }
    }
}

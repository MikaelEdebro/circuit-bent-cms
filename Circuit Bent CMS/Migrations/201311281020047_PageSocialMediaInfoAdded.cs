namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageSocialMediaInfoAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "SocialMediaTitle", c => c.String(maxLength: 95));
            AddColumn("dbo.Pages", "SocialMediaDescription", c => c.String(maxLength: 297));
            AddColumn("dbo.Pages", "SocialMediaImage", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "SocialMediaImage");
            DropColumn("dbo.Pages", "SocialMediaDescription");
            DropColumn("dbo.Pages", "SocialMediaTitle");
        }
    }
}

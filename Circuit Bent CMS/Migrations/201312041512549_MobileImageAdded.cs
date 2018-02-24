namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MobileImageAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSettings", "MobileImage", c => c.String(maxLength: 4000));
            DropColumn("dbo.SiteSettings", "MetaKeywords");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SiteSettings", "MetaKeywords", c => c.String(maxLength: 500));
            DropColumn("dbo.SiteSettings", "MobileImage");
        }
    }
}

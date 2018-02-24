namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PagesExternalLinkUrlAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "ExternalLinkUrl", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "ExternalLinkUrl");
        }
    }
}

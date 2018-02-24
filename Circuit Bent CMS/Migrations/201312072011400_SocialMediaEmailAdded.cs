namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SocialMediaEmailAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SocialMedias", "Email", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SocialMedias", "Email");
        }
    }
}

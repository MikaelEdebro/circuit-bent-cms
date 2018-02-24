namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoogleAnalyticsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSettings", "GoogleAnalytics", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteSettings", "GoogleAnalytics");
        }
    }
}

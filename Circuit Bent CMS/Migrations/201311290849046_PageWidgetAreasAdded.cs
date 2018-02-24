namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageWidgetAreasAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "WidgetArea1", c => c.String(maxLength: 4000));
            AddColumn("dbo.Pages", "WidgetArea2", c => c.String(maxLength: 4000));
            AddColumn("dbo.Pages", "WidgetArea3", c => c.String(maxLength: 4000));
            AddColumn("dbo.Pages", "WidgetArea4", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "WidgetArea4");
            DropColumn("dbo.Pages", "WidgetArea3");
            DropColumn("dbo.Pages", "WidgetArea2");
            DropColumn("dbo.Pages", "WidgetArea1");
        }
    }
}

namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowAnnotationsUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shows", "Time", c => c.String(maxLength: 20));
            AlterColumn("dbo.Shows", "Price", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shows", "Price", c => c.String(maxLength: 10));
            AlterColumn("dbo.Shows", "Time", c => c.String(maxLength: 10));
        }
    }
}

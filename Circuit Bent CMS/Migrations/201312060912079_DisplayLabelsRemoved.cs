namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayLabelsRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MailSettings", "ContactPageText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MailSettings", "ContactPageText", c => c.String(maxLength: 2000));
        }
    }
}

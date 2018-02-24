namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageSliderTransitionModeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageSliderSettings", "TransitionMode", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageSliderSettings", "TransitionMode");
        }
    }
}

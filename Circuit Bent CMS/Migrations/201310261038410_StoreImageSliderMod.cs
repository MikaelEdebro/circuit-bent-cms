namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreImageSliderMod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageSliders", "StoreSettings_StoreSettingsId", c => c.Int());
            AddForeignKey("dbo.ImageSliders", "StoreSettings_StoreSettingsId", "dbo.StoreSettings", "StoreSettingsId");
            CreateIndex("dbo.ImageSliders", "StoreSettings_StoreSettingsId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ImageSliders", new[] { "StoreSettings_StoreSettingsId" });
            DropForeignKey("dbo.ImageSliders", "StoreSettings_StoreSettingsId", "dbo.StoreSettings");
            DropColumn("dbo.ImageSliders", "StoreSettings_StoreSettingsId");
        }
    }
}

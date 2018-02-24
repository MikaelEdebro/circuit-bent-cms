namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreSettingsRemovedImageSliders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageSliders", "StoreSettings_StoreSettingsId", "dbo.StoreSettings");
            DropIndex("dbo.ImageSliders", new[] { "StoreSettings_StoreSettingsId" });
            DropColumn("dbo.ImageSliders", "StoreSettings_StoreSettingsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageSliders", "StoreSettings_StoreSettingsId", c => c.Int());
            CreateIndex("dbo.ImageSliders", "StoreSettings_StoreSettingsId");
            AddForeignKey("dbo.ImageSliders", "StoreSettings_StoreSettingsId", "dbo.StoreSettings", "StoreSettingsId");
        }
    }
}

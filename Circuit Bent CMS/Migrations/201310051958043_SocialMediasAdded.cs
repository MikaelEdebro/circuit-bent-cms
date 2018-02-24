namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SocialMediasAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SocialMedias", "Bandcamp", c => c.String(maxLength: 150));
            AddColumn("dbo.SocialMedias", "Flickr", c => c.String(maxLength: 150));
            AddColumn("dbo.SocialMedias", "LinkedIn", c => c.String(maxLength: 150));
            AddColumn("dbo.SocialMedias", "LastFm", c => c.String(maxLength: 150));
            AddColumn("dbo.SocialMedias", "Pinterest", c => c.String(maxLength: 150));
            AddColumn("dbo.SocialMedias", "Tumblr", c => c.String(maxLength: 150));
            DropColumn("dbo.SocialMedias", "ShowFacebookFeed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SocialMedias", "ShowFacebookFeed", c => c.Boolean(nullable: false));
            DropColumn("dbo.SocialMedias", "Tumblr");
            DropColumn("dbo.SocialMedias", "Pinterest");
            DropColumn("dbo.SocialMedias", "LastFm");
            DropColumn("dbo.SocialMedias", "LinkedIn");
            DropColumn("dbo.SocialMedias", "Flickr");
            DropColumn("dbo.SocialMedias", "Bandcamp");
        }
    }
}

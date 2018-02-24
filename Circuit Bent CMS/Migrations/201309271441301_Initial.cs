namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ShowId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Date = c.DateTime(nullable: false),
                        Venue = c.String(maxLength: 150),
                        Time = c.String(maxLength: 10),
                        Price = c.String(maxLength: 10),
                        LinkToEvent = c.String(maxLength: 150),
                        Cancelled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShowId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Headline = c.String(nullable: false, maxLength: 100),
                        Text = c.String(nullable: false),
                        EnableComments = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId);
            
            CreateTable(
                "dbo.SiteSettings",
                c => new
                    {
                        SiteSettingsId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 150),
                        MetaKeywords = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        MetaLanguage = c.String(maxLength: 5),
                        Favicon = c.String(maxLength: 4000),
                        FooterText = c.String(),
                        BackgroundImage = c.String(maxLength: 4000),
                        BackgroundImageFullScreen = c.Boolean(nullable: false),
                        HeaderImage = c.String(maxLength: 4000),
                        FooterImage = c.String(maxLength: 4000),
                        BackgroundColor = c.String(maxLength: 11),
                        ContainerColor = c.String(maxLength: 11),
                        ContainerOpacity = c.Int(nullable: false),
                        HeadingColor = c.String(maxLength: 11),
                        TextColor = c.String(maxLength: 11),
                        LinkColor = c.String(maxLength: 11),
                        HeadingFont = c.String(maxLength: 150),
                        HeadingSize = c.String(maxLength: 4000),
                        TextFont = c.String(maxLength: 150),
                        TextSize = c.String(maxLength: 4000),
                        ContainerWidth = c.Int(nullable: false),
                        CustomCSS = c.String(),
                        CustomHtml = c.String(),
                    })
                .PrimaryKey(t => t.SiteSettingsId);
            
            CreateTable(
                "dbo.MailSettings",
                c => new
                    {
                        MailSettingsId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 150),
                        ContactPageText = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.MailSettingsId);
            
            CreateTable(
                "dbo.ImageGallerySettings",
                c => new
                    {
                        ImageGallerySettingsId = c.Int(nullable: false, identity: true),
                        ImageGalleryStyle = c.String(maxLength: 4000),
                        ThumbnailWidth = c.Int(nullable: false),
                        ThumbnailHeight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageGallerySettingsId);
            
            CreateTable(
                "dbo.StoreItems",
                c => new
                    {
                        StoreItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 4000),
                        Description = c.String(),
                        Price = c.String(nullable: false, maxLength: 4000),
                        SoldOut = c.Boolean(nullable: false),
                        PaypalCode = c.String(),
                    })
                .PrimaryKey(t => t.StoreItemId);
            
            CreateTable(
                "dbo.StoreItemImages",
                c => new
                    {
                        StoreItemImageId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(maxLength: 4000),
                        StoreItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoreItemImageId)
                .ForeignKey("dbo.StoreItems", t => t.StoreItemId, cascadeDelete: true)
                .Index(t => t.StoreItemId);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        ShowOnMenu = c.Boolean(nullable: false),
                        Content = c.String(),
                        SideBarContent = c.String(),
                        LastUpdated = c.DateTime(nullable: false),
                        SubPageToPageId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        HomePage = c.Boolean(nullable: false),
                        CustomPage = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.PageId);
            
            CreateTable(
                "dbo.SocialMedias",
                c => new
                    {
                        SocialMediaId = c.Int(nullable: false, identity: true),
                        Facebook = c.String(maxLength: 150),
                        ShowFacebookFeed = c.Boolean(nullable: false),
                        Myspace = c.String(maxLength: 150),
                        Soundcloud = c.String(maxLength: 150),
                        Twitter = c.String(maxLength: 150),
                        Instagram = c.String(maxLength: 150),
                        GooglePlus = c.String(maxLength: 150),
                        Youtube = c.String(maxLength: 150),
                        Spotify = c.String(maxLength: 150),
                        Vimeo = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.SocialMediaId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(maxLength: 4000),
                        Title = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Photographer = c.String(maxLength: 4000),
                        PhotographerUrl = c.String(maxLength: 4000),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 4000),
                        FileExtension = c.String(maxLength: 4000),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Fonts",
                c => new
                    {
                        FontId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        FontFamily = c.String(maxLength: 4000),
                        CssCode = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.FontId);
            
            CreateTable(
                "dbo.ImageSliderSettings",
                c => new
                    {
                        ImageSliderSettingsId = c.Int(nullable: false, identity: true),
                        ShowSlider = c.Boolean(nullable: false),
                        TransitionSpeed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageSliderSettingsId);
            
            CreateTable(
                "dbo.ImageSliderImages",
                c => new
                    {
                        ImageSliderImageId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(maxLength: 4000),
                        LinkUrl = c.String(maxLength: 4000),
                        Title = c.String(maxLength: 4000),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageSliderImageId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.StoreItemImages", new[] { "StoreItemId" });
            DropForeignKey("dbo.StoreItemImages", "StoreItemId", "dbo.StoreItems");
            DropTable("dbo.ImageSliderImages");
            DropTable("dbo.ImageSliderSettings");
            DropTable("dbo.Fonts");
            DropTable("dbo.Files");
            DropTable("dbo.Images");
            DropTable("dbo.SocialMedias");
            DropTable("dbo.Pages");
            DropTable("dbo.StoreItemImages");
            DropTable("dbo.StoreItems");
            DropTable("dbo.ImageGallerySettings");
            DropTable("dbo.MailSettings");
            DropTable("dbo.SiteSettings");
            DropTable("dbo.Blogs");
            DropTable("dbo.Shows");
        }
    }
}

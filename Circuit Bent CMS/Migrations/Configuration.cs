namespace CircuitBentCMS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CircuitBentCMS.Models;
    using System.Web.Security;
    using WebMatrix.WebData;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<CircuitBentCMS.Models.CircuitBentCMSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CircuitBentCMS.Models.CircuitBentCMSContext context)
        {
            // site settings, if it doesn't already exists
            if (context.SiteSettings.FirstOrDefault() == null)
            {
                context.SiteSettings.AddOrUpdate(
                    s => s.Title,
                    new SiteSettings
                    {
                        Title = "Page title",
                        MetaDescription = "",
                        MetaLanguage = "en",
                        Favicon = "",
                        FooterText = "",
                        BackgroundImage = "",
                        HeaderImage = "",
                        FooterImage = "",
                        MobileImage = "",
                        BackgroundColor = "255,255,255",
                        ContainerColor = "0,0,0",
                        ContainerOpacity = 7,
                        HeadingColor = "0,0,0",
                        TextColor = "0,0,0",
                        LinkColor = "0,0,255",
                        HeadingFont = "Verdana",
                        HeadingSize = "8",
                        TextFont = "Verdana",
                        TextSize = "6",
                        ContainerWidth = 800,
                        CustomCSS = "",
                        CustomHtml = ""
                    }
                );
            }

            // mail settings, if it doesn't already exists
            if (context.MailSettings.FirstOrDefault() == null)
            {
                context.MailSettings.AddOrUpdate(
                    s => s.Email,
                    new MailSettings
                    {
                        Email = ""
                    }
                );
            }

            // mail settings, if it doesn't already exists
            if (context.SocialMedias.FirstOrDefault() == null)
            {
                context.SocialMedias.AddOrUpdate(
                    s => s.Facebook,
                    new SocialMedia
                    {
                        Facebook = ""
                    }
                );
            }

            // store settings, if it doesn't already exists
            if (context.StoreSettings.FirstOrDefault() == null)
            {
                context.StoreSettings.AddOrUpdate(
                    s => s.StoreSettingsId,
                    new StoreSettings
                    {
                        ProductsInRow = 4,
                        ThumbnailHeight = 180
                    }
                );
            }



            // add fonts to database
            List<Font> fonts = new List<Font>
            {
                new Font { Name = "Impact", FontFamily = "Impact, Charcoal, sans-serif" },
                new Font { Name = "Comic Sans", FontFamily = "'Comic Sans MS', cursive, sans-serif" },
                new Font { Name = "Palatino", FontFamily = "'Palatino Linotype', 'Book Antiqua', Palatino, serif" },
                new Font { Name = "Tahoma", FontFamily = "Tahoma, Geneva, sans-serif" },
                new Font { Name = "Century Gothic", FontFamily = "Century Gothic, sans-serif" },
                new Font { Name = "Lucida Sans", FontFamily = "'Lucida Sans Unicode', 'Lucida Grande', sans-serif" },
                new Font { Name = "Arial Black", FontFamily = "'Arial Black', Gadget, sans-serif" },
                new Font { Name = "Times New Roman", FontFamily = "'Times New Roman', Times, serif" },
                new Font { Name = "Arial Narrow", FontFamily = "'Arial Narrow', sans-serif" },
                new Font { Name = "Verdana", FontFamily = "Verdana, Geneva, sans-serif" },
                new Font { Name = "Cooperplate Gothic", FontFamily = "Copperplate, 'Copperplate Gothic Light', sans-serif" },
                new Font { Name = "Lucida Console", FontFamily = "'Lucida Console', Monaco, monospace" },
                new Font { Name = "Gill Sans", FontFamily = "'Gill Sans', 'Gill Sans MT', sans-serif" },
                new Font { Name = "Trebuchet MS", FontFamily = "'Trebuchet MS', Helvetica, sans-serif" },
                new Font { Name = "Courier New", FontFamily = "'Courier New', Courier, monospace" },
                new Font { Name = "Arial", FontFamily = "Arial, Helvetica, sans-serif" },
                new Font { Name = "Georgia", FontFamily = "Georgia, Serif" },
                new Font { Name = "Helvetica", FontFamily = "'Helvetica Neue', 'Lucida Grande', Helvetica, Arial, Verdana, sans-serif" }
                
            };
            fonts.ForEach(s => context.Fonts.AddOrUpdate(i => i.Name, s));
            context.SaveChanges();


            // image slider settings, if it doesn't already exists
            if (context.ImageGallerySettings.FirstOrDefault() == null)
            {
                context.ImageGallerySettings.AddOrUpdate(
                    s => s.ImageGallerySettingsId,
                    new ImageGallerySettings
                    {
                        ImageGalleryStyle = "slider",
                        ThumbnailHeight = 100,
                        ThumbnailWidth = 100
                    }
                );
            }
            
            // image slider settings, if it doesn't already exists
            if (context.ImageSliderSettings.FirstOrDefault() == null)
            {
                context.ImageSliderSettings.AddOrUpdate(
                    s => s.ImageSliderSettingsId,
                    new ImageSliderSettings
                    {
                        ImageSliderOnHomepage = 0,
                        TransitionMode = "horizontal",
                        TransitionSpeed = 12
                    }
                );
            }

            // create initial page, if it doesn't already exists
            if (context.Pages.FirstOrDefault() == null)
            {
                context.Pages.AddOrUpdate(
                    s => s.PageId,
                    new Page
                    {
                        Title = "Home",
                        ShowOnMenu = true,
                        HomePage = true,
                        Content = "Welcome!",
                        SideBarContent = "",
                        LastUpdated = DateTime.Now,
                        Order = 1,
                        SubPageToPageId = 0
                    }
                );
            }

        }
    }
}

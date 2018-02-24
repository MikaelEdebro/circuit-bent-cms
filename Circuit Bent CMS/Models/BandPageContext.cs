using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class CircuitBentCMSContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<CircuitBentCMS.Models.CircuitBentCMSContext>());

        public DbSet<CircuitBentCMS.Models.Show> Shows { get; set; }

        public DbSet<CircuitBentCMS.Models.Blog> Blogs { get; set; }

        public DbSet<CircuitBentCMS.Models.SiteSettings> SiteSettings { get; set; }

        public DbSet<CircuitBentCMS.Models.MailSettings> MailSettings { get; set; }

        public DbSet<CircuitBentCMS.Models.ImageGallerySettings> ImageGallerySettings { get; set; }

        public DbSet<CircuitBentCMS.Models.StoreItem> StoreItems { get; set; }
        public DbSet<CircuitBentCMS.Models.StoreItemImage> StoreItemImages { get; set; }
        public DbSet<CircuitBentCMS.Models.StoreSettings> StoreSettings { get; set; }

        public DbSet<CircuitBentCMS.Models.Page> Pages { get; set; }

        public DbSet<CircuitBentCMS.Models.SocialMedia> SocialMedias { get; set; }

        public DbSet<CircuitBentCMS.Models.Image> Images { get; set; }

        public DbSet<CircuitBentCMS.Models.File> Files { get; set; }

        public DbSet<CircuitBentCMS.Models.Font> Fonts { get; set; }

        public DbSet<CircuitBentCMS.Models.ImageSlider> ImageSliders { get; set; }
        public DbSet<CircuitBentCMS.Models.ImageSliderSettings> ImageSliderSettings { get; set; }
        public DbSet<CircuitBentCMS.Models.ImageSliderImage> ImageSliderImages { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CircuitBentCMS.Models
{
    public class ImageGallerySettings
    {
        public int ImageGallerySettingsId { get; set; }
        public string ImageGalleryStyle { get; set; }
        public int ThumbnailWidth { get; set; }
        public int ThumbnailHeight { get; set; }
    }
}
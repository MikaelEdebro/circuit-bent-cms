using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircuitBentCMS.Models
{
    public class ImageGallerySettingsViewModel
    {
        public IEnumerable<Image> Images { get; set; }
        public ImageGallerySettings ImageGallerySettings { get; set; }
        public IList<SelectListItem> ImageGalleryTypes { get; set; }

        public ImageGallerySettingsViewModel()
        {
            ImageGalleryTypes = new List<SelectListItem>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class ImageGalleryViewModel
    {
        public IList<Image> Images { get; set; }
        public ImageGallerySettings ImageGallerySettings { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class ImageSliderImage
    {
        public int ImageSliderImageId { get; set; }
        public int ImageSliderId { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }

        public virtual ImageSlider ImageSlider { get; set; }
    }
}
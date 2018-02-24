using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class ImageSlider
    {
        public int ImageSliderId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Title { get; set; }

        public virtual IList<ImageSliderImage> ImageSliderImages { get; set; }
    }
}
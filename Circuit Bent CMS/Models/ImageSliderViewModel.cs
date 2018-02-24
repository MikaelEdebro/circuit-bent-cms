using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircuitBentCMS.Models
{
    public class ImageSliderViewModel
    {
        public IEnumerable<ImageSlider> ImageSliders { get; set; }
        public ImageSliderSettings ImageSliderSettings { get; set; }
        public IList<SelectListItem> TransitionModes { get; set; }

        public ImageSliderViewModel()
        {
            TransitionModes = new List<SelectListItem>();
        }
    }
}
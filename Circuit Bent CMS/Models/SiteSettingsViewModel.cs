using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class SiteSettingsViewModel
    {
        public SiteSettings SiteSettings { get; set; }
        public IList<ImageSlider> ImageSliders { get; set; }
    }
}
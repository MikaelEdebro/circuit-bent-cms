using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class ImageSliderSettings
    {
        public int ImageSliderSettingsId { get; set; }
        public int ImageSliderOnHomepage { get; set; }
        public string TransitionMode { get; set; }
        public int TransitionSpeed { get; set; }

    }
}
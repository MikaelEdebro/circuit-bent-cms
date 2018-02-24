using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircuitBentCMS.Models
{
    public class StoreViewModel
    {
        // get store items
        public IList<CircuitBentCMS.Models.StoreItem> StoreItems { get; set; }

        // get store settings
        public StoreSettings StoreSettings { get; set; }

        // the image slider to display in the shop
        public int TransitionSpeed { get; set; }

        // a list of all available image sliders to display in the settings panel
        public IList<ImageSlider> ImageSliders { get; set; }
    }
}
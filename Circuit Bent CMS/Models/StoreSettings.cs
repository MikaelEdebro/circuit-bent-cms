using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class StoreSettings
    {
        public int StoreSettingsId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(1, 10)]
        public int ProductsInRow { get; set; }

        public int ThumbnailHeight { get; set; }

        public int ImageSliderId { get; set; }
    }
}
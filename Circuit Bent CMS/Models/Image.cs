using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Photographer { get; set; }
        public string PhotographerUrl { get; set; }
        public int Order { get; set; }
    }
}
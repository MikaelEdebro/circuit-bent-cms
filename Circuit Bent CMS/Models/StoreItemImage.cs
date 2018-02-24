using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class StoreItemImage
    {
        public int StoreItemImageId { get; set; }
        public string ImageUrl { get; set; }
        public int StoreItemId { get; set; }

        public virtual StoreItem StoreItem { get; set; }
    }
}
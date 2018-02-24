using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class NavigationViewModel
    {
        public IList<Page> Pages { get; set; }
        public string MobileImage { get; set; }
    }
}
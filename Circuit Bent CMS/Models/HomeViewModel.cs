using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class HomeViewModel
    {
        public Page Page { get; set; }
        public IList<Page> SubPages { get; set; }
        public string MainPageTitle { get; set; }
    }
}
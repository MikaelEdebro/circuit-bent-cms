using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CircuitBentCMS.Models;

namespace CircuitBentCMS.Models
{
    public class EventViewModel
    {
        public IList<Show> Events { get; set; }
        public Page PageInfo { get; set; }
    }
}
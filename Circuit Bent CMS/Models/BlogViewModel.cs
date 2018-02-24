using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CircuitBentCMS.Models;

namespace CircuitBentCMS.Models
{
    public class BlogViewModel
    {
        public List<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
    }
}
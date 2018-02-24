using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircuitBentCMS.Models
{
    public class Font
    {
        public int FontId { get; set; }

        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string Name { get; set; }

        public string FontFamily { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string CssCode { get; set; }
    }
}
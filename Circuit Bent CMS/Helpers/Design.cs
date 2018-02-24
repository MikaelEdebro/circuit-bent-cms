using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CircuitBentCMS.Models;
using System.Web.Mvc;

namespace CircuitBentCMS.Helpers
{
    public static class Design
    {
        private static CircuitBentCMSContext context = new CircuitBentCMSContext();

        public static List<SelectListItem> GetFonts()
        {
            var selectItems = context.Fonts.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            }).ToList();

            return selectItems;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CircuitBentCMS
{
    public class RouteConfig
    {
        private Models.CircuitBentCMSContext context = new Models.CircuitBentCMSContext();

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 "sendMail", // Route name
                 "email/sendmail", // URL with parameters
                 new { controller = "Email", action = "SendMail" }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            routes.MapRoute(
                 "blogArchive", // Route name
                 "blog/archive", // URL with parameters
                 new { controller = "Blog", action = "Archive" }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            routes.MapRoute(
                 "blogSlug", // Route name
                 "blog/{slug}", // URL with parameters
                 new { controller = "Blog", action = "Index", slug = UrlParameter.Optional }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            routes.MapRoute(
                 "shopDetails", // Route name
                 "shop/{slug}", // URL with parameters
                 new { controller = "Shop", action = "Details" }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            routes.MapRoute(
                 "shop", // Route name
                 "shop", // URL with parameters
                 new { controller = "Shop", action = "Index" }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            routes.MapRoute(
                 "events", // Route name
                 "events", // URL with parameters
                 new { controller = "Home", action = "Events" }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            routes.MapRoute(
                 "gallery", // Route name
                 "gallery", // URL with parameters
                 new { controller = "Home", action = "Gallery" }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            
            routes.MapRoute(
                 "subPage", // Route name
                 "{slug}/{subPageSlug}", // URL with parameters
                 new { controller = "Home", action = "Index", slug = UrlParameter.Optional, subPageSlug = UrlParameter.Optional }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            routes.MapRoute(
                 "slug", // Route name
                 "{slug}", // URL with parameters
                 new { controller = "Home", action = "Index", slug = UrlParameter.Optional }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
            routes.MapRoute(
                 "Default", // Route name
                 "{controller}/{action}/{slug}", // URL with parameters
                 new { controller = "Home", action = "Index", slug = UrlParameter.Optional }, // Parameter defaults
                 new string[] { "CircuitBentCMS.Controllers" }
            );
        }
    }
}
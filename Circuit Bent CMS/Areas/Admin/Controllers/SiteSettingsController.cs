using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;
using System.IO;

namespace CircuitBentCMS.Areas.Admin.Controllers
{   
    [Authorize]
    public class SiteSettingsController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        public ViewResult Index()
        {
            var siteSettings =  context.SiteSettings.FirstOrDefault();
            return View(siteSettings);
        }
        
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(HttpPostedFileBase Favicon)
        {
            
            var siteSettings = context.SiteSettings.FirstOrDefault();
            siteSettings.Title = Request.Form["Title"];
            siteSettings.MetaDescription = Request.Form["MetaDescription"];
            siteSettings.FooterText = Request.Form["FooterText"];
            siteSettings.GoogleAnalytics = Request.Form["GoogleAnalytics"];

            context.Entry(siteSettings).State = EntityState.Modified;
            context.SaveChanges();

            // display a friendly success message
            TempData["StatusMessage"] = "The changes were saved successfully!";
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
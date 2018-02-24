using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;

namespace CircuitBentCMS.Areas.Admin.Controllers
{   
    [Authorize]
    public class MailSettingsController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        //
        // GET: /MailSettings/

        public ViewResult Index()
        {
            MailSettings mailSettings = context.MailSettings.FirstOrDefault();
            return View(mailSettings);
        }


        [HttpPost]
        public ActionResult Create(MailSettings mailsettings)
        {
            if (ModelState.IsValid)
            {
                context.MailSettings.Add(mailsettings);
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "The changes were saved successfully!<br /><br />If you have changed the e-mail settings it is highly recommended to try the contact form to make sure it works.";
                TempData["MessageDuration"] = 6000;

                return RedirectToAction("Index");  
            }

            return View(mailsettings);
        }
        
        
        [HttpPost]
        public ActionResult Edit(MailSettings mailsettings)
        {
            if (ModelState.IsValid)
            {
                context.Entry(mailsettings).State = EntityState.Modified;
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "The changes were saved successfully!<br /><br />If you have changed the e-mail settings it is highly recommended to try the contact form to make sure it works.";
                TempData["MessageDuration"] = 6000;

                return RedirectToAction("Index");
            }
            return View(mailsettings);
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
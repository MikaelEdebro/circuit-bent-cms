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
    public class SocialMediaController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        //
        // GET: /SocialMedia/

        public ViewResult Index()
        {
            SocialMedia socialmedia = context.SocialMedias.FirstOrDefault();
            return View(socialmedia);
        }


        //
        // POST: /SocialMedia/Edit/5
        [HttpPost]
        public ActionResult Edit(SocialMedia socialmedia)
        {
            if (ModelState.IsValid)
            {
                context.Entry(socialmedia).State = EntityState.Modified;
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "The changes were saved successfully!";
                                
            }
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
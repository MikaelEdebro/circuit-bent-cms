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
    public class ShowsController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        //
        // GET: /Shows/

        public ViewResult Index()
        {
            // fetch the shows and return them
            var shows = context.Shows.OrderBy(a => a.Date).ToList();
            return View(shows);
        }

        //
        // GET: /Shows/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Shows/Create

        [HttpPost]
        public ActionResult Create(Show show)
        {
            if (ModelState.IsValid)
            {
                context.Shows.Add(show);
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "Show added!";
                
                return RedirectToAction("Index");  
            }

            return View(show);
        }
        
        //
        // GET: /Shows/Edit/5
 
        public ActionResult Edit(int id)
        {
            Show show = context.Shows.Single(x => x.ShowId == id);
            return View(show);
        }

        //
        // POST: /Shows/Edit/5

        [HttpPost]
        public ActionResult Edit(Show show)
        {
            if (ModelState.IsValid)
            {
                context.Entry(show).State = EntityState.Modified;
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "The changes were saved successfully!";

                return RedirectToAction("Index");
            }
            return View(show);
        }

        //
        // GET: /Shows/Delete/5
 
        public ActionResult Delete(int id)
        {
            Show show = context.Shows.Single(x => x.ShowId == id);
            return View(show);
        }

        //
        // POST: /Shows/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Show show = context.Shows.Single(x => x.ShowId == id);
            context.Shows.Remove(show);
            context.SaveChanges();

            // display a friendly success message
            TempData["StatusMessage"] = "Show deleted!";

            return RedirectToAction("Index");
        }

        public ActionResult CancelShow(int id)
        {
            var show = context.Shows.Find(id);

            try
            {
                show.Cancelled = true;
                context.Entry(show).State = EntityState.Modified;
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "Show cancelled!";
            }
            catch { }

            return RedirectToAction("Index");
        }

        public ActionResult ReinstateShow(int id)
        {
            var show = context.Shows.Find(id);

            try
            {
                show.Cancelled = false;
                context.Entry(show).State = EntityState.Modified;
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "Show reinstated!";
            }
            catch { }

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
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
    public class BlogsController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        //
        // GET: /Blogs/

        public ViewResult Index()
        {
            return View(context.Blogs.OrderByDescending(a => a.Date).ToList());
        }


        //
        // GET: /Blogs/Create

        public ActionResult Create()
        {
            Blog blog = new Blog();
            //blog.EnableComments = true;
            return View(blog);
        } 

        //
        // POST: /Blogs/Create

        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            blog.Date = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                context.Blogs.Add(blog);
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "Blog post created successfully!";

                return RedirectToAction("Edit", new { id = blog.BlogId });  
            }

            return View(blog);
        }
        
        //
        // GET: /Blogs/Edit/5
 
        public ActionResult Edit(int id)
        {
            Blog blog = context.Blogs.Single(x => x.BlogId == id);
            return View(blog);
        }

        //
        // POST: /Blogs/Edit/5

        [HttpPost]
        public ActionResult Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                context.Entry(blog).State = EntityState.Modified;
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "The changes were saved successfully!";

                return RedirectToAction("Edit", new { id = blog.BlogId });
            }
            return View(blog);
        }

        //
        // GET: /Blogs/Delete/5
 
        public ActionResult Delete(int id)
        {
            Blog blog = context.Blogs.Single(x => x.BlogId == id);
            return View(blog);
        }

        //
        // POST: /Blogs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = context.Blogs.Single(x => x.BlogId == id);
            context.Blogs.Remove(blog);
            context.SaveChanges();
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
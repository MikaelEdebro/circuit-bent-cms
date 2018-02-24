using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;

namespace CircuitBentCMS.Controllers
{
    public class BlogController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();
        //
        // GET: /Blog/

        public ActionResult Index(string slug = "")
        {
            // get the latest blog posts
            BlogViewModel BVM = new BlogViewModel();
            BVM.Blogs = context.Blogs.OrderByDescending(a => a.Date).Take(10).ToList();
            
            // check if there is any blog posts. if not, redirect to the home page
            if (BVM.Blogs.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            // get a specific blog post that matches the slug
            if (!String.IsNullOrEmpty(slug))
            {
                BVM.Blog = (from s in context.Blogs.AsEnumerable()
                           where CustomHelpers.CreateSlug(s.Headline) == slug
                           select s).SingleOrDefault();
            }
            
            // show the last blog post if the id isn't there, or if the requested id doesn't exist
            if (BVM.Blog == null)
            {
                // redirect to the most recent blog post
                return RedirectToAction("Index", new { slug = CustomHelpers.CreateSlug(BVM.Blogs.FirstOrDefault().Headline) });
            }

            // return the blog post
            return View(BVM);
        }

        public ActionResult Archive()
        {
            var blogPosts = context.Blogs.OrderByDescending(a => a.Date);

            return View(blogPosts);
        }
    }
}

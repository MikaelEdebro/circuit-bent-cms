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
    public class PagesController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        //
        // GET: /Pages/

        public ViewResult Index()
        {
            var pages = context.Pages.OrderBy(a => a.Order).ToList();

            return View(pages);
        }

        //
        // GET: /Pages/Create

        public ActionResult Create()
        {
            Page page = new Page();
            return View(page);
        } 

        //
        // POST: /Pages/Create

        [HttpPost]
        public ActionResult Create(Page page, HttpPostedFileBase socialImage)
        {
            if (ModelState.IsValid)
            {
                page.LastUpdated = DateTime.Now;

                // perfor some basic verification that it is an int
                int subPageId = Convert.ToInt32(page.SubPageToPageId);

                // set the order by counting the current pages
                // also need to check if it's a main page or a sub page
                if (subPageId == 0)
                {
                    // update the order for the remaining pages
                    // this is because the order is not correct otherwise
                    var pages = context.Pages.Where(a => a.SubPageToPageId == 0).OrderBy(a => a.Order).ToList();
                    for (int i = 0; i < pages.Count; i++)
                    {
                        pages[i].Order = i + 1;
                    }

                    context.SaveChanges();

                    page.Order = context.Pages.Where(a => a.SubPageToPageId == 0).ToList().Count + 1;
                }
                else
                {
                    // update the order for the remaining pages
                    // this is because the order is not correct otherwise
                    var pages = context.Pages.Where(a => a.SubPageToPageId == subPageId).OrderBy(a => a.Order).ToList();
                    for (int i = 0; i < pages.Count; i++)
                    {
                        pages[i].Order = i + 1;
                    }

                    context.SaveChanges();

                    page.Order = context.Pages.Where(a => a.SubPageToPageId == subPageId).ToList().Count + 1;
                }

                // allowed file extensions
                string[] fileExt = { ".png", ".jpg", ".gif", ".jpeg" };

                // save the social media image
                if (socialImage != null && socialImage.ContentLength > 0)
                {
                    try
                    {
                        // make sure that the file extension is among the allowed
                        if (Array.IndexOf(fileExt, Path.GetExtension(socialImage.FileName.ToLower())) < 0)
                        {
                            throw new Exception("Social media image was in an unsupported format. Only images (JPEG, GIF, PNG) allowed.");
                        }

                        var path = Path.Combine(Server.MapPath("~/Images/PageImages"), CustomHelpers.RemoveSwedishChars(socialImage.FileName));
                        page.SocialMediaImage = CustomHelpers.RemoveSwedishChars(socialImage.FileName);
                        socialImage.SaveAs(path);
                    }
                    catch (Exception e)
                    {
                        TempData["ErrorMessage"] = e.Message;
                    }
                }

                context.Pages.Add(page);
                context.SaveChanges();
                                
                // display a friendly success message
                TempData["StatusMessage"] = "Page created successfully!";

                // if the user added a special page, redirect back to index
                // this is because there isn't really much to continue editing except the title
                if (!String.IsNullOrEmpty(page.CustomPage))
                {
                    return RedirectToAction("Index");
                }

                // the user added a blank page, therefore we redirect back to the edit page
                return RedirectToAction("Edit", new { id = page.PageId }); 
            }

            return View(page);
        }
        
        //
        // GET: /Pages/Edit/5
 
        public ActionResult Edit(int id)
        {
            Page page = context.Pages.Single(x => x.PageId == id);

            return View(page);
        }

        //
        // POST: /Pages/Edit/5

        [HttpPost]
        public ActionResult Edit(Page page, HttpPostedFileBase socialImage)
        {
            var path = Server.MapPath("~/Images/PageImages");

            if (ModelState.IsValid)
            {
                page.LastUpdated = DateTime.Now;

                // allowed file extensions
                string[] fileExt = { ".png", ".jpg", ".gif", ".jpeg" };

                // save the social media image
                if (socialImage != null && socialImage.ContentLength > 0)
                {
                    try
                    {
                        // make sure that the file extension is among the allowed
                        if (Array.IndexOf(fileExt, Path.GetExtension(socialImage.FileName.ToLower())) < 0)
                        {
                            throw new Exception("Social media image was in an unsupported format. Only images (JPEG, GIF, PNG) allowed.");
                        }

                        // delete the current image
                        if (!String.IsNullOrEmpty(page.SocialMediaImage))
                        {
                            if (System.IO.File.Exists(Path.Combine(path, page.SocialMediaImage))) { System.IO.File.Delete(Path.Combine(path, page.SocialMediaImage)); }
                        }
                        

                        page.SocialMediaImage = CustomHelpers.RemoveSwedishChars(socialImage.FileName);
                        socialImage.SaveAs(Path.Combine(Server.MapPath("~/Images/PageImages"), CustomHelpers.RemoveSwedishChars(socialImage.FileName)));
                    }
                    catch (Exception e)
                    {
                        TempData["ErrorMessage"] = e.Message;
                    }
                }

                // update the page
                context.Entry(page).State = EntityState.Modified;
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "The changes were saved successfully!";

                // if the user added a special page, redirect back to index
                // this is because there isn't really much to continue editing except the title
                if (!String.IsNullOrEmpty(page.CustomPage))
                {
                    return RedirectToAction("Index");
                }

                // redirect to the same edit page in case the user wants to make more changes
                if (!String.IsNullOrEmpty(Request.QueryString["scripts"]))
                {
                    return RedirectToAction("Edit", new { id = page.PageId, scripts = "true" });
                }

                return RedirectToAction("Edit", new { id = page.PageId });
            }


            // ModelState not valid
            return View(page);
        }

        [HttpPost]
        public ActionResult SetAsHomepage(int id)
        {
            var page = context.Pages.Single(a => a.PageId == id);

            if (page == null)
            {
                return Json(new { success = false, message = "An error occurred. Couldn't set the page as home." });
            }
            
            // make sure that only one page is set as homepage
            // loop through and set all pages to HomePage = false
            foreach (var item in context.Pages)
            {
                item.HomePage = false;
            }
            
            // update requested page
            page.HomePage = true;

            context.Entry(page).State = EntityState.Modified;
            context.SaveChanges();

            return Json(new { success = true, message = String.Format("The page \"{0}\" is now set as your home page.", page.Title) });
        }



        // change image order, move images up or down in the "play list"
        [HttpPost]
        public ActionResult UpdateOrder(string arrId)
        {
            // counter for the order
            int i = 1;

            // split the array that got passed along, and loop through the values
            foreach (string id in arrId.Split(','))
            {
                // find the entity with the matching id
                var page = context.Pages.AsEnumerable().Single(a => a.PageId == Convert.ToInt32(id));
                // update order
                page.Order = i;

                // save changes
                context.Entry(page).State = EntityState.Modified;
                context.SaveChanges();

                // update counter
                i++;
            }
            return Json(new { success = true });
        }


        //
        // GET: /Pages/Delete/5
 
        public ActionResult Delete(int id)
        {
            var page = context.Pages.Single(x => x.PageId == id);
            return View(page);
        }

        //
        // POST: /Pages/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // remove the page itself
            var page = context.Pages.Single(x => x.PageId == id);

            // check if there is any sub pages
            var subPages = context.Pages.Where(a => a.SubPageToPageId == page.PageId).ToList();

            // if there is any sub pages, make them to main pages
            if (subPages.Count > 0)
            {
                foreach (var item in subPages)
                {
                    item.SubPageToPageId = 0;
                }
            }

            // delete social media image from disk
            if (!String.IsNullOrEmpty(page.SocialMediaImage))
            {
                var path = Path.Combine(Server.MapPath("~/Images/PageImages"), page.SocialMediaImage);
                if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
            }

            context.Pages.Remove(page);
            context.SaveChanges();

            TempData["StatusMessage"] = "Page deleted successfully!";

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SetAsMainPage(int id)
        {
            var page = context.Pages.Single(a => a.PageId == id);

            // return false if the page doesn't exist
            if (page == null) { return Json(new { success = false }); }

            var pages = context.Pages.Where(a => a.SubPageToPageId == 0).OrderBy(a => a.Order).ToList();
            
            // update order for pages
            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].Order = i + 1;
                context.Entry(pages[i]).State = EntityState.Modified;
                context.SaveChanges();
            }

            // make the page the last in the order
            page.Order = pages.Count + 1;

            // make it as a main page
            page.SubPageToPageId = 0;

            context.Entry(page).State = EntityState.Modified;
            context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult SetAsSubPage(int id, int subPageId)
        {
            var page = context.Pages.Single(a => a.PageId == id);

            // return false if the page doesn't exist
            if (page == null) { return Json(new { success = false }); }

            // update the order of the main pages
            var pages = context.Pages.Where(a => a.SubPageToPageId == subPageId).OrderBy(a => a.Order).ToList();

            // update order for pages
            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].Order = i + 1;
                context.Entry(pages[i]).State = EntityState.Modified;
                context.SaveChanges();
            }

            // make the page the last in the order
            page.Order = pages.Count + 1;

            // make it as a main page
            page.SubPageToPageId = subPageId;

            context.Entry(page).State = EntityState.Modified;
            context.SaveChanges();

            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult DeleteSocialMediaImage(int id)
        {
            var page = context.Pages.Single(a => a.PageId == id);

            if (page != null)
            {
                try
                {
                    // delete image from disk
                    var path = Path.Combine(Server.MapPath("~/Images/PageImages"), page.SocialMediaImage);
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, errorMessage = e.Message });
                }

                page.SocialMediaImage = "";

                // remove image from db
                context.Entry(page).State = EntityState.Modified;
                context.SaveChanges();
            }

            return Json(new { success = true });
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
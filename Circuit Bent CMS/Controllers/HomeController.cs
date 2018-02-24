using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;

namespace CircuitBentCMS.Controllers
{
    public class HomeController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        public ActionResult Index(string slug, string subPageSlug)
        {
            HomeViewModel HVM = new HomeViewModel();

            // try to fetch a sub page, if there is a sub page slug supplied
            if (!String.IsNullOrEmpty(subPageSlug))
            {
                HVM.Page = (from s in context.Pages.AsEnumerable()
                        where CustomHelpers.CreateSlug(s.Title) == subPageSlug
                        select s).SingleOrDefault();
            }
            else
            {
                // no slug supplied, use the page set as home
                if (String.IsNullOrEmpty(slug))
                {
                    // get the page that is set to home
                    HVM.Page = context.Pages.SingleOrDefault(a => a.HomePage);

                    // get the image slider settings to find out if we should output an image slider
                    var imageSliderSettings = context.ImageSliderSettings.FirstOrDefault();
                    if (imageSliderSettings != null && imageSliderSettings.ImageSliderOnHomepage != 0)
                    {
                        // add the images in the viewbag
                        var imageSliderImages = context.ImageSliderImages
                                        .Where(a => a.ImageSliderId == imageSliderSettings.ImageSliderOnHomepage)
                                        .OrderBy(a => a.Order).ToList();
                        if (imageSliderImages.Count > 0)
                        {
                            ViewBag.ImageSliderImages = imageSliderImages;
                        }
                    }

                    // if the page is a special page (blog, shop etc), we need to redirect instead of returning the page
                    if (!String.IsNullOrEmpty(HVM.Page.CustomPage))
                    {
                        return RedirectToRoute(HVM.Page.CustomPage);
                    }
                }
                else
                {
                    // try to fetch a page that matches the slug
                    HVM.Page = (from s in context.Pages.AsEnumerable()
                            where CustomHelpers.CreateSlug(s.Title) == slug
                            select s).SingleOrDefault();

                }
            }

            // if the page isn't found, display error msg and redirect to home
            if (HVM.Page == null)
            {
                TempData["ErrorMessage"] = "Page not found. The page has probably been removed, or the link was incorrect.";

                // clear the slugs and redirect to index
                return RedirectToAction("Index", new { slug = String.Empty, subPageSlug = String.Empty });
            }

            // populate the sub pages to show in a menu
            // show the menus both on the main page, but also on all the sub pages
            // SubPageToPageId = 0 means that it is a main page
            if (HVM.Page.SubPageToPageId == 0)
            {
                HVM.MainPageTitle = HVM.Page.Title;
                HVM.SubPages = context.Pages.Where(a => a.ShowOnMenu && a.SubPageToPageId == HVM.Page.PageId).OrderBy(a => a.Order).ToList();
            }
            else
            {
                HVM.MainPageTitle = context.Pages.Where(a => a.PageId == HVM.Page.SubPageToPageId).FirstOrDefault().Title;
                HVM.SubPages = context.Pages.Where(a => a.ShowOnMenu && a.SubPageToPageId == HVM.Page.SubPageToPageId).OrderBy(a => a.Order).ToList();
            }

            
            
            // return the view model
            return View(HVM);
        }

        //
        // GET /events
        public ActionResult Events()
        {
            EventViewModel EVM = new EventViewModel();
            // check if the user have added a "Events" page
            EVM.PageInfo = context.Pages.SingleOrDefault(a => a.CustomPage == "events");
            
            EVM.Events = context.Shows.OrderByDescending(a => a.Date).ToList();
            return View(EVM);
        }

        //
        // GET /gallery
        public ActionResult Gallery()
        {
            // check if the user have added a "Events" page
            var pageTitle = context.Pages.SingleOrDefault(a => a.CustomPage == "imagegallery");

            // if page doesn't exist, use the generic page title
            ViewBag.PageTitle = (pageTitle == null) ? "Gallery" : pageTitle.Title;

            ImageGalleryViewModel IGVM = new ImageGalleryViewModel();
            IGVM.Images = context.Images.OrderBy(a => a.Order).ToList();
            IGVM.ImageGallerySettings = context.ImageGallerySettings.FirstOrDefault();

            return View(IGVM);
        }

    }
}

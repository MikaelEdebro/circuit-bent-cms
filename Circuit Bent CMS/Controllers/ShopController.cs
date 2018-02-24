using CircuitBentCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircuitBentCMS.Controllers
{
    public class ShopController : Controller
    {
        private CircuitBentCMSContext context = new Models.CircuitBentCMSContext();

        public ActionResult Index()
        {
            var pageTitle = context.Pages.FirstOrDefault(a => a.CustomPage == "shop");

            ViewBag.PageTitle = (pageTitle == null) ? "Shop" : pageTitle.Title;

            StoreViewModel SVM = new StoreViewModel();
            SVM.StoreItems = context.StoreItems.Where(a => !a.SoldOut).OrderBy(a => a.Order).ToList();
            SVM.StoreSettings = context.StoreSettings.FirstOrDefault();
            SVM.ImageSliders = null;
            SVM.TransitionSpeed = context.ImageSliderSettings.FirstOrDefault().TransitionSpeed;

            if (SVM.StoreSettings.ImageSliderId != 0)
            {
                var imageSliderImages = context.ImageSliderImages
                                    .Where(a => a.ImageSliderId == SVM.StoreSettings.ImageSliderId)
                                    .OrderBy(a => a.Order).ToList();
                if (imageSliderImages.Count > 0)
                {
                    ViewBag.ImageSliderImages = imageSliderImages;
                }
            }
            return View(SVM);
        }

        public ActionResult Details(string slug)
        {
            var storeItem = (   from s in context.StoreItems.AsEnumerable()
                                where CustomHelpers.CreateSlug(s.Title) == slug
                                select s).FirstOrDefault();

            if (storeItem == null)
            {
                TempData["ErrorMessage"] = "Item not found.";

                return RedirectToAction("Index");
            }

            return View(storeItem);
        }

    }
}

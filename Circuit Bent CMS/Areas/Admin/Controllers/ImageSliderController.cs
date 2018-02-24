using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;
using ImageResizer;
using System.IO;

namespace CircuitBentCMS.Areas.Admin.Controllers
{
    [Authorize]
    public class ImageSliderController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        //
        // GET: /Admin/Files/
        public ActionResult Index()
        {
            ImageSliderViewModel ISVM = new ImageSliderViewModel();
            ISVM.ImageSliderSettings = context.ImageSliderSettings.FirstOrDefault();
            ISVM.ImageSliders = context.ImageSliders;
            ISVM.TransitionModes.Add(new SelectListItem { Value = "horizontal", Text = "Slide horizontal" });
            ISVM.TransitionModes.Add(new SelectListItem { Value = "vertical", Text = "Slide vertical" });
            ISVM.TransitionModes.Add(new SelectListItem { Value = "fade", Text = "Fade" });

            return View(ISVM);

        }

        [HttpPost]
        public ActionResult ImageUpload(IEnumerable<HttpPostedFileBase> files)
        {
            // allowed file extensions
            string[] fileExt = { ".png", ".jpg", ".gif", ".jpeg" };
            var imageSliderId = Convert.ToInt32(Request.Form["ImageSliderId"]);
            var addSliderTitle = Request.Form["AddSliderTitle"];

            // check if the user have selected an existing slider, or wants to create a new
            try
            {
                if (!String.IsNullOrEmpty(addSliderTitle))
                {
                    // make sure that there isn't already a slider with the same name
                    var checkIfSliderExists = context.ImageSliders.SingleOrDefault(a => a.Title == addSliderTitle);
                    if (checkIfSliderExists != null)
                    {
                        throw new Exception("There already exists an image slider with that name. Please select another name.");
                    }

                    // add new image slider to db
                    ImageSlider imageSlider = new ImageSlider
                    {
                        Title = addSliderTitle
                    };
                    context.ImageSliders.Add(imageSlider);
                    context.SaveChanges();

                    // set the image slider id to the newly created
                    imageSliderId = imageSlider.ImageSliderId;
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Index");
            }

            foreach (var fileBase in files)
            {
                // check if the image is valid
                if (fileBase != null && fileBase.ContentLength > 0)
                {
                    try
                    {
                        // make sure that the file extension is among the allowed
                        if (Array.IndexOf(fileExt, Path.GetExtension(fileBase.FileName.ToLower())) < 0)
                        {
                            throw new Exception("Image was in an unsupported format. Only images (JPEG, GIF, PNG) allowed.");
                        }

                        // make sure that the image slider has a correct value
                        if (imageSliderId == 0)
                        {
                            throw new Exception("You have to either select an existing image slider, or create a new one.");
                        }

                        var fileName = Path.GetFileName(fileBase.FileName);

                        // fix bug that iPad/iPhone names all the files "image.jpg"
                        var userAgent = HttpContext.Request.UserAgent.ToLower();
                        if (userAgent.Contains("iphone;") || userAgent.Contains("ipad;"))
                        {
                            fileName = String.Format("{0}{1}", CustomHelpers.GeneratePassword(6), Path.GetExtension(fileBase.FileName.ToLower()));
                        }

                        var path = Server.MapPath("~/Images/ImageSlider/");

                        // instantiate object
                        var image = new CircuitBentCMS.Models.ImageSliderImage();
                        image.ImageUrl = fileName;
                        image.LinkUrl = "";
                        image.Title = "";
                        image.ImageSliderId = imageSliderId;

                        // find out how many images there are and get the image order
                        image.Order = context.ImageSliderImages
                                        .Where(a => a.ImageSliderId == imageSliderId)
                                        .ToList().Count + 1;

                        context.ImageSliderImages.Add(image);
                        context.SaveChanges();

                        fileBase.SaveAs(Path.Combine(path, fileName));

                        // if the image is larger than 0,5 mb, shrink it
                        if (fileBase.ContentLength > 500000)
                        {
                            ImageBuilder.Current.Build(Path.Combine(path, fileName), Path.Combine(path, fileName), new ResizeSettings("maxwidth=1600&maxheight=1600&crop=auto"));
                        }

                        ImageBuilder.Current.Build(Path.Combine(path, fileName), Path.Combine(path, "thumb_" + fileName), new ResizeSettings("maxwidth=180&maxheight=80&crop=auto"));
                    }
                    catch (Exception e)
                    {
                        TempData["ErrorMessage"] = e.Message;
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var image = context.ImageSliderImages.FirstOrDefault(a => a.ImageSliderImageId == id);

            if (image == null)
            {
                TempData["ErrorMessage"] = "Not found";
                return RedirectToAction("Index");
            }

            return View(image);
        }

        [HttpPost]
        public ActionResult Edit(ImageSliderImage image)
        {
            if (ModelState.IsValid)
            {
                context.Entry(image).State = EntityState.Modified;
                context.SaveChanges();

                TempData["StatusMessage"] = "Changes saved successfully!";

                return RedirectToAction("Index");
            }
            
            return View(image);
        }

        public ActionResult Delete(int id)
        {
            var image = context.ImageSliderImages.SingleOrDefault(a => a.ImageSliderImageId == id);

            // if the file exists in the database
            if (image != null)
            {
                // try to remove files from disk
                try
                {
                    // remove image from database
                    context.ImageSliderImages.Remove(image);
                    context.SaveChanges();

                    var path = Path.Combine(Server.MapPath("~/Images/ImageSlider"), image.ImageUrl);
                    // if the file exists, delete it
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                    path = Path.Combine(Server.MapPath("~/Images/ImageSlider"), "thumb_" + image.ImageUrl);
                    // if the file exists, delete it
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                    TempData["StatusMessage"] = "File deleted successfully!";
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "File does not exist!";
            }

            // update the order for the remaining store items
            // this is because the order is not correct otherwise
            var images = context.ImageSliderImages.OrderBy(a => a.Order).ToList();
            for (int i = 0; i < images.Count; i++)
            {
                images[i].Order = i + 1;
            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // change image order, move images up or down in the "play list"
        [HttpPost]
        public ActionResult UpdateOrder(string arrId, int imageSliderId)
        {
            // counter for the order
            int i = 1;
            
            // split the array that got passed along, and loop through the values
            foreach (string id in arrId.Split(','))
            {
                // find the entity with the matching id
                // only select the records that have the correct ImageSliderId 
                var imageSliderImage = context.ImageSliderImages.Where(a => a.ImageSliderId == imageSliderId).AsEnumerable().Single(a => a.ImageSliderImageId == Convert.ToInt32(id));
                // update order
                imageSliderImage.Order = i;

                // save changes
                context.Entry(imageSliderImage).State = EntityState.Modified;
                context.SaveChanges();

                // update counter
                i++;
            }
            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult Settings(ImageSliderSettings imageSliderSettings)
        {
            context.Entry(imageSliderSettings).State = EntityState.Modified;
            context.SaveChanges();

            TempData["StatusMessage"] = "Changes saved successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteImageSlider(int id)
        {
            

            try
            {
                var imageSliderImages = context.ImageSliderImages.Where(a => a.ImageSliderId == id);
                // loop through the images and delete them
                if (imageSliderImages != null)
                {
                    foreach (var image in imageSliderImages)
                    {
                        var path = Path.Combine(Server.MapPath("~/Images/ImageSlider"), image.ImageUrl);
                        // if the file exists, delete it
                        if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                        path = Path.Combine(Server.MapPath("~/Images/ImageSlider"), "thumb_" + image.ImageUrl);
                        // if the file exists, delete it
                        if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                        // remove from db
                        context.ImageSliderImages.Remove(image);
                    }
                    context.SaveChanges();
                }

                // get image slider
                var imageSlider = context.ImageSliders.SingleOrDefault(a => a.ImageSliderId == id);

                context.ImageSliders.Remove(imageSlider);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { success = false, errorMessage = e.Message });
            }

            return Json(new { success = true, id = id });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
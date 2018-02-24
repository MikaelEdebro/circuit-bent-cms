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
    public class ImageGalleryController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        //
        // GET: /Admin/Files/
        public ActionResult Index()
        {
            var IGSVM = new ImageGallerySettingsViewModel();
            IGSVM.ImageGallerySettings = context.ImageGallerySettings.FirstOrDefault();

            // populate the drop down
            IGSVM.ImageGalleryTypes.Add(new SelectListItem { Value = "slider", Text = "Image slider" });
            IGSVM.ImageGalleryTypes.Add(new SelectListItem { Value = "thumbnails", Text = "Thumbnails with Lightbox" });

            // fetch the images and return them
            IGSVM.Images = context.Images.OrderBy(a => a.Order);
            return View(IGSVM);
        }

        [HttpPost]
        public ActionResult ImageUpload(IEnumerable<HttpPostedFileBase> files)
        {
            TempData["ErrorMessage"] = "";
            // allowed file extensions
            string[] fileExt = { ".png", ".jpg", ".gif", ".jpeg" };

            foreach (var fileBase in files)
            {
                // make sure the image exists
                if (fileBase != null && fileBase.ContentLength > 0)
                {
                    try
                    {
                        // make sure that the file extension is among the allowed
                        if (Array.IndexOf(fileExt, Path.GetExtension(fileBase.FileName.ToLower())) < 0)
                        {
                            throw new Exception("Image was in an unsupported format. Only images (JPEG, GIF, PNG) allowed.");
                        }

                        var fileName = Path.GetFileName(fileBase.FileName);

                        // fix bug that iPad/iPhone names all the files "image.jpg"
                        var userAgent = HttpContext.Request.UserAgent.ToLower();
                        if (userAgent.Contains("iphone;") || userAgent.Contains("ipad;"))
                        {
                            fileName = String.Format("{0}{1}", CustomHelpers.GeneratePassword(6), Path.GetExtension(fileBase.FileName.ToLower()));
                        }

                        var path = Server.MapPath("~/Images/ImageGallery/");

                        // instantiate object
                        var image = new CircuitBentCMS.Models.Image();
                        image.ImageUrl = fileName;
                        image.Title = "";
                        image.Photographer = "";
                        image.PhotographerUrl = "";

                        // find out how many images there are and get the image order
                        image.Order = context.Images.ToList().Count + 1;

                        context.Images.Add(image);
                        context.SaveChanges();

                        fileBase.SaveAs(Path.Combine(path, fileName));
                        
                        // if the image is larger than 0,5 mb, shrink it
                        if (fileBase.ContentLength > 500000)
                        {
                            ImageBuilder.Current.Build(Path.Combine(path, fileName), Path.Combine(path, fileName), new ResizeSettings("maxwidth=1600&maxheight=1600&crop=auto"));
                        }
                        
                        // get the size of the thumbnails from ImageGallerySettings table
                        var imageGallerySettings = context.ImageGallerySettings.FirstOrDefault();
                        ImageBuilder.Current.Build(Path.Combine(path, fileName), Path.Combine(path, "thumb_" + fileName), new ResizeSettings(String.Format("width={0}&height={1}&crop=auto", imageGallerySettings.ThumbnailWidth, imageGallerySettings.ThumbnailHeight)));
                    }
                    catch (Exception e)
                    {
                        TempData["ErrorMessage"] = e.Message;
                    }
                }
            }

            TempData["StatusMessage"] = "Images uploaded successfully!<br><br>If you want to supply additional information (like title, photographer etc) about the image, just edit it.";
            TempData["MessageDuration"] = 5000;

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var image = context.Images.FirstOrDefault(a => a.ImageId == id);

            if (image != null)
            {
                return View(image);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                context.Entry(image).State = EntityState.Modified;
                context.SaveChanges();

                TempData["StatusMessage"] = "Changes saved successfully!";
            }
            else
            {
                return View(image);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var image = context.Images.SingleOrDefault(a => a.ImageId == id);

            // if the file exists in the database
            if (image != null)
            {
                // try to remove files from disk
                try
                {
                    // remove image from database
                    context.Images.Remove(image);
                    context.SaveChanges();
                    
                    var path = Path.Combine(Server.MapPath("~/Images/ImageGallery"), image.ImageUrl);
                    // if the file exists, delete it
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                    path = Path.Combine(Server.MapPath("~/Images/ImageGallery"), "thumb_" + image.ImageUrl);
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
            var images = context.Images.OrderBy(a => a.Order).ToList();
            for (int i = 0; i < images.Count; i++)
            {
                images[i].Order = i + 1;
            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }


        // show the shows on the menu and add the text to the page settings
        [HttpPost]
        public ActionResult Settings(ImageGallerySettings imageGallerySettings, string ImageGalleryName)
        {
            if (ModelState.IsValid)
            {
                context.Entry(imageGallerySettings).State = EntityState.Modified;
                context.SaveChanges();
            }
            
            // display a friendly success message
            TempData["StatusMessage"] = "The changes were saved successfully!";

            return RedirectToAction("Index");
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
                var imageGalleryImage = context.Images.AsEnumerable().Single(a => a.ImageId == Convert.ToInt32(id));
                // update order
                imageGalleryImage.Order = i;

                // save changes
                context.Entry(imageGalleryImage).State = EntityState.Modified;
                context.SaveChanges();

                // update counter
                i++;
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;
using System.IO;
using ImageResizer;

namespace CircuitBentCMS.Areas.Admin.Controllers
{   
    [Authorize]
    public class StoreController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();

        //
        // GET: /StoreItems/

        public ViewResult Index()
        {
            StoreViewModel SVM = new StoreViewModel();
            SVM.StoreItems = context.StoreItems.OrderBy(a => a.Order).ToList();
            SVM.StoreSettings = context.StoreSettings.FirstOrDefault();

            // get list of image sliders to show as dropdown
            SVM.ImageSliders = context.ImageSliders.ToList();

            return View(SVM);
        }

        //
        // GET: /StoreItems/Create

        public ActionResult Create()
        {
            StoreItem item = new StoreItem();
            item.StoreItemImages = new List<StoreItemImage>();
            return View(item);
        } 

        //
        // POST: /StoreItems/Create

        [HttpPost]
        public ActionResult Create(StoreItem storeItem, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                // update the counter
                storeItem.Order = context.StoreItems.ToList().Count + 1;

                // add the store item
                context.StoreItems.Add(storeItem);
                context.SaveChanges();

                // allowed file extensions
                string[] fileExt = { ".png", ".jpg", ".gif", ".jpeg" };

                foreach (var fileBase in files)
                {
                    // save the background image
                    if (fileBase != null && fileBase.ContentLength > 0)
                    {
                        try
                        {
                            // make sure that the file extension is among the allowed
                            if (Array.IndexOf(fileExt, Path.GetExtension(fileBase.FileName.ToLower())) < 0)
                            {
                                throw new Exception(String.Format("File extension not allowed. Only these files are allowed:<br><br>{0}", string.Join(", ", fileExt)));
                            }

                            var fileName = Path.GetFileName(fileBase.FileName);

                            // fix bug that iPad/iPhone names all the files "image.jpg"
                            if (fileName == "image.jpg")
                            {
                                fileName = String.Format("{0}{1}", CustomHelpers.GeneratePassword(6), Path.GetExtension(fileBase.FileName.ToLower()));
                            }

                            var path = Server.MapPath("~/Images/Store/");

                            // instantiate object
                            var image = new CircuitBentCMS.Models.StoreItemImage();
                            image.ImageUrl = fileName;
                            image.StoreItemId = storeItem.StoreItemId;
                            
                            context.StoreItemImages.Add(image);

                            fileBase.SaveAs(Path.Combine(path, fileName));

                            ImageBuilder.Current.Build(Path.Combine(path, fileName), Path.Combine(path, "thumb_" + fileName), new ResizeSettings("maxwidth=250&maxheight=250&crop=auto"));
                        }
                        catch (Exception e)
                        {
                            TempData["ErrorMessage"] = e.Message;
                        }
                    }
                }

                // save the context
                context.SaveChanges();

                TempData["StatusMessage"] = "Store item added successfully!";
                
                return RedirectToAction("Index");  
            }

            return View(storeItem);
        }
        
        //
        // GET: /StoreItems/Edit/5
 
        public ActionResult Edit(int id)
        {
            StoreItem storeitem = context.StoreItems.Single(x => x.StoreItemId == id);
            return View(storeitem);
        }

        //
        // POST: /StoreItems/Edit/5

        [HttpPost]
        public ActionResult Edit(StoreItem storeItem, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                // allowed file extensions
                string[] fileExt = { ".png", ".jpg", ".gif", ".jpeg" };

                foreach (var fileBase in files)
                {
                    // save the background image
                    if (fileBase != null && fileBase.ContentLength > 0)
                    {
                        try
                        {
                            // make sure that the file extension is among the allowed
                            if (Array.IndexOf(fileExt, Path.GetExtension(fileBase.FileName.ToLower())) < 0)
                            {
                                throw new Exception(String.Format("File extension not allowed. Only these files are allowed:<br><br>{0}", string.Join(", ", fileExt)));
                            }

                            var fileName = Path.GetFileName(fileBase.FileName);

                            // fix bug that iPad/iPhone names all the files "image.jpg"
                            if (fileName == "image.jpg")
                            {
                                fileName = String.Format("{0}{1}", CustomHelpers.GeneratePassword(6), Path.GetExtension(fileBase.FileName.ToLower()));
                            }

                            var path = Server.MapPath("~/Images/Store/");

                            // instantiate object
                            var image = new CircuitBentCMS.Models.StoreItemImage();
                            image.ImageUrl = fileName;
                            image.StoreItemId = storeItem.StoreItemId;

                            context.StoreItemImages.Add(image);

                            fileBase.SaveAs(Path.Combine(path, fileName));

                            ImageBuilder.Current.Build(Path.Combine(path, fileName), Path.Combine(path, "thumb_" + fileName), new ResizeSettings("maxwidth=240&maxheight=240&crop=auto"));
                        }
                        catch (Exception e)
                        {
                            TempData["ErrorMessage"] = e.Message;
                        }
                    }
                }
                
                context.Entry(storeItem).State = EntityState.Modified;
                context.SaveChanges();

                // display a friendly success message
                TempData["StatusMessage"] = "The changes were saved successfully!";

                return RedirectToAction("Edit", new { id = storeItem.StoreItemId });
            }
            return View(storeItem);
        }

        //
        // GET: /StoreItems/Delete/5
 
        public ActionResult Delete(int id)
        {
            StoreItem storeitem = context.StoreItems.Single(x => x.StoreItemId == id);
            return View(storeitem);
        }

        //
        // POST: /StoreItems/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreItem storeitem = context.StoreItems.Single(x => x.StoreItemId == id);

            // loop through the images and delete
            foreach (var image in context.StoreItemImages.Where(a => a.StoreItemId == id))
            {
                try
                {
                    // delete image from disk
                    var path = Path.Combine(Server.MapPath("~/Images/Store"), image.ImageUrl);
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                    // delete thumbnail from disk
                    path = Path.Combine(Server.MapPath("~/Images/Store"), "thumb_" + image.ImageUrl);
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                }
                catch { }

                // remove image from db
                context.StoreItemImages.Remove(image);
            }

            // remove the item
            context.StoreItems.Remove(storeitem);
            context.SaveChanges();

            // update the order for the remaining store items
            // this is because the order is not correct otherwise
            var storeItems = context.StoreItems.OrderBy(a => a.Order).ToList();
            for (int i = 0; i < storeItems.Count; i++)
            {
                storeItems[i].Order = i + 1;
            }

            // save the context
            context.SaveChanges();

            TempData["StatusMessage"] = "Store item deleted successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            var image = context.StoreItemImages.Single(a => a.StoreItemImageId == id);

            if (image != null)
            {
                try
                {
                    // check if the image if being used in another store item
                    var multipleImages = context.StoreItemImages.Where(a => a.ImageUrl == image.ImageUrl).ToList();

                    // only remove the image if no other store item is using the same image
                    if (multipleImages.Count < 2)
                    {
                        // delete image from disk
                        var path = Path.Combine(Server.MapPath("~/Images/Store"), image.ImageUrl);
                        if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                        // delete thumbnail from disk
                        path = Path.Combine(Server.MapPath("~/Images/Store"), "thumb_" + image.ImageUrl);
                        if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, errorMessage = e.Message });
                }

                // remove image from db
                context.StoreItemImages.Remove(image);
                context.SaveChanges();
            }

            return Json(new { success = true });
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
                var storeItem = context.StoreItems.AsEnumerable().Single(a => a.StoreItemId == Convert.ToInt32(id));
                // update order
                storeItem.Order = i;

                // save changes
                context.Entry(storeItem).State = EntityState.Modified;
                context.SaveChanges();

                // update counter
                i++;
            }
            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult Settings(StoreSettings storeSettings)
        {
            // update settings
            context.Entry(storeSettings).State = EntityState.Modified;
            context.SaveChanges();

            TempData["StatusMessage"] = "Changes saved successfully!";

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
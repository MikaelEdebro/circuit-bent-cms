using CircuitBentCMS.Models;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircuitBentCMS.Areas.Admin.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {

        private CircuitBentCMSContext context = new CircuitBentCMSContext();
        
        //
        // GET: /Admin/Files/
        public ActionResult Index()
        {
            var files = context.Files.OrderByDescending(a => a.LastUpdated);
            return View(files);
        }

        public ActionResult InsertFile()
        {
            // use another layout
            ViewBag.UsePopupLayout = true;

            var files = context.Files.OrderByDescending(a => a.LastUpdated);
            return View("~/Areas/Admin/Views/Files/Index.cshtml", files);
        }

        [HttpPost]
        public ActionResult FileUpload(IEnumerable<HttpPostedFileBase> files)
        {
            // allowed file extensions
            string[] fileExt = {    ".png", ".jpg", ".gif", ".jpeg", ".zip", ".rar", ".flac", ".pdf", 
                                    ".doc", ".docx", ".psd", ".eps", ".ai", ".wav", ".tar.gz", ".odt",
                                    ".txt", ".rtf", ".mp3", ".ogg", ".mov", ".mkv", ".mpeg", ".mpeg2", ".html"
                               };
            string[] imageExt = { ".png", ".jpg", ".gif", ".jpeg" };
                

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
                        var userAgent = HttpContext.Request.UserAgent.ToLower();
                        if (userAgent.Contains("iphone;") || userAgent.Contains("ipad;"))
                        {
                            fileName = String.Format("{0}{1}", CustomHelpers.GeneratePassword(6), Path.GetExtension(fileBase.FileName.ToLower()));
                        }

                        var path = Server.MapPath("~/UserUploads");

                        // instantiate object
                        var file = new CircuitBentCMS.Models.File();
                        file.FileName = fileName;
                        file.FileExtension = Path.GetExtension(fileBase.FileName);
                        file.LastUpdated = DateTime.Now;

                        context.Files.Add(file);
                        context.SaveChanges();

                        fileBase.SaveAs(Path.Combine(path, fileName));

                        // check if it's an image, in that case save a thumbnail
                        if (Array.IndexOf(imageExt, Path.GetExtension(fileBase.FileName.ToLower())) > -1)
                        {
                            // if the image is larger than 0,5 mb, shrink it
                            if (fileBase.ContentLength > 500000)
                            {
                                ImageBuilder.Current.Build(Path.Combine(path, fileName), Path.Combine(path, fileName), new ResizeSettings("maxwidth=1600&maxheight=1600&crop=auto"));
                            }
                            // creat thumbnail
                            ImageBuilder.Current.Build(Path.Combine(path, fileName), Path.Combine(path, "thumb_" + fileName), new ResizeSettings("maxwidth=120&maxheight=120&crop=auto"));
                        }
                    }
                    catch (Exception e)
                    {
                        TempData["ErrorMessage"] = e.Message;
                    }
                }
            }

            // if the upload happened from the popup window, redirect to the view withouth an alternative layout
            if (String.IsNullOrEmpty(Request.Form["UsePopupLayout"]))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("InsertFile");
            }
            
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var file = context.Files.SingleOrDefault(a => a.FileId == id);

            // if the file exists in the database
            if (file != null)
            {
                // try to remove files from disk
                try
                {
                    var path = Path.Combine(Server.MapPath("~/UserUploads"), file.FileName);
                    // if the file exists, delete it
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                    // also check if there is a thumbnail and remove it
                    path = Path.Combine(Server.MapPath("~/UserUploads"), "thumb_" + file.FileName);
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                    // remove image from database
                    context.Files.Remove(file);
                    context.SaveChanges();

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

            if (TempData["ErrorMessage"] != null) { return Json(new { success = false }); }

            return Json(new { success = true, fileId = id });
        }

    }
}

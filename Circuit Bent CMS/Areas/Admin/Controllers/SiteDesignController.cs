using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;
using System.IO;
using System.Data.Entity.Validation;

namespace CircuitBentCMS.Areas.Admin.Controllers
{
    [Authorize]
    public class SiteDesignController : Controller
    {
        private CircuitBentCMSContext context = new CircuitBentCMSContext();
        //
        // GET: /Admin/Styles/

        public ActionResult Index()
        {
            SiteSettings siteSettings = context.SiteSettings.FirstOrDefault();
            
            // get the fonts to populate the Dropdown
            ViewBag.FontSelect = Helpers.Design.GetFonts();
            if (siteSettings == null)
            {
                siteSettings = new SiteSettings();
            }
            return View(siteSettings);
        }


        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase BackgroundImage, HttpPostedFileBase HeaderImage, HttpPostedFileBase FooterImage, HttpPostedFileBase MobileImage, HttpPostedFileBase Favicon)
        {
            // allowed file extensions
            string[] fileExt = { ".png", ".jpg", ".gif", ".jpeg", ".ico" };

            // get the site settings
            var siteSettings = context.SiteSettings.FirstOrDefault();

            // populate from form values
            siteSettings.BackgroundColor = Request.Form["background-color"];
            siteSettings.ContainerColor = Request.Form["container-color"];
            siteSettings.ContainerWidth = Convert.ToInt32(Request.Form["container-width"]);

            // set container opacity, and check that the values are between 0 - 100
            int containerOpacity = Convert.ToInt32(Request.Form["ContainerOpacity"]);
            if (containerOpacity > 100)
            {
                siteSettings.ContainerOpacity = 100;
            }
            else if (containerOpacity < 0)
            {
                siteSettings.ContainerOpacity = 0;
            }
            else
            {
                siteSettings.ContainerOpacity = containerOpacity;
            }
                
            siteSettings.HeadingColor = Request.Form["heading-color"];
            siteSettings.TextColor = Request.Form["text-color"];
            siteSettings.LinkColor = Request.Form["link-color"];

            siteSettings.HeadingFont = Request.Form["heading-font"];
            siteSettings.HeadingSize = Request.Form["heading-size"];
            siteSettings.TextFont = Request.Form["text-font"];
            siteSettings.TextSize = Request.Form["text-size"];
            
            //siteSettings.BackgroundImageFullScreen = false;

            if (String.IsNullOrEmpty(Request.Form["BackgroundImageFullScreen"]))
            {
                siteSettings.BackgroundImageFullScreen = false;
            }
            else
            {
                siteSettings.BackgroundImageFullScreen = true;
            }
            

            // save the background image
            if (BackgroundImage != null && BackgroundImage.ContentLength > 0)
            {
                try
                {
                    // make sure that the file extension is among the allowed
                    if (Array.IndexOf(fileExt, Path.GetExtension(BackgroundImage.FileName.ToLower())) < 0)
                    {
                        throw new Exception("Background image was in an unsupported format. Only images (JPEG, GIF, PNG) allowed.");
                    }

                    // delete the existing background image
                    DeleteImage("background");

                    var path = Path.Combine(Server.MapPath("~/Images/PageSettings"), BackgroundImage.FileName);
                    siteSettings.BackgroundImage = BackgroundImage.FileName;
                    BackgroundImage.SaveAs(path);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                }

            }

            // save the header image
            if (HeaderImage != null && HeaderImage.ContentLength > 0)
            {
                try
                {
                    // make sure that the file extension is among the allowed
                    if (Array.IndexOf(fileExt, Path.GetExtension(HeaderImage.FileName.ToLower())) < 0)
                    {
                        throw new Exception("Header image was in an unsupported format. Only images (JPEG, GIF, PNG) allowed.");
                    }

                    // delete the existing header image
                    DeleteImage("header");

                    var path = Path.Combine(Server.MapPath("~/Images/PageSettings"), HeaderImage.FileName);
                    siteSettings.HeaderImage = HeaderImage.FileName;
                    HeaderImage.SaveAs(path);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                }

            }

            // save the footer image
            if (FooterImage != null && FooterImage.ContentLength > 0)
            {
                try
                {
                    // make sure that the file extension is among the allowed
                    if (Array.IndexOf(fileExt, Path.GetExtension(FooterImage.FileName.ToLower())) < 0)
                    {
                        throw new Exception("Footer image was in an unsupported format. Only images (JPEG, GIF, PNG) allowed.");
                    }

                    // delete the existing footer image
                    DeleteImage("footer");

                    var path = Path.Combine(Server.MapPath("~/Images/PageSettings"), FooterImage.FileName);
                    siteSettings.FooterImage = FooterImage.FileName;
                    FooterImage.SaveAs(path);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                }
            }

            // save the mobile image
            if (MobileImage != null && MobileImage.ContentLength > 0)
            {
                try
                {
                    // make sure that the file extension is among the allowed
                    if (Array.IndexOf(fileExt, Path.GetExtension(MobileImage.FileName.ToLower())) < 0)
                    {
                        throw new Exception("Mobile image was in an unsupported format. Only images (JPEG, GIF, PNG) allowed.");
                    }

                    // delete the existing footer image
                    DeleteImage("mobile");

                    var path = Path.Combine(Server.MapPath("~/Images/PageSettings"), MobileImage.FileName);
                    siteSettings.MobileImage = MobileImage.FileName;
                    MobileImage.SaveAs(path);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                }
            }

            // save the favicon image
            if (Favicon != null && Favicon.ContentLength > 0)
            {
                try
                {
                    // make sure that the file extension is among the allowed
                    if (Array.IndexOf(fileExt, Path.GetExtension(Favicon.FileName.ToLower())) < 0)
                    {
                        throw new Exception("Favicon was in an unsupported format. Only images (JPEG, GIF, PNG, ICO) allowed.");
                    }

                    // delete the existing footer image
                    DeleteImage("favicon");

                    var path = Path.Combine(Server.MapPath("~/Images/PageSettings"), Favicon.FileName);
                    siteSettings.Favicon = Favicon.FileName;
                    Favicon.SaveAs(path);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                }
            }

            context.Entry(siteSettings).State = EntityState.Modified;
            context.SaveChanges();
            
            TempData["StatusMessage"] = "Changes saved successfully!";

            return RedirectToAction("Index");
        }

        
        public ActionResult DeleteImage(string type)
        {
            var pageSettings = context.SiteSettings.FirstOrDefault();

            // path to save the images
            var path = Server.MapPath("~/Images/PageSettings/");

            try
            {
                switch (type)
                {
                    case "background":
                        // delete background
                        if (System.IO.File.Exists(path + pageSettings.BackgroundImage)) { System.IO.File.Delete(path + pageSettings.BackgroundImage); }

                        // update the model
                        pageSettings.BackgroundImage = "";
                        break;
                    case "header":
                        // delete header image
                        if (System.IO.File.Exists(path + pageSettings.HeaderImage)) { System.IO.File.Delete(path + pageSettings.HeaderImage); }

                        // update the model
                        pageSettings.HeaderImage = "";
                        break;
                    case "footer":
                        // delete footer image
                        if (System.IO.File.Exists(path + pageSettings.FooterImage)) { System.IO.File.Delete(path + pageSettings.FooterImage); }

                        // update the model
                        pageSettings.FooterImage = "";
                        break;
                    case "mobile":
                        // delete footer image
                        if (System.IO.File.Exists(path + pageSettings.MobileImage)) { System.IO.File.Delete(path + pageSettings.MobileImage); }

                        // update the model
                        pageSettings.MobileImage = "";
                        break;
                    case "favicon":
                        // delete footer image
                        if (System.IO.File.Exists(path + pageSettings.Favicon)) { System.IO.File.Delete(path + pageSettings.Favicon); }

                        // update the model
                        pageSettings.Favicon = "";
                        break;
                }

                context.Entry(pageSettings).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch { }

            // display a friendly success message
            TempData["StatusMessage"] = "Image deleted successfully!";
            
            return RedirectToAction("Index");
        }


        public ActionResult AddFont()
        {
            var fonts = context.Fonts.OrderBy(a => a.Name);
            return View(fonts);
        }

        [HttpPost]
        public ActionResult AddFont(Font font, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                // allowed file extensions
                string[] fileExt = { ".ttf", ".eot", ".woff", ".svg" };
                
                try
                {
                    foreach (var fileBase in files)
                    {
                       // save the background image
                        if (fileBase != null && fileBase.ContentLength > 0)
                        {
                            // make sure that the file extension is among the allowed
                            if (Array.IndexOf(fileExt, Path.GetExtension(fileBase.FileName)) < 0)
                            {
                                throw new Exception("Font was in an unsupported format. Only the file types .ttf, .eot, .woff and .svg are allowed.");
                            }

                            var fileName = Path.GetFileName(fileBase.FileName);

                            // fix bug that iPad/iPhone names all the files "image.jpg"
                            var userAgent = HttpContext.Request.UserAgent.ToLower();
                            if (userAgent.Contains("iphone;") || userAgent.Contains("ipad;"))
                            {
                                throw new Exception("Due to restrictions in iOS you can't add fonts from your mobile device. We recommend you to do this from a regular computer. Sorry for the inconvenience!");
                            }

                            var path = Path.Combine(Server.MapPath("~/Fonts"), fileName);

                            fileBase.SaveAs(path);
                        }
                    }

                    // remove any surrounding ' to add them later in the css
                    font.Name = font.Name.Replace("'", "");
                    font.FontFamily = String.Format("'{0}'", font.Name);

                    // save font to database
                    context.Fonts.Add(font);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Model not valid";
            }

            TempData["StatusMessage"] = "Font uploaded successfully!";
            return RedirectToAction("AddFont", new { closeWindow = "true" });
        }

        public ActionResult DeleteFont(int id)
        {
            var font = context.Fonts.SingleOrDefault(a => a.FontId == id);

            if (font != null)
            {
                try
                {
                    context.Fonts.Remove(font);
                    context.SaveChanges();

                    TempData["StatusMessage"] = String.Format("Font \"{0}\" deleted successfully!", font.Name);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = String.Format("Could not delete font. The following error occurred:<br /><br />{0}", e.Message);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Could not delete font. It seems it doesn't exist in the database.";
            }

            return RedirectToAction("AddFont");
        }

        public ActionResult LoadFontCss(string fontName)
        {
            var fontCss = context.Fonts.FirstOrDefault(a => a.Name == fontName);
            string strCss = "";

            if (fontCss == null)
                return Json(new { cssCode = "", fontName = "Error" });
            
            if (!String.IsNullOrEmpty(fontCss.CssCode))
                strCss = fontCss.CssCode.Replace("url('", "url('/Fonts/");

            return Json(new { cssCode = strCss, fontName = fontCss.FontFamily  });
        }


        public ActionResult CustomCss()
        {
            var siteSettings = context.SiteSettings.FirstOrDefault();

            ViewBag.CustomCss = siteSettings.CustomCSS;
            ViewBag.CustomHtml = siteSettings.CustomHtml;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CustomCss(string CustomCss, string CustomHtml)
        {
            try
            {
                var sitesettings = context.SiteSettings.FirstOrDefault();
                sitesettings.CustomCSS = CustomCss;
                sitesettings.CustomHtml = CustomHtml;

                context.Entry(sitesettings).State = EntityState.Modified;
                context.SaveChanges();

                TempData["StatusMessage"] = "Changes saved successfully!";
            }
            catch { }

            return RedirectToAction("CustomCss");
        }

    }
}

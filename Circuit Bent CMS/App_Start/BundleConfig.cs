using System.Web;
using System.Web.Optimization;

namespace CircuitBentCMS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;   //enable CDN support

            //add link to jquery on the CDN
            var jqueryCdnPath = "//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js";
            var jqueryUiCdnPath = "//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js";


            // SCRIPTS
            // ----------------------------------------
            bundles.Add(new ScriptBundle("~/bundles/scripts/jquery",
                        jqueryCdnPath).Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/scripts/jqueryui",
                        jqueryUiCdnPath).Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));


            // codemirror is a plugin that makes a textarea into a programming editor with code coloration
            bundles.Add(new ScriptBundle("~/bundles/scripts/codemirror").Include(
                        "~/Scripts/codemirror/lib/codemirror.js",
                        "~/Scripts/codemirror/mode/javascript/javascript.js",
                        "~/Scripts/codemirror/mode/css/css.js",
                        "~/Scripts/codemirror/mode/xml/xml.js"
                        ));
            bundles.Add(new StyleBundle("~/bundles/css/codemirror").Include(
                        "~/Scripts/codemirror/lib/codemirror.css",
                        "~/Scripts/codemirror/theme/neat.css"
                        ));

            // general scripts for the admin section
            // third party scripts for the admin section
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin").Include(
                        "~/Scripts/Admin.js"
                        ));

            // third party scripts for the admin section
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/components").Include(
                        "~/Content/bootstrap/js/bootstrap.min.js",
                        "~/Scripts/toastmessage/src/main/javascript/jquery.toastmessage.js",
                        "~/Scripts/fancybox/source/jquery.fancybox.pack.js",
                        "~/Scripts/jquery.ui.touch-punch.min.js"
                        ));

            // general scripts for the public site
            // third party scripts for the public site
            bundles.Add(new ScriptBundle("~/bundles/scripts/public").Include(
                        "~/Scripts/Public.js",
                        "~/Scripts/ContactForm.js"
                        ));

            // third party scripts for the admin section
            bundles.Add(new ScriptBundle("~/bundles/scripts/public/components").Include(
                        "~/Content/bootstrap/js/bootstrap.min.js",
                        "~/Scripts/toastmessage/src/main/javascript/jquery.toastmessage.js",
                        "~/Scripts/fancybox/source/jquery.fancybox.pack.js",
                        "~/Scripts/bxslider/jquery.bxslider.min.js"
                        ));

            

            // STYLESHEETS
            // -------------------------------------------
            // stylesheet for the public site
            bundles.Add(new StyleBundle("~/bundles/css/public").Include("~/Content/Site.css", new CssRewriteUrlTransform()));

            // third party stylesheets for the admin section
            bundles.Add(new StyleBundle("~/bundles/css/public/components")
                .Include("~/Content/bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/toastmessage/src/main/resources/css/jquery.toastmessage.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/fancybox/source/jquery.fancybox.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/bxslider/jquery.bxslider.css", new CssRewriteUrlTransform()));
            
            
            // stylesheet for the admin section
            bundles.Add(new StyleBundle("~/bundles/css/admin").Include("~/Content/Admin.css", new CssRewriteUrlTransform()));

            // third party stylesheets for the admin section
            bundles.Add(new StyleBundle("~/bundles/css/admin/components")
                .Include("~/Content/bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/toastmessage/src/main/resources/css/jquery.toastmessage.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/fancybox/source/jquery.fancybox.css", new CssRewriteUrlTransform()));

            

        }
    }
}
using System.Web;
using System.Web.Optimization;

namespace DemoCreate
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/uploadImage").Include(
                      "~/Scripts/site.uploadImage.js"));

            bundles.Add(new ScriptBundle("~/bundles/jcrop").Include(
                      "~/Scripts/jquery.Jcrop.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryform").Include(
                       "~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/vote").Include(
                        "~/Scripts/site.vote.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css").Include("~/Content/font-awesome.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/jcrop").Include(
                      "~/Content/jquery.Jcrop.css"));

            bundles.Add(new StyleBundle("~/Content/uploadImage").Include(
                      "~/Content/uploadImage.css"));
        }
    }
}

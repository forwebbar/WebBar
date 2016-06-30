using System.Web.Optimization;

namespace WebBar.Site.CompositionRoot
{
   public class BundleConfig
   {
      // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
      public static void RegisterBundles(BundleCollection bundles)
      {
         bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js"));

         bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                     "~/Scripts/jquery.validate*"));

         bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                     "~/Scripts/gridmvc*"));

         bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Scripts/modernizr-*"));

         bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                   "~/Scripts/bootstrap.js",
                   "~/Scripts/moment.js",
                   "~/Scripts/modal.js",
                   "~/Scripts/moment-with-locales.js",
                   "~/Scripts/bootstrap-datetimepicker.js",
                   "~/Scripts/respond.js"));

         bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/bootstrap-datetimepicker.css",
                   "~/Content/Gridmvc.css",
                   "~/Content/site.css",
                   "~/Content/signin.css",
                   "~/Content/normalize.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.watable").Include(
                   "~/Scripts/jquery.watable.js"));

         // Set EnableOptimizations to false for debugging. For more information,
         // visit http://go.microsoft.com/fwlink/?LinkId=301862
         BundleTable.EnableOptimizations = false;
      }
   }
}

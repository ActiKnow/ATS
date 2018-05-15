using System.Web;
using System.Web.Optimization;

namespace ATS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/plugins/jquery-3.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/plugins/popper.min.js",
                      "~/Scripts/plugins/bootstrap.min.js",
                     "~/Scripts/plugins/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                      "~/Scripts/plugins/pace.min.js",
                      "~/Scripts/plugins/chart.js",
                      "~/Scripts/plugins/bootstrap-datepicker.min.js",
                      "~/Scripts/plugins/bootstrap-notify.min.js",
                      "~/Scripts/plugins/moment.min.js",
                      "~/Scripts/plugins/fullcalendar.min.js",
                      "~/Scripts/plugins/jquery-ui.custom.min.js",
                      "~/Scripts/plugins/jquery.dataTables.min.js",
                      "~/Scripts/plugins/dataTables.bootstrap.min.js",
                      "~/Scripts/plugins/select2.min.js",
                      "~/Scripts/plugins/sweetalert.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/main.css"));
        }
    }
}

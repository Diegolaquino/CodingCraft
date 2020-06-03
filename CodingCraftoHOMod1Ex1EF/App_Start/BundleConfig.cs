using System.Web;
using System.Web.Optimization;

namespace CodingCraftoHOMod1Ex1EF
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/datatables-demo").Include(
                       "~/Scripts/demo/datatables-demo.js")
               );

            bundles.Add(new ScriptBundle("~/bundles/chart")
                        .Include("~/Scripts/demo/vendor/Chart.js")
                        .Include("~/Scripts/demo/vendor/Chart.bundle.js")
                        .Include("~/Scripts/demo/chart-area-demo.js")
                        .Include("~/Scripts/demo/chart-bar-demo.js")
                        .Include("~/Scripts/demo/chart-pie-demo.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/sb-admin").Include(
                        "~/Scripts/sb-admin-2.js")
                        .Include("~/Scripts/sb-admin-2.min.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryMin").Include(
                        "~/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatablesMin").Include(
                       "~/Scripts/DataTables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js"));

            

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                     "~/Scripts/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));


            bundles.Add(new StyleBundle("~/Content/site").Include(
                     "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                      "~/Content/admin/sb-admin-2.css")
                .Include("~/Content/admin/sb-admin-2.min.css")
                );

            bundles.Add(new StyleBundle("~/bundles/datatablesCss").Include(
                        "~/Content/DataTables/css/jquery.dataTables.min.css"));
        }
    }
}

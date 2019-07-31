using System.Web.Optimization;

namespace Aafp.Events.Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout-js").Include(
                        "~/Scripts/event.js",
                        "~/Scripts/jquery.scrollTo.min.js",
                        "~/Scripts/scroll-to-top.js",
                        "~/Scripts/highlight-input.js",
                        "~/Scripts/jquery.hotkeys.js",
                        "~/Scripts/search-bar.js",
                        "~/Scripts/keyboard-shortcuts.js"));

            bundles.Add(new ScriptBundle("~/bundles/search-results-js").Include(
                "~/Scripts/search-results.js",
                "~/Scripts/select2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/pending-registration-js").Include(
                "~/Scripts/table.js",
                "~/Scripts/tabs.js",
                "~/Scripts/jquery-masked-input.js",
                "~/Scripts/pending-registration.js"));

            bundles.Add(new ScriptBundle("~/bundles/edit-registration-js").Include(
                "~/Scripts/table.js",
                "~/Scripts/tabs.js",
                "~/Scripts/jquery-masked-input.js",
                "~/Scripts/edit-registration.js"));

            bundles.Add(new StyleBundle("~/Content/css/plugins-css").Include(
                 "~/Content/font-awesome.4.4.0.min.css",
                 "~/Content/jquery-ui.flick.1.11.4.min.css",
                 "~/Content/select2.4.0.1.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/batch-registration-js").Include(
                  "~/Scripts/batch-registration.js",
                  "~/Scripts/select2.min.js",
                  "~/Scripts/tableExport.js",
                  "~/Scripts/jquery.base64.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}

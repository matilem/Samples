using System.Web.Optimization;

namespace Aafp.MyCme.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/common")
                .Include("~/Scripts/App Scripts/analytics.js"));

            bundles.Add(new ScriptBundle("~/bundles/home")
                .Include("~/Scripts/App Scripts/analytics.js")
                .Include("~/Scripts/App Scripts/home.js"));

            bundles.Add(new ScriptBundle("~/bundles/liveActivity")
                .Include("~/Scripts/App Scripts/analytics.js")
                .Include("~/Scripts/App Scripts/liveactivity.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}

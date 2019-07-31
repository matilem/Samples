using System.Web.Optimization;

namespace Aafp.Events.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/intro-js")
                .Include("~/Scripts/intro.js"));

            bundles.Add(new ScriptBundle("~/bundles/warning-js")
                .Include("~/Scripts/intro.js")
                .Include("~/Scripts/warning.js"));

            bundles.Add(new ScriptBundle("~/bundles/contact-info-js")
                .Include("~/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.min.js")
                .Include("~/Scripts/jquery-masked-input.js")
                .Include("~/Scripts/contact-info.js"));

            bundles.Add(new ScriptBundle("~/bundles/session-registration-js")
                .Include("~/Scripts/session-animations.js")
                .Include("~/Scripts/session-registration.js"));

            bundles.Add(new ScriptBundle("~/bundles/conflicts-js")
                .Include("~/Scripts/session-animations.js")
                .Include("~/Scripts/conflicts.js"));

            bundles.Add(new ScriptBundle("~/bundles/edit-session-registration-js")
                .Include("~/Scripts/session-animations.js")
                .Include("~/Scripts/edit-session-registration.js"));

            bundles.Add(new ScriptBundle("~/bundles/confirmation-js")
                .Include("~/Scripts/jquery-masked-input.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.min.js")
                .Include("~/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.min.js")
                .Include("~/Scripts/confirmation.js"));

            bundles.Add(new ScriptBundle("~/bundles/navigation-js")
                .Include("~/Scripts/navigation.js"));

            bundles.Add(new ScriptBundle("~/bundles/payment-bar-js")
                .Include("~/Scripts/payment-calculation.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
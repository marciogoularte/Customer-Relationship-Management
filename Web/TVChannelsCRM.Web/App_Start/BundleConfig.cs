namespace TVChannelsCRM.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterScriptBundles(bundles);
            
            // Clear all items from the default ignore list to allow minified CSS and JavaScript files to be included in debug mode
            bundles.IgnoreList.Clear();
            // Add back the default ignore list rules sans the ones which affect minified files and debug mode
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap-theme-metro").Include(
                "~/Content/theme-styles/bootstrap-responsive.min.css",
                "~/Content/theme-styles/chosen.css",
                "~/Content/theme-styles/elfinder.min.css",
                "~/Content/theme-styles/elfinder.theme.css",
                "~/Content/theme-styles/font-awesome-ie7.min.css",
                "~/Content/theme-styles/font-awesome.min.css",
                "~/Content/theme-styles/fullcalendar.css",
                "~/Content/theme-styles/glyphicons.css",
                "~/Content/theme-styles/halflings.css",
                "~/Content/theme-styles/ie.css",
                "~/Content/theme-styles/ie9.css",
                "~/Content/theme-styles/jquery-ui-1.8.21.custom.css",
                "~/Content/theme-styles/jquery.cleditor.css",
                "~/Content/theme-styles/jquery.gritter.css",
                "~/Content/theme-styles/jquery.iphone.toggle.css",
                "~/Content/theme-styles/jquery.noty.css",
                "~/Content/theme-styles/noty_theme_default.css",
                "~/Content/theme-styles/style-forms.css",
                "~/Content/theme-styles/style-responsive.css",
                "~/Content/theme-styles/style.css",
                "~/Content/theme-styles/uniform.default.css",
                "~/Content/theme-styles/uploadify.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/theme-styles/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryUI").Include(
                "~/Content/jquery-ui.min.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.common-bootstrap.min.css",
                "~/Content/kendo/kendo.flat.min.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-theme-metro").Include(
                "~/Scripts/theme-scripts/jquery-1.9.1.min.js",
                "~/Scripts/theme-scripts/jquery-migrate-1.0.0.min.js",
                "~/Scripts/theme-scripts/jquery-ui-1.10.0.custom.min.js",
                "~/Scripts/theme-scripts/jquery.ui.touch-punch.js",
                "~/Scripts/theme-scripts/modernizr.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/theme-scripts/jquery.cookie.js",
                "~/Scripts/theme-scripts/fullcalendar.min.js",
                "~/Scripts/theme-scripts/jquery.dataTables.min.js",
                "~/Scripts/theme-scripts/excanvas.js",
                "~/Scripts/theme-scripts/jquery.flot.js",
                "~/Scripts/theme-scripts/jquery.flot.pie.js",
                "~/Scripts/theme-scripts/jquery.flot.stack.js",
                "~/Scripts/theme-scripts/jquery.flot.resize.min.js",
                "~/Scripts/theme-scripts/jquery.chosen.min.js",
                "~/Scripts/theme-scripts/jquery.uniform.min.js",
                "~/Scripts/theme-scripts/jquery.cleditor.min.js",
                "~/Scripts/theme-scripts/jquery.noty.js",
                "~/Scripts/theme-scripts/jquery.elfinder.min.js",
                "~/Scripts/theme-scripts/jquery.raty.min.js",
                "~/Scripts/theme-scripts/jquery.iphone.toggle.js",
                "~/Scripts/theme-scripts/jquery.uploadify-3.1.min.js",
                "~/Scripts/theme-scripts/jquery.gritter.min.js",
                "~/Scripts/theme-scripts/jquery.imagesloaded.js",
                "~/Scripts/theme-scripts/jquery.masonry.min.js",
                "~/Scripts/theme-scripts/jquery.knob.modified.js",
                "~/Scripts/theme-scripts/jquery.sparkline.min.js",
                "~/Scripts/theme-scripts/counter.js",
                "~/Scripts/theme-scripts/retina.js",
                "~/Scripts/theme-scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.min.js"));

            // Jquery bundles
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo/pako_deflate.min.js",
                "~/Scripts/kendo/jszip.min.js"));
        }
    }
}
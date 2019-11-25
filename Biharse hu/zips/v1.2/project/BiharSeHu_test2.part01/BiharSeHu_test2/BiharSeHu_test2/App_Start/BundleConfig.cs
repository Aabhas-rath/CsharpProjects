using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BiharSeHu_test2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/jarllax").Include(
                "~/Scripts/Jarllax/jarallax.js"));
            bundles.Add(new ScriptBundle("~/parallax").Include(
                "~/Scripts/parallax/parallax.min.js"));

            bundles.Add(new ScriptBundle("~/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap.bundle.js",
                "~/Scripts/popper.js"));
            bundles.Add(new StyleBundle("~/demo").Include(
                 "~/Content/demo.css"));
            
            bundles.Add(new StyleBundle("~/css").Include(
                 "~/Content/Site.css",
                 "~/Content/ui-bootstrap-csp.css"));
            bundles.Add(new StyleBundle("~/bootstrapCss").Include(
                "~/Content/bootstrap.css", 
                "~/Content/bootstrap-grid.css"));
            bundles.Add(new ScriptBundle("~/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-route.js"));
            BundleTable.EnableOptimizations = true;

        }
    }
}
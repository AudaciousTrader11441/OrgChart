﻿using System.Web;
using System.Web.Optimization;

namespace OrgChart
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery-ui.min.js", "~/Scripts/jquery.orgchart.js", "~/Scripts/jquery.slidereveal.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js", "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-grid.css", "~/Content/bootstrap-reboot.css", "~/Content/bootstrap.css",
                      "~/Content/site.css", "~/Content/OrgChartk.css", "~/Content/jquery.orgchart.css", "~/Content/font-awesome.min.css", "~/Content/Flip.css"));
        }
    }
}

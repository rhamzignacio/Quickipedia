﻿using System.Web;
using System.Web.Optimization;

namespace Quickipedia
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/vendors/bootstrap/dist/css/bootstrap.css",
                      "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                      "~/Content/vendors/nprogress/nprogress.css",
                      "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.css",
                      "~/Content/vendors/select2/dist/css/select2.min.css",
                      "~/Content/build/css/custom.css",                    
                      "~/Content/vendors/switchery/dist/switchery.min.css",
                      "~/Content/vendors/starrr/dist/starrr.cs",
                      "~/Content/vendors/growl/angular-growl.min.css",
                      "~/Content/vendors/iCheck/skins/flat/green.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Content/vendors/jquery/dist/jquery.min.js",
                "~/Content/vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/vendors/fastclick/lib/fastclick.js",
                "~/Content/vendors/nprogress/nprogress.js",
                "~/Content/vendors/Chart.js/dist/Chart.min.js",
                "~/Content/vendors/jquery-sparkline/dist/jquery.sparkline.min.js",
                "~/Content/vendors/Flot/jquery.flot.js",
                "~/Content/vendors/Flot/jquery.flot.pie.js",
                "~/Content/vendors/Flot/jquery.flot.time.js",
                "~/Content/vendors/Flot/jquery.flot.stack.js",
                "~/Content/vendors/Flot/jquery.flot.resize.js",
                "~/Content/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                "~/Content/vendors/flot-spline/js/jquery.flot.spline.min.js",
                "~/Content/vendors/flot.curvedlines/curvedLines.js",
                "~/Content/vendors/DateJS/build/date.js",
                "~/Content/vendors/moment/min/moment.min.js",
                "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/vendors/select2/dist/js/select2.full.min.js",
                "~/Content/build/js/custom.min.js",
                "~/Content/vendors/jquery.tagsinput/src/jquery.tagsinput.js",
                "~/Content/vendors/switchery/dist/switchery.min.js",
                "~/Scripts/angular.js",
                "~/Scripts/respond.js",
                "~/Scripts/angular-growl.min.js",
                "~/Content/vendors/autosize/dist/autosize.min.js",
                "~/Content/vendors/starrr/dist/starrr.js",
                "~/Content/vendors/bootstrap-wysiwyg/js/bootstrap-wysiwyg.min.js",
                "~/Content/vendors/jquery.hotkeys/jquery.hotkeys.js",
                "~/Content/angular-file-upload.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/App/App.js",
                "~/App/Controller/BasicInfo.js",
                "~/App/Controller/PricingAndFinancial.js",
                "~/App/Controller/Air.js",
                "~/App/Controller/Hotel.js",
                "~/App/Controller/Car.js",
                "~/App/Controller/Rail.js",
                "~/App/Controller/VisaDocumentation.js",
                "~/App/Controller/Mice.js",
                "~/App/Controller/VIP.js",
                "~/App/Controller/Login.js",
                "~/App/Controller/ClientBooker.js",
                "~/App/Controller/User.js",
                "~/App/Controller/Advisory.js",
                "~/App/Controller/ProfileManagement.js",
                "~/App/Controller/BookingProcess.js",
                "~/App/Controller/Ancillaries.js"
                ));
        }
    }
}

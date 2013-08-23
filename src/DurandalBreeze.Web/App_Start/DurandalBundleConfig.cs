using System;
using System.Web.Optimization;

namespace DurandalBreeze {
  public class DurandalBundleConfig {
    public static void RegisterBundles(BundleCollection bundles) {
      bundles.IgnoreList.Clear();
      AddDefaultIgnorePatterns(bundles.IgnoreList);

      bundles.Add(
        new ScriptBundle("~/scripts/vendor")
          .Include("~/Scripts/jquery-{version}.js")
          .Include("~/Scripts/knockout-{version}.js")
          .Include("~/Scripts/sammy-{version}.js")
          .Include("~/Scripts/bootstrap.min.js")
          .Include("~/Scripts/q.min.js")
          .Include("~/Scripts/breeze.min.js")
          .Include("~/Scripts/toastr.min.js")
          .Include("~/Scripts/jquery-migrate-{version}.min.js")
          .Include("~/Scripts/jquery.signalR-{version}.min.js")
        );

      bundles.Add(
        new StyleBundle("~/Content/css")
          .Include("~/Content/ie10mobile.css")
          .Include("~/Content/bootstrap.min.css")
          .Include("~/Content/bootstrap-responsive.min.css")
          .Include("~/Content/font-awesome.min.css")
		  .Include("~/Content/durandal.css")
          .Include("~/Content/app.css")
          .Include("~/Content/breezesample.css")
          .Include("~/Content/toastr.min.css")
        );
    }

    public static void AddDefaultIgnorePatterns(IgnoreList ignoreList) {
      if(ignoreList == null) {
        throw new ArgumentNullException("ignoreList");
      }

      ignoreList.Ignore("*.intellisense.js");
      ignoreList.Ignore("*-vsdoc.js");
      ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
      //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
      //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
    }
  }
}
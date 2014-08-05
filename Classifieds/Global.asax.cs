using Classifieds.Models;
using Classifieds.Models.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Classifieds
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Register custom model binder
           // ModelBinders.Binders.DefaultBinder = new ClassifiedsModelBinder();

            BundleTable.EnableOptimizations = false;
            //configure which subfolders to be searched for partial views
            ViewEngines.Engines.Add(new RazorViewEngine
            {
                PartialViewLocationFormats = new string[]
                    {
                        "~/Views/{1}/Partials/{0}.cshtml",
                        "~/Views/Shared/Search/{0}.cshtml"
                    }
            });
        }
    }
}

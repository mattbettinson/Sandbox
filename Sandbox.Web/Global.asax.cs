using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Sandbox.Web;
using System.Web.Hosting;
using Sandbox.Common;
using System.Reflection;


namespace Sandbox.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HostingEnvironment.RegisterVirtualPathProvider(
              new EmbeddedViewPathProvider(new Dictionary<string, Assembly>()
                {
                    {"~/Views/CommonShared/", typeof (Sandbox.Web.Mvc.Views.Class).Assembly}
                }));

            var viewEngine = new RazorViewEngine();
            viewEngine.FileExtensions = new[] { "cshtml" };
            viewEngine.ViewLocationFormats = new List<string>(viewEngine.ViewLocationFormats) { "~/Views/CommonShared/{0}.cshtml" }.ToArray();
            viewEngine.PartialViewLocationFormats = new List<string>(viewEngine.PartialViewLocationFormats) { "~/Views/CommonShared/{0}.cshtml" }.ToArray();
            viewEngine.MasterLocationFormats = new List<string>(viewEngine.MasterLocationFormats) { "~/Views/CommonShared/{0}.cshtml" }.ToArray();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(viewEngine);
        }
    }
}

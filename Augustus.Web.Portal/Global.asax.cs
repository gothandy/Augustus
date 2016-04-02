using Augustus.Web.Framework.ModelBinders;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Augustus.Web.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
    }
}

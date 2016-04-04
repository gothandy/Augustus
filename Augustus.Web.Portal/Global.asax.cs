using Augustus.Web.Framework.Filters;
using Augustus.Web.Framework.ModelBinders;
using System.Configuration;
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

            var useAzureAuth = (ConfigurationManager.AppSettings["crm:UseAzureAuth"] ?? "true") == "true";

            if (useAzureAuth)
            {
                GlobalFilters.Filters.Add(new AntiForgeryTokenAuthorizationFilter());
                GlobalFilters.Filters.Add(new PreventDuplicatePostActionFilter());
            }
       }
    }
}

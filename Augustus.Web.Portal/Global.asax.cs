using Augustus.Web.Framework.ModelBinders;
using Augustus.Web.Portal.Filters;
using System.Configuration;
using System.Web.Helpers;
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
                GlobalFilters.Filters.Add(new CrmAntiForgeryTokenFilter());
                GlobalFilters.Filters.Add(new PreventDuplicateRequestFilter());
            }
       }
    }
}

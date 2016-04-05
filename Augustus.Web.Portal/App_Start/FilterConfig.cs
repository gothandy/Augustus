using Augustus.Web.Config;
using Augustus.Web.Framework.Filters;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Portal
{
    public class FilterConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new RequireHttpsAttribute());

            if (AppSettings.UseAzureAuth)
            {
                filters.Add(new AntiForgeryTokenAuthorizationFilter());
                filters.Add(new PreventDuplicatePostActionFilter());
            }
        }
    }
}

using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CrmAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var useAzureAuth = (ConfigurationManager.AppSettings["crm:UseAzureAuth"] ?? "true") == "true";

            if (useAzureAuth)
            {
                return base.AuthorizeCore(httpContext);
            }
            else
            {
                return true;
            }
        }
    }
}
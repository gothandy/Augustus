using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CrmAuthorizeAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string useAzureAuth = ConfigurationManager.AppSettings["crm:UseAzureAuth"];

            if (useAzureAuth == "true" || useAzureAuth == null)
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
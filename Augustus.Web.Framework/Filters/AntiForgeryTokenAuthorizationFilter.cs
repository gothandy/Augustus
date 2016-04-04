using System.Net.Http;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Augustus.Web.Framework.Filters
{
    public class AntiForgeryTokenAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (IsHttpPostRequest(filterContext))
            {
                AntiForgery.Validate();
            }
        }

        private static bool IsHttpPostRequest(AuthorizationContext filterContext)
        {
            return filterContext.RequestContext.HttpContext.Request.HttpMethod == HttpMethod.Post.ToString();
        }

    }
}
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Framework.Filters
{
    public class PreventDuplicatePostActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsHttpPostRequest(filterContext))
            {
                doStuff(filterContext);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Nothing to do.
        }

        private static bool IsHttpPostRequest(ActionExecutingContext filterContext)
        {
            return filterContext.RequestContext.HttpContext.Request.HttpMethod == HttpMethod.Post.ToString();
        }

        private  void doStuff(ActionExecutingContext filterContext)
        {
            var currentToken = (HttpContext.Current.Request["__RequestVerificationToken"] ?? string.Empty).ToString();

            if (currentToken != string.Empty)
            {
                LockAndCheckLastProcessedToken(filterContext, currentToken);
            }
        }

        private static void LockAndCheckLastProcessedToken(ActionExecutingContext filterContext, string currentToken)
        {
            lock (HttpContext.Current.Session["LastProcessedToken"])
            {
                var lastToken = (HttpContext.Current.Session["LastProcessedToken"] ?? string.Empty).ToString();

                if (lastToken != string.Empty && lastToken == currentToken)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "The form was submitted twice.");
                }
                else
                {
                    HttpContext.Current.Session["LastProcessedToken"] = currentToken;
                }
            }
        }
    }
}

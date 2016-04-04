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
            if (HttpContext.Current.Request["__RequestVerificationToken"] != null)
            {
                CheckLastProcessedToken(filterContext, HttpContext.Current.Request["__RequestVerificationToken"].ToString());
            }
        }

        private static void CheckLastProcessedToken(ActionExecutingContext filterContext, string currentToken)
        {
            if (HttpContext.Current.Session["LastProcessedToken"] == null)
            {
                HttpContext.Current.Session["LastProcessedToken"] = currentToken;
            }
            else
            {
                LockLastProcessedToken(filterContext, currentToken);
            }
        }

        private static void LockLastProcessedToken(ActionExecutingContext filterContext, string currentToken)
        {
            lock (HttpContext.Current.Session["LastProcessedToken"])
            {
                var lastToken = HttpContext.Current.Session["LastProcessedToken"].ToString();

                if (lastToken == currentToken)
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

using System;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Filters
{
    // http://stackoverflow.com/questions/4250604/how-do-i-prevent-multiple-form-submission-in-net-mvc-without-using-javascript

    public class PreventDuplicateRequestFilter : IActionFilter
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
            // do nothing.
        }

        private static bool IsHttpPostRequest(ActionExecutingContext filterContext)
        {
            return filterContext.RequestContext.HttpContext.Request.HttpMethod == HttpMethod.Post.ToString();
        }

        private  void doStuff(ActionExecutingContext filterContext)
        {
            var currentTokenRequest = HttpContext.Current.Request["__RequestVerificationToken"];
            if (currentTokenRequest == null) return;
            var currentToken = currentTokenRequest.ToString();

            if (HttpContext.Current.Session["LastProcessedToken"] == null)
            {
                HttpContext.Current.Session["LastProcessedToken"] = currentToken;
                return;
            }

            lock (HttpContext.Current.Session["LastProcessedToken"])
            {
                var lastToken = HttpContext.Current.Session["LastProcessedToken"].ToString();

                if (lastToken == currentToken)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "The form was submitted twice.");
                    return;
                }

                HttpContext.Current.Session["LastProcessedToken"] = currentToken;
            }
        }
    }
}

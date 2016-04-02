using System;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Framework.ActionFilters
{
    // http://stackoverflow.com/questions/4250604/how-do-i-prevent-multiple-form-submission-in-net-mvc-without-using-javascript

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PreventDuplicateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Request["__RequestVerificationToken"] == null)
                return;

            var currentToken = HttpContext.Current.Request["__RequestVerificationToken"].ToString();

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

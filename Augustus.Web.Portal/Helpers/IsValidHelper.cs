using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Augustus.Web.Portal.Helpers
{
    public static class IsValidHelper
    {
        public static bool IsValidFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var message = htmlHelper.ValidationMessageFor(expression).ToString();

            if (message.Contains("field-validation-error"))
                {
                return false;
            } else if (message.Contains("field-validation-valid"))
            {
                return true;
            } else
            {
                throw (new NotSupportedException("Excepted field-validation-error or field-validation-valid in ValidationMessage."));
            }
        }
    }
}
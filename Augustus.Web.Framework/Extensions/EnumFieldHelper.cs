using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Augustus.Web.Framework.Extensions
{
    public static class EnumFieldHelper
    {
        public static bool IsEnumValid(this HtmlHelper helper)
        {
            return EnumHelper.IsValidForEnumHelper(helper.ViewData.ModelMetadata);
        }

        public static MvcHtmlString EnumDisplayTextFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, Enum>> expression)
        {
            if (!EnumHelper.IsValidForEnumHelper(htmlHelper.ViewData.ModelMetadata))
            {
                throw (new InvalidOperationException("ViewData is not valid for use with EnumHelper."));
            }

            // Making this look like other helpers supplied by Microsoft.
            // Expression is redundant and costly step, but interested to
            // see if there are any side effects.

            var function = expression.Compile();
            var value = function(htmlHelper.ViewData.Model);

            foreach (SelectListItem item in EnumHelper.GetSelectList(htmlHelper.ViewData.ModelMetadata, value))
            {
                if (item.Selected) return new MvcHtmlString(item.Text);
            }

            return MvcHtmlString.Empty;
        }
    }
}
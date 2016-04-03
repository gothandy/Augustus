using Augustus.Web.Portal.Interfaces;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Augustus.Web.Portal.Extensions
{
    public static class HtmlAccountDropDownFor
    {
        public static MvcHtmlString AccountDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var container = (IAccountDropDown)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Container;

            var selectList = new SelectList(container.Accounts, "Id", "Name", container.AccountId);

            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, selectList, htmlAttributes);
        }
    }
}
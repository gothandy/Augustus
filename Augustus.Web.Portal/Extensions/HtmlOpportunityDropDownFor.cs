using Augustus.Web.Portal.Interfaces;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Augustus.Web.Portal.Extensions
{
    public static class HtmlOpportunityDropDownFor
    {
        public static MvcHtmlString OpportunityDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var container = (IOpportunityDropDown)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Container;

            var selectList = new SelectList(container.OpportunityLookup, "Id", "Name", container.OpportunityId);

            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, selectList, htmlAttributes);
        }
    }
}
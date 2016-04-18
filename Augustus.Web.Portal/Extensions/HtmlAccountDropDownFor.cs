using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Augustus.Web.Portal.Extensions
{
    public static class HtmlAccountDropDownFor
    {
        public static MvcHtmlString AccountDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool allowNull = false)
        {
            var container = (IAccountDropDown)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Container;
            var dropDown = container.AccountDropDown;
            var list = dropDown.Items.ToList();

            if (dropDown.AllowNull)
            {
                list.Insert(0, new Account { Name = " " });
            }

            if (dropDown.RemoveSelfId.HasValue)
            {
                RemoveSelf(dropDown, list);
            }

            var selectList = new SelectList(list, "Id", "Name", dropDown.SelectedId);

            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, selectList, htmlAttributes);
        }

        private static void RemoveSelf(ViewModels.DropDownViewModel<Account> dropDown, System.Collections.Generic.List<Account> list)
        {
            var self = list.SingleOrDefault(i => i.Id == dropDown.RemoveSelfId.Value);
            if (self != null) list.Remove(self);
        }
    }
}
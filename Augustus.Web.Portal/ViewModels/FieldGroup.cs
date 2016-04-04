using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Augustus.Web.Portal.ViewModels
{
    public class FieldGroup
    {
        public MvcHtmlString Label { get; set; }
        public MvcHtmlString Value { get; set; }
        public MvcHtmlString Message { get; set; }

        public FieldGroup()
        {
        }

        public FieldGroup(MvcHtmlString label, MvcHtmlString value)
        {
            Label = label;
            Value = value;
        }

        public static FieldGroup DisplayFor<TModel, TValue>(HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return new FieldGroup
            {
                Label = html.DisplayNameFor(expression),
                Value = html.DisplayFor(expression)
            };
        }
    }
}
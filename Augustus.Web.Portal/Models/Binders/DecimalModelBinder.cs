using System;
using System.Globalization;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Model.Binders
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(valueResult.AttemptedValue))
                    actualValue = Decimal.Parse(valueResult.AttemptedValue, NumberStyles.Currency, CultureInfo.CurrentCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
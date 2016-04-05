using Augustus.Web.Framework.ModelBinders;
using System.Web.Mvc;

namespace Augustus.Web.Portal
{
    public class BinderConfig
    {
        public static void RegisterBinders(ModelBinderDictionary binders)
        {
            binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
    }
}
using Augustus.CRM.Entities;
using Microsoft.Xrm.Sdk;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.Extensions
{
    public static class StringAttributeExtension
    {
        public static string GetAttributeString(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            return entity.GetAttributeValue<string>(attributeLogicalName);
        }

        public static void SetAttributeString(this BaseEntity entity, string value, [CallerMemberName] string caller = "")
        {
            if (value != null)
            {
                var attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

                entity.SetBaseAttributeValue(attributeLogicalName, value);
            }
        }
    }
}

using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.AttributeExtensions
{
    public static class StatusAttributeExtension
    {
        public static int? GetAttributeStatus(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            var attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);
            var statusLookup = AttributeHelper.GetStatusLookup(entity, caller);

            OptionSetValue statusCode = entity.GetAttributeValue<OptionSetValue>(attributeLogicalName);

            if (statusCode == null)
            {
                return null;
            }
            else
            {
                return Array.FindIndex(statusLookup, a => statusCode.Value == 100000000 + a);
            }
        }

        public static void SetAttributeStatus(this BaseEntity entity, int? value, [CallerMemberName] string caller = "")
        {
            if (value.HasValue)
            {
                var attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);
                var statusLookup = AttributeHelper.GetStatusLookup(entity, caller);


                var newStatusCode = new OptionSetValue(statusLookup[value.Value] + 100000000);
                var oldStatusCode = entity.GetAttributeValue<OptionSetValue>(attributeLogicalName);

                if (oldStatusCode == null || newStatusCode.Value != oldStatusCode.Value)
                {
                    entity.SetBaseAttributeValue(attributeLogicalName, newStatusCode);
                }
            }
        }
    }
}

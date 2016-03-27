using System;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.Extensions
{
    public static class DateTimeAttributeExtension
    {
        public static DateTime? GetAttributeDateTime(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            var attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            var date = entity.GetAttributeValue<DateTime?>(attributeLogicalName);

            if (date.HasValue)
            {
                return date.Value.ToLocalTime();
            }
            else
            {
                return null;
            }
        }

        public static void SetAttributeDateTime(this BaseEntity entity, DateTime? value, [CallerMemberName] string caller = "")
        {
            var attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            if (value.HasValue)
            {
                entity.SetBaseAttributeValue(attributeLogicalName, value);
            }
        }
    }
}

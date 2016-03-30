using System;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.Extensions
{
    public static class DateTimeAttributeExtension
    {
        public static DateTime? GetAttributeDateTime(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            var attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            var utcDateTime = entity.GetAttributeValue<DateTime?>(attributeLogicalName);

            if (utcDateTime.HasValue)
            {
                var gmtStandardTime = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                var localDateTime = TimeZoneInfo.ConvertTime(utcDateTime.Value, gmtStandardTime);

                return localDateTime;
            }
            else
            {
                return null;
            }
        }

        public static void SetAttributeDateTime(this BaseEntity entity, DateTime? value, [CallerMemberName] string caller = "")
        {
            if (value.HasValue)
            {
                var attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);
                entity.SetBaseAttributeValue(attributeLogicalName, value);
            }
        }
    }
}

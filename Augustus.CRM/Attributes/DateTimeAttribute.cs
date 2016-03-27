using System;

namespace Augustus.CRM.Attributes
{
    public static class DateTimeAttribute
    {
        public static DateTime? GetAttributeDateTime(this BaseEntity entity, string attributeLogicalName)
        {
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

        public static void SetAttributeDateTime(this BaseEntity entity, string attributeLogicalName, DateTime? value)
        {
            if (value.HasValue)
            {
                entity.SetBaseAttributeValue(attributeLogicalName, value);
            }
        }
    }
}

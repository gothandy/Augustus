using System;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.AttributeExtensions
{
    public static class IdAttributeExtension
    {
        public static Guid GetAttributeId(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            return entity.GetAttributeValue<Guid>(attributeLogicalName);
        }

        public static void SetAttributeId(this BaseEntity entity, Guid value, [CallerMemberName] string caller = "")
        {
            string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            var oldValue = entity.GetAttributeValue<Guid>(attributeLogicalName);

            if (value != oldValue)
            {
                entity.SetBaseAttributeValue(attributeLogicalName, (Guid?)value);
                entity.SetBaseIdValue(value);
            }
        }
    }
}

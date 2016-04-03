using System.Runtime.CompilerServices;

namespace Augustus.CRM.AttributeExtensions
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

                var oldValue = entity.GetAttributeValue<string>(attributeLogicalName);

                if (value != oldValue)
                {
                    entity.SetBaseAttributeValue(attributeLogicalName, value);
                }
            }
        }
    }
}

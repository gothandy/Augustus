using Microsoft.Xrm.Sdk;

namespace Augustus.CRM.AttributeExtensions
{
    public static class ActiveAttributeExtension
    {
        public static bool? GetAttributeState(this BaseEntity entity)
        {
            var optionSet = entity.GetAttributeValue<OptionSetValue>("statecode");
            if ((optionSet != null))
            {
                return (optionSet.Value == 0);
            }
            else
            {
                return null;
            }
        }

        public static void SetAttributeState(this BaseEntity entity, bool? value)
        {
            if (value.HasValue)
            {
                var newOptionSetValue = new OptionSetValue(value.Value ? 0 : 1);
                var oldOptionSetValue = entity.GetAttributeValue<OptionSetValue>("statecode");

                if (oldOptionSetValue == null || newOptionSetValue.Value != oldOptionSetValue.Value)
                {
                    entity.SetBaseAttributeValue("statecode",newOptionSetValue);
                }
            }
        }
    }
}

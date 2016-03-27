using Microsoft.Xrm.Sdk;

namespace Augustus.CRM.Extensions
{
    public static class ActiveAttributeExtension
    {
        public static bool? GetAttributeState(this BaseEntity entity)
        {
            OptionSetValue optionSet = entity.GetAttributeValue<OptionSetValue>("statecode");
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
                entity.SetBaseAttributeValue("statecode", new OptionSetValue(value.Value ? 0: 1));
            }
        }
    }
}

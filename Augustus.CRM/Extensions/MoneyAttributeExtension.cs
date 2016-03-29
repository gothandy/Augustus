using Microsoft.Xrm.Sdk;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.Extensions
{
    public static class MoneyAttributeExtension
    {
        public static decimal? GetAttributeMoney(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            var money = entity.GetAttributeValue<Money>(attributeLogicalName);

            if (money == null)
            {
                return null;
            }
            else
            {
                return money.Value;
            }
        }

        public static void SetAttributeMoney(this BaseEntity entity, decimal? value, [CallerMemberName] string caller = "")
        {
            if (value.HasValue)
            {
                string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

                var money = new Money(value.Value);
               
                entity.SetBaseAttributeValue(attributeLogicalName, money);
            }
        }
    }
}

using Microsoft.Xrm.Sdk;

namespace Augustus.CRM.Attributes
{
    public static class MoneyAttribute
    {
        public static decimal? GetAttributeMoney(this BaseEntity entity, string attributeLogicalName)
        {
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

        public static void SetAttributeMoney(this BaseEntity entity, string attributeLogicalName, decimal? value)
        {
            if (value.HasValue)
            {
                var money = new Money(value.Value);
               
                entity.SetBaseAttributeValue(attributeLogicalName, money);
            }
            else
            {
                //entity.SetBaseAttibuteValue(attributeLogicalName, null);
            }
        }
    }
}

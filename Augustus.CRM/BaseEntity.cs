using Microsoft.Xrm.Sdk;
using System;

namespace Augustus.CRM
{
    public class BaseEntity : Entity
    {
        public BaseEntity(string entityLogicalName) :
            base(entityLogicalName)
        {
        }

        protected decimal? GetAttributeValueMoney(string attributeLogicalName)
        {
            Money money = GetAttributeValue<Money>(attributeLogicalName);

            if (money == null)
            {
                return null;
            }
            else
            {
                return money.Value;
            }
        }

        protected Guid? GetAttributeValueEntityReference(string attributeLogicalName)
        {
            EntityReference entRef = GetAttributeValue<EntityReference>(attributeLogicalName);

            if (entRef == null)
            {
                return null;
            }
            else
            {
                return entRef.Id;
            }
        }
    }
}

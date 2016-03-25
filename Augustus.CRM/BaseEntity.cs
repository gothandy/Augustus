using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.Serialization;

namespace Augustus.CRM
{
    [DataContract()]
    public class BaseEntity : Entity
    {
        public BaseEntity(string entityLogicalName) : base(entityLogicalName) { }

        [AttributeLogicalName("createdon")]
        public DateTime? Created
        {
            get
            {
                return GetAttributeValueDateTime("createdon");
            }
        }

        protected DateTime? GetAttributeValueDateTime(string attributeLogicalName)
        {
            var date = GetAttributeValue<DateTime?>(attributeLogicalName);

            if (date.HasValue)
            {
                return date.Value.ToLocalTime();
            }
            else
            {
                return null;
            }
        }

        protected void SetAttributeValueDateTime(string attributeLogicalName, DateTime? value)
        {
            if (value.HasValue)
            {
                SetAttributeValue(attributeLogicalName, value.Value.ToUniversalTime());
            }
            else
            {
                SetAttributeValue(attributeLogicalName, value);
            }
        }

        protected decimal? GetAttributeValueMoney(string attributeLogicalName)
        {
            var money = GetAttributeValue<Money>(attributeLogicalName);

            if (money == null)
            {
                return null;
            }
            else
            {
                return money.Value;
            }
        }

        protected void SetAttributeValueMoney(string attributeLogicalName, decimal? value)
        {
            if (value.HasValue)
            {
                var money = new Money(value.Value);
                SetAttributeValue(attributeLogicalName, money);
            }
            else
            {
                SetAttributeValue(attributeLogicalName, null);
            }
        }

        protected Guid? GetAttributeEntityReferenceId(string attributeLogicalName)
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

        protected void SetAttributeEntityReferenceId(string attributeLogicalName, string entityLogicalName, Guid? id)
        {
            EntityReference entRef = null;

            if (id.HasValue) entRef = new EntityReference(entityLogicalName, id.Value);

            SetAttributeValue(attributeLogicalName, entRef);
        }
    }
}

using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.Serialization;

namespace Augustus.CRM
{
    [DataContract()]
    public class BaseEntity : Entity
    {
        public BaseEntity(string entityLogicalName) : base(entityLogicalName) { }

        [AttributeLogicalName("statuscode")]
        public int Status
        {
            get
            {
                return GetAttributeValue<OptionSetValue>("statuscode").Value;
            }
            /*set
            {
                this.SetAttributeValue("statuscode", value);
            }*/
        }

        [AttributeLogicalName("createdon")]
        public DateTime? Created
        {
            get
            {
                return GetAttributeValue<DateTime?>("createdon");
            }
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

        protected Guid? GetAttributeValueEntityReferenceId(string attributeLogicalName)
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

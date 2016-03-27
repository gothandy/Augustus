using Augustus.CRM.Attributes;
using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.Serialization;

namespace Augustus.CRM
{
    [DataContract()]
    public class BaseEntity : Entity
    {
        public BaseEntity(string entityLogicalName) : base(entityLogicalName) { }

        public void SetBaseIdValue(Guid id)
        {
            base.Id = id;
        }

        public void SetBaseAttributeValue(string attributeLogicalName, object value)
        {
            base.SetAttributeValue(attributeLogicalName, value);
        }

        [AttributeLogicalName("createdon")]
        public DateTime? Created
        {
            get
            {
                return this.GetAttributeDateTime("createdon");
            }
        }
    }
}

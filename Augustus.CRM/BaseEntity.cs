using Augustus.CRM.Extensions;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Augustus.CRM
{
    [DataContract()]
    public class BaseEntity : Entity
    {
        public Dictionary<string, Attribute> AttributeCache = new Dictionary<string, Attribute>();

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
            get { return this.GetAttributeDateTime(); }
        }
    }
}

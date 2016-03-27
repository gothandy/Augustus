using Augustus.CRM.Attributes;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace Augustus.CRM.Entities
{
    [EntityLogicalName("opportunity")]
    public class OpportunityEntity : BaseEntity
    {
        public OpportunityEntity() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "opportunity";
        public const int EntityTypeCode = 3;

        [AttributeLogicalName("opportunityid")]
        public override Guid Id
        {
            get { return this.GetAttributeId("opportunityid"); }
            set { this.SetAttributeId("opportunityid", value); }
        }

        [AttributeLogicalName("customerid")]
        public Guid? AccountId
        {
            get { return this.GetAttributeEntityReference("customerid"); }
            set { this.SetAttributeEntityReference("customerid", AccountEntity.EntityLogicalName, value); }
        }

        [AttributeLogicalName("name")]
        public string Name
        {
            get { return this.GetAttributeString("name"); }
            set { this.SetAttributeString("name", value); }
        }
    }
}

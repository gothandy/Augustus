using Augustus.CRM.Extensions;
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
            get { return this.GetAttributeId(); }
            set { this.SetAttributeId(value); }
        }

        [AttributeLogicalName("customerid")]
        [EntityReference(AccountEntity.EntityLogicalName)]
        public Guid? AccountId
        {
            get { return this.GetAttributeEntityReference(); }
            set { this.SetAttributeEntityReference(value); }
        }

        [AttributeLogicalName("name")]
        public string Name
        {
            get { return this.GetAttributeString(); }
            set { this.SetAttributeString(value); }
        }
    }
}

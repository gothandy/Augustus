using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Runtime.Serialization;

namespace Augustus.CRM.Entities
{

    [EntityLogicalName("opportunity")]
    public class Opportunity : BaseEntity
    {

        public Opportunity() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "opportunity";
        public const int EntityTypeCode = 3;

        [AttributeLogicalName("new_invoiceid")]
        public Guid? OpportunityId
        {
            get
            {
                return GetAttributeValue<Guid?>("opportunityid");
            }
            set
            {
                SetAttributeValue("opportunityid", value);
                base.Id = value.GetValueOrDefault();
            }
        }

        [AttributeLogicalName("opportunityid")]
        public override Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                this.OpportunityId = value;
            }
        }

        [AttributeLogicalName("accountid")]
        public Guid? AccountId
        {
            get
            {
                return GetAttributeValueEntityReferenceId("accountid");
            }
        }

        [AttributeLogicalName("parentaccountid")]
        [RelationshipSchemaName("opportunity_parent_account")]
        public Account ParentAccount
        {
            get
            {
                return GetRelatedEntity<Account>("opportunity_parent_account", null);
            }
            set
            {
                SetRelatedEntity("opportunity_parent_account", null, value);
            }
        }

        [AttributeLogicalName("name")]
        public string Name
        {
            get
            {
                return GetAttributeValue<string>("name");
            }
            set
            {
                SetAttributeValue("name", value);
            }
        }
    }
}

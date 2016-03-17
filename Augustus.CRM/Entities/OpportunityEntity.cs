using Augustus.Domain.Objects;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Runtime.Serialization;

namespace Augustus.CRM.Entities
{

    [EntityLogicalName("opportunity")]
    public class OpportunityEntity : BaseEntity
    {

        public OpportunityEntity() : base(EntityLogicalName) { }

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

        private const string CustomerIdLogicalName = "customerid";

        [AttributeLogicalName(CustomerIdLogicalName)]
        public Guid? AccountId
        {
            get
            {
                return GetAttributeEntityReferenceId(CustomerIdLogicalName);
            }
            set
            {
                SetAttributeEntityReferenceId(CustomerIdLogicalName, AccountEntity.EntityLogicalName, value);
            }
        }

        public static Opportunity ToDomainObject(OpportunityEntity o)
        {
            return new Opportunity
            {
                AccountId = o.AccountId,
                Created = o.Created,
                Id = o.Id,
                Name = o.Name
            };
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

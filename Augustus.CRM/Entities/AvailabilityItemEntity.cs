using Augustus.CRM.Attributes;
using Augustus.CRM.AttributeExtensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace Augustus.CRM.Entities
{
    [EntityLogicalName("new_availablity")]
    public partial class AvailabilityItemEntity : BaseEntity
    {
        public AvailabilityItemEntity() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "new_availablity";
        public const int EntityTypeCode = 10049;

        [AttributeLogicalName("new_cost")]
        public decimal? Cost
        {
            get { return this.GetAttributeMoney(); }
            set { this.SetAttributeMoney(value); }
        }

        [AttributeLogicalName("new_availabledays")]
        public decimal? AvailableDays
        {
            get { return this.GetAttributeDecimal(); }
            set { this.SetAttributeDecimal(value); }
        }

        [AttributeLogicalName("new_availabilitydate")]
        public DateTime? AvailabilityDate
        {
            get { return this.GetAttributeDateTime(); }
            set { this.SetAttributeDateTime(value); }
        }

        [AttributeLogicalName("new_availablityid")]
        public override Guid Id
        {
            get { return this.GetAttributeId(); }
            set { this.SetAttributeId(value); }
        }

        [AttributeLogicalName("statecode")]
        public bool? Active
        {
            get { return this.GetAttributeState(); }
            set { this.SetAttributeState(value); }
        }

        [AttributeLogicalName("new_account")]
        [EntityReference(AccountEntity.EntityLogicalName)]
        public Guid? AccountId
        {
            get { return this.GetAttributeEntityReference(); }
            set { this.SetAttributeEntityReference(value); }
        }
    }
}


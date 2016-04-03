using Augustus.CRM.Attributes;
using Augustus.CRM.AttributeExtensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace Augustus.CRM.Entities
{
    [EntityLogicalName("new_workdoneitem")]
    public partial class WorkDoneItemEntity : BaseEntity
    {
        public WorkDoneItemEntity() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "new_workdoneitem";
        public const int EntityTypeCode = 10049;

        [AttributeLogicalName("new_invoiceid")]
        [EntityReference(InvoiceEntity.EntityLogicalName)]
        public Guid? InvoiceId
        {
            get { return this.GetAttributeEntityReference(); }
            set { this.SetAttributeEntityReference(value); }
        }

        [AttributeLogicalName("new_margin")]
        public decimal? Margin
        {
            get { return this.GetAttributeMoney(); }
            set { this.SetAttributeMoney(value); }
        }

        [AttributeLogicalName("new_workdonedate")]
        public DateTime? WorkDoneDate
        {
            get { return this.GetAttributeDateTime(); }
            set { this.SetAttributeDateTime(value); }
        }

        [AttributeLogicalName("new_workdoneitemid")]
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

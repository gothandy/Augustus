using Augustus.CRM.Attributes;
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
        public Guid? InvoiceId
        {
            get { return this.GetAttributeEntityReference("new_invoiceid"); }
            set { this.SetAttributeEntityReference("new_invoiceid", InvoiceEntity.EntityLogicalName, value); }
        }

        [AttributeLogicalName("new_margin")]
        public decimal? Margin
        {
            get { return this.GetAttributeMoney("new_margin"); }
            set { this.SetAttributeMoney("new_margin", value); }
        }

        [AttributeLogicalName("new_workdonedate")]
        public DateTime? WorkDoneDate
        {
            get { return this.GetAttributeDateTime("new_workdonedate"); }
            set { this.SetAttributeDateTime("new_workdonedate", value); }
        }

        [AttributeLogicalName("new_workdoneitemid")]
        public override Guid Id
        {
            get { return this.GetAttributeId("new_workdoneitemid"); }
            set { this.SetAttributeId("new_workdoneitemid", value); }
        }

        [AttributeLogicalName("statecode")]
        public bool? Active
        {
            get { return this.GetAttributeState(); }
            set { this.SetAttributeState(value); }
        }

        [AttributeLogicalName("new_account")]
        public Guid? AccountId
        {
            get { return this.GetAttributeEntityReference("new_account"); }
            set { this.SetAttributeEntityReference("new_account", AccountEntity.EntityLogicalName, value); }
        }
    }
}

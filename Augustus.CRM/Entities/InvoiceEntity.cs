using Augustus.CRM.Attributes;
using Augustus.CRM.AttributeExtensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace Augustus.CRM.Entities
{
    [EntityLogicalName("new_invoice")]
    public class InvoiceEntity : BaseEntity
    {
        public InvoiceEntity() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "new_invoice";
        public const int EntityTypeCode = 10010;

        [AttributeLogicalName("statecode")]
        public bool? Active
        {
            get { return this.GetAttributeState(); }
            set { this.SetAttributeState(value); }
        }

        [AttributeLogicalName("statuscode")]
        [StatusLookup(new int[] { 4, 0, 5, 6, 7, 1, 8, 2, 3 })]
        public int? Status
        {
            get { return this.GetAttributeStatus(); }
            set { this.SetAttributeStatus(value); }
        }

        [AttributeLogicalName("new_invoiceid")]
        public override Guid Id
        {
            get { return this.GetAttributeId(); }
            set { this.SetAttributeId(value); }
        }

        [AttributeLogicalName("new_invoicedate")]
        public DateTime? InvoiceDate
        {
            get { return this.GetAttributeDateTime(); }
            set { this.SetAttributeDateTime(value); }
        }

        [AttributeLogicalName("new_cost")]
        public decimal? Cost
        {
            get { return this.GetAttributeMoney(); }
            set { this.SetAttributeMoney(value); }
        }

        [AttributeLogicalName("new_directclient")]
        [EntityReference(AccountEntity.EntityLogicalName)]
        public Guid? AccountId
        {
            get { return this.GetAttributeEntityReference(); }
            set { this.SetAttributeEntityReference(value); }
        }

        [AttributeLogicalName("new_proposalapproveddate")]
        public DateTime? ProposalApproved
        {
            get { return this.GetAttributeDateTime(); }
            set { this.SetAttributeDateTime(value); }
        }

        [AttributeLogicalName("new_clientapproveddate")]
        public DateTime? SdnApproved
        {
            get { return this.GetAttributeDateTime(); }
            set { this.SetAttributeDateTime(value); }
        }

        [AttributeLogicalName("new_invoiceno")]
        public string InvoiceNo
        {
            get { return this.GetAttributeString(); }
            set { this.SetAttributeString(value); }
        }

        [AttributeLogicalName("new_margin")]
        public decimal? Margin
        {
            get { return this.GetAttributeMoney(); }
            set { this.SetAttributeMoney(value); }
        }

        [AttributeLogicalName("new_name")]
        public string Name
        {
            get { return this.GetAttributeString(); }
            set { this.SetAttributeString(value); }
        }

        [AttributeLogicalName("new_opportunity")]
        [EntityReference(InvoiceEntity.EntityLogicalName)]
        public Guid? OpportunityId
        {
            get { return this.GetAttributeEntityReference(); }
            set { this.SetAttributeEntityReference(value); }
        }

        [AttributeLogicalName("new_ponumber")]
        public string PONumber
        {
            get { return this.GetAttributeString(); }
            set { this.SetAttributeString(value); }
        }

        [AttributeLogicalName("new_revenue")]
        public decimal? Revenue
        {
            get { return this.GetAttributeMoney(); }
            set { this.SetAttributeMoney(value); }
        }

        [AttributeLogicalName("new_wip_current")]
        public decimal? WipCurrent
        {
            get { return this.GetAttributeMoney(); }
        }

        [AttributeLogicalName("new_wip_previous")]
        public decimal? WipPevious
        {
            get { return this.GetAttributeMoney(); }
        }
    }
}

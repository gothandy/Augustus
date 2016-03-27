using Augustus.CRM.Attributes;
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

        private static int[] statusLookup = { 4, 0, 5, 6, 7, 1, 8, 2, 3 };

        [AttributeLogicalName("statuscode")]
        public int? Status
        {
            get { return this.GetAttributeOptionSet("statuscode", statusLookup); }
            /*set
            {
                this.SetAttributeValue("statuscode", value);
            }*/
        }

        [AttributeLogicalName("new_invoiceid")]
        public override Guid Id
        {
            get { return this.GetAttributeId("new_invoiceid"); }
            set { this.SetAttributeId("new_invoiceid", value); }
        }

        [AttributeLogicalName("new_invoicedate")]
        public DateTime? InvoiceDate
        {
            get { return this.GetAttributeDateTime("new_invoicedate"); }
            set { this.SetAttributeDateTime("new_invoicedate", value); }
        }

        [AttributeLogicalName("new_cost")]
        public decimal? Cost
        {
            get { return this.GetAttributeMoney("new_cost"); }
            set { this.SetAttributeMoney("new_cost", value); }
        }

        [AttributeLogicalName("new_directclient")]
        public Guid? AccountId
        {
            get { return this.GetAttributeEntityReference("new_directclient"); }
            set { this.SetAttributeEntityReference("new_directclient", AccountEntity.EntityLogicalName, value); }
        }

        [AttributeLogicalName("new_clientapproveddate")]
        public DateTime? ClientApprovedDate
        {
            get { return this.GetAttributeDateTime("new_clientapproveddate"); }
            set { this.SetAttributeDateTime("new_clientapproveddate", value); }
        }

        [AttributeLogicalName("new_invoiceno")]
        public string InvoiceNo
        {
            get { return this.GetAttributeString("new_invoiceno"); }
            set { this.SetAttributeString("new_invoiceno", value); }
        }

        [AttributeLogicalName("new_margin")]
        public decimal? Margin
        {
            get { return this.GetAttributeMoney("new_margin"); }
            set { this.SetAttributeMoney("new_margin", value); }
        }

        [AttributeLogicalName("new_name")]
        public string Name
        {
            get { return this.GetAttributeString("new_name"); }
            set { this.SetAttributeString("new_name", value); }
        }

        [AttributeLogicalName("new_opportunity")]
        public Guid? OpportunityId
        {
            get { return this.GetAttributeEntityReference("new_opportunity"); }
            set { this.SetAttributeEntityReference("new_opportunity", InvoiceEntity.EntityLogicalName, value); }
        }

        [AttributeLogicalName("new_ponumber")]
        public string PONumber
        {
            get { return this.GetAttributeString("new_ponumber"); }
            set { this.SetAttributeString("new_ponumber", value); }
        }

        [AttributeLogicalName("new_revenue")]
        public decimal? Revenue
        {
            get { return this.GetAttributeMoney("new_revenue"); }
            set { this.SetAttributeMoney("new_revenue", value); }
        }

        [AttributeLogicalName("new_wip_current")]
        public decimal? WipCurrent
        {
            get { return this.GetAttributeMoney("new_wip_current"); }
        }

        [AttributeLogicalName("new_wip_previous")]
        public decimal? WipPevious
        {
            get { return this.GetAttributeMoney("new_wip_previous"); }
        }
    }
}

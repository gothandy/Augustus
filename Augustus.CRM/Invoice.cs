using Augustus.Interfaces;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Runtime.Serialization;

namespace Augustus.CRM
{
    [DataContract()]
    [EntityLogicalName("new_invoice")]
    public class Invoice : BaseEntity, IInvoice
    {

        public Invoice() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "new_invoice";
        public const int EntityTypeCode = 10010;

        [AttributeLogicalName("new_invoiceid")]
        public Guid? InvoiceId
        {
            get
            {
                return GetAttributeValue<Guid?>("new_invoiceid");
            }
            set
            {
                this.SetAttributeValue("new_invoiceid", value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
            }
        }

        [AttributeLogicalName("new_invoiceid")]
        public override Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                this.InvoiceId = value;
            }
        }

        [AttributeLogicalName("new_invoicedate")]
        public DateTime? InvoiceDate
        {
            get
            {
                return GetAttributeValue<DateTime?>("new_invoicedate");
            }
            set
            {
                this.SetAttributeValue("new_invoicedate", value);
            }
        }

        [AttributeLogicalName("new_cost")]
        public decimal? Cost
        {
            get
            {
                return GetAttributeValueMoney("new_cost");
            }
            /*set
            {
                SetAttributeValue("new_cost", value);
            }*/
        }

        [AttributeLogicalName("new_directclient")]
        public Guid? DirectClientId
        {
            get
            {
                return GetAttributeValueEntityReferenceId("new_directclient");
            }
            /*set
            {
                this.SetAttributeValue("new_directclient", value);
            }*/
        }

        [AttributeLogicalName("new_clientapproveddate")]
        public DateTime? ClientApprovedDate
        {
            get
            {
                return GetAttributeValue<DateTime?>("new_clientapproveddate");
            }
            set
            {
                this.SetAttributeValue("new_clientapproveddate", value);
            }
        }

        [AttributeLogicalName("new_invoiceno")]
        public string InvoiceNo
        {
            get
            {
                return this.GetAttributeValue<string>("new_invoiceno");
            }
            set
            {
                this.SetAttributeValue("new_invoiceno", value);
            }
        }

        [AttributeLogicalName("new_margin")]
        public decimal? Margin
        {
            get
            {
                return GetAttributeValueMoney("new_margin");
            }
            /*set
            {
                this.SetAttributeValue("new_margin", value);
            }*/
        }

        [AttributeLogicalName("new_name")]
        public string Name
        {
            get
            {
                return GetAttributeValue<string>("new_name");
            }
            set
            {
                SetAttributeValue("new_name", value);
            }
        }

        [AttributeLogicalName("new_opportunity")]
        public Guid? OpportunityId
        {
            get
            {
                return GetAttributeValueEntityReferenceId("new_opportunity");
            }
            /*set
            {
                this.SetAttributeValue("new_opportunity", value);
            }*/
        }

        [AttributeLogicalName("new_ponumber")]
        public string PONumber
        {
            get
            {
                return GetAttributeValue<string>("new_ponumber");
            }
            set
            {
                this.SetAttributeValue("new_ponumber", value);
            }
        }

        [AttributeLogicalName("new_revenue")]
        public decimal? Revenue
        {
            get
            {
                return GetAttributeValueMoney("new_revenue");
            }
            /*set
            {
                this.SetAttributeValue("new_revenue", value);
            }*/
        }

        [AttributeLogicalName("new_wip_current")]
        public decimal? WipCurrent
        {
            get
            {
                return GetAttributeValueMoney("new_wip_current");
            }
            /*set
            {
                this.SetAttributeValue("new_wip_current", value);
            }*/
        }

        [AttributeLogicalName("new_wip_previous")]
        public decimal? WipPevious
        {
            get
            {
                return GetAttributeValueMoney("new_wip_previous");
            }
            /*set
            {
                this.SetAttributeValue("new_wip_previous", value);
            }*/
        }

        [AttributeLogicalName("statuscode")]
        public int Status
        {
            get
            {
                return GetAttributeValue<OptionSetValue>("statuscode").Value;
            }
            /*set
            {
                this.SetAttributeValue("statuscode", value);
            }*/
        }
    }
}

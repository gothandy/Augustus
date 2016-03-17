using Augustus.Domain.Objects;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Runtime.Serialization;

namespace Augustus.CRM.Entities
{
    [EntityLogicalName("new_invoice")]
    public class InvoiceEntity : BaseEntity
    {

        public InvoiceEntity() : base(EntityLogicalName) { }

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

        public static Invoice ToDomainObject(InvoiceEntity i)
        {
            return new Invoice()
            {
                Id = i.Id,
                Name = i.Name,
                AccountId = i.AccountId,
                ClientApprovedDate = i.ClientApprovedDate,
                Cost = i.Cost,
                Created = i.Created,
                InvoiceDate = i.InvoiceDate,
                InvoiceId = i.InvoiceId,
                InvoiceNo = i.InvoiceNo,
                Margin = i.Margin,
                OpportunityId = i.OpportunityId,
                PONumber = i.PONumber,
                Revenue = i.Revenue
            };
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
        public Guid? AccountId
        {
            get
            {
                return GetAttributeEntityReferenceId("new_directclient");
            }
            set
            {
                if (value.HasValue)
                {
                    SetAttributeValue("new_directclient",
                        new EntityReference(
                            AccountEntity.EntityLogicalName,
                            value.Value));
                }
                
            }
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

        private const string OpportunityIdLogicalName = "new_opportunity";
        [AttributeLogicalName(OpportunityIdLogicalName)]
        public Guid? OpportunityId
        {
            get
            {
                return GetAttributeEntityReferenceId(OpportunityIdLogicalName);
            }
            set
            {
                SetAttributeEntityReferenceId(OpportunityIdLogicalName, OpportunityEntity.EntityLogicalName, value);
            }
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
    }
}

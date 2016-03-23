using Augustus.Domain.Objects;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Linq;
using System.Runtime.Serialization;

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
            get
            {
                OptionSetValue statusCode = GetAttributeValue<OptionSetValue>("statuscode");
                if (statusCode == null)
                {
                    return null;
                }
                else
                {
                    return Array.FindIndex(statusLookup, a => statusCode.Value == 100000000 + a);
                }
            }
            /*set
            {
                this.SetAttributeValue("statuscode", value);
            }*/
        }

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

        public void SetUsingDomain(Invoice domain)
        {
            var entity = this;

            entity.Name = domain.Name;
            // AccountId set using lookup on Opportunity in query.
            entity.OpportunityId = domain.OpportunityId;
            entity.InvoiceNo = domain.InvoiceNo;
            entity.PONumber = domain.PONumber;
            entity.Cost = domain.Cost;
            entity.Revenue = domain.Revenue;
            entity.Margin = domain.Margin;
            entity.InvoiceDate = domain.InvoiceDate;
            entity.ClientApprovedDate = domain.ClientApprovedDate;
        }

        public Invoice ToDomainObject()
        {
            return InvoiceEntity.ToDomainObject(this);
        }

        public static Invoice ToDomainObject(InvoiceEntity entity)
        {
            var domain = new Invoice();

            domain.Id = entity.Id;
            domain.Name = entity.Name;
            domain.AccountId = entity.AccountId;
            domain.OpportunityId = entity.OpportunityId;
            domain.ClientApprovedDate = entity.ClientApprovedDate;
            domain.Created = entity.Created;
            domain.InvoiceDate = entity.InvoiceDate;
            domain.InvoiceNo = entity.InvoiceNo;
            domain.PONumber = entity.PONumber;
            domain.Revenue = entity.Revenue;
            domain.Cost = entity.Cost;
            //Margin is calculated
            domain.Status = (InvoiceStatus)entity.Status;

            return domain;
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
            set
            {
                SetAttributeValueMoney("new_cost", value);
            }
        }

        private const string AccountIdLogicalName = "new_directclient";
        [AttributeLogicalName(AccountIdLogicalName)]
        public Guid? AccountId
        {
            get
            {
                return GetAttributeEntityReferenceId("new_directclient");
            }
            set
            {
                SetAttributeEntityReferenceId(AccountIdLogicalName, AccountEntity.EntityLogicalName, value);
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
            set
            {
                SetAttributeValueMoney("new_margin", value);
            }
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
            set
            {
                SetAttributeValueMoney("new_revenue", value);
            }
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

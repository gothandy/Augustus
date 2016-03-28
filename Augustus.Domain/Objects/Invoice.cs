using Augustus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Augustus.Domain.Objects
{
    [DataContract]
    public class Invoice
    {
        [DataMember]
        public Guid? Id { get; set; }

        [DataMember]
        public Guid? AccountId { get; set; }

        [DataMember]
        public Guid? OpportunityId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Display(Name="P.O. No.")]
        public string PONumber { get; set; }

        [DataMember]

        [Display(Name="Invoice No.")]
        public string InvoiceNo { get; set; }

        [DataMember]

        public decimal? Cost { get; set; }

        [DataMember]

        public decimal? Revenue { get; set; }

        [DataMember]
        [Display(Name = "Proposal Approved")]
        public DateTime? ProposalApproved { get; set; }

        [DataMember]
        [Display(Name = "SDN Approved")]
        public DateTime? SdnApproved { get; set; }

        [DataMember]
        public DateTime? Created { get; set; }

        [DataMember]
        [Display(Name = "Invoice Date")]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public IEnumerable<WorkDoneItem> WorkDoneItems { get; set; }

        [DataMember]
        [EnumDataType(typeof(InvoiceStatus))]
        public InvoiceStatus? Status { get; set; }

        public Account Account { get; set; }
        public Opportunity Opportunity { get; set; }

        public decimal? Margin
        {
            get
            {
                if (!Revenue.HasValue && !Cost.HasValue)
                {
                    return null;
                }
                else
                {
                    return Revenue.GetValueOrDefault() - Cost.GetValueOrDefault();
                }
            }
        }
    }
}

using Augustus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Augustus.Domain.Objects
{
    public class Invoice
    {
        public Guid? Id { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? OpportunityId { get; set; }
        public string Name { get; set; }

        [Display(Name="P.O. No.")]
        public string PONumber { get; set; }

        [Display(Name="Invoice No.")]
        public string InvoiceNo { get; set; }

        public decimal? Cost { get; set; }

        public decimal? Revenue { get; set; }

        [Display(Name = "SDN Approved")]
        public DateTime? ClientApprovedDate { get; set; }

        public DateTime? Created { get; set; }

        [Display(Name = "Invoice Date")]
        public DateTime? InvoiceDate { get; set; }

        public Account Account { get; set; }
        public Opportunity Opportunity { get; set; }
        public IEnumerable<WorkDoneItem> WorkDoneItems { get; set; }

        [EnumDataType(typeof(InvoiceStatus))]
        public InvoiceStatus Status { get; set; }

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

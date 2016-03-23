using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Augustus.Domain.Objects
{
    public enum InvoiceStatus : int
    {
        New,
        Proposed,
        Approved,
        [Display(Name = "In Progress")]
        InProgress,
        Accepted,
        [Display(Name = "SDN Sent")]
        SDNSent,
        [Display(Name = "SDN Approved")]
        SDNApproved,
        [Display(Name = "Invoice Sent")]
        InvoiceSent,
        [Display(Name = "Invoice Paid")]
        InvoicePaid
    }

    public class Invoice
    {
        public Guid? Id { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? OpportunityId { get; set; }
        public string Name { get; set; }
        public string PONumber { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Revenue { get; set; }
        public DateTime? ClientApprovedDate { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public Account Account { get; set; }
        public Opportunity Opportunity { get; set; }
        public IEnumerable<WorkDoneItem> WorkDoneItems { get; set; }
        public InvoiceStatus? Status { get; set; }

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

using System.ComponentModel.DataAnnotations;

namespace Augustus.Domain.Enums
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
}

using System;

namespace Augustus.Domain.Objects
{
    public class ReportAccount
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal WorkDoneAndInvoiced { get; set; }
        public decimal WorkDoneTotal { get; set; }
    }
}

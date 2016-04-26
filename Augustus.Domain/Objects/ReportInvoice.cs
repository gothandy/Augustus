using System;

namespace Augustus.Domain.Objects
{
    public class ReportInvoice
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Margin { get; set; }
        public decimal WorkDone { get; set; }
    }
}

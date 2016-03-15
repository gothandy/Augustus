using System;

namespace Augustus.Domain.Objects
{
    public class Invoice
    {
        public Guid? AccountId { get; set; }
        public DateTime? ClientApprovedDate { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? Created { get; set; }
        public Guid Id { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public Guid? InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? Margin { get; set; }
        public string Name { get; set; }
        public Guid? OpportunityId { get; set; }
        public string PONumber { get; set; }
        public decimal? Revenue { get; set; }
    }
}

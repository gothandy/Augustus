using System;

namespace Augustus.Interfaces
{
    public interface IInvoice
    {
        Guid Id { get; set; }
        Guid? InvoiceId { get; set; }
        Guid? AccountId { get; }
        Guid? OpportunityId { get; }
        string Name { get; set; }
        string PONumber { get; set; }
        string InvoiceNo { get; set; }
        DateTime? InvoiceDate { get; set; }
        DateTime? ClientApprovedDate { get; set; }
        decimal? Revenue { get; }
        decimal? Cost { get; }
        decimal? Margin { get; }
        decimal? WipCurrent { get; }
        decimal? WipPevious { get; }
    }
}
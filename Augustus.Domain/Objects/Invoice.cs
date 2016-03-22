using System;
using System.Collections.Generic;

namespace Augustus.Domain.Objects
{
    public class Invoice
    {
        public Guid? Id { get; set; }
        //public Guid? AccountId { get; set; }
        public Guid? OpportunityId { get; set; }
        public string Name { get; set; }
        public string PONumber { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Revenue { get; set; }
        public DateTime? ClientApprovedDate { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public IEnumerable<WorkDoneItem> WorkDoneItems { get; set; }

        public decimal? Margin
        {
            get
            {
                return Revenue - Cost;
            }
        }
    }
}

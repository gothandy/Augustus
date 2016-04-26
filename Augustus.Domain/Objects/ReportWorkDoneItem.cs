using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.Domain.Objects
{
    public class ReportWorkDoneItem
    {
        public Account Account { get; set; }
        public Invoice Invoice { get; set; }
        public WorkDoneItem WorkDoneItem { get; set; }

        /*
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public Guid InvoiceId { get; set; }
        public string InvoiceName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceMargin { get; set; }
        public Guid WorkDoneItemId { get; set; }
        public decimal WorkDoneDate { get; set; }
        public decimal WorkDoneMargin { get; set; }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.Domain.Objects
{
    public class ReportAccount
    {
        public string Account { get; set; }
        public Guid AccountId { get; set; }

        public decimal InvoiceTotal { get; set; }
        public decimal WorkNotDoneAndInvoiced { get; set; }
        public decimal WorkDoneAndInvoiced { get; set; }
        public decimal WorkDoneAndNotInvoiced { get; set; }
        public decimal WorkDoneTotal { get; set; }

    }
}

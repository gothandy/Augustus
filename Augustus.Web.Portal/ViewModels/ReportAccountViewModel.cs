using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;
using System.Collections.Generic;

namespace Augustus.Web.Portal.ViewModels
{
    public class ReportAccountViewModel : ISharedLayoutViewModel
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public IEnumerable<ReportInvoice> Invoices { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Account Account { get; set; }
    }
}
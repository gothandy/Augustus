using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;
using System.Collections.Generic;

namespace Augustus.Web.Portal.ViewModels
{
    public class ReportMonthViewModel : ISharedLayoutViewModel
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public IEnumerable<ReportAccount> Accounts { get; set; }
    }
}
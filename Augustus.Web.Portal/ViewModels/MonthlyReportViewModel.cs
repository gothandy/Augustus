using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;
using System.Collections.Generic;

namespace Augustus.Web.Portal.ViewModels
{
    public class MonthlyReportViewModel : ISharedLayoutViewModel
    {
        public string Title { get; set; }
        public Breadcrumb Breadcrumb { get; set; }
        public List<ReportMonth> Months { get; set; }
    }
}
using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class InvoiceReadViewModel : IPageViewModel, IReadViewModel<Invoice>
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public Invoice Invoice { get; set; }
        public Invoice DomainModel { get { return Invoice; } set { Invoice = value; } }
    }
}
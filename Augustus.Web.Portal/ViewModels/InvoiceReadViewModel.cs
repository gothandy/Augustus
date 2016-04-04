using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;
using System.Collections.Generic;

namespace Augustus.Web.Portal.ViewModels
{
    public class InvoiceReadViewModel : ISharedLayoutViewModel, IReadViewModel<Invoice>
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public List<AlertViewModel> Alerts { get; set; }
        public ButtonViewModel OpenButton { get; set; }
        public ButtonViewModel EditButton { get; set; }
        public Invoice Invoice { get; set; }
        public Invoice DomainModel { get { return Invoice; } set { Invoice = value; } }
    }
}
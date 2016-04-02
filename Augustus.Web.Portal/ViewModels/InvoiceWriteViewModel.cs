using Augustus.Domain.Objects;
using System.Collections.Generic;
using System;
using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class InvoiceWriteViewModel : IPageViewModel, IWriteModelView<Invoice>, IOpportunityDropDown
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public FormButtons FormButtons { get; set; }
        public Invoice Invoice { get; set; }
        public Invoice DomainModel
        {
            get { return Invoice; }
            set { Invoice = value; }
        }

        public Guid OpportunityId { get; set; }
        public IEnumerable<Opportunity> Opportunities { get; set; }
    }
}
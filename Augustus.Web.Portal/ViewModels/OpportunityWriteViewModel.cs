using Augustus.Domain.Objects;
using System.Collections.Generic;
using System;
using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class OpportunityWriteViewModel : ISharedLayoutViewModel, IWriteModelView<Opportunity>, IAccountDropDown
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public FormButtons FormButtons { get; set; }
        public Opportunity Opportunity { get; set; }
        public Opportunity DomainModel
        {
            get { return Opportunity; }
            set { Opportunity = value; }
        }

        public Guid AccountId { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
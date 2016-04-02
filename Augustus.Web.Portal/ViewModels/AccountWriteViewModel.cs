using System;
using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class AccountWriteViewModel : IPageViewModel, IWriteModelView<Account>
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public FormButtons FormButtons { get; set; }
        public Account Account { get; set; }
        public Account DomainModel
        {
            get { return Account; }
            set { Account = value; }
        }
    }
}
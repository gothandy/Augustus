using System;
using System.Collections.Generic;
using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class AccountWriteViewModel : ISharedLayoutViewModel, IWriteModelView<Account>, IAccountDropDown
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public FormButtons FormButtons { get; set; }
        public Account Account { get; set; }
        public DropDownViewModel<Account> AccountDropDown { get; set; }
        public Account DomainModel
        {
            get { return Account; }
            set { Account = value; }
        }
    }
}
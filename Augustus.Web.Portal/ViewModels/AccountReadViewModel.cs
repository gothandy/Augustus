using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class AccountReadViewModel : ISharedLayoutViewModel, IReadViewModel<Account>
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public Account Account { get; set; }
        public Account DomainModel { get { return Account; } set { Account = value; } }
        public ButtonViewModel CreateButton { get; set; }
        public ButtonViewModel EditButton { get; set; }
    }
}
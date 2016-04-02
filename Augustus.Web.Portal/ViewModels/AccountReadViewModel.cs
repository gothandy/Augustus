using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class AccountReadViewModel : IPageViewModel, IReadViewModel<Account>
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public Account Account { get; set; }
        public Account DomainModel { get { return Account; } set { Account = value; } }
    }
}
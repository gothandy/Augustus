using Augustus.Domain.Objects;
using Augustus.Web.Portal.ViewModels;

namespace Augustus.Web.Portal.Interfaces
{
    public interface IAccountDropDown
    {
        DropDownViewModel<Account> AccountDropDown { get; set; }
    }
}

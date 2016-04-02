using Augustus.Web.Portal.ViewModels;

namespace Augustus.Web.Portal.Interfaces
{
    public interface IPageViewModel
    {
        string Title { get; set; }
        Breadcrumb Breadcrumb { get; set; }
    }
}
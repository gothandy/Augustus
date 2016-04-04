using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class HomeViewModel : ISharedLayoutViewModel
    {
        public Breadcrumb Breadcrumb
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }
    }
}
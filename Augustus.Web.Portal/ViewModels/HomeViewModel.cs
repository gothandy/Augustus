using Augustus.Web.Portal.Interfaces;
using System.Collections.Generic;

namespace Augustus.Web.Portal.ViewModels
{
    public class HomeViewModel : ISharedLayoutViewModel
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public List<AlertViewModel> Alerts { get; set; }
    }
}
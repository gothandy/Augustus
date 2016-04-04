using Augustus.Web.Portal.ViewModels;
using System.Collections.Generic;

namespace Augustus.Web.Portal.Interfaces
{
    public interface ISharedLayoutViewModel
    {
        string Title { get; set; }
        Breadcrumb Breadcrumb { get; set; }
    }
}
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    [Authorize]
    public class HomeController : CrmBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
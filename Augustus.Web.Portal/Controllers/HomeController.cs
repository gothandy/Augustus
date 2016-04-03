using Augustus.Web.Portal.ViewModels;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("ActiveAccounts", "Organization");
            }

            return View(new HomeViewModel { Title = "Augustus" });
        }
    }
}
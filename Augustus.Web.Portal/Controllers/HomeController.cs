using Augustus.Web.Portal.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index(bool signOut = false)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("ActiveAccounts", "Organization");
            }

            var model = new HomeViewModel
            {
                Title = "Augustus"
            };

            if (signOut)
            {
                model.Alerts = new List<AlertViewModel>()
                {
                    new AlertViewModel
                    {
                        Text = "You have been successfully signed out.",
                        Level = AlertLevel.Success
                    }
                };
            }
            
            return View(model);
        }
    }
}
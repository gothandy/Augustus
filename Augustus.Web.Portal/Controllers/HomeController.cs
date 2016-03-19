using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class HomeController : CrmBaseController
    {
        public async Task<ActionResult> Index()
        {
            using (var query = await GetOrganizationQuery())
            {
                ViewBag.ActiveAccounts = query.GetActiveAccounts(lastThreeMonths);
                ViewBag.NewAccounts = query.GetNewAccounts(lastMonth);
                return View();
            }
        }
    }
}
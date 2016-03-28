using System.Threading.Tasks;
using System.Web.Mvc;
using Augustus.CRM.Queries;

namespace Augustus.Web.Portal.Controllers
{
    public class OrganizationController : CrmBaseController
    {

        // GET: /Organization/ActiveAccounts
        public async Task<ActionResult> ActiveAccounts()
        {
            using (var context = await GetCrmContext())
            {
                var query = new OrganizationQuery(context);

                ViewBag.Title = "True Clarity";

                var activeAccounts = query.GetActiveAccounts();

                return View(activeAccounts);
            }
        }

        // GET: /Organization/NewAccounts
        public async Task<ActionResult> NewAccounts()
        {
            using (var context = await GetCrmContext())
            {
                var query = new OrganizationQuery(context);

                ViewBag.Title = "True Clarity";

                var newAccounts = query.GetNewAccounts();

                return View(newAccounts);
            }
        }
    }
}
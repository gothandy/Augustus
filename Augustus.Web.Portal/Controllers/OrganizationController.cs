using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class OrganizationController : CrmBaseController
    {

        // GET: /Organization/ActiveAccounts
        public async Task<ActionResult> ActiveAccounts()
        {
            using (var query = await GetOrganizationQuery())
            {
                ViewBag.Title = "True Clarity";
                return View(query.GetActiveAccounts(invoicesFrom: lastYear));
            }
        }

        // GET: /Organization/NewAccounts
        public async Task<ActionResult> NewAccounts()
        {
            using (var query = await GetOrganizationQuery())
            {
                ViewBag.Title = "True Clarity";
                return View(query.GetNewAccounts(createdAfter: lastThreeMonths));
            }
        }
    }
}
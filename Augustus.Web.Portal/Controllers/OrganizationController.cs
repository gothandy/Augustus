using System.Threading.Tasks;
using System.Web.Mvc;
using Augustus.CRM.Queries;

namespace Augustus.Web.Portal.Controllers
{
    public class OrganizationController : CrmBaseController
    {
        private OrganizationQuery query;
        public OrganizationController() : base()
        {
            query = new OrganizationQuery(context);
        }

        // GET: /Organization/ActiveAccounts
        public ActionResult ActiveAccounts()
        {
            ViewBag.Title = "True Clarity";

            var activeAccounts = query.GetActiveAccounts();

            return View(activeAccounts);
        }

        // GET: /Organization/NewAccounts
        public ActionResult NewAccounts()
        {
            ViewBag.Title = "True Clarity";

            var newAccounts = query.GetNewAccounts();

            return View(newAccounts);
        }
    }
}
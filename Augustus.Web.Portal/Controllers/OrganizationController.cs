using System.Threading.Tasks;
using System.Web.Mvc;
using Augustus.CRM.Queries;
using Augustus.Web.Portal.ViewModels;
using Augustus.Domain.Objects;

namespace Augustus.Web.Portal.Controllers
{
    public class OrganizationController : BaseCrmController
    {

        // GET: /Organization/ActiveAccounts
        public async Task<ActionResult> ActiveAccounts()
        {
            using (var context = await GetCrmContext())
            {
                var query = new OrganizationQuery(context);

                var viewModel = new OrganizationViewModel
                {
                    Title = "True Clarity",
                    Accounts = query.GetActiveAccounts()
                };

                return View(viewModel);
            }
        }

        // GET: /Organization/NewAccounts
        public async Task<ActionResult> NewAccounts()
        {
            using (var context = await GetCrmContext())
            {
                var query = new OrganizationQuery(context);

                var viewModel = new OrganizationViewModel
                {
                    Title = "True Clarity",
                    Accounts = query.GetNewAccounts()
                };

                return View(viewModel);
            }
        }
    }
}
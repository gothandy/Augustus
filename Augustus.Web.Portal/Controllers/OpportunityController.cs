using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class OpportunityController : CrmBaseController
    {
        // GET: Opportunity
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Opportunity/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid id)
        {
            using (var query = await GetOpportunityQuery())
            {
                Response.AppendHeader("guid", id.ToString());

                ViewBag.Account = query.GetAccount(id);
                ViewBag.Opportunity = query.GetItem(id);

                return View(query.GetInvoices(id));
            }
        }

        // GET: Opportunity/Create/{id}
        public async Task<ActionResult> Create(Guid id)
        {
            using (var query = await GetAccountQuery())
            {
                ViewBag.Account = query.GetItem(id);
            }

            using (var query = await GetOrganizationQuery())
            {
                ViewBag.Title = "Create Opportunity";

                ViewBag.Accounts = query.GetNewAndActiveAccounts(
                    createdAfter: lastThreeMonths,
                    withInvoicesFrom: lastYear);

                return View();
            }
        }

        // POST: Opportunity/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name,AccountId")] Opportunity opportunity)
        {
            using (var query = await GetOpportunityQuery())
            {
                var id = query.Create(opportunity);

                return RedirectToAction("Invoices", new { id = id });
            }
        }

        // GET: Opportunity/Edit/{id}
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Title = "Edit Opportunity";

            using (var query = await GetOrganizationQuery())
            {
                ViewBag.Accounts = query.GetNewAndActiveAccounts(
                    createdAfter: DateTime.Now.AddMonths(-3),
                    withInvoicesFrom: DateTime.Now.AddYears(-1));
            }

            using (var query = await GetOpportunityQuery())
            {
                ViewBag.Account = query.GetAccount(id);
                return View(query.GetItem(id));
            }
        }

        // POST: Opportunity/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = "Name,AccountId")] Opportunity opportunity)
        {
            using (var query = await GetOpportunityQuery())
            {
                opportunity.Id = id;
                query.Update(opportunity);

                return RedirectToAction("Invoices", new { Id = id });
            }
        }

        // GET: Opportunity/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var query = await GetOpportunityQuery())
            {
                var opportunity = query.GetItem(id);
                query.Delete(id);
                return RedirectToAction("Opportunities", "Account", new { id = opportunity.AccountId });
            };
        }
    }
}

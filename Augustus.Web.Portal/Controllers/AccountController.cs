using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class AccountController : CrmBaseController
    {

        // GET: /Account
        public async Task<ActionResult> Index()
        {
            using (var query = await GetOrganizationQuery())
            {
                ViewBag.NewDate = lastThreeMonths;
                return View(query.GetNewAndActiveAccounts(
                    createdAfter:lastThreeMonths,
                    invoicesFrom:lastYear));
            }
        }

        //GET: /Account/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid id)
        {
            using (var query = await GetAccountQuery())
            {
                Response.AppendHeader("guid", id.ToString());
                ViewBag.Account = query.GetAccount(id);
                return View(query.GetInvoices(id, from:lastYear));
            }
        }

        // GET: /Account/Opportunities/{id}
        public async Task<ActionResult> Opportunities(Guid id)
        {
            using (var query = await GetAccountQuery())
            {
                ViewBag.Account = query.GetAccount(id);
                return View(query.GetNewAndActiveOpportunities(
                    accountId: id,
                    createdAfter: lastThreeMonths,
                    invoiceFrom: lastYear));
            }
        }

        public ActionResult Create()
        {
            ViewBag.Title = "New Account";
            ViewBag.SubmitButton = "Create";
            return View("Form");
        }

        // POST: /Account/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name")] Account account)
        {
            using (var query = await GetAccountQuery())
            {
                var id = query.CreateAccount(account);
                return RedirectToAction("Invoices", new { id = id });
            }
        }

        // GET: Account/Edit/{id}
        public async Task<ActionResult> Edit(Guid id)
        {
            using (var query = await GetAccountQuery())
            {
                ViewBag.Title = "Edit Account";
                ViewBag.SubmitButton = "Edit";
                return View("Form", query.GetAccount(id));
            }
        }

        // POST: Account/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = "Name")] Account account)
        {
            using (var query = await GetAccountQuery())
            {
                account.Id = id;
                query.UpdateAccount(account);
                return RedirectToAction("Invoices", new { Id = id });
            }
        }

        // GET: /Account/Delete/{id}
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var query = await GetAccountQuery())
            {
                query.DeleteAccount(id);
                return RedirectToAction("Index");
            }
        }
    }
}
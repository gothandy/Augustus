using Augustus.Domain.Objects;
using System;
using System.Net;
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
                var lastYear = DateTime.Now.AddYears(-1);
                var lastThreeMonths = DateTime.Now.AddMonths(-3);

                ViewBag.NewDate = lastThreeMonths;
                return View(query.GetNewAndActiveAccounts(createdAfter:lastThreeMonths, invoicesFrom:lastYear));
            }
        }

        //GET: /Account/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var query = await GetAccountQuery())
            {
                var pastYear = DateTime.Now.AddYears(-1);

                ViewBag.Account = query.GetAccount(id.Value);

                return View(query.GetInvoices(id.Value, from:pastYear));
            }
        }

        // GET: /Account/Opportunities/{id}
        public async Task<ActionResult> Opportunities(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var query = await GetAccountQuery())
            {
                var invoiceFrom = DateTime.Now.AddYears(-1);
                var createdAfter = DateTime.Now.AddMonths(-3);

                ViewBag.ActiveDate = invoiceFrom;
                ViewBag.Account = query.GetAccount(id.Value);
                return View(query.GetNewAndActiveOpportunities(id.Value, createdAfter, invoiceFrom));
            }
        }

        public ActionResult Create()
        {
            return View();
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
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var query = await GetAccountQuery())
            {
                return View(query.GetAccount(id.Value));
            }
                
        }

        // POST: Account/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid? id, [Bind(Include = "Name")] Account account)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var query = await GetAccountQuery())
            {
                account.Id = id.Value;
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
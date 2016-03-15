using Augustus.CRM;
using Augustus.CRM.Queries;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    [Authorize]
    public class AccountController : CrmBaseController
    {
        // GET: /Account
        public async Task<ActionResult> Index()
        {
            using (var query = await GetOrganizationQuery())
            {
                query.ActiveDate = DateTime.Now.AddYears(-1);
                query.NewDate = DateTime.Now.AddMonths(-3);

                ViewBag.NewDate = query.NewDate;
                return View(query.GetNewAndActiveAccounts());
            }
        }

        //GET: /Account/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var query = await GetAccountQuery())
            {
                query.Id = id.Value;
                query.ActiveDate = DateTime.Now.AddYears(-1);

                ViewBag.Account = query.GetAccount();

                return View(query.GetInvoices());
            }
        }

        // GET: /Account/Opportunities/{id}
        public async Task<ActionResult> Opportunities(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (OrgQueryable org = await GetOrgQueryable())
            {
                var account = new AccountQuery()
                {
                    Organization = org,
                    Id = id.Value,
                    ActiveDate = DateTime.Now.AddYears(-1)
                };

                ViewBag.Account = account.GetAccount();
                ViewBag.ActiveDate = account.ActiveDate;

                return View(account.GetOpportunities());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: /Account/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name")] CRM.Entities.AccountEntity account)
        {
            if (ModelState.IsValid)
            {
                using (OrgQueryable org = await GetOrgQueryable())
                {
                    org.Create<CRM.Entities.AccountEntity>(account);
                    org.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Opportunity/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (OrgQueryable org = await GetOrgQueryable())
            {
                var account = new AccountQuery()
                {
                    Organization = org,
                    Id = id.Value
                };

                return View(account.GetAccount());
            }
                
        }

        // POST: Opportunity/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid? id, [Bind(Include = "Name")] Account newAccount)
        {
            using (OrgQueryable org = await GetOrgQueryable())
            {
                // Do this inside query.
                var account = new AccountQuery()
                {
                    Organization = org,
                    Id = id.Value
                };
                var oldAccount = account.GetAccount();

                oldAccount.Name = newAccount.Name;

                //org.Update<CRM.Entities.AccountEntity>(oldAccount);
                
                org.SaveChanges();

                return RedirectToAction("Invoices", new { Id = id });
            }
        }

        // DELETE: /Account/Delete/{id}
        // GET: /Account/Delete/{id}
        public async Task<ActionResult> Delete(Guid id)
        {
            using (OrgQueryable org = await GetOrgQueryable())
            {
                var account = org.Accounts.Single(a => a.Id == id);

                org.Delete<CRM.Entities.AccountEntity>(account);
                org.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
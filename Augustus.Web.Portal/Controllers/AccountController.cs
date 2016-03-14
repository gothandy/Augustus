using Augustus.CRM;
using Augustus.Domain;
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
            using (var org = await GetOrgQueryable())
            {
                var accounts = new Accounts()
                {
                    Organization = org,
                    ActiveDate = DateTime.Now.AddYears(-1),
                    NewDate = DateTime.Now.AddMonths(-3)
                };

                ViewBag.NewDate = accounts.NewDate;
                return View(accounts.GetNewAndActiveAccounts());
            }
        }

        //GET: /Account/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var org = await GetOrgQueryable())
            {
                var account = new Domain.Account()
                {
                    Organization = org,
                    Id = id.Value,
                    ActiveDate = DateTime.Now.AddYears(-1)
                };

                ViewBag.Account = account.GetAccount();
                return View(account.GetInvoices());
            }
        }

        // GET: /Account/Opportunities/{id}
        public async Task<ActionResult> Opportunities(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (OrgQueryable org = await GetOrgQueryable())
            {
                var account = new Domain.Account()
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
        public async Task<ActionResult> Create([Bind(Include = "Name")] CRM.Entities.Account account)
        {
            if (ModelState.IsValid)
            {
                using (OrgQueryable org = await GetOrgQueryable())
                {
                    org.Create<CRM.Entities.Account>(account);
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
                var account = new Account()
                {
                    Organization = org,
                    Id = id.Value
                };

                return View(account.GetAccount());
            }
                
        }

        // POST: Opportunity/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid? id, [Bind(Include = "Name")] CRM.Entities.Account newAccount)
        {
            using (OrgQueryable org = await GetOrgQueryable())
            {
                var account = new Account()
                {
                    Organization = org,
                    Id = id.Value
                };
                var oldAccount = account.GetAccount();

                oldAccount.Name = newAccount.Name;

                org.Update<CRM.Entities.Account>(oldAccount);
                
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

                org.Delete<CRM.Entities.Account>(account);
                org.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
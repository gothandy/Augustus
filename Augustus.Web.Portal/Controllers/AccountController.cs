using Augustus.CRM;
using System;
using System.Collections.Generic;
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
            IEnumerable<Account> accounts;

            var newDate = DateTime.Now.AddMonths(-12);

            ViewBag.NewDate = newDate;

            using (OrgQueryable org = await GetOrgQueryable())
            {
                var activeAccounts = (from a in org.Accounts
                                  join i in org.Invoices
                                  on a.Id equals i.AccountId
                                  where i.InvoiceDate > newDate
                                  orderby a.Name ascending
                                  select a).Distinct().AsEnumerable();

                var newAccounts = (from a in org.Accounts
                               where a.Created > newDate
                               select a).AsEnumerable();

                accounts = activeAccounts.Union(newAccounts).Distinct().OrderBy(a => a.Name);
            }

            return View(accounts);
        }

        //GET: /Account/Details/{id}
        public async Task<ActionResult> Details(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IEnumerable<Invoice> invoices;

            using (OrgQueryable org = await GetOrgQueryable())
            {
                ViewBag.Account = (from a in org.Accounts
                                   where a.Id == id.Value
                                   select a).Single();

                invoices = (from i in org.Invoices
                            where i.AccountId == id.Value
                            && i.InvoiceDate > new DateTime(2015, 3, 1)
                            orderby i.InvoiceDate descending
                            select i).AsEnumerable();
            }

            return View(invoices);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: /Account/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name")] Account account)
        {
            if (ModelState.IsValid)
            {
                using (OrgQueryable org = await GetOrgQueryable())
                {
                    org.Create<Account>(account);
                    org.Save();
                }

                return RedirectToAction("Index");
            }

            return View(account);
        }

        // DELETE: /Account/Delete/{id}
        // GET: Invoice/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            using (OrgQueryable org = await GetOrgQueryable())
            {
                var account = org.Accounts.Single(a => a.Id == id);

                //Delete
            }

            return RedirectToAction("Index");
        }
    }
}
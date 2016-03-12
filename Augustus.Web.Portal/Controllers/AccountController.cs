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
        public async Task<ActionResult> Index()
        {
            IEnumerable<Account> activeAccounts;

            using (OrgQueryable org = await GetOrgQueryable())
            {
                activeAccounts = (from a in org.Accounts
                                  join i in org.Invoices
                                  on a.Id equals i.AccountId
                                  where i.InvoiceDate > new DateTime(2015, 3, 1)
                                  orderby a.Name ascending
                                  select a).Distinct().AsEnumerable();
            }

            return View(activeAccounts);
        }

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
        public ActionResult Create([Bind(Include = "Name")] Account account)
        {
            if (ModelState.IsValid)
            {
                //db.Movies.Add(account);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }
    }
}
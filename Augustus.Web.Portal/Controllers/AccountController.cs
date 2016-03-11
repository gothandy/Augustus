using Augustus.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
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
                                  on a.Id equals i.DirectClientId
                                  where i.InvoiceDate > new DateTime(2015, 3, 1)
                                  orderby a.Name ascending
                                  select a).Distinct().AsEnumerable();
            }

            return View(activeAccounts);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            IEnumerable<Invoice> invoices;

            using (OrgQueryable org = await GetOrgQueryable())
            {
                ViewBag.Account = (from a in org.Accounts
                                   where a.Id == id
                                   select a).Single();

                invoices = (from i in org.Invoices
                            where i.DirectClientId == id
                            && i.InvoiceDate > new DateTime(2015, 3, 1)
                            orderby i.InvoiceDate descending
                            select i).AsEnumerable();
            }

            return View(invoices);
        }
    }
}
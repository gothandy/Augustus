using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class AccountController : CrmBaseController
    {
        //GET: /Account/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new AccountQuery(context);
                Response.AppendHeader("guid", id.ToString());
                return View(query.GetItem(id));
            }
        }

        // GET: /Account/Opportunities/{id}
        public async Task<ActionResult> Opportunities(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new AccountQuery(context);
                Response.AppendHeader("guid", id.ToString());
                return View(query.GetItem(id));
            }
        }

        public async Task<ActionResult> Create()
        {
            using (var context = await GetCrmContext())
            {
                var query = new AccountQuery(context);
                ViewBag.Title = "New Account";
                ViewBag.SubmitButton = "Create";
                return View();
            }
        }

        // POST: /Account/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name")] Account account)
        {
            using (var context = await GetCrmContext())
            {
                var query = new AccountQuery(context);
                var id = query.Create(account);
                return RedirectToAction("Invoices", new { id = id });
            }
        }

        // GET: Account/Edit/{id}
        public async Task<ActionResult> Edit(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new AccountQuery(context);
                ViewBag.Title = "Edit Account";
                ViewBag.SubmitButton = "Edit";
                return View(query.GetItem(id));
            }
        }

        // POST: Account/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = "Name")] Account account)
        {
            using (var context = await GetCrmContext())
            {
                var query = new AccountQuery(context);
                account.Id = id;
                query.Update(account);
                return RedirectToAction("Invoices", new { Id = id });
            }
        }

        // GET: /Account/Delete/{id}
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new AccountQuery(context);
                query.Delete(id);
                return RedirectToAction("ActiveAccounts", "Organization");
            }
        }
    }
}
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class AccountController : CrmBaseController
    {
        private AccountQuery query;
        public AccountController() : base()
        {
            query = new AccountQuery(context);
        }

        //GET: /Account/Invoices/{id}
        public ActionResult Invoices(Guid id)
        {
            Response.AppendHeader("guid", id.ToString());
            return View(query.GetItem(id));
        }

        // GET: /Account/Opportunities/{id}
        public ActionResult Opportunities(Guid id)
        {
            Response.AppendHeader("guid", id.ToString());
            return View(query.GetItem(id));
        }

        public ActionResult Create()
        {
            ViewBag.Title = "New Account";
            ViewBag.SubmitButton = "Create";
            return View();
        }

        // POST: /Account/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name")] Account account)
        {
            var id = query.Create(account);
            return RedirectToAction("Invoices", new { id = id });
        }

        // GET: Account/Edit/{id}
        public ActionResult Edit(Guid id)
        {
            ViewBag.Title = "Edit Account";
            ViewBag.SubmitButton = "Edit";
            return View(query.GetItem(id));
        }

        // POST: Account/Edit/{id}
        [HttpPost]
        public ActionResult Edit(Guid id, [Bind(Include = "Name")] Account account)
        {
            account.Id = id;
            query.Update(account);
            return RedirectToAction("Invoices", new { Id = id });
        }

        // GET: /Account/Delete/{id}
        public ActionResult Delete(Guid id)
        {
            query.Delete(id);
            return RedirectToAction("ActiveAccounts", "Organization");
        }
    }
}
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Augustus.CRM;
using Augustus.Domain.Interfaces;
using Augustus.Web.Portal.Models;

namespace Augustus.Web.Portal.Controllers
{
    public class AccountController : BaseWriteController<Account>
    {
        public AccountController():base()
        {
            bindInclude = "Name";
        }

        protected override IQuery<Account> GetQuery(CrmContext context)
        {
            return new AccountQuery(context);
        }

        protected override void SetCreateViewBag(CrmContext context, Guid? parentId)
        {
            ViewBag.Title = "Create Account";
            ViewBag.FormButtons = new FormButtons(GetParentUrl(parentId));
            ViewBag.Breadcrumb = new Breadcrumb();
        }

        protected override void SetEditViewBag(CrmContext context, Guid id)
        {
            ViewBag.Title = "Edit Account";
            ViewBag.FormButtons = new FormButtons(id, GetDefaultUrl(id));
            ViewBag.Breadcrumb = new Breadcrumb
            {
                Account = new AccountQuery(context).GetItem(id)
            };
        }
        protected override string GetParentUrl(Guid? parentId)
        {
            return Url.Action("ActiveAccounts", "Organization");
        }

        protected override string GetDefaultUrl(Guid id)
        {
            return Url.Action("Invoices", new { Id = id });
        }

        //GET: /Account/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid id)
        {
            Response.AppendHeader("guid", id.ToString());

            using (var context = await GetCrmContext())
            {
                var query = GetQuery(context);
                var model = query.GetItem(id);

                ViewBag.Title = model.Name;
                ViewBag.Breadcrumb = new Breadcrumb();
                return View(model);
            }
        }

        // GET: /Account/Opportunities/{id}
        public async Task<ActionResult> Opportunities(Guid id)
        {
            Response.AppendHeader("guid", id.ToString());

            using (var context = await GetCrmContext())
            {
                var query = GetQuery(context);
                var model = query.GetItem(id);

                ViewBag.Title = model.Name;
                ViewBag.Breadcrumb = new Breadcrumb();
                return View(model);
            }
        }
    }
}
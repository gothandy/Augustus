using Augustus.CRM;
using Augustus.CRM.Queries;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using Augustus.Web.Portal.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class OpportunityController : BaseWriteController<Opportunity>
    {
        public OpportunityController():base()
        {
            bindInclude = "Name,AccountId";
        }

        protected override IQuery<Opportunity> GetQuery(CrmContext context)
        {
            return new OpportunityQuery(context);
        }

        protected override void SetCreateViewBag(CrmContext context, Guid? parentId)
        {
            ViewBag.Title = "Create Opportunity";
            ViewBag.FormButtons = new FormButtons(GetParentUrl(parentId));
            ViewBag.Accounts = new OrganizationQuery(context).GetNewAndActiveAccounts();
            ViewBag.Account = new AccountQuery(context).GetItem(parentId.Value);
        }

        protected override void SetEditViewBag(CrmContext context, Guid id)
        {
            ViewBag.Title = "Edit Opportunity";
            ViewBag.FormButtons = new FormButtons(id, GetDefaultUrl(id));
            ViewBag.Accounts = new OrganizationQuery(context).GetNewAndActiveAccounts();
            ViewBag.Breadcrumb = new Breadcrumb
            {
                Account = new OpportunityQuery(context).GetParent(id),
                Opportunity = new OpportunityQuery(context).GetItem(id)
            };
        }

        protected override string GetParentUrl(Guid? parentId)
        {
            return Url.Action("Opportunities", "Account", new { Id = parentId });
        }

        protected override string GetDefaultUrl(Guid id)
        {
            return Url.Action("Invoices", new { Id = id });
        }

        //GET: /Opportunity/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid id)
        {
            Response.AppendHeader("guid", id.ToString());

            using (var context = await GetCrmContext())
            {
                var query = new OpportunityQuery(context);
                var model = query.GetItem(id);

                ViewBag.Title = model.Name;
                ViewBag.Breadcrumb = new Breadcrumb
                {
                    Account = query.GetParent(id)
                };
                return View(model);
            }
        }
    }
}

using Augustus.CRM;
using Augustus.CRM.Queries;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using Augustus.Web.Portal.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class OpportunityController : BaseWriteController<OpportunityWriteViewModel, Opportunity>
    {
        private const string bindAttributes =  "Name,AccountId";

        protected override IQuery<Opportunity> GetQuery(CrmContext context)
        {
            return new OpportunityQuery(context);
        }

        protected override void RefreshCreateViewModel(CrmContext context, Guid? parentId, ref OpportunityWriteViewModel model)
        {
            model.Title = "Create Opportunity";
            model.FormButtons = new FormButtons(GetParentUrl(parentId));
            model.Accounts = new OrganizationQuery(context).GetNewAndActiveAccounts();
            model.AccountId = parentId.Value;
            model.Breadcrumb = new Breadcrumb
            {
                Account = new AccountQuery(context).GetItem(parentId.Value)
            };
        }

        protected override void RefreshEditViewModel(CrmContext context, Guid id, ref OpportunityWriteViewModel model)
        {
            model.Title = "Edit Opportunity";
            model.FormButtons = new FormButtons(id, GetDefaultUrl(id));
            model.Accounts = new OrganizationQuery(context).GetNewAndActiveAccounts();
            model.AccountId = new OpportunityQuery(context).GetParent(id).Id.Value;
            model.Breadcrumb = new Breadcrumb
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
                var viewModel = new OpportunityReadViewModel
                {
                    Title = model.Name,
                    Opportunity = model,
                    Breadcrumb = new Breadcrumb
                    {
                        Account = query.GetParent(id)
                    }
                };
                return View(viewModel);
            }
        }
    }
}

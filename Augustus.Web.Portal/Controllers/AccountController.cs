using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Augustus.CRM;
using Augustus.Domain.Interfaces;
using Augustus.Web.Portal.ViewModels;

namespace Augustus.Web.Portal.Controllers
{
    public class AccountController : BaseWriteController<AccountWriteViewModel, Account>
    {
        private const string bindAttributes = "Name";

        protected override IQuery<Account> GetQuery(CrmContext context)
        {
            return new AccountQuery(context);
        }

        protected override void RefreshCreateViewModel(CrmContext context, Guid? parentId, ref AccountWriteViewModel model)
        {
            model.Title = "Create Account";
            model.Breadcrumb = new Breadcrumb();
            model.FormButtons = new FormButtons(GetParentUrl(parentId));
        }

        protected override void RefreshEditViewModel(CrmContext context, Guid id, ref AccountWriteViewModel model)
        {
            model.Title = "Edit Account";
            model.FormButtons = new FormButtons(id, GetDefaultUrl(id));
            model.Breadcrumb = new Breadcrumb
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
                var viewModel = new AccountReadViewModel
                {
                    Title = model.Name,
                    Breadcrumb = new Breadcrumb(),
                    Account = model,
                    EditButton = ButtonViewModel.Create(
                        ButtonViewModel.Edit, "Account", 
                        Url.Action("Edit", "Account", new { id = model.Id }))
                };

                return View(viewModel);
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

                var viewModel = new AccountReadViewModel
                {
                    Title = model.Name,
                    Breadcrumb = new Breadcrumb(),
                    DomainModel = model,
                    CreateButton = ButtonViewModel.Create(
                        ButtonViewModel.New, "Opportunity",
                        Url.Action("Create", "Opportunity", new { id = model.Id })
                        ),
                    EditButton = ButtonViewModel.Create(
                        ButtonViewModel.Edit, "Account",
                        Url.Action("Edit", "Account", new { id = model.Id }))
                };
                
                return View(viewModel);
            }
        }
    }
}
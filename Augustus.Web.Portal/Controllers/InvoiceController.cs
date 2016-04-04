using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Augustus.CRM;
using Augustus.Domain.Interfaces;
using Augustus.Web.Portal.ViewModels;
using System.Collections.Generic;
using Augustus.CRM.Entities;

namespace Augustus.Web.Portal.Controllers
{
    public class InvoiceController : BaseWriteController<InvoiceWriteViewModel, Invoice>
    {
        private const string bindAttributes = "OpportunityId,Name,Revenue,Cost,InvoiceDate,PONumber,InvoiceNo,Status";

        protected override IQuery<Invoice> GetQuery(CrmContext context)
        {
            return new InvoiceQuery(context);
        }
        protected override void RefreshCreateViewModel(CrmContext context, Guid? parentId, ref InvoiceWriteViewModel model)
        {
            if (!parentId.HasValue) throw new ArgumentNullException("Invoice requires a ParentId.");
            var opportunityId = parentId.Value;

            var opportunity = new OpportunityQuery(context).GetItem(opportunityId);

            model.Title = "Create Invoice";
            model.OpportunityId = opportunity.Id.Value;
            model.Opportunities = new InvoiceQuery(context).GetParentLookup(opportunityId);
            model.FormButtons = new FormButtons(GetParentUrl(parentId));
            model.Breadcrumb = new Breadcrumb
            {
                Account = new OpportunityQuery(context).GetParent(opportunityId),
                Opportunity = opportunity
            };
        }

        protected override void RefreshEditViewModel(CrmContext context, Guid id, ref InvoiceWriteViewModel model)
        {
            var invoice = new InvoiceQuery(context).GetItem(id);
            var opportunityId = invoice.OpportunityId.Value;

            model.Title = "Edit Invoice";
            model.Opportunities = new InvoiceQuery(context).GetParentLookup(opportunityId);
            model.OpportunityId = opportunityId;
            model.FormButtons = new FormButtons(id, GetDefaultUrl(id));
            model.Breadcrumb = new Breadcrumb
            {
                Account = new OpportunityQuery(context).GetParent(opportunityId),
                Opportunity = new OpportunityQuery(context).GetItem(opportunityId),
                Invoice = invoice
            };
        }

        protected override string GetDefaultUrl(Guid id)
        {
            return Url.Action("Details", new { id = id });
        }

        protected override string GetParentUrl(Guid? parentId)
        {
            return Url.Action("Invoices", "Opportunity", new { id = parentId.Value });
        }

        // GET: Invoice/Details/{id}
        public async Task<ActionResult> Details(Guid id)
        {
            Response.AppendHeader("guid", id.ToString());

            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);
                var model = query.GetItem(id);

                var viewModel = new InvoiceReadViewModel
                {
                    Title = model.Name,
                    Invoice = model,
                    Breadcrumb = new Breadcrumb
                    {
                        Account = new OpportunityQuery(context).GetParent(model.OpportunityId.Value),
                        Opportunity = new OpportunityQuery(context).GetItem(model.OpportunityId.Value)
                    },
                    Alerts = CheckForAlerts(model),
                    OpenButton = ButtonViewModel.Create(ButtonViewModel.Open, "CRM", string.Format("https://trueclarity4.crm4.dynamics.com/main.aspx?etc={0}&id={{{1}}}&pagetype=entityrecord", InvoiceEntity.EntityTypeCode, model.Id)),
                    EditButton = ButtonViewModel.Create(ButtonViewModel.Edit, "Invoice", Url.Action("Edit", new { id = model.Id }))
                };

                return View(viewModel);
            }
        }

        private static List<AlertViewModel> CheckForAlerts(Invoice model)
        {
            List<AlertViewModel> alerts = new List<AlertViewModel>();

            CheckMarginEqualsWorkDoneTotal(model, alerts);

            return (alerts.Count == 0) ? null : alerts;
        }

        private static void CheckMarginEqualsWorkDoneTotal(Invoice model, List<AlertViewModel> alerts)
        {
            if (model.WorkDoneItems != null && model.Margin != model.WorkDoneItems.Sum(i => i.Margin))
            {
                alerts.Add(
                    new AlertViewModel
                    {
                        Text = "The total of Work Done margin should equal the invoice margin amount.",
                        Level = AlertLevel.Warning
                    });

            }
        }
    }
}

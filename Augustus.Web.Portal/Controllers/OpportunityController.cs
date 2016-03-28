using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class OpportunityController : CrmBaseController
    {
        //GET: /Opportunity/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new OpportunityQuery(context);
                Response.AppendHeader("guid", id.ToString());

                var opp = query.GetItem(id);

                return View(opp);
            }
        }

        // GET: Opportunity/Create/{id}
        public async Task<ActionResult> Create(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new OpportunityQuery(context);
                ViewBag.Title = "Create Opportunity";

                ViewBag.Accounts = query.GetParentLookup();

                return View(query.GetNew(id));
            }
        }

        // POST: Opportunity/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name,AccountId")] Opportunity opportunity)
        {
            using (var context = await GetCrmContext())
            {
                var query = new OpportunityQuery(context);
                var id = query.Create(opportunity);

                return RedirectToAction("Invoices", new { id = id });
            }
        }

        // GET: Opportunity/Edit/{id}
        public async Task<ActionResult> Edit(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new OpportunityQuery(context);
                ViewBag.Title = "Edit Opportunity";

                ViewBag.Accounts = query.GetParentLookup();

                return View(query.GetItem(id));
            }
        }

        // POST: Opportunity/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = "Name,AccountId")] Opportunity opportunity)
        {
            using (var context = await GetCrmContext())
            {
                var query = new OpportunityQuery(context);
                opportunity.Id = id;
                query.Update(opportunity);

                return RedirectToAction("Invoices", new { Id = id });
            }
        }

        // GET: Opportunity/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new OpportunityQuery(context);

                var opportunity = query.GetItem(id);
                query.Delete(id);
                return RedirectToAction("Opportunities", "Account", new { id = opportunity.AccountId });
            }
        }
    }
}

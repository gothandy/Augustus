using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class InvoiceController : BaseCrmController
    {
        private const string bindAttributes = "OpportunityId,Name,Revenue,Cost,InvoiceDate,PONumber,InvoiceNo,Status";

        // GET: Invoice/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);
                return View(query.GetItem(id));
            }
        }

        // GET: Invoice/Create/{id}
        public async Task<ActionResult> Create(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);
                ViewBag.Opportunities = query.GetParentLookup(id);

                return View(query.GetNewItem(id));
            }
        }

        // POST: Invoice/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = bindAttributes)] Invoice invoice)
        {
            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);
                var id = query.Create(invoice);

                return RedirectToAction("Details", new { id = id });
            }
        }

        // GET: Invoice/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);
                ViewBag.Title = "Edit Invoice";

                var inv = query.GetItem(id);

                ViewBag.Opportunities = query.GetParentLookup(inv.OpportunityId.Value);

                return View(inv);
            }
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = bindAttributes)] Invoice invoice)
        {
            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);
                invoice.Id = id;

                query.Update(invoice);

                return RedirectToAction("Details", new { id = id });
            }
        }

        // GET: Invoice/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);
                var inv = query.GetItem(id);

                query.Delete(id);

                return RedirectToAction("Invoices", "Opportunity", new { id = inv.OpportunityId });
            }
        }
    }
}

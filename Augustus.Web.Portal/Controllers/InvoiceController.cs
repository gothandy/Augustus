using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class InvoiceController : CrmBaseController
    {
        private const string bindAttributes = "OpportunityId,Name,Revenue,Cost,InvoiceDate,PONumber,InvoiceNo,Status";

        InvoiceQuery query;

        public InvoiceController()
        {
            query = new InvoiceQuery(context);
        }

        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        // GET: Invoice/Details/5
        public ActionResult Details(Guid id)
        {
            return View(query.GetItem(id));
        }

        // GET: Invoice/Create/{id}
        public ActionResult Create(Guid id)
        {
            ViewBag.Opportunities = query.GetParentLookup(id);

            return View(query.GetNew(id));
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = bindAttributes)] Invoice invoice)
        {
            var id = query.Create(invoice);

            return RedirectToAction("Details", new { id = id });
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(Guid id)
        {
            ViewBag.Title = "Edit Invoice";

            var inv = query.GetItem(id);

            ViewBag.Opportunities = query.GetParentLookup(inv.OpportunityId.Value);

            return View(inv);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, [Bind(Include = bindAttributes)] Invoice invoice)
        {
            invoice.Id = id;

            query.Update(invoice);

            return RedirectToAction("Details", new { id = id });
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(Guid id)
        {
            var inv = query.GetItem(id);

            query.Delete(id);

            return RedirectToAction("Invoices", "Opportunity", new { id = inv.OpportunityId });
        }
    }
}

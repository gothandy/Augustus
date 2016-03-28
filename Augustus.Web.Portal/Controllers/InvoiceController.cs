using Augustus.CRM;
using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class InvoiceController : CrmBaseController
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        // GET: Invoice/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            using (var query = await GetInvoiceQuery())
            {
                return View(query.GetItem(id));
            }
        }

        // GET: Invoice/Create/{id}
        public async Task<ActionResult> Create(Guid id)
        {
            var inv = new Invoice();

            using (var query = await GetOpportunityQuery())
            {
                inv.Account = query.GetAccount(id);
                inv.Opportunity = query.GetItem(id);
            }

            using (var query = await GetAccountQuery())
            {
                var acc = query.GetItem(inv.Account.Id.Value);

                ViewBag.Opportunities = acc.Opportunities;
            }

            return View(inv);
        }

        private const string bindAttributes = "OpportunityId,Revenue,Cost,InvoiceDate,PONumber,InvoiceNo,Status";

        // POST: Invoice/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = bindAttributes)] Invoice invoice)
        {
            using (var query = await GetInvoiceQuery())
            {
                var id = query.Create(invoice);

                return RedirectToAction("Details", new { id = id });
            }
        }

        // GET: Invoice/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Title = "Edit Invoice";
            Invoice inv = null;

            using (var query = await GetInvoiceQuery())
            {
                inv = query.GetItem(id);
            }

            using (var query = await GetAccountQuery())
            {
                ViewBag.Opportunities = query.GetItem(inv.AccountId.Value).Opportunities;
            }

            return View("Form", inv);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = bindAttributes)] Invoice invoice)
        {
            using (var query = await GetInvoiceQuery())
            {
                query.Update(invoice);

                return RedirectToAction("Invoices", new { id = id });
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

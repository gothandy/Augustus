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
                return View(query.GetInvoice(id));
            }
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Title = "Edit Invoice";
            Invoice inv = null;

            using (var query = await GetInvoiceQuery())
            {
                inv = query.GetInvoice(id);
            }

            using (var query = await GetAccountQuery())
            {
                ViewBag.Opportunities = query.GetNewAndActiveOpportunities(
                    accountId: inv.AccountId.Value,
                    createdAfter: DateTime.Now.AddMonths(-3),
                    invoicesFrom: DateTime.Now.AddYears(-1));
            }

            return View("Form", inv);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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

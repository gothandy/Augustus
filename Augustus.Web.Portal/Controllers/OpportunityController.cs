using Augustus.CRM;
using Augustus.CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class OpportunityController : CrmBaseController
    {
        // GET: Opportunity
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Opportunity/Invoices/{id}
        public async Task<ActionResult> Invoices(Guid? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IEnumerable<Invoice> invoices;

            var date = DateTime.Now.AddMonths(-12);

            using (OrgQueryable org = await GetOrgQueryable())
            {
                ViewBag.Opportunity = org.Opportunities.Single(o => o.Id == id.Value);

                ViewBag.Account = (from a in org.Accounts
                                   join o in org.Opportunities
                                   on a.Id equals o.AccountId
                                   where o.Id == id.Value
                                   select a).Single();

                invoices = (from i in org.Invoices
                            where i.AccountId == id.Value
                            && i.InvoiceDate > date
                            orderby i.InvoiceDate descending
                            select i).AsEnumerable();
            }

            return View(invoices);
        }

        // GET: Opportunity/Details/5
        public ActionResult Details(Guid? id)
        {
            return View();
        }

        // GET: Opportunity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opportunity/Create
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

        // GET: Opportunity/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Opportunity/Edit/5
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

        // GET: Opportunity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Opportunity/Delete/5
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

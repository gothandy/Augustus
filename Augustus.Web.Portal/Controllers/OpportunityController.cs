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

            using (OrgQueryable org = await GetOrgQueryable())
            {
                var opp = new Domain.Opportunity()
                {
                    Id = id.Value,
                    Organization = org
                };

                ViewBag.Account = opp.GetAccount();
                ViewBag.Opportunity = opp.GetOpportunity();

                return View(opp.GetInvoices());
            }
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

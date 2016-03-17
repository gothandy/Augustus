using Augustus.CRM;
using Augustus.CRM.Entities;
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
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
        public async Task<ActionResult> Invoices(Guid id)
        {
            using (var query = await GetOpportunityQuery())
            {
                ViewBag.Account = query.GetAccount(id);
                ViewBag.Opportunity = query.GetOpportunity(id);

                return View(query.GetInvoices(id));
            }
        }

        // GET: Opportunity/Create/{id}
        public async Task<ActionResult> Create(Guid id)
        {
            using (var query = await GetOrganizationQuery())
            {
                ViewBag.AccountId = id;
                ViewBag.Accounts = query.GetNewAndActiveAccounts(
                    createdAfter: DateTime.Now.AddMonths(-3),
                    invoicesFrom: DateTime.Now.AddYears(-1));

                return View();
            }
        }

        // POST: Opportunity/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name,AccountId")] Opportunity opportunity)
        {
            using (var query = await GetOpportunityQuery())
            {
                var id = query.CreateOpportunity(opportunity);

                return RedirectToAction("Invoices", new { id = id });
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
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var query = await GetOpportunityQuery())
            {
                var opportunity = query.GetOpportunity(id);
                query.DeleteOpportunity(id);
                return RedirectToAction("Opportunites", "Account", new { id = opportunity.AccountId });
            };
        }
    }
}

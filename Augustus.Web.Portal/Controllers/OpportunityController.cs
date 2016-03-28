using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class OpportunityController : CrmBaseController
    {
        OpportunityQuery query;

        public OpportunityController () : base()
        {
            query = new OpportunityQuery(context);
        }

        // GET: Opportunity
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Opportunity/Invoices/{id}
        public ActionResult Invoices(Guid id)
        {
            Response.AppendHeader("guid", id.ToString());

            var opp = query.GetItem(id);

            return View(opp);
        }

        // GET: Opportunity/Create/{id}
        public ActionResult Create(Guid id)
        {
            ViewBag.Title = "Create Opportunity";

            ViewBag.Accounts = query.GetParentLookup();

            return View(query.GetNew(id));
        }

        // POST: Opportunity/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,AccountId")] Opportunity opportunity)
        {
            var id = query.Create(opportunity);

            return RedirectToAction("Invoices", new { id = id });
        }

        // GET: Opportunity/Edit/{id}
        public ActionResult Edit(Guid id)
        {
            ViewBag.Title = "Edit Opportunity";

            ViewBag.Accounts = query.GetParentLookup();

            return View(query.GetItem(id));
        }

        // POST: Opportunity/Edit/{id}
        [HttpPost]
        public ActionResult Edit(Guid id, [Bind(Include = "Name,AccountId")] Opportunity opportunity)
        {
            var query = new OpportunityQuery(context);
            opportunity.Id = id;
            query.Update(opportunity);

            return RedirectToAction("Invoices", new { Id = id });
        }

        // GET: Opportunity/Delete/5
        public ActionResult Delete(Guid id)
        {
            var query = new OpportunityQuery(context);
            var opportunity = query.GetItem(id);
            query.Delete(id);
            return RedirectToAction("Opportunities", "Account", new { id = opportunity.AccountId });
        }
    }
}

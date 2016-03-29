using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class WorkDoneController : CrmBaseController
    {
        // GET: WorkDone/Edit/{id}
        public async Task<ActionResult> Edit(Guid id)
        {
            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);

                ViewBag.Title = "Edit Work Done";

                var inv = query.GetItem(id);

                return View(inv);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, Invoice invoice)
        {
            using (var context = await GetCrmContext())
            {
                var query = new InvoiceQuery(context);

                invoice.Id = id;

                query.Update(invoice);

                return RedirectToAction("Details", "Invoice", new { id = id });
            }
        }

    }
}
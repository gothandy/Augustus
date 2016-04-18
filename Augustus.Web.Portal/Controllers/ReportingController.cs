using Augustus.CRM.Queries;
using Augustus.Web.Portal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class ReportingController : BaseCrmController
    {
        // GET: Reporting
        public async Task<ActionResult> Index()
        {
            using (var context = await GetCrmContext())
            {
                var query = new ReportQuery(context);

                var viewModel = new MonthlyReportViewModel
                {
                    Title = "Monthly Report",
                    Breadcrumb = new Breadcrumb(),
                    Months = query.GetMonths().ToList()
                };

                return View(viewModel);
            }
        }

        // GET: Reporting/{year}/{month}
        public async Task<ActionResult> Month(int year, int? month)
        {
            using (var context = await GetCrmContext())
            {
                var query = new ReportQuery(context);
                var date = new DateTime(year, month.Value, 1);

                var viewModel = new MonthReportViewModel
                {
                    Title = date.ToShortDateString(),
                    Breadcrumb = new Breadcrumb(),
                    Accounts = query.GetAccounts(date)

                };
                
                return View(viewModel);
            }
        }
    }
}
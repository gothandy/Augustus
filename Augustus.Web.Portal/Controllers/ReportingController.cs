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

                var viewModel = new ReportOrganizatonViewModel
                {
                    Title = "Monthly Report",
                    Breadcrumb = new Breadcrumb(),
                    Months = query.GetMonths().ToList()
                };

                return View(viewModel);
            }
        }

        // GET: Reporting/Month/{year}/{month}
        public async Task<ActionResult> Month(int year, int month)
        {
            using (var context = await GetCrmContext())
            {
                var query = new ReportQuery(context);
                var date = new DateTime(year, month, 1);

                var viewModel = new ReportMonthViewModel
                {
                    Title = date.ToShortDateString(),
                    Breadcrumb = new Breadcrumb(),
                    Accounts = query.GetAccounts(date),
                    Year = year,
                    Month = month
                };
                
                return View(viewModel);
            }
        }

        // GET: Reporting/Account/{year}/{month}/{account}
        public async Task<ActionResult> Account(int year, int month, Guid? account)
        {
            using (var context = await GetCrmContext())
            {
                var query = new ReportQuery(context);
                var date = new DateTime(year, month, 1);

                var viewModel = new ReportAccountViewModel
                {
                    Title = date.ToShortDateString(),
                    Breadcrumb = new Breadcrumb(),
                    Invoices = query.GetInvoices(date, account.Value),
                    Year = year,
                    Month = month,
                    Account = new AccountQuery(context).GetItem(account.Value)
                };

                return View(viewModel);
            }
        }
    }
}
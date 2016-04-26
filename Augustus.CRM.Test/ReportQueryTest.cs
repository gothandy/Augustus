using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class ReportQueryTest : BaseQueryTest
    {
        private static ReportQuery query;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();
            query = new ReportQuery(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public void CRM_ReportQuery_GetMonths()
        {
            var months = query.GetMonths().ToList();

            foreach(var m in months)
            {
                Console.WriteLine("{0} {1} {2}", m.Month, m.Invoice, m.WorkDone);
            }

            var apr15 = (from m in months where m.Month == new DateTime(2015, 4, 1) select m).Single();

            Assert.AreEqual((decimal)375461.2500, apr15.Invoice);
            Assert.AreEqual((decimal)314528.4100, apr15.WorkDone);

        }

        [TestMethod]
        public void CRM_ReportQuery_GetAccounts()
        {
            var accounts = query.GetAccounts(new DateTime(2016, 3, 1));

            foreach(var a in accounts)
            {
                Console.WriteLine("{0} {1} {2} {3}",
                    a.AccountName,
                    a.InvoiceTotal,
                    a.WorkDoneAndInvoiced,
                    a.WorkDoneTotal);
            }
        }

        //Reporting/Account/2016/3/b371603e-776a-e411-85fd-6c3be5becb24
        [TestMethod]
        public void CRM_ReportQuery_GetInvoices()
        {
            var invoices = query.GetInvoices(new DateTime(2016, 3, 1), new Guid("b371603e-776a-e411-85fd-6c3be5becb24"));

            foreach (var a in invoices)
            {
                Console.WriteLine("{0} {1} {2} {3}",
                    a.InvoiceName,
                    a.InvoiceDate,
                    a.Margin,
                    a.WorkDone);
            }
        }

        [TestMethod]
        public void CRM_ReportQuery_GetWorkDoneErrors()
        {
            var invoices = query.GetWorkDoneErrors();

            foreach (var a in invoices)
            {
                Console.WriteLine("{0} {1} {2} {3}",
                    a.InvoiceName,
                    a.InvoiceDate,
                    a.Margin,
                    a.WorkDone);
            }
        }

        [TestMethod]
        public void CRM_ReportQuery_GetExportData()
        {
            var data = query.GetExportData();

            foreach (var d in data)
            {
                Console.WriteLine("{0} {1} {2}",
                    d.Account.Name,
                    d.Invoice.Name,
                    d.WorkDoneItem.WorkDoneDate);
            }
        }
    }
}

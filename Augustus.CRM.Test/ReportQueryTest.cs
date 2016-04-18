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
                Console.WriteLine("{0} {1} {2} {3} {4} {5}",
                    a.Account,
                    a.InvoiceTotal,
                    a.WorkNotDoneAndInvoiced,
                    a.WorkDoneAndInvoiced,
                    a.WorkDoneAndNotInvoiced,
                    a.WorkDoneTotal);
            }
        }
    }
}

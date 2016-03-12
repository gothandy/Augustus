using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class WorkDoneItemCrudTests
    {
        private static OrgQueryable org;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            org = new OrgQueryable(connectionString);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            org.Dispose();
        }

        [TestMethod]
        public void CRM_WorkDoneItemCrud()
        {

            Invoice invoiceTest01 = org.Invoices.Single(a => a.Name == "Augustus Test 01");

            WorkDoneItem item1 = new WorkDoneItem()
            {
                InvoiceId = invoiceTest01.InvoiceId,
                WorkDoneDate = new DateTime(2015, 6, 1),
                Margin = (decimal?)12345.67
            };

            org.Create<WorkDoneItem>(item1);
            org.Save();

            item1 = org.WorkDoneItems.Single(i => i.Id == item1.Id);
            org.Delete<WorkDoneItem>(item1);
            org.Save();
        }
    }
}

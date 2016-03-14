using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using Augustus.CRM.Entities;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OrgEnumerableTests
    {
        private static Organization org;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            org = new Organization(connectionString);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            org.Dispose();
        }

        [TestMethod]
        public void OrgEnumerable_GetAccount()
        {
            var account = (Account)org.Accounts.Single(a => a.Name == "easyJet");
            Assert.AreEqual(new Guid("33b343c7-a684-e011-8da5-00271336e9df"), account.Id);
        }

        [TestMethod]
        public void OrgEnumerable_GetIAccount()
        {
            var account = org.Accounts.Single(a => a.Name == "easyJet");
            Assert.AreEqual(new Guid("33b343c7-a684-e011-8da5-00271336e9df"), account.Id);
        }

        [TestMethod]
        public void OrgEnumerable_GetInvoices()
        {
            var invoices = (from i in org.Invoices
                            where i.InvoiceDate > new DateTime(2016, 1, 1)
                            select i);

            foreach(var invoice in invoices)
            {
                Console.WriteLine("{0} {1} {2}", invoice.AccountId, invoice.InvoiceDate, invoice.Cost);
            }
        }

        [TestMethod]
        public void OrgEnumerable_AccountJoinInvoice()
        {
            var activeAccounts = (from a in org.Accounts
                                  join i in org.Invoices
                                  on a.Id equals i.AccountId
                                  where i.InvoiceDate > new DateTime(2015, 1, 1)
                                  select a).Distinct();

            foreach(var account in activeAccounts)
            {
                Console.WriteLine(account.Name);
            }
        }
    }
}

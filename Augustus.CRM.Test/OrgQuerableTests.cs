using Augustus.CRM.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OrgQueryableTests
    {
        private static CrmContext context;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            var svc = new CrmServiceConnectionString(connectionString);

            context = svc.GetContext();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public void OrgQueryable_GetAccount()
        {
            var account = (AccountEntity)context.Accounts.Single(a => a.Name == "easyJet");
            Assert.AreEqual(new Guid("33b343c7-a684-e011-8da5-00271336e9df"), account.Id);
        }

        [TestMethod]
        public void OrgQueryable_GetIAccount()
        {
            var account = context.Accounts.Single(a => a.Name == "easyJet");
            Assert.AreEqual(new Guid("33b343c7-a684-e011-8da5-00271336e9df"), account.Id);
        }

        [TestMethod]
        public void OrgQueryable_GetInvoices()
        {
            var invoices = (from i in context.Invoices
                            where i.InvoiceDate > new DateTime(2016, 1, 1)
                            select i);

            foreach(var invoice in invoices)
            {
                Console.WriteLine("{0} {1} {2}", invoice.AccountId, invoice.InvoiceDate, invoice.Cost);
            }
        }

        [TestMethod]
        public void OrgQueryable_AccountJoinInvoice()
        {
            var activeAccounts = (from a in context.Accounts
                                  join i in context.Invoices
                                  on a.Id equals i.AccountId
                                  where i.InvoiceDate > new DateTime(2015, 1, 1)
                                  select a).Distinct();

            foreach(var account in activeAccounts)
            {
                Console.WriteLine(account.Name);
            }
        }

        [TestMethod]
        public void OrgQueryable_AccountByCreated()
        {
            var newAccounts = from a in context.Accounts
                              where a.Created > new DateTime(2015, 1, 1)
                              select a;

            foreach (var account in newAccounts)
            {
                Console.WriteLine(account.Name);
            }
        }
    }
}

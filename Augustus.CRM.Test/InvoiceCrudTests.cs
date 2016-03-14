using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using Augustus.CRM.Entities;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class InvoiceCrudTests
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
        public void CRM_CrudInvoice()
        {
            Account testAccount = new Account()
            {
                Name = "TestAccount"
            };

            org.Create<Account>(testAccount);
            org.SaveChanges();

            Console.WriteLine(testAccount.Id);

            //TODO

            testAccount = org.Accounts.Single(a => a.Name == "TestAccount");

            Console.WriteLine(testAccount.Id);

            org.Delete<Account>(testAccount);
            org.SaveChanges();

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Linq;
using Augustus.Interfaces;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OrgQueryableTests
    {
        private static OrgQueryable org;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

            IOrganizationService _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient;

            org = new OrgQueryable(_orgService);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            org.Dispose();
        }

        [TestMethod]
        public void OrgQueryable_GetAccount()
        {
            var account = (Account)org.Accounts.Single(a => a.Name == "easyJet");
            Assert.AreEqual(new Guid("33b343c7-a684-e011-8da5-00271336e9df"), account.Id);
        }

        [TestMethod]
        public void OrgQueryable_GetIAccount()
        {
            var account = org.Accounts.Single(a => a.Name == "easyJet");
            Assert.AreEqual(new Guid("33b343c7-a684-e011-8da5-00271336e9df"), account.Id);
        }

        [TestMethod]
        public void OrgQueryable_GetInvoices()
        {
            var invoices = (from i in org.Invoices
                            where i.InvoiceDate > new DateTime(2016, 1, 1)
                            select i);

            foreach(var invoice in invoices)
            {
                Console.WriteLine("{0} {1} {2}", invoice.DirectClientId, invoice.InvoiceDate, invoice.Cost);
            }
        }

        [TestMethod]
        public void OrgQueryable_AccountJoinInvoice()
        {
            var activeAccounts = (from a in org.Accounts
                                  join i in org.Invoices
                                  on a.Id equals i.DirectClientId
                                  where i.InvoiceDate > new DateTime(2015, 1, 1)
                                  select a).Distinct();

            foreach(var account in activeAccounts)
            {
                Console.WriteLine(account.Name);
            }
        }
    }
}

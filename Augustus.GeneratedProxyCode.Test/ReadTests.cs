using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;
using System.Linq;

namespace Augustus.CRM.GeneratedProxyCode.Test
{
    [TestClass]
    public class ReadTests
    {
        private static CrmServiceContext context;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

            IOrganizationService _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient;

            context = new CrmServiceContext(_orgService);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public void GeneratedProxyCode_GetAccount()
        {
            Account account = context.AccountSet.First(a => a.Name == "easyJet");
            Assert.AreEqual("easyJet", account.Name);
        }

        [TestMethod]
        public void GeneratedProxyCode_GetActiveAccounts()
        {
            var Accounts = (from a in context.AccountSet
                            join i in context.new_invoiceSet
                            on a.Id equals i.new_account_new_invoice_DirectClient.Id
                            where i.new_InvoiceDate > new DateTime(2015,1,1)
                            select a).Distinct();

            foreach (var account in Accounts)
            {
                Console.WriteLine(account.Name);
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Linq;
using System.Diagnostics;

namespace Augustus.CRM.GeneratedProxyCode.Test
{
    [TestClass]
    public class WriteTest
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
        public void GeneratedProxyCode_CrudAccount()
        {
            createAccount();
            updateAccount();
            deleteAccount();
        }

        private void createAccount()
        {
            Account account = new Account()
            {
                Name = "TestAccount"
            };

            context.AddObject(account);
            context.SaveChanges();
        }

        private void updateAccount()
        {
            Account account = getAccount("TestAccount");
            account.Name = "TestAccount2";
            context.UpdateObject(account);
            context.SaveChanges();
        }

        private void deleteAccount()
        {
            Account account = getAccount("TestAccount2");
            context.DeleteObject(account);
            context.SaveChanges();
        }

        private Account getAccount(string name)
        {
            return context.AccountSet.Single(a => a.Name == name);
        }
    }
}

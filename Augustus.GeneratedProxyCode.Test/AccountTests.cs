using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Augustus.CRM.GeneratedProxyCode.Test
{
    [TestClass]
    public class AccountTests
    {
        private static CrmServiceContext context;
        private static AccountTools accountTools;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            context = ContextCreator.Create();
            accountTools = new AccountTools(context);
            accountTools.DeleteAll();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            accountTools.DeleteAll();
            context.Dispose();
        }

        [TestMethod]
        public void GeneratedProxyCode_Account_GetAccount()
        {
            var account = accountTools.Get("easyJet");
            Assert.AreEqual("easyJet", account.Name);
        }

        [TestMethod]
        public void GeneratedProxyCode_Organization_GetActiveAccounts()
        {
            var Accounts = (from a in context.AccountSet
                            join i in context.new_invoiceSet
                            on a.Id equals i.new_account_new_invoice_DirectClient.Id
                            where i.new_InvoiceDate > new DateTime(2015, 1, 1)
                            select a).Distinct().ToList();

            Assert.AreNotEqual(0, Accounts.Count());
            foreach (var account in Accounts)
            {
                Console.WriteLine(account.Name);
            }
        }

        [TestMethod]
        public void GeneratedProxyCode_Account_CrUD()
        {
            accountTools.Create(AccountTools.Name);
            accountTools.Update(AccountTools.Name, AccountTools.Rename);
            accountTools.Delete(AccountTools.Rename);
        }
    }
}

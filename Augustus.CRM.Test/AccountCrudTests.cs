using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class AccountCrudTests
    {
        private static OrgQueryable org;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            org = new OrgQueryable(connectionString);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            org.Dispose();
        }

        [TestMethod]
        public void CRM_CrudAccount()
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

            org.Create<Account>(account);
            org.Save();
        }

        private void updateAccount()
        {
            Account account = getAccount("TestAccount");
            account.Name = "TestAccount2";
            org.Update<Account>(account);
            org.Save();
        }

        private void deleteAccount()
        {
            Account account = getAccount("TestAccount2");
            org.Delete<Account>(account);
            org.Save();
        }

        private Account getAccount(string name)
        {
            return org.Accounts.Single(a => a.Name == name);
        }
    }
}

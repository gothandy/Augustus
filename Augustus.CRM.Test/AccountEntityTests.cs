using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class AccountEntityTests : BaseQueryTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();
            deleteAllTestAccounts();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllTestAccounts();
            context.Dispose();
        }

        [TestMethod]
        public void CRM_Entity_Account_CrUD()
        {
            createAccount(accountName);
            updateAccount(accountName, accountRename);
            deleteAccount(accountRename);
        }
    }
}

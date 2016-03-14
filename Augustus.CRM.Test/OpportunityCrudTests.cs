using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OpportunityCrudTests : BaseCrudTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            org.Dispose();
        }

        [TestMethod]
        public void CRM_CrudOpportunity()
        {
            createAccount("TestAccount");
            createOpportunity();
            deleteOpportunity();
            deleteAccount("TestAccount");
        }
    }
}

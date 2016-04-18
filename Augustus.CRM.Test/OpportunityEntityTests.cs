using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OpportunityEntityTests : BaseQueryTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();

            deleteAllOpportunities();
            deleteAllTestAccounts();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllOpportunities();
            deleteAllTestAccounts();
            context.Dispose();
        }

        [TestMethod]
        public void CRM_Entity_Opportunity_CrUD()
        {
            createAccount(accountName);
            var account = getAccount(accountName);

            createOpportunity(opportunityName, account);
            var opportunity = getOpportunity(opportunityName);

            Assert.AreEqual(opportunityName, opportunity.Name);
            Assert.IsNotNull(opportunity.AccountId);

            deleteOpportunity(opportunityName);

            deleteAccount(accountName);
        }
    }
}

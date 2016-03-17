using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace Augustus.CRM.GeneratedProxyCode.Test
{
    [TestClass]
    public class OpportunityTests
    {
        private static CrmServiceContext context;
        private static OpportunityTools opportunityTools;
        private static AccountTools accountTools;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            context = ContextCreator.Create();
            accountTools = new AccountTools(context);
            opportunityTools = new OpportunityTools(context);

            opportunityTools.DeleteAll();
            accountTools.DeleteAll();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            opportunityTools.DeleteAll();
            accountTools.DeleteAll();
            context.Dispose();
        }

        [TestMethod]
        public void GeneratedProxyCode_Opportunity_GetAccount()
        {
            var opportunity = context.OpportunitySet.Single(o => o.Id == new Guid("2d1a82de-4479-e411-be1f-6c3be5becb24"));

            var entRef = (EntityReference)opportunity.CustomerId;

            Assert.IsNotNull(entRef);

            var account = context.AccountSet.Single(a => a.Id == entRef.Id);
        }

        [TestMethod]
        public void GeneratedProxyCode_Opportunity_CrUD()
        {
            accountTools.Create(AccountTools.Name);
            Account account = accountTools.Get(AccountTools.Name);

            opportunityTools.Create(OpportunityTools.Name, account);

            var opportunity = opportunityTools.Get(OpportunityTools.Name);

            Assert.AreEqual(OpportunityTools.Name, opportunity.Name);
            Assert.IsNotNull(opportunity.CustomerId);
            Assert.AreEqual(AccountTools.Name, opportunity.CustomerId.Name);

            opportunityTools.Update(OpportunityTools.Name, OpportunityTools.Rename);
            opportunityTools.Delete(OpportunityTools.Rename);

            accountTools.Delete(AccountTools.Name);

        }
    }
}

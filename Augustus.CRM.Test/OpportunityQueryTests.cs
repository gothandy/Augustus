using Augustus.CRM.Entities;
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OpportunityQueryTests : BaseCrudTest
    {
        private static OpportunityQuery query;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();
            query = new OpportunityQuery { Organization = org };

            deleteAllOpportunities();
            deleteAllAccounts();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllOpportunities();
            deleteAllAccounts();
            org.Dispose();
        }

        [TestMethod]
        public void CRM_Query_Opportunity_GetInvoices()
        {
            Opportunity opp = OpportunityEntity.ToDomainObject(org.Opportunities.Single(o => o.Id == new Guid("2d1a82de-4479-e411-be1f-6c3be5becb24")));

            var invoices = query.GetInvoices(opp.Id);

            Assert.AreNotEqual(0, invoices.Count());

            foreach(var invoice in invoices)
            {
                Console.WriteLine(invoice.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Opportunity_GetAccount()
        {
            Opportunity opp = OpportunityEntity.ToDomainObject(org.Opportunities.Single(o => o.Id == new Guid("2d1a82de-4479-e411-be1f-6c3be5becb24")));

            var account = query.GetAccount(opp.Id);

            Assert.AreEqual("easyJet", account.Name);
        }

        [TestMethod]
        public void CRM_Query_Opportunity_CrUD()
        {
            // Create Account
            createAccount(accountName);
            var accountId = getAccount(accountName).Id;
            
            // Create Opportunity
            Opportunity opportunity = new Opportunity { Name = opportunityName, AccountId = accountId };
            var id = query.CreateOpportunity(opportunity);

            // Update Opportunity
            opportunity.Id = id;
            opportunity.Name = opportunityRename;
            query.UpdateOpportunity(opportunity);

            // Delete Opportunity
            query.DeleteOpportunity(id);

            // Delete Account
            deleteAccount(accountName);
        }
    }
}

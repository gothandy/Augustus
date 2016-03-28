using Augustus.CRM.Converters;
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
            query = new OpportunityQuery(context);

            deleteAllOpportunities();
            deleteAllAccounts();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllOpportunities();
            deleteAllAccounts();
            context.Dispose();
        }

        [TestMethod]
        public void CRM_Query_Opportunity_GetInvoices()
        {
            Opportunity opp = OpportunityConverter.ToDomainObject(context.Opportunities.Single(o => o.Id == new Guid("2d1a82de-4479-e411-be1f-6c3be5becb24")));

            var invoices = query.GetInvoices(opp.Id.Value);

            Assert.AreNotEqual(0, invoices.Count());

            foreach(var invoice in invoices)
            {
                Console.WriteLine(invoice.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Opportunity_GetAccount()
        {
            Opportunity opp = OpportunityConverter.ToDomainObject(context.Opportunities.Single(o => o.Id == new Guid("2d1a82de-4479-e411-be1f-6c3be5becb24")));

            var account = query.GetAccount(opp.Id.Value);

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
            var id = query.Create(opportunity);

            // Update Opportunity
            opportunity.Id = id;
            opportunity.Name = opportunityRename;
            query.Update(opportunity);

            // Delete Opportunity
            query.Delete(id);

            // Delete Account
            deleteAccount(accountName);
        }
    }
}

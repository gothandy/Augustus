using Augustus.CRM.Entities;
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class AccountQueryTests : BaseCrudTest
    {
        private static AccountQuery query;
        private static DateTime pastYear;
        private static AccountEntity easyJet;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();
            query = new AccountQuery { Organization = org };
            pastYear = DateTime.Now.AddYears(-1);
            easyJet = org.Accounts.Single(a => a.Name == "easyJet");

            deleteAllAccounts();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllAccounts();
            org.Dispose();
        }

        [TestMethod]
        public void CRM_Query_Account_GetInvoices()
        {
            var invoices = query.GetInvoices(easyJet.Id, from: pastYear);

            Assert.AreNotEqual(0, invoices.Count());

            foreach(var invoice in invoices)
            {
                Console.WriteLine(invoice.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Account_GeNewOpportunities()
        {
            var opportunities = query.GetNewOpportunities(easyJet.Id, createdAfter: pastYear);

            AssertNotEmptyAndWriteNames(opportunities);
        }

        [TestMethod]
        public void CRM_Query_Account_GetActiveOpportunities()
        {
            var opportunities = query.GetActiveOpportunities(easyJet.Id, invoicesFrom: pastYear);

            AssertNotEmptyAndWriteNames(opportunities);
        }

        [TestMethod]
        public void CRM_Query_Account_GetNewAndActiveOpportunities()
        {
            var opportunities = query.GetNewAndActiveOpportunities(easyJet.Id, pastYear, pastYear);

            AssertNotEmptyAndWriteNames(opportunities);
        }

        private static void AssertNotEmptyAndWriteNames(IEnumerable<Opportunity> opportunities)
        {
            Assert.AreNotEqual(0, opportunities.Count());

            foreach (var opportunity in opportunities)
            {
                Console.WriteLine(opportunity.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Account_CrUD()
        {
            // Create
            Account account = new Account { Name = accountName };
            var id = query.Create(account);

            // Updated
            account.Id = id;
            account.Name = accountRename;
            query.Update(account);
            var accountEntity = getAccount(accountRename);
            Assert.AreEqual(accountRename, accountEntity.Name);

            // Delete
            query.Delete(id);
        }
    }
}

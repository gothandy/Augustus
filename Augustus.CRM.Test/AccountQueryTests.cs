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
    public class AccountQueryTests : BaseQueryTest
    {
        private static Guid child = new Guid("2fd8a9d7-7dfa-e511-8119-3863bb343bf8");
        private static Guid parent = new Guid("5863a90f-16fb-e511-8124-3863bb34fa68");

        private static AccountQuery query;
        private static AccountEntity easyJet;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();

            query = new AccountQuery(context);

            easyJet = context.Accounts.Single(a => a.Name == "easyJet");

            deleteAllTestAccounts();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllTestAccounts();
            context.Dispose();
        }

        [TestMethod]
        public void CRM_Query_Account_GetParent()
        {
            var acc = query.GetItem(child);
            Assert.AreEqual(parent, acc.ParentAccountId);
        }

        [TestMethod]
        public void CRM_Query_Account_GetInvoices()
        {
            var acc = query.GetItem(easyJet.Id);

            Assert.AreNotEqual(0, acc.Invoices.Count());

            var inv = acc.Invoices.First();

            //Assert.IsNotNull(inv.Opportunity);

            foreach(var invoice in acc.Invoices)
            {
                Console.WriteLine(invoice.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Account_GetNewAndActiveOpportunities()
        {
            var acc = query.GetItem(easyJet.Id);

            AssertNotEmptyAndWriteNames(acc.Opportunities);
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
            Account account = new Account
            {
                Name = accountName,
                FullName = accountName + " Full Name",
                ParentAccountId = null
            };
            var id = query.Create(account);

            // Get
            account = query.GetItem(id);
            Assert.AreEqual(accountName, account.Name);
            Assert.AreEqual(accountName + " Full Name", account.FullName);


            // Updated
            account = new Account
            {
                Id = id,
                Name = accountRename,
                FullName = accountRename + " Full Name",
                ParentAccountId = parent
            };
            query.Update(account);
            var accountEntity = getAccount(accountRename);
            Assert.AreEqual(accountRename, accountEntity.Name);
            Assert.AreEqual(accountRename + " Full Name", accountEntity.FullName);
            Assert.AreEqual(parent, accountEntity.ParentAccountId);

            // Delete
            query.Delete(id);
        }
    }
}

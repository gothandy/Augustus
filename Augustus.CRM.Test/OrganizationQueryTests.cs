using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OrganizationQueryTests : BaseCrudTest
    {
        private static OrganizationQuery query;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();
            query = new OrganizationQuery(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public void CRM_Query_Organization_GetActiveAccounts()
        {
            var accounts = query.GetActiveAccounts(withInvoicesFrom: DateTime.Now.AddYears(-1));

            Assert.AreNotEqual(0, accounts.Count());

            foreach (var account in accounts)
            {
                Console.WriteLine(account.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Organization_GetNewAccounts()
        {
            var accounts = query.GetNewAccounts(createdAfter:DateTime.Now.AddMonths(-3));

            Assert.AreNotEqual(0, accounts.Count());

            foreach (var account in accounts)
            {
                Console.WriteLine(account.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Organization_GetNewAndActiveAccounts()
        {
            var accounts = query.GetNewAndActiveAccounts(
                createdAfter: DateTime.Now.AddYears(-1),
                withInvoicesFrom: DateTime.Now.AddMonths(-3));

            Assert.AreNotEqual(0, accounts.Count());

            foreach (var account in accounts)
            {
                Console.WriteLine(account.Name);
            }
        }
    }
}

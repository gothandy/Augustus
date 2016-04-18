using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OrganizationQueryTests : BaseQueryTest
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
            var accounts = query.GetActiveAccounts();

            Assert.AreNotEqual(0, accounts.Count());

            foreach (var account in accounts)
            {
                Console.WriteLine(account.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Organization_GetNewAccounts()
        {
            var accounts = query.GetNewAccounts();

            Assert.AreNotEqual(0, accounts.Count());

            foreach (var account in accounts)
            {
                Console.WriteLine(account.Name);
            }
        }

        [TestMethod]
        public void CRM_Query_Organization_GetNewAndActiveAccounts()
        {
            var accounts = query.GetNewAndActiveAccounts();

            Assert.AreNotEqual(0, accounts.Count());

            foreach (var account in accounts)
            {
                Console.WriteLine(account.Name);
            }
        }
    }
}

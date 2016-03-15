using Augustus.CRM.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class BaseCrudTest
    {
        protected static OrgQueryable org;

        protected static void CreateOrg()
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            org = new OrgQueryable(connectionString);
        }

        protected void createAccount(string name)
        {
            AccountEntity account = new AccountEntity()
            {
                Name = name
            };

            org.Create<AccountEntity>(account);
            org.SaveChanges();
        }

        protected void updateAccount(string name1, string name2)
        {
            AccountEntity account = getAccount(name1);
            account.Name = name2;
            org.Update<AccountEntity>(account);
            org.SaveChanges();
        }

        protected void deleteAccount(string name)
        {
            AccountEntity account = getAccount(name);
            org.Delete<AccountEntity>(account);
            org.SaveChanges();
        }

        protected AccountEntity getAccount(string name)
        {
            return org.Accounts.Single(a => a.Name == name);
        }

        protected void createOpportunity()
        {
            AccountEntity testAccount = org.Accounts.Single(a => a.Name == "TestAccount");

            OpportunityEntity newOpp = new OpportunityEntity()
            {
                Name = "Test Opp",
                ParentAccount = testAccount
            };

            org.Create<OpportunityEntity>(newOpp);
            org.SaveChanges();
        }

        protected void deleteOpportunity()
        {
            OpportunityEntity newOpp = org.Opportunities.Single(o => o.Name == "Test Opp");

            org.Delete<OpportunityEntity>(newOpp);

            org.SaveChanges();
        }

    }
}

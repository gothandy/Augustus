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
            Account account = new Account()
            {
                Name = name
            };

            org.Create<Account>(account);
            org.SaveChanges();
        }

        protected void updateAccount(string name1, string name2)
        {
            Account account = getAccount(name1);
            account.Name = name2;
            org.Update<Account>(account);
            org.SaveChanges();
        }

        protected void deleteAccount(string name)
        {
            Account account = getAccount(name);
            org.Delete<Account>(account);
            org.SaveChanges();
        }

        protected Account getAccount(string name)
        {
            return org.Accounts.Single(a => a.Name == name);
        }

        protected void createOpportunity()
        {
            Account testAccount = org.Accounts.Single(a => a.Name == "TestAccount");

            Opportunity newOpp = new Opportunity()
            {
                Name = "Test Opp",
                ParentAccount = testAccount
            };

            org.Create<Opportunity>(newOpp);
            org.SaveChanges();
        }

        protected void deleteOpportunity()
        {
            Opportunity newOpp = org.Opportunities.Single(o => o.Name == "Test Opp");

            org.Delete<Opportunity>(newOpp);

            org.SaveChanges();
        }

    }
}

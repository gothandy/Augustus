using Augustus.CRM.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class BaseCrudTest
    {
        protected const string accountName = "CRM Account Name";
        protected const string accountRename = "CRM Account Name 2";
        protected const string opportunityName = "CRM Opportunity Name";
        protected const string opportunityRename = "CRM Opportunity Name 2";

        protected static OrgQueryable org;

        protected static void CreateOrg()
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            org = new OrgQueryable(connectionString);
        }

        protected static void createAccount(string name)
        {
            AccountEntity account = new AccountEntity()
            {
                Name = name
            };

            org.Create<AccountEntity>(account);
            org.SaveChanges();
        }

        protected static void updateAccount(string name1, string name2)
        {
            AccountEntity account = getAccount(name1);
            account.Name = name2;
            org.Update<AccountEntity>(account);
            org.SaveChanges();
        }

        protected static void deleteAccount(string name)
        {
            AccountEntity account = getAccount(name);
            org.Delete<AccountEntity>(account);
            org.SaveChanges();
        }

        protected static AccountEntity getAccount(string name)
        {
            return org.Accounts.Single(a => a.Name == name);
        }

        protected static OpportunityEntity getOpportunity(string name)
        {
            return org.Opportunities.Single(a => a.Name == name);
        }

        protected static void createOpportunity(string name, AccountEntity account)
        {
            OpportunityEntity newOpp = new OpportunityEntity()
            {
                Name = name,
                AccountId = account.Id
            };

            org.Create<OpportunityEntity>(newOpp);
            org.SaveChanges();
        }

        protected static void deleteOpportunity(string name)
        {
            OpportunityEntity newOpp = org.Opportunities.Single(o => o.Name == name);

            org.Delete<OpportunityEntity>(newOpp);

            org.SaveChanges();
        }

        protected static void deleteAllAccounts()
        {
            deleteAccounts(accountName);
            deleteAccounts(accountRename);
        }

        private static void deleteAccounts(string name)
        {
            var accounts = org.Accounts.Where(a => a.Name == name);

            foreach (var account in accounts)
            {
                org.Delete<AccountEntity>(account);
            }
            org.SaveChanges();
        }

        protected static void deleteAllOpportunities()
        {
            deleteOpportunities(accountName);
            deleteOpportunities(accountRename);
        }

        private static void deleteOpportunities(string name)
        {
            var entities = org.Opportunities.Where(a => a.Name == name);

            foreach (var entity in entities)
            {
                org.Delete<OpportunityEntity>(entity);
            }
            org.SaveChanges();
        }
    }
}

using Augustus.CRM.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class BaseCrudTest
    {
        protected const string accountName = "Augustus Account Name";
        protected const string accountRename = "Augustus  Account Rename";
        protected const string opportunityName = "Augustus Opportunity Name";
        protected const string opportunityRename = "Augustus Opportunity Rename";
        protected const string invoiceName = "Augustus Invoice Name";
        protected const string invoiceRename = "Augustus Invoice Rename";

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



        protected static AccountEntity getAccount(string name)
        {
            return org.Accounts.Single(a => a.Name == name);
        }

        protected static OpportunityEntity getOpportunity(string name)
        {
            return org.Opportunities.Single(a => a.Name == name);
        }
        protected static InvoiceEntity getInvoice(string name)
        {
            return org.Invoices.Single(a => a.Name == name);
        }

        protected static void createOpportunity(string name, AccountEntity account)
        {
            var newOpp = new OpportunityEntity()
            {
                Name = name,
                AccountId = account.Id
            };

            org.Create<OpportunityEntity>(newOpp);
            org.SaveChanges();
        }

        protected static void createInvoice(string name, OpportunityEntity opportunity)
        {
            var newInv = new InvoiceEntity()
            {
                Name = name,
                AccountId = opportunity.AccountId,
                OpportunityId = opportunity.Id
            };

            org.Create<InvoiceEntity>(newInv);
            org.SaveChanges();
        }

        protected static void deleteAccount(string name)
        {
            org.Delete(getAccount(name));
            org.SaveChanges();
        }

        protected static void deleteOpportunity(string name)
        {
            org.Delete(getOpportunity(name));
            org.SaveChanges();
        }

        protected static void deleteInvoice(string name)
        {
            org.Delete(getInvoice(name));
            org.SaveChanges();
        }

        protected static void deleteAllAccounts()
        {
            deleteAccounts(accountName);
            deleteAccounts(accountRename);
        }

        private static void deleteAccounts(string name)
        {
            DeleteEntities(org.Accounts, a => a.Name == name, a => a.Name);
        }

        protected static void deleteAllOpportunities()
        {
            deleteOpportunities(opportunityName);
            deleteOpportunities(opportunityRename);
        }

        private static void deleteOpportunities(string name)
        {
            DeleteEntities(org.Opportunities, a => a.Name == name, a => a.Name);
        }

        protected static void deleteAllInvoices()
        {
            deleteInvoices(invoiceName);
            deleteInvoices(invoiceRename);
        }



        private static void deleteInvoices(string name)
        {
            DeleteEntities(org.Invoices, a => a.Name == name, a => a.Name);
        }

        protected static void deleteAllWorkDoneItems()
        {
            deleteWorkDoneItems(invoiceName);
            deleteWorkDoneItems(invoiceRename);
        }

        protected static void deleteWorkDoneItems(string invoiceName)
        {
            var invoices = org.Invoices.Where(i => i.Name == invoiceName);

            var save = false;

            foreach (var invoice in invoices)
            {
                if (invoice.Name.StartsWith("Augustus"))
                {
                    var items = org.WorkDoneItems.Where(i => i.InvoiceId.Value == invoice.Id);

                    foreach (var item in items)
                    {
                        if (item.InvoiceId == invoice.Id)
                        {
                            org.Delete<WorkDoneItemEntity>(item);
                            Console.WriteLine("{0} {1} Work Done Item Deleted.", invoice.Name, item.WorkDoneDate);
                            save = true;
                        }
                    }
                }
            }

            if (save) org.SaveChanges();
        }

        private static void DeleteEntities<T>(IQueryable<T> entities, Func<T, bool> whereClause, Func<T, string> getName) where T : BaseEntity
        {
            var entitiesToDelete = entities.Where(whereClause);

            var save = false;

            foreach (var entity in entitiesToDelete)
            {
                // Danger, danger, check name just in case whereClause fails and returns everything.
                if (getName(entity).StartsWith("Augustus"))
                {
                    org.Delete<T>(entity);
                    Console.WriteLine("{0} Entity Deleted.", getName(entity));
                    save = true;
                }
            }

            if (save) org.SaveChanges();
        }
    }
}

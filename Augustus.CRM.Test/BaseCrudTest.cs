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

        protected static CrmContext context;

        protected static void CreateOrg()
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            var svc = new CrmServiceConnectionString(connectionString);

            context = new CrmContext(svc);
        }

        protected static void createAccount(string name)
        {
            AccountEntity account = new AccountEntity()
            {
                Name = name
            };

            context.Create<AccountEntity>(account);
            context.SaveChanges();
        }

        protected static void updateAccount(string name1, string name2)
        {
            AccountEntity account = getAccount(name1);
            account.Name = name2;
            context.Update<AccountEntity>(account);
            context.SaveChanges();
        }



        protected static AccountEntity getAccount(string name)
        {
            return context.Accounts.Single(a => a.Name == name);
        }

        protected static OpportunityEntity getOpportunity(string name)
        {
            return context.Opportunities.Single(a => a.Name == name);
        }
        protected static InvoiceEntity getInvoice(string name)
        {
            return context.Invoices.Single(a => a.Name == name);
        }

        protected static void createOpportunity(string name, AccountEntity account)
        {
            var newOpp = new OpportunityEntity()
            {
                Name = name,
                AccountId = account.Id
            };

            context.Create<OpportunityEntity>(newOpp);
            context.SaveChanges();
        }

        protected static void createInvoice(string name, OpportunityEntity opportunity)
        {
            var newInv = new InvoiceEntity()
            {
                Name = name,
                AccountId = opportunity.AccountId,
                OpportunityId = opportunity.Id
            };

            context.Create<InvoiceEntity>(newInv);
            context.SaveChanges();
        }

        protected static void deleteAccount(string name)
        {
            context.Delete(getAccount(name));
            context.SaveChanges();
        }

        protected static void deleteOpportunity(string name)
        {
            context.Delete(getOpportunity(name));
            context.SaveChanges();
        }

        protected static void deleteInvoice(string name)
        {
            context.Delete(getInvoice(name));
            context.SaveChanges();
        }

        protected static void deleteAllAccounts()
        {
            deleteAccounts(accountName);
            deleteAccounts(accountRename);
        }

        private static void deleteAccounts(string name)
        {
            var accounts = context.Accounts.Where(i => i.Name == name);

            var save = false;

            foreach (var account in accounts)
            {
                if (account.Name.StartsWith("Augustus"))
                {
                    context.Delete<AccountEntity>(account);
                    Console.WriteLine("{0} Account Deleted.", account.Name);
                    save = true;
                }
            }

            if (save) context.SaveChanges();
        }

        protected static void deleteAllOpportunities()
        {
            deleteOpportunities(opportunityName);
            deleteOpportunities(opportunityRename);
        }

        private static void deleteOpportunities(string name)
        {
            var opportunities = context.Opportunities.Where(i => i.Name == name);

            var save = false;

            foreach (var opportunity in opportunities)
            {
                if (opportunity.Name.StartsWith("Augustus"))
                {
                    context.Delete<OpportunityEntity>(opportunity);
                    Console.WriteLine("{0} Opportunity Deleted.", opportunity.Name);
                    save = true;
                }
            }

            if (save) context.SaveChanges();
        }

        protected static void deleteAllInvoices()
        {
            deleteInvoices(invoiceName);
            deleteInvoices(invoiceRename);
        }

        private static void deleteInvoices(string name)
        {
            var invoices = context.Invoices.Where(i => i.Name == name);

            var save = false;

            foreach (var invoice in invoices)
            {
                if (invoice.Name.StartsWith("Augustus"))
                {
                    context.Delete<InvoiceEntity>(invoice);
                    Console.WriteLine("{0} Invoice Deleted.", invoice.Name);
                    save = true;
                }
            }

            if (save) context.SaveChanges();
        }

        protected static void deleteAllWorkDoneItems()
        {
            deleteWorkDoneItems(invoiceName);
            deleteWorkDoneItems(invoiceRename);
        }

        protected static void deleteWorkDoneItems(string name)
        {
            var invoices = context.Invoices.Where(i => i.Name == name);

            var save = false;

            foreach (var invoice in invoices)
            {
                if (invoice.Name.StartsWith("Augustus"))
                {
                    var items = context.WorkDoneItems.Where(i => i.InvoiceId.Value == invoice.Id);

                    foreach (var item in items)
                    {
                        if (item.InvoiceId == invoice.Id)
                        {
                            context.Delete<WorkDoneItemEntity>(item);
                            Console.WriteLine("{0} {1} Work Done Item Deleted.", invoice.Name, item.WorkDoneDate);
                            save = true;
                        }
                    }
                }
            }

            if (save) context.SaveChanges();
        }
    }
}

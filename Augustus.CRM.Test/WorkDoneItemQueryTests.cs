using Augustus.CRM.Entities;
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace Augustus.CRM.Test
{
    [TestClass]
    public class WorkDoneItemQueryTests : BaseQueryTest
    {
        private static Guid easyJetJan16Inv;
        private static InvoiceQuery invQuery;
        private static WorkDoneItemQuery wdiQuery;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();
            invQuery = new InvoiceQuery(context);
            wdiQuery = new WorkDoneItemQuery(context);
           
            easyJetJan16Inv = new Guid("28D8BB1A-0DB3-E511-8118-3863BB34FA68");

            deleteAllWorkDoneItems();
            deleteAllInvoices();
            deleteAllOpportunities();
            deleteAllTestAccounts();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllWorkDoneItems();
            deleteAllInvoices();
            deleteAllOpportunities();
            deleteAllTestAccounts();

            context.Dispose();
        }

        [TestMethod]
        public void CRM_Query_WorkDoneItem_GetItems()
        {
            var inv = invQuery.GetItem(easyJetJan16Inv);

            foreach(var item in inv.WorkDoneItems)
            {
                Console.WriteLine("{0} {1}", item.WorkDoneDate, item.Margin);
            }

            Assert.AreEqual(6, inv.WorkDoneItems.Count());

            var wdi = inv.WorkDoneItems.First();

            Assert.AreEqual(inv.AccountId, wdi.AccountId);
            Assert.AreEqual((decimal)99684.3, wdi.Margin);
            Assert.AreEqual(new DateTime(2016,1,1), wdi.WorkDoneDate);
            Assert.IsNull(inv.WorkDoneItems[5].Margin);

        }

        [TestMethod]
        public void CRM_Query_WorkDoneItem_CrUD()
        {
            // Create Account
            createAccount(accountName);
            var account = getAccount(accountName);

            // Create Opportunity
            createOpportunity(opportunityName, account);
            var opportunity = getOpportunity(opportunityName);

            // Create Invoice
            createInvoice(invoiceName, opportunity);
            var invoice = getInvoice(invoiceName);

            // Create Work Done Item
            var item = new WorkDoneItem()
            {
                AccountId = account.Id,
                InvoiceId = invoice.Id,
                WorkDoneDate = new DateTime(2005, 5, 1),
                Margin = (decimal?)12345.67
            };
            var id = wdiQuery.Create(item);

            // Check item
            item = wdiQuery.GetItem(id);
            Assert.AreEqual(account.Id, item.AccountId);
            Assert.AreEqual(invoice.Id, item.InvoiceId);
            Assert.AreEqual(new DateTime(2005, 5, 1), item.WorkDoneDate);
            Assert.AreEqual((decimal?)12345.67, item.Margin);

            // Delete Work Done Item
            wdiQuery.Delete(id);

            // Delete Others
            deleteInvoice(invoiceName);
            deleteOpportunity(opportunityName);
            deleteAccount(accountName);
            context.SaveChanges();
        }
    }
}

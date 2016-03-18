using Augustus.CRM.Entities;
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class InvoiceQueryTests : BaseCrudTest
    {
        private static InvoiceQuery query;
        private static Guid easyJetJan16Inv;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();
            query = new InvoiceQuery { Organization = org };
            easyJetJan16Inv = new Guid("28D8BB1A-0DB3-E511-8118-3863BB34FA68");

            deleteAllOpportunities();
            deleteAllAccounts();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllOpportunities();
            deleteAllAccounts();
            org.Dispose();
        }

        [TestMethod]
        public void CRM_Query_Invoice_GetInvoice()
        {
            var invoice = query.GetInvoice(easyJetJan16Inv);

            Assert.AreEqual(new DateTime(2016,1,31), invoice.InvoiceDate);
        }

        [TestMethod]
        public void CRM_Query_Invoice_GetAccount()
        {
            var account = query.GetAccount(easyJetJan16Inv);

            Assert.AreEqual("easyJet", account.Name);
        }

        [TestMethod]
        public void CRM_Query_Invoice_GetOpportunity()
        {
            var opp = query.GetOpportunity(easyJetJan16Inv);

            Assert.AreEqual(new DateTime(2016,1,11,11,42,34), opp.Created);
        }

        [TestMethod]
        public void CRM_Query_Invoice_GetWorkDoneItems()
        {
            var items = query.GetWorkDoneItems(easyJetJan16Inv);

            Assert.AreEqual(5, items.Count());

            foreach(var item in items)
            {
                Console.WriteLine(item.Margin);
            }
        }


        /*
        [TestMethod]
        public void CRM_Query_Invoice_CrUD()
        {
            // Create Account
            createAccount(accountName);
            var accountId = getAccount(accountName).Id;
            
            // Create Invoice
            Invoice Invoice = new Invoice { Name = InvoiceName, AccountId = accountId };
            var id = query.CreateInvoice(Invoice);

            // Update Invoice
            Invoice.Id = id;
            Invoice.Name = InvoiceRename;
            query.UpdateInvoice(Invoice);

            // Delete Invoice
            query.DeleteInvoice(id);

            // Delete Account
            deleteAccount(accountName);
        }
        */
    }
}

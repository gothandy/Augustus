using Augustus.CRM.Entities;
using Augustus.CRM.Queries;
using Augustus.Domain.Enums;
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
            var invoice = query.GetItem(easyJetJan16Inv);

            Assert.AreEqual(new DateTime(2016,1,31), invoice.InvoiceDate);
            Assert.AreEqual(InvoiceStatus.InvoiceSent, invoice.Status);
        }

        [TestMethod]
        public void CRM_Query_Invoice_GetAccount()
        {
            var inv = query.GetItem(easyJetJan16Inv);

            Assert.AreEqual("easyJet", inv.Account.Name);
        }

        [TestMethod]
        public void CRM_Query_Invoice_GetOpportunity()
        {
            var inv = query.GetItem(easyJetJan16Inv);

            Assert.AreEqual(new DateTime(2016,1,11,11,42,34), inv.Opportunity.Created);
        }

        [TestMethod]
        public void CRM_Query_Invoice_GetWorkDoneItems()
        {
            var inv = query.GetItem(easyJetJan16Inv);

            Assert.AreEqual(5, inv.WorkDoneItems.Count());

            foreach(var item in inv.WorkDoneItems)
            {
                Console.WriteLine(item.Margin);
            }
        }


        [TestMethod]
        public void CRM_Query_Invoice_CrUD()
        {
            // Create Account
            createAccount(accountName);
            var account = getAccount(accountName);

            // Create Opportunity
            createOpportunity(opportunityName, account);
            var opportunity = getOpportunity(opportunityName);

            // Create Invoice
            Invoice invoice = new Invoice
            {
                Name = invoiceName,
                OpportunityId = opportunity.Id,
                Cost = 5000,
                Revenue = 15000,
                ClientApprovedDate = new DateTime(2016, 1, 1),
                InvoiceDate = new DateTime(2016, 3, 1),
                InvoiceNo = "Inv No.",
                PONumber = "PO No."
            };

            var id = query.Create(invoice);

            // Assert values
            var inv = query.GetItem(id);
            Assert.AreEqual(10000, inv.Margin);
            Assert.AreEqual(opportunity.Id, inv.OpportunityId);
            Assert.AreEqual(new DateTime(2016, 3, 1), inv.InvoiceDate);
            Assert.AreEqual(account.Id, inv.AccountId);


            // Update Invoice
            var updateInv = new Invoice
            {
                Id = id,
                Name = invoiceRename
            };
            query.Update(updateInv);
            inv = query.GetItem(id);
            Assert.AreEqual(invoiceRename, inv.Name);
            Assert.AreEqual(10000, inv.Margin);
            Assert.AreEqual(opportunity.Id, inv.OpportunityId);
            Assert.AreEqual(new DateTime(2016, 3, 1), inv.InvoiceDate);
            Assert.AreEqual(account.Id, inv.AccountId);

            // Delete Invoice
            query.Delete(id);

            // Delete Opportunity
            deleteOpportunity(opportunityName);

            // Delete Account
            deleteAccount(accountName);
        }
    }
}

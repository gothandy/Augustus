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


        [TestMethod]
        public void CRM_Query_Invoice_CrUD()
        {
            // Create Account
            createAccount(accountName);
            var account = getAccount(accountName);

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

            var id = query.CreateInvoice(invoice);

            // Assert values
            var inv = query.GetInvoice(id);
            Assert.AreEqual(10000, inv.Margin);
            Assert.AreEqual(opportunity.Id, inv.OpportunityId);
            

            // Update Invoice
            invoice.Id = id;
            invoice.Name = invoiceRename;
            query.UpdateInvoice(invoice);

            // Delete Invoice
            query.DeleteInvoice(id);

            // Delete Opportunity
            deleteOpportunity(opportunityName);

            // Delete Account
            deleteAccount(accountName);
        }
    }
}
